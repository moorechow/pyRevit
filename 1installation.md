---
layout: page
title: Installation and Removal
permalink: /installation/
---

## Installation (Using the installer):

- Download the setup package, extract to your machine.
- Run `Setup.bat` and all scripts will be downloaded to this folder.
- Setup script will create the necessary `.addin` file for Revit 2015 and 2016 to load the scripts at Revit startup.
- Run Revit and a `pyRevit` tab will be added to the Revit ribbon.

This package adds an addin on your Revit that its sole purpose is to run the `__init__.py` at Revit startup. This addin is named [RevitPythonLoader](https://github.com/eirannejad/revitpythonloader) and is a fork of [RevitPythonShell](https://github.com/architecture-building-systems/revitpythonshell).

How does it find the `__init__.py` you ask? Through a windows environment variable named `%pyRevit%` that it also automatically created by `Setup.bat` at installation. This variable points to the folder containing the `__init__.py` file which is t he downloaded pyRevit library. This folder can be a local folder or a nework folder (e.g `%pyRevit% = //Server/BIM/Revit/pyRevit/` 

Neil Reilly has prepared a handy video taking you through the installation and showing some of the more useful tools. Click here to go to his Youtube page and watch the video.

[![NeilReillyVideo](http://eirannejad.github.io/pyRevit/images/neilreillyvideo1.jpg)](https://www.youtube.com/watch?v=71rvCspWNHs)

**If you encountered any errors during the installation, please use the manual installation method described below:**

## Installation (Manual installation):

This method needs basic knowledge of cloning/downloading git repositories:

- Clone/Download the pyRevit repository onto your machine.

**If you DO have [RevitPythonShell](https://github.com/architecture-building-systems/revitpythonshell) installed on your Revit:**

- Go to `RevitPythonShell` Configuration, under the `InitScript \ Startup Script` tab, click on the browse button for the Startup Script and browse to `__init__.py` in the cloned pyRevit folder.
- Restart your Revit and `RevitPythonShell` will load the `__init__.py` script and a `pyRevit` tab will be added to the ribbon panel.

**If you DO NOT have [RevitPythonShell](https://github.com/architecture-building-systems/revitpythonshell) installed on your Revit:**

- Clone/Download the [RevitPythonLoader](https://github.com/eirannejad/revitpythonloader) repository onto your machine.
- Create an environment variable named `%pyrevit%` on your machine and assign the location of `__init__.py` script to this variable (e.g. `%pyrevit% = "C:\pyrevit\"`
- Download this [template add-in file](http://eirannejad.github.io/pyRevit/misc/RevitPythonLoader.addin) and add it to your Revit addin folder (usually: `appdata%\Autodesk\Revit\Addins\2016` for Revit 2016)
- Edit the downloaded addin file with a text editor and replace the `<RPL_repo_location>` with the folder address of the cloned `RevitPythonLoader` repository.
- Start Revit. Revit will find `RevitPythonLoader` with the help of the `addin` file, and RevitPythonLoader will read the value of `%pyrevit%` and will load the `__init__.py` script. A `pyRevit` tab will be added to the ribbon panel.


**About Versioning:** I'm using semantic versioning with MAJOR.MINOR.PATCH format. (MAJOR: incompatible API changes, MINOR: add functionality and scripts in a backwards-compatible manner, PATCH: backwards-compatible bug fixes). You can see your pyRevit version under `Settings -> aboutPyRevit`


## Reinstall / Uninstall:
Run `Setup.bat` and it'll prompt you that the pyRevit or RevitPythonLoader folders already exist and if you want to Reinstall pyRevit. If you answer yes, it'll delete the folders and re-clones the github repositories just like a fresh install.
 
![Reinstall](http://eirannejad.github.io/pyRevit/images/reinstall.png)

If you answer No, It'll ask you if you want to uninstall the tool. The setup script will remove the `.addin` files and `%pyrevit%` environment variable when uninstalling pyRevit.

![Reinstall](http://eirannejad.github.io/pyRevit/images/uninstallComplete.png)