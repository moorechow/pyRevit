---
layout: page
title: Installation and Removal
permalink: /installation/
---

## Installation (Using the installer):

- Download the setup package to your machine.
- Run `pyRevitSetup.exe` and all scripts will be downloaded to the installation folder. You need internet access for setup to work.
- Setup script will create the necessary `.addin` file for Revit 2015 and 2016 to load the scripts at Revit startup.
- Run Revit and a `pyRevit` tab will be added to the Revit ribbon.

## Installation (Manual installation):

This method needs basic knowledge of cloning/downloading git repositories:

- Clone/Download the pyRevit repository onto your machine. You would need to install the git tool for windows if you're comfortable using shell, or you can use the free Github software and clone the pyRevit repository to a folder on your machine.
- After cloning, browse to pyRevit/release/ folder and run `install_addin.bat`. The `.addin` files will be added to all installed Revit versions.
- Run Revit and a `pyRevit` tab will be added to the Revit ribbon.


## Uninstall:
You can uninstall pyRevit like any other applciation from Program and Features in Windows Control Panel.