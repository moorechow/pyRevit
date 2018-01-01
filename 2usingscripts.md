---
layout: page
title: Staying updated
permalink: /howtouse/
---

## Keeping your library up to date:
Use the `Update` button under the `pyRevit` tab slide-out to fetch all the recent changes from the github repository.

&nbsp;

![]({{ site.url }}/pyRevit/images/update45.png)

&nbsp;

**pyRevit** will open a window and will fetch the most recent changes from the github repository. Keep in mind that the changes you have made to the original scripts included in the library will be overwritten. Any extra scripts and file will remain intact. After the update, pyRevit will automatically Reload to reflect the new changes.

&nbsp;

![]({{ site.url }}/pyRevit/images/2017-01-01 09_46_37-UpdateWindow.png)

&nbsp;

Every once in a while there will be a core update. But Revit keeps the `pyRevitLoader.dll` open when you're working with Revit and the pyRevit updater can not overwrite this dll. 

So the Update tool will checks the commit messages in the pyRevit repository to see if any of them includes `COREUPDATE`. This effectively tells the Update tool that there is an core update available and pyRevit needs to be updated when Revit is closed.

The Update tool will show this warning message and won't update your installed pyRevit repo:

&nbsp;

![]({{ site.url }}/pyRevit/images/coreupdatemessage.png)

&nbsp;

So to update pyRevit when Revit is closed, go to the folder listed in the message and run the batch tool. The batch script will pull and merge the most current pyRevit repository.

After update start Revit again and everything should be good to go.