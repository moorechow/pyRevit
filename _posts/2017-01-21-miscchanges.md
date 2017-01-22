---
layout: post
title:  "New print tool and Weekly change report"
date:   2017-01-21 14:00:00 -0900
categories: pyrevit update
comments: true
---
![]({{ site.url }}/pyRevit/images/printorderedbutton.png)

I was finally able to sit down and build a tool to print the project sheets in order. It is similar to the [PrintFromIndex](https://github.com/McCulloughRT/PrintFromIndex) tool developed by [Ryan McCullough](https://github.com/McCulloughRT) but it lists the sheets for the Selected Index in the same order as the index and also allows for quick last minute manual reordering. Update your pyRevit and give it a try. Here is how it looks like:


![]({{ site.url }}/pyRevit/images/printorderedsheets.png)


Also here is a quick list of other misc changes that's been made in the past week:

- `pyRevitPlus` extension updated for pyRevit v4 and added to extension manager
- Added pages to the pyRevit website on creating and sharing custom extensions.
- Default script naming has been revised to be more forgiving and include any script that ends with `script.py` or `script.cs`. Same applies to the config scripts. Website documentation has been revised to reflect this change.
- Added Revit version information to the startup log
- Started core documentation
- Resolved issues with Sphinx autodoc system.
- pyRevit seed documentation is created on [readthedocs.com](https://readthedocs.com) and documentation button added to pyRevit website homepage. pyRevit documentation is still work-in-progress. `Readthedocs` will update the docs website based on the latest git repository changes.
