---
layout: post
title:  "Semantic Versioning and pyRevit v3"
date:   2016-08-16 8:00:00 -0900
categories: pyrevit update
comments: true
---


Today I learned more about [Semantic Versioning ](http://semver.org) and realized I'm incrementing the version numbers incorrectly in pyRevit.

Semantic versioning proposes X.Y.Z (Major.Minor.Patch) format but also says when Y is incremented, Z must be reset to zero. But until now, I did not do any such resetting on the values.

With the current versioning method, the X is for major backward-incompatible changes; Y is for new scripts added to the library; and Z is for all the minor patches and fixes to any of the scripts.

Now to the more exciting part:

We're testing the pyRevit v3.0.0-alpha right now. The beta will be available soon and it has a few important changes:

- pyRevit now supports Extensions! There is a package manager that handles installation/update/and removal of thrid-party extensions. [Gui Talarico](https://github.com/gtalarico) is working on the first of them and it's called ***PyRevitPlus***. Each extension will get its own tab. This means that you can create your own libraries of scripts and share them on github and other users can install them using the package manager.
- If the current file naming convention is confusing for you, we've modified the startup script to work with scripts organized in folders. This means that instead of properly naming `*.png` files to create panels and pulldown buttons, you can throw the scripts in a folder for the pulldown button and place that inside another folder for the panel. I'll publish the details of this later.  ***PyRevitPlus*** will be organized under this system and it's a great example.
- pyRevit now has a settings window. You can enable/disable Verbose reporting at startup, and the usage log system as of now.

	![]({{ site.url }}/pyRevit/images/2016-08-16 18_18_07-pyRevit user settings.png)

- There is also a nicer About window with links to this website and credits.

	![]({{ site.url }}/pyRevit/images/2016-08-16 18_17_54-About pyRevit.png)

- pyRevit v3 also has a better Install and Uninstall system. The setup package is a smaller download, it's easier to use, and installs pyRevit in the `%appdata%` folder by default. This means that pyRevit lives in the user roaming directory and follows a user about in an Active Directory environment. It is also only available to just the user that installs it. This can be useful if, for example, you have multiple Revit users on the same machine, who may need to run different configurations of pyRevit based on their Revit level of expertise.


Back to the semantic versioning. I'll use it the right way starting with version 3.0.0 :)

Stay tuned.