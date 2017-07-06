---
layout: post
title:  "pyRevit v4.4"
date:   2017-07-04 09:00:00 -0900
categories: pyrevit update
comments: true
---

Finally pyRevit 4.4 is here. Here is a list of the most important changes:

- **Compatible with Revit 2018:** Yay (finally)
- **Adopted [revitpythonwrapper](http://revitpythonwrapper.readthedocs.io/en/latest/) module**: This means that you can import rpw in your pyRevit scripts without installing the `revitpythonwrapper` extension. You should disable/remove the `revitpythonwrapper` extension if you already have it. It's not necessary anymore. The rpw inside pyRevit will be updated on a weekly basis to always have the most recent stable version. Read more below.
- **pyRevit Installation for All or Current User:** Installer now has the option to install for *All Users* versus for only the *Current user*. Read more below.
- **Config File Naming:** pyRevit configuration and temporary files no longer include the username in the file naming. (e.g `pyRevit_eirannejad_config.ini` is now `pyRevit_config.ini`). This should help sysAdmins to push pyRevit configurations easier.
- **Zero Document mode:** A selection of pyRevit tools are now available in zero-document mode (e.g. You can access pyRevit settings even when no documents are open inside Revit. (this is done by setting `__context__ = 'zerodoc'` inside your scripts)

&nbsp;

**THIS UPDATE INCLUDES A CORE UPDATE AND NEEDS TO BE DONE WHEN REVIT IS CLOSED.**

[**See this video on how to update pyRevit.**]()

<div style='position: relative; width: 100%; height: 0px; padding-bottom: 60%;'>
<iframe style='position: absolute; left: 0px; top: 0px; width: 100%; height: 100%' src="https://www.youtube.com/embed/9HIjzfY9xz8?showinfo=0" frameborder="0" allowfullscreen></iframe>
</div>

&nbsp;


## rpw [(revitpythonwrapper)](http://revitpythonwrapper.readthedocs.io/en/latest/):

I talked about the change from `revitutils` to `rpw` in a previous post [(Read here)]({{ site.url }}/pyRevit/pyrevit/update/2017/04/01/revitutilsimprovements.html). I have started this process and as of now the `revitpythonwrapper` module has been added to the pyRevit standard library. I will be working on upgrading the existing tools to use the rpw for Revit API access. Through this process we will improve the rpw further more so hopefully all you can have access to a more comprehensive wrapper for Revit API.

As of now, you can import rpw in your scripts. Read the [Basic Components](http://revitpythonwrapper.readthedocs.io/en/latest/#basic-components) section on [revitpythonwrapper website](http://revitpythonwrapper.readthedocs.io/en/latest/) for more information on rpw's capabilities.

&nbsp;

## pyRevit Installer Update:

pyRevit installer now includes an option to install pyRevit for All users or for the Current user only.

[**See this video on how to install pyRevit using the new installer**](https://www.youtube.com/embed/-hIMH_dIUuw)

<div style='position: relative; width: 100%; height: 0px; padding-bottom: 60%;'>
<iframe style='position: absolute; left: 0px; top: 0px; width: 100%; height: 100%' src="https://www.youtube.com/embed/9HIjzfY9xz8?showinfo=0" frameborder="0" allowfullscreen></iframe>
</div>


### For sysAdmins:

- **For GUI install:** The installer has the Current User / All Users option now and installation works

- **For Silent install:** I could not figure out a way to pass an option to the installer for silent installs. However since the `install_addin.bat` now has a `--alluser` options, the admin can:

``` batch
REM Install using silent installer
pyRevitSetup-v4.exe /VERYSILENT /DIR="C:\pyRevit"

REM then change directory to the release directory inside pyrevit repo
cd \D "C:\pyRevit\pyRevit\release"

REM remove the current addin files
uninstall_addin.bat

REM re-create the addin files using the --allusers option
install_addin.bat --allusers
```

&nbsp;

## Other Resolved Issues:

Here is a list of issues that has been resolved in this update:

- [Issue #209](https://github.com/eirannejad/pyRevit/issues/209)
- [Issue #208](https://github.com/eirannejad/pyRevit/issues/208)
- [Issue #205](https://github.com/eirannejad/pyRevit/issues/205)
- [Issue #204](https://github.com/eirannejad/pyRevit/issues/204)
- [Issue #200](https://github.com/eirannejad/pyRevit/issues/200)
- [Issue #195](https://github.com/eirannejad/pyRevit/issues/195)
- [Issue #180](https://github.com/eirannejad/pyRevit/issues/180)
