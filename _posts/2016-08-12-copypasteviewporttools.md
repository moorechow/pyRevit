---
layout: post
title:  "Copy/Paste Viewport placement tools"
date:   2016-08-12 08:00:00 -0900
categories: pyrevit update
comments: true
---


Hello again!!

It's been a while since my first post on this blog, so you know how excited I am right now to update you on the recent changes.

[Gui Talarico](https://github.com/gtalarico), an awesome python programmer who is also heavily involved with BIM Automation and Scripting has started contributing to pyRevit and that's very very exciting for me. He's developed a host of amazing tools for the company that he's working for and is also contributing and writing scripts for the pyRevit library on his own time.

***Teaser:*** we're working on modifying the pyRevit to be able to accept and automatically load 'Extensions' (meaning other third-party independent python script libraries). This gives developers and enthusiasts the ability to easily add their own workflow-specific scripts to the pyRevit library without any modifications to the original pyRevit repository and use other libraries shared by others and stay updated.

### New scripts for matching viewport placement:

So I was saying, Gui, has developed two scripts for copying and pasting viewport placement from and onto viewports which tremendously helps with aligning viewports on multiple sheets. I also made the scripts smarter in regards to plan model views (Plan/RCP) and model space so it can place model views accurately and not based on their relative viewport centers and really based on their model space coordinates. This way you can place a 2nd floor plan view in the exact same spot as the 1st floor plan view on a sheet, independent of their relative crop boundaries and viewport centers.


Make sure to test them out. These scripts are:

-	`Memory > Memory_copyViewportPlacement.py`
-	`Memory > Memory_pasteViewportPlacement.py`

![](http://eirannejad.github.io/pyRevit/images/copyviewportplacement.png)
![](http://eirannejad.github.io/pyRevit/images/pasteviewportplacement.png)
