---
layout: post
title:  "Adding Additional Revit Build Numbers"
date:   2018-11-23 09:00:00 -0910
categories: pyrevit updates cli
comments: true
---

Hey all pyRevit fans,

As you might have seen in the videos and the new installations, pyRevit has a CLI tool that helps in installation and management of pyRevit clones on your machines. This tool is capable of recognizing installed Revit versions and attaching pyRevit to them. There are however specific Revit versions out there with build numbers that are not published by Autodesk yet and it makes it hard for pyRevit to know what exact Revit version it is dealing with. I and the pyRevit contributers have built as much as these build numbers as we could in the code but I'm sure we're still missing a few.

[Please take a look at the listed build numbers here](https://github.com/eirannejad/pyRevitLabs/blob/master/pyRevitLabs/pyRevitLabs.TargetApps.Revit/RevitController.cs#L230)

Let us know if your Revit build number is missing and it will be added to the code as well.

Prepare your Revit info in this format (You can grab this info from Revit About window):

`<complete build number>, <complete version number>, <complete version name>`

For example:

`20170816_0615, 17.0.1146.0, 2017.2.2`

and add to the [comments on this issue](https://github.com/eirannejad/pyRevitLabs/issues/8)


Thanks for the support