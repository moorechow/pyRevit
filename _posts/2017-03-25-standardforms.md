---
layout: post
title:  "Two more productivity tools"
date:   2017-03-22 14:00:00 -0900
categories: pyrevit update
comments: true
---

I'm working on a host of new changes to the core this week but I managed to add these two tools. One suggested by [thazel](https://github.com/thazell) under [issue #166](https://github.com/eirannejad/pyRevit/issues/166), and another came out from a conversation I had with a colleague on an easier way to make placeholders and sheets.

## Load family types:

This tool will look into the original family file of a selected family instance and will get a list of types that are available for that family. The user then selects the family types that are needed and the tool will load them into the model.

This is especially helpful with families such as Structural Framing that have many types and not all the types need to be loaded in the model.

**Exceptions:** There are instances that this tool can not find the original family file since it expects the family to know about its original saved location, which families usually do. I have tried to include descriptive prompts that inform the user on any errors.

![]({{ site.url }}/pyRevit/images/loadmorefamtypes1.png)

![]({{ site.url }}/pyRevit/images/loadmorefamtypes2.png)


## Batch Sheet Maker:

This is really a very simple tool and is helpful in creating a number of sheets or placeholders really quickly or to have a list of standard sheets and use this list to create sheets in many models and keep them consistent.

The tool is pretty self-explanatory. Type in the sheet number and sheet name (separate them by a single tab), pick the sheet or placeholder options and create the sheets:

![]({{ site.url }}/pyRevit/images/batchsheetmaker.png)
