---
layout: post
title:  "pyRevit v4.2"
date:   2017-03-11 14:00:00 -0900
categories: pyrevit update
comments: true
---

Till now pyRevit had a big issue. If you would keep using pyRevit for an extensive amount of time without restarting Revit, the IronPython engine would slowly leak memory and would make the C# garbage collector extremely slow. This would cause Revit to slow down, and halt for a few seconds after each command. I ended up using the Windows native Performance Monitor tool and finally figured out that this short halt was due to the garbage collector taking too much time to clean up the unused memory between Revit actions. (Monitored the `.NET CLR Memory` usage for Revit, especifically `% of Time in GC`)

Anyways, the problem is solved now. I also added a tool under `Labs` pulldown that basically tests the IronPython engine under extreme use (Create an engine and run a script for 500 times). It also measures the execution time and draws a chart at the end.

There are also code cleanups and improvements to the output window under this new update, and as I posted here before, the addition of the charts module to pyRevit.

As always, looking forward to hearing your feedbacks.
