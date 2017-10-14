---
layout: post
title:  "pyRevit Seed Config"
date:   2017-10-14 09:00:00 -0900
categories: pyrevit update
comments: true
---

Hey all,

Alongside the continuous updates and bug fixes, one specific change stands out as more important and I'll describe it here:

### Setting pyRevit seed configuration file:

#### The change:
At Revit startup, if pyRevit can not find its configuration file under `%appdata%/pyRevit`, it will look into `%programdata%/pyRevit` for any file with extension `.ini` and will copy that into `%appdata%/pyRevit` and will use as its default config.

If no config is found under `%programdata%/pyRevit` either, it will default to the hard-coded configuration as before.

This sets the initial configuration for pyRevit but does not limit the users ability to modify the config using the Settings tools later on (If you want to limit the user access to pyRevit config, you can modify the pyRevitCore extension and remove the settings tool. This is more advanced so make sure you watch the pyRevit YouTube videos on managing extensions).

#### More info on the [change](https://github.com/eirannejad/pyRevit/commit/1ed3bcf2eb999d48107cbe03dea2efdd9bdf51c2):

The change request came up from a user that is setting up pyRevit with his custom company extensions in a large environment and wanted to set the default initial configuration in pyRevit. Here is the original request, verbatim:

&nbsp;

> Hi Ehsan,
>  
> Thanks for your comment responses on your blog. I’m going to try explain in a bit more detail what I was suggesting. If you prefer me posting this on Github, please let me know and I will do that.
>  
> My suggestion is based on how a good number of other addins handle default settings. For example Revit handles the Revit.ini file in a very similar way, where the user settings file is set up by copying a version stored in the UserDataCache folder. So we create a custom file and copy it there (ex: C:\ProgramData\Autodesk\RVT 2018\UserDataCache\Revit.ini) by coding this within the install script. When Revit is opened the first time by the user, it sets up the user version of the Revit.ini file (in %appdata%\Autodesk\Revit\Autodesk Revit 2018 for example) by simply making a copy of the one in UserDataCache.
>  
> When we push install via SCCM, the install script uses the SYSTEM account and cannot do anything in the user profile (it has no access to %appdata%; there is no User context for that account and in fact can install while the machine is logged off), so the above description of how Revit works enables us to distribute common settings easily. If we need to make changes to the user copy, we have to do that via a separate login script which operates in the User context and thus has access to %appdata%. However that represents a lot of extra work and tracking of things (we have to then be very careful not to alter settings that the user might have customized). A few addins  that utilize a very similar concept of having a firmwide standard settings file somewhere in ProgramData that then becomes the basis of the user settings file in %appdata% are the CTC Revit Express Tools and  Revolution Design’s workFlow tools such as autoLink. The latter actually uses a copy of the settings file located within the application folder (Program Files (x86)) as the “seed” settings file.
>  
> So my suggestion is to simply provide for the ability to place a settings file somewhere convenient within the pyRevit folder in ProgramData (this could be optional). Once pyRevit starts up the first time and sets up the user profile settings file (in %appdata%), it would simply use a copy of this file if it exists in the ProgramData path and if not, sets up the copy with default settings as it is already doing right now. I hope this clarifies the issue and as always, thanks again for your work!

&nbsp;

The current challenge is that pyRevit saves its configuration under `%appdata%/pyRevit` and this means that the sysAdmin needs to write a separate login script which operates in the user context to be able to write the desired initial pyRevit configuration to this directory. (System user does not have access to user private folders that includes the `%appdata%`).

At Revit startup, pyRevit will look into the `%appdata%/pyRevit` folder to find its configuration. If it does NOT find the config, it sets to create a default configuration file. Up to now, pyRevit would create this default config file based on the configuration baked into the pyRevit code.

But now, at Revit startup, if pyRevit can not find its configuration file under `%appdata%/pyRevit`, it will look into `%programdata%/pyRevit` for any file with extension `.ini` and will copy that into `%appdata%/pyRevit` and will use as its default config.

If no config is found under `%programdata%/pyRevit` either, it will default to the hard-coded configuration as before.

&nbsp;

Hope this help setting up pyRevit easier in your work environments.

&nbsp;

Happy pyReviting :D