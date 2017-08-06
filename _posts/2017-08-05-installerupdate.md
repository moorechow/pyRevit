---
layout: post
title:  "Installer update"
date:   2017-08-05 09:00:00 -0900
categories: pyrevit update
comments: true
---

I just pushed an update for the pyRevit installer to address these two issues:
[Issue #209](https://github.com/eirannejad/pyRevit/issues/209)
[Issue #208](https://github.com/eirannejad/pyRevit/issues/208)

[**See this video on pyRevit installer updates.**]()

<div style='position: relative; width: 100%; height: 0px; padding-bottom: 60%;'>
<iframe style='position: absolute; left: 0px; top: 0px; width: 100%; height: 100%' src="https://www.youtube.com/embed/L1GFOJAc7qE?showinfo=0" frameborder="0" allowfullscreen></iframe>
</div>


&nbsp;

System admins can also use the `pyrevitgitservices.exe` tool that is shipped with pyRevit to perform basic tasks on the pyRevit repository. [See this page for full documentation of the tool.](https://github.com/eirannejad/pyRevit/tree/master/release)

For example this is how you can set the version of pyRevit repo using this tool:

```batch
pyrevitgitservices.exe setversion <pyrevit_installation_path> <hash commit>
```

And as a more specific example, this would set the pyRevit repo version to [this commit](https://github.com/eirannejad/pyRevit/commit/71ec1e5d4588205ae0064e9b35ec10c3dc113248):

```batch
pyrevitgitservices.exe setversion "C:\pyRevit" 71ec1e5
```

This new executive lives under:

`<pyrevit install path>\release\pyrevitgitservices\pyrevitgitservices\bin\Release\`

Hope this change helps managing pyRevit in large environment a bit easier.