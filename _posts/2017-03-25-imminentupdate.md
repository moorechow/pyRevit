---
layout: post
title:  "Important: Upcoming core update"
date:   2017-03-25 09:00:00 -0900
categories: pyrevit update
comments: true
---

Please update your pyRevits. Here is why:

The branch `unstable-w12` has a major update to the IronPython core. I've updated the IronPython to 2.7.7 which resolves a large number of errors with the IronPython engine including better unicode string support.

I'll merge this update at the end of March. But to be able to get this update gracefully you need to update pyRevit now so you can get the new `Update` tool that has been made smarter to look for core updates.

This was my challenge:

Revit keeps the `pyRevitLoader.dll` open when you're working with Revit and the pyRevit updater can not overwrite this dll. I had to figure out a way to fix this issue forever.

So the solution that I came up with was to modify the updates to check the commit messages in the pyRevit repository and check to see if any of them includes `COREUPDATE`. This effectively tells the updater that there is an core update available and pyRevit needs to be updated when Revit is closed.

The updater will show this warning message and won't update your installed pyRevit repo:

&nbsp;

![]({{ site.url }}/pyRevit/images/coreupdatemessage.png)

&nbsp;

So to update pyRevit when Revit is closed, go to the folder below under the installed folder and run the batch tool. The batch script will pull and merge the pyRevit repository.

After update start Revit again and everything should be good to go.


### OR

You can just remove and reinstall pyRevit :)


Sorry. Updates are hard!
