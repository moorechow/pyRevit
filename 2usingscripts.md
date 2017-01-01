---
layout: page
title: Using and staying updated
permalink: /howtouse/
---

## Using the scripts:
After you installed pyRevit and launched Revit, the startup script will find all the individual scripts and creates the UI buttons for the commands.

Just click on the pyRevit tab and click on the command you'd like run. Most command names are self-explanatory but there is a tooltip on the more complicated commands that describes the function.

####Shift-Clicking:
Some buttons show a black dot after the button name. These buttons have their own configuration. You can open the configuration by Shift-Clicking on the button.

####Ctrl-Clicking:
This puts the command in DEBUG mode and will allow the command to print all debug messages. This is obviously really helpful for debugging.

####Alt-Clicking:
This will show the script file that is tied to this command button in Windows Explorer.

## Keeping your library up to date:
Use the `Update` button under the `pyRevit` tab slide-out to fetch all the recent changes from the github repository.

![]({{ site.url }}/pyRevit/images/)

**pyRevit** will open a window and will fetch the most recent changes from the github repository. Keep in mind that the changes you have made to the original scripts included in the library will be overwritten. Any extra scripts and file will remain intact. After the update, pyRevit will automatically Reload to reflect the new changes.

![]({{ site.url }}/pyRevit/images/)