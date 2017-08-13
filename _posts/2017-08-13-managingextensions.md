---
layout: post
title:  "Managing pyRevit Extensions"
date:   2017-08-13 09:00:00 -0900
categories: pyrevit extension
comments: true
---

Hello :D

I just published a video explaining methods to control and manage your company wide extensions. If you're involved in such a task, I highly encourage you to watch this as it explains the inner workings of the pyRevit Extension Manager and how to control access to your extensions:


<div style='position: relative; width: 100%; height: 0px; padding-bottom: 60%;'>
<iframe style='position: absolute; left: 0px; top: 0px; width: 100%; height: 100%' src="https://www.youtube.com/embed/v8d5FA-7suw?showinfo=0" frameborder="0" allowfullscreen></iframe>
</div>

&nbsp;

Here is the example of the dictionary key to control user access to your extensions (add it inside your own pyRevit extension definition file - watch the video if you don't know how):

``` python
# add as many users as need access to this extension
# the usernames are Revit usernames (not necessarily/always the machine user name)
"authusers": ["username1", "username2", "username3"]
```

&nbsp;

I also posted a video on using git to manage your source codes inside your extension and also as a way to share that with people outside your company:


<div style='position: relative; width: 100%; height: 0px; padding-bottom: 60%;'>
<iframe style='position: absolute; left: 0px; top: 0px; width: 100%; height: 100%' src="https://www.youtube.com/embed/n8K-JXfmv-s?showinfo=0" frameborder="0" allowfullscreen></iframe>
</div>
