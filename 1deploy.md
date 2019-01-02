---
layout: page
title: Deployment
permalink: /deploy/
---

### Deployment Guide

pyRevit could be deployed used the installer or the CLI tool. As shown in the chart below, the installer should be used for personal single-user installs. The installer does not need admin rights as it installs into `%appdata%` by default and adds the binary path to the user `%PATH%` environment variable.

The CLI tool however can be used to deploy pyRevit to other users and create custom deployments as needed by your organization and teams.


![]({{ site.url }}/pyRevit/images/installtargets.png)

&nbsp;


#### Creating a pyRevit deploy script for your org

The video below, takes you thru the example scripts below and explains how to create custom deploy scripts for pyRevit.

&nbsp;

<div style='position: relative; width: 100%; height: 0px; padding-bottom: 60%;'>
<iframe style='position: absolute; left: 0px; top: 0px; width: 100%; height: 100%' src="https://www.youtube.com/embed/hnlAEEfzhok" frameborder="0" allowfullscreen></iframe>
</div>

&nbsp;

This is the main script discussed in the video above:

```powershell
# =============================================================
# Silent installing pyRevit CLI 
# =============================================================
# install dotnet if necessary
Write-Output "Installing .Net (4.7.1)..."
Start-Process -FilePath ".\msi\NDP471-KB4033342-x86-x64-AllOS-ENU.exe" -ArgumentList "/q /norestart" -Wait -Verb RunAs > $null

# install/update pyRevit CLI
Write-Output "Installing pyRevit..."
Start-Process -FilePath ".\msi\pyRevitCLI_signed.exe" -Wait -ArgumentList "/qn" > $null


# =============================================================
# Using pyRevit CLI to deploy pyRevit and any extensions
# =============================================================
# add pyrevit cli path to env so we can use in next commands
$env:Path = "C:\Program Files\pyRevit CLI";

# setup globals
$logfile=$(Write-Output \\SharedDir\Logs\$($Env:Computername).log)
$reportfile=$(Write-Output \\SharedDir\Logs\$($Env:Computername)_report.log)

# set the global variables
$clonename = "main"
$pyrevitroot = "C:\pyRevit"
$pyrevitexts = $(Join-Path $pyrevitroot "Extensions")

# clone pyrevit
pyrevit clone $clonename base --dest=$pyrevitroot --log=$logfile

# extend pyrevit
pyrevit extend pyApex $pyrevitexts

# =============================================================
# Using pyRevit CLI to configure pyRevit for all users
# =============================================================
# pyrevit configuration
pyrevit configs logs none
pyrevit configs checkupdates disable
pyrevit configs autoupdate disable
pyrevit configs rocketmode enable
pyrevit configs filelogging disable
pyrevit configs loadbeta disable
pyrevit configs usagelogging disable

# limiting user configurations
pyrevit configs usercanupdate No
pyrevit configs usercanextend No
pyrevit configs usercanconfig No

# seed the config - this is critical!
pyrevit configs seed


# =============================================================
# Attaching pyRevit to installed Revits
# =============================================================
# attach pyrevit in %PROGRAMDATA% for all users
pyrevit attach $clonename 277 2019 --allusers
pyrevit attach $clonename 277 2018 --allusers

```

&nbsp;

#### PowerShell Utility Module

Save this script as a .psm file and use `Import-Module` to import into other powershell scripts:

```powershell
# uninstalls an applicaiton using msi uninstaller
# Example: Uninstall-App "Autodesk Revit 2019"
function Uninstall-App ([string] $appName) {
    Write-Output "Uninstalling $($appName)"
    foreach($obj in Get-ChildItem "HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall") {
        $dname = $obj.GetValue("DisplayName")
        if ($dname -contains $appName) {
            Write-Output $obj
            $uninstString = $obj.GetValue("UninstallString")
            Write-Output $uninstString
            foreach ($line in $uninstString) {
                $found = $line -match '(\{.+\}).*'
                If ($found) {
                    $appid = $matches[1]
                    Write-Output $appid
                    start-process "msiexec.exe" -arg "/X $appid /qb" -Wait
                }
            }
        }
    }
}

# confirms target path exists and is empty. removes existing if found
function Confirm-Path ([string] $targetpath) {
    Write-Output "Confirming $($targetpath)"
    If (Test-Path $targetpath) {
        Remove-Item -Path $targetpath -Recurse -Force
    }

    mkdir $targetpath > $null
}


# test if user belongs to a security group with provided SID
# Example: Test-UserGroup "S-1-5-21-3106616158-3581391655-863375417-1660"
function Test-UserGroup ([string] $groupSID)
{
    Add-Type -AssemblyName 'System.Security.Principal'
    $currentUser = [System.Security.Principal.WindowsIdentity]::GetCurrent()
    foreach($group in $currentUser.Groups){
        if ($group.Value -eq $groupSID){
            return $true
        }
    }
    return $false
}


# test whether a command exists in the environment
# Example: Test-CommandExists "pyrevit"
function Test-CommandExists {
    param ($command)
    $oldPreference = $ErrorActionPreference
    $ErrorActionPreference = 'stop'
    try {
        if(Get-Command $command){
            return $true
        }
    }
    catch {
        return $false
    }
    finally {
        $ErrorActionPreference=$oldPreference
    }
}
```