---
layout: page
title: Using and staying updated
permalink: /howtouse/
---

## Using the scripts:
After you installed pyRevit and launched Revit, the startup script will find all the individual scripts and creates the UI buttons for the commands.

Just click on the pyRevit tab and click on the command you'd like run. Most command names are self-explanatory but there is a tooltip on the more complicated commands that describes the function. This tooltip is created from `__doc__` string inside each `.py` file.


## Keeping your library up to date:
Use the `downloadUpdates` button under the `Settings` pull down to fetch all the recent changes from the github repository.

![DownloadUpdates](http://eirannejad.github.io/pyRevit/images/downloadUpdates.png)

**pyRevit** will open a window and will fetch the most recent changes from the github repository. Keep in mind that the changes you have made to the original scripts included in the library will be overwritten. Any extra scripts and file will remain intact. After the update, click on Reload Scripts to get buttons for any newly added script.

![FetchingUpdates](http://eirannejad.github.io/pyRevit/images/fetchingupdates.png)