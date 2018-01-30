---
layout: page
title: Installing, Uninstalling
permalink: /installation/
---

## Installation Using the Installer:

- Download the setup package to your machine.
- Run `pyRevitSetup.exe` and all scripts will be downloaded to the installation folder. You need internet access for setup to work.
- Setup script will create the necessary `.addin` file for installed Revit versions, to load the scripts at Revit startup.
- Run Revit and a `pyRevit` tab will be added to the Revit ribbon.

<div style='position: relative; width: 100%; height: 0px; padding-bottom: 60%;'>
<iframe style='position: absolute; left: 0px; top: 0px; width: 100%; height: 100%' src="https://www.youtube.com/embed/-hIMH_dIUuw?showinfo=0" frameborder="0" allowfullscreen></iframe>
</div>

## Uninstall:

You can uninstall pyRevit like any other application from Program and Features in Windows Control Panel.

## Manual installation:

This method needs basic knowledge of cloning/downloading git repositories:

- Clone/Download the pyRevit repository onto your machine. You would need to install the git tool for windows if you're comfortable using shell, or you can use the free Github software and clone the pyRevit repository to a folder on your machine.
- After cloning, browse to pyRevit/release/ folder and run `install_addin.bat`. The `.addin` files will be added to all installed Revit versions.
- Run Revit and a `pyRevit` tab will be added to the Revit ribbon.

<div style='position: relative; width: 100%; height: 0px; padding-bottom: 60%;'>
<iframe style='position: absolute; left: 0px; top: 0px; width: 100%; height: 100%' src="https://www.youtube.com/embed/hrlkPRoUfXc?showinfo=0" frameborder="0" allowfullscreen></iframe>
</div>


## For Administrators:

### Silent Install:

I could not figure out a way to pass an option to the installer for silent installs to set the _all user_ or _current user_ mode. 

However pyRevit ships with two batch scripts that create and remove the addin manifest files for installed Revit versions. These batch scripts are under **release** directory in pyRevit installation.

- `install_addin.bat` creates the addin files.
- `uninstall_addin.bat` removes the addin files (from both `%appdata%` and `%programdata%`).

The `install_addin.bat` has `--alluser` option, so the admin can:

``` batch
REM Install using silent installer
REM The installed path will be C:/Addons/pyRevit
pyRevitSetup-v4.exe /VERYSILENT /DIR="C:\Addons"

REM then change directory to the release directory inside pyrevit repo
cd \D "C:\Addons\pyRevit\release"

REM run the addin manifest file uninstaller
REM and remove the current addin files,
REM since they're in %appdata% folder
uninstall_addin.bat

REM re-create the addin files using the --allusers option
REM this will create the addin files in %programdata%
install_addin.bat --allusers
```


### Setting pyRevit version:

System admins can also use the `pyrevitgitservices.exe` tool that is shipped with pyRevit to perform basic tasks on the pyRevit repository. [See this page for more info on the tool.](https://github.com/eirannejad/pyRevit/tree/master/release)

For example this is how you can set the version of pyRevit repo using this tool:

```batch
pyrevitgitservices.exe setversion <pyrevit_git_repository_path> <commit_hash>
```

And as a more specific example, this would set the pyRevit repo version to [this commit](https://github.com/eirannejad/pyRevit/commit/71ec1e5d4588205ae0064e9b35ec10c3dc113248):

```batch
pyrevitgitservices.exe setversion "C:\pyRevit" 71ec1e5
```

This new executive lives under this path, inside the pyRevit installation:

`release\pyrevitgitservices\pyrevitgitservices\bin\Release\`
