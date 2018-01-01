---
layout: page
title: What's pyRevit
permalink: /whatspyrevit/
---

## What is pyRevit?

***pyRevit*** is an IronPython script library for Revit.
However, it is not really written as an example library.
It is a working set of tools fully written in IronPython that explores the power of scripting for Revit and also adds some cool functionality.

Download and install it, launch Revit and you will note the new ***pyRevit*** tab that hosts buttons to launch all the scripts provided by the package to easily run them without the need to load them in [RevitPythonShell](https://github.com/architecture-building-systems/revitpythonshell) or some similar IronPython console.

You can also write your own scripts and add them to pyRevit. There is even a Reload button than dynamically adds the new scripts to the current Revit session without the need to restart Revit.

All the scripts are provided in the `pyRevit/extensions` folder which is downloaded at installation. You can look into them and learn how to use IronPython for Revit.


#### This article has two main sections:

- [A Quick Look at some pyRevit Scripts](#a-quick-look-at-some-pyrevit-scripts)
	- [Selection Memory](#selection-memory)
	- [Copy and Convert Legend Views](#copy-and-convert-legend-views)
	- [Matching Element Graphic Overrides](#matching-element-graphic-overrides)
- [An Even Quicker but Deeper Look at pyRevit](#an-even-quicker-but-deeper-look-at-pyrevit)

&nbsp;
&nbsp;
&nbsp;

## A Quick Look at a few pyRevit Scripts

Let's take a quick look at some of the more useful scripts in this library:

### Selection Memory

A couple of scripts help you select object more efficiently in Revit. They are similar to the M+, M-, buttons on calculators where you can add or deduct values from memory and read the final value using the MR button.

Under the **pyRevit** tab, you'll find MAppend, MAppendOverwrite, MRead, MDeduct, and MClear buttons that add, add and overwrite, read, deduct, and clear the contents of the selection memory. Using these tools, you can navigate between multiple views and select objects, add them to the memory and when you're done, recall the selection. These tools work really well in combination with other selection tools under **pyRevit** tab. See images here for the tools and tooltips.

Each tooltip shows the button name, the script that the button is associated with and a description of what the script does.

![MAppendOverWrite]({{ site.url }}/pyRevit/images/mread45.png)

### Copy and Convert Legend Views

This set of scripts help you copy Legend Views to all other project open within a Revit session.
You can copy the Legends as Legend views or as Drafting views.

![CopyLegends]({{ site.url }}/pyRevit/images/legends45.png)

Two more scripts duplicate and convert Legend views to Drafting views and vice versa within the same project.

### Matching Element Graphic Overrides

This one is pretty obvious. Run the script, select your source object to pick up the style, and then one by one, select the destination objects to apply the graphic overrides. You can also navigate to other views and apply to objects within that view.

![MatchGraphicOverrides]({{ site.url }}/pyRevit/images/match45.png)

***pyRevit*** provides many other powerful scripts, and most of them are really useful in a production environment.


&nbsp;
&nbsp;
&nbsp;

## A Quicker but Deeper Look

Now let's take an even quicker and slightly deeper look at [pyRevit](https://github.com/eirannejad/pyRevit). pyRevit has two main components.

- **pyRevit Core** (The part that is in charge of parsing extensions and creating necessary assembly files and user interface for them). pyRevit core is an IronPython module and lives under `pyRevit/pyrevitlib/pyrevit` folder.

- **pyRevit Extensions** (All standard pyRevit extensions are under `pyRevit/extensions` folder. There are two types of extensions:)
	- UI Extensions (Collection of tools accessible thru a Ribbon tab)
	- Library Extensions (IronPython modules shared between tools)


The most basic component of a UI extension is a command bundle. Each command bundle is a folder that contains a script file (`script.py`) and might also include an icon (`icon.png`) and is named like `Command Name.pushbutton`

![pyrevitFolder]({{ site.url }}/pyRevit/images/bundle.png)

Command bundles are organzied into Command bundle groups, panels, and tabs that correspond to the well-known UI components in Revit's user interface. pyRevit core uses this structure to create the user interface for the extension.

![pyrevitFolder]({{ site.url }}/pyRevit/images/extension.png)

If you'd like to find out more about ***pyRevit*** and how to add your own scripts, visit the [Adding your own scripts page]({{ site.url }}/pyRevit/howtoaddscriptsandtabs) and everything you want to know about it is provided.

Happy scripting!
		