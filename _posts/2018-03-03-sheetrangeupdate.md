---
layout: post
title:  "Updates report"
date:   2018-03-03 09:30:00 -0900
categories: pyrevit update
comments: true
---

Here is a list of most recent updates on pyRevit tools:

#### Batch Sheet Maker

Batch sheet maker now lets you create a range or list of sheets with the same sheet name very easily.

This example will create a range of sheets (A102 to A108) in the current model:

``` text
A102:A108			Sheet Name
```

You can also list specific sheet numbers that use the same sheet name. This example creates sheets A100, S100, E100, and M100 all with the same sheet name:

``` text
A100,S100,E100,M100			General Notes
```


![]({{ site.url }}/pyRevit/images/sheetrange.png)

#### Copy Sheets to Open Documents

This tool now can copy and set the revisions on the sheets being copied to the destination models as well:

![]({{ site.url }}/pyRevit/images/copysheetrevisions.png)


#### Other Misc Updates

Other tools have also been updated to show a list of views instead of relying on view selection in the project browser:

![]({{ site.url }}/pyRevit/images/selectviewsfromlist.png)

The revision tools have also been updated to allow setting, unsetting multiple revisions at the same time:

![]({{ site.url }}/pyRevit/images/selectmultiplerevisions.png)
