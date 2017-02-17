---
layout: post
title:  "pyRevit Log Viewer"
date:   2017-02-16 14:00:00 -0900
categories: pyrevit update
comments: true
---

The log viewer tool is finally in good shape. This tool processes the log files (when logging to external file is activated in settings) and displays the log entries in a list.

&nbsp;

![]({{ site.url }}/pyRevit/images/logviewer.png)

&nbsp;

The viewer, processes the time, date, entry type, and logging module for each log entry and compiles a list of filters that could be used to filter the entry list.

&nbsp;

![]({{ site.url }}/pyRevit/images/logviewerfilter.png)

&nbsp;


You can also search for specific terms in log messages:

&nbsp;

![]({{ site.url }}/pyRevit/images/logviewersearch.png)

&nbsp;

This viewer helps me a lot in debugging the reported issues.