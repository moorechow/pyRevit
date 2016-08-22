---
layout: post
title:  "More Updates on v3"
date:   2016-08-21 13:00:00 -0900
categories: pyrevit update
comments: true
---

There are a handful of pyRevit scripts that are pretty much identical and only differ by a line or two. Take the `pick` tools for example. There are a couple of them under the `Select` pulldown and each picks a centain element type. But the truth is that the underlying logic is the same in all these scripts and the only difference is one line of code that tells the script to pick a door or a window. This creates two problems:

- First is a maintenance issue. Every time I update one of the scripts, I need to make that same adjustment in all the script files.
- Second is that I have 7 almost-identical script files that increases the load time and create 7 buttons in Revit UI that take up so much space and make the pulldowns harder to use. It's not worth the screen real state and the load time overhead.

This problem has been solved now. How? Well, simple. Let's take the `pick` tool again as example. In pyRevit v3, instead of 7 buttons, there is only one pick tool. When clicked, it'll show a list of options that you can select from. Here is how it looks like:

![]({{ site.url }}/pyRevit/images/combinedpick.png)

And here is the window that shows all the options for that tool:

![]({{ site.url }}/pyRevit/images/pickswitches.png)

I hate to increase the click count for the tools, so that's why every one of the options listed in this ***Options Window*** is a button. So in pyRevit v3, one click on the `pick` tool and then a click on `wall` option button is equal too running the `pickWalls` tool in pyRevit v2. 

One extra click, but more efficient and less wasted screen space. It's also possible to add more options to these tools now without creating more Revit UI buttons.

Here are a few more examples. All copy and paste tools have been replaced with a single `copyState` and `pasteState` button:

![]({{ site.url }}/pyRevit/images/combinedcopypaste.png)

 Here is the option window for the `pasteState`:

![]({{ site.url }}/pyRevit/images/pasteswitches.png)

And here are the option window for the `list` tool (replacing the many individual `listXXX` scripts), and also the `isolate` tool:

![]({{ site.url }}/pyRevit/images/listswitches.png)

![]({{ site.url }}/pyRevit/images/isolateswitches.png)