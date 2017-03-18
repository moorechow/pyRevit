---
layout: post
title:  "pyRevit unstable branch or how I learned to love unittest"
date:   2017-03-18 08:00:00 -0900
categories: pyrevit update
comments: true
---

I made a rookie mistake this past week. I made some changes to the way output window works and without fully testing it, pushed it to the pyRevit's github repository. This broke pyRevit for some of you and I deeply apologise for that.

To make sure this never happens again, I thought I can create a new unstable branch every week (This week's is `unstable-w11`), make my changes and improvements during the week, run tests over the weekend and fix any issue, and then fully commit the changes to the online pyRevit repository on github.

pyRevit is still in it's infancy and needs a lot of work. That's why I like to get you the changes and updates as quickly as possible so you all are involved in the process and can plan your workflows accordingly or give me feedback on how you think certain features should work.

## Revision report tool:
To showcase some of the power of the output window and also to make a useful tool that I can use during the Construction Admin of my current project, I added a tool to the `Revisions` pull down, that reports the current state of the revisions being made to the project.

This output can be printed directly to a printer using the `Print Preview` and `Print` options on the output window context menu (right-click).

![]({{ site.url }}/pyRevit/images/revisionreport.png)

## Other changes:
Anyway, I committed this week's changes to the master. Here are a list of improvements:

- Pattern maker now can export patterns to PAT files. I also made other changes to the pattern maker to improve performance with very small detail patterns.

![]({{ site.url }}/pyRevit/images/exportpatfile.png)

- You can set the height and width of charts using `.set_width()` and `.set_height()` methods on the chart object:


``` python 
chart = console.make_bar_chart()
chart.set_height(100)		# in pixels
```
	
- To better control the output results use these methods on the output window:
	- Lock the output window size using `.lock_size()`
	- To add a page break use `.next_page()` method. Any contents printed after this method, will be created on the new page when printing.
	- To add a horizontal line use `.insert_divider()` method. This draws a horizontal line in the output window and is a great way to separate content.
	- If you're using markdown or html output, you can use the `.add_style()` method to add `CSS` styling to the output window (this adds the style to the html head so it applies to html code that has been already printed as well):

``` python 
console = this_script.output
console.set_height(800)
console.lock_size()
print('TITLE')
console.insert_divider()

# create a page break in html output
console.next_page()

# adding css styles
console.add_style("""
table { width:100% }
table, th, td { border-bottom: 1px solid #aaa; }
""")

```

- Markdown module was also updated to 2.6.8
- I created a pyRevit development tools package and moved the labs and work-in-progress tools into the extension. This extension is disabled by default so if you want to see it please activate it in the `Extensions` tool. I added a Unit tests button to this extension and will work on creating multiple unit tests to automate testing of pyRevit components before publishing updates.

![]({{ site.url }}/pyRevit/images/devtools.png)

- Untill now, the output window had a output buffer size limit. IronPython's stream breaks the output stream in chunks of 1024 characters. This means that if a script is printing a large string, or a big chunk of html data (could be a large graph for example), the output stream would break this stream into multiple pieces and in order to show them correctly in the html format, it would wrap each broken piece in a `<div>` tag. This meant that the html code for the graph would be broken and not displayed correctly. I finally found a way to fix this issue.
