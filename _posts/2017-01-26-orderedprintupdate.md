---
layout: post
title:  "Updates to print tool"
date:   2017-01-26 13:00:00 -0900
categories: pyrevit update
comments: true
---

Taking to [Austin Rosenfeld](https://github.com/ThubanPDX) on printing Architectural sheets to PDF (in order) and combining all drawings from all disciplines into a single PDF, he suggested that the print tool should show the placeholder (non-printable) sheets and allow for indexing space for these sheets.

As an example, Civil and Landscape drawings usually fall in between Cover sheet and Architectural sheets. By allowing indexing space, these sheets can be manually numbered later and added to the printed Architectural sheets folder so all of them can be combined in one pass into a single PDF document.

So the Print Ordered Sheets tool has been updated and now shows the placeholder sheets and gives the user the option to allow indexing space:

![]({{ site.url }}/pyRevit/images/printorderedsheetsupdated.png)
