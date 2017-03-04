---
layout: post
title:  "Pattern Maker for Revit"
date:   2017-03-04 14:00:00 -0900
categories: pyrevit update
comments: true
---

I finally finished a tool that I have been working on for over a week now. I always thought it's really odd that Revit does not have a native pattern maker and the users have to buy third-party products for a simple vital feature like this.

So I made one for pyRevit. Here is how it works:

**Step 1:** Draw your pattern tile in Revit using detail lines. Cool thing is that you can use all sorts of curves and splines to draw your pattern and the pattern maker will approximate the curves with small line segments later.

&nbsp;

![]({{ site.url }}/pyRevit/images/pattern1.png)

&nbsp;

**Step 2:** Select the pattern lines and curves (don't include your pattern boundary)

&nbsp;

![]({{ site.url }}/pyRevit/images/pattern2.png)

&nbsp;

**Step 3:** run the `Make Pattern` tool under `Modify > Edit` pulldown.

&nbsp;

![]({{ site.url }}/pyRevit/images/pattern3.png)

&nbsp;

**Step 4:** Type a name for the pattern, select the pattern type (Model or Detail) and select the `Create Filled Region` checkbox if you also want a filled region type to be created (with the same name) for this new pattern. Then hit `Create Pattern`. 

&nbsp;

![]({{ site.url }}/pyRevit/images/pattern4.png)

&nbsp;

**Step 5:** Select the opposite boundary points. (Normally bottom-left and top-right but it really doesn't matter which diagonal is picked.)

&nbsp;

![]({{ site.url }}/pyRevit/images/pattern5.png)

&nbsp;

And voila, the pattern is created and pattern maker will show a pop-up window when it's completed.

&nbsp;

![]({{ site.url }}/pyRevit/images/pattern6.png)

&nbsp;

