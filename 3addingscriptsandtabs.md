---
layout: page
title: Adding your own scripts
permalink: /howtoaddscriptsandtabs/
---

## Adding your own scripts:

The `__init__.py` startup script will setup a ribbon panel named 'pyRevit' (After the folder name that contains the scripts). There are 5 ways to add buttons to this ribbon panel and categorize them under sub-panels.

...

#### All the methods listed below, require a 32x32 pixel `.png` image file that will be used as the icon for the button or button group. If you're creating your own icons, in Photoshop, just use Save For Web, Legacy PNG-8. Otherwise the icon might be displayed larger than expected.

...

### Creating Pull Down Buttons:

![PulldownDemo](http://eirannejad.github.io/pyRevit/images/pulldownbuttondemo.png)  

- **Step 1:** Create a `.png` file, with this naming pattern:
`<00 Panel Order><00 Button Order>_<Panel Name>_PulldownButton_<Button Group Name>.png`
  
  **Example:**  
  `1003_Selection_PulldownButton_Filter.png`  
  This `.png` file, defines a sub-panel under `pyRevit` ribbon panel named `Selection`, and a `PulldownButton` named `Filter` under this panel. Startup script will use the order numbers to sort the panels and buttons and later to create them in order.

- **Step 2:** All `.py` script under the home directory should have the below name pattern:
`<Button Group Name>_<Script Command Name>.py`.  
For the example above, any script that its name starts with `Filter_` will be added to this PullDown button.

Scripts will be organized under the group button specified in the source file name. For example a script file named `Filter_filterGroupedElements.py` will be placed under group button `Filter` (defined by the `.png` above) and its command name will be `filterGroupedElements`. The `.png` file defining the Pulldown Button will be used as button icon by default, however, if there is a `.png` file with a matching name to a script, that `.png` file will override the default image and will be used as the button icon.

### Split Buttons: Creating Pull Down buttons that remember the last clicked button

Same as Method 1 except it will create Split Buttons (The last selected sub-item will be the default active item).

For a SplitButton, create a `.png` file, with this naming pattern:
`<00 Panel Order><00 Button Order>_<Panel Name>_SplitButton_<Button Group Name>.png`

![SplitDemo](http://eirannejad.github.io/pyRevit/images/splitbuttondemo.png)  


### Creating Push Buttons:  

![PushbuttonDemo](http://eirannejad.github.io/pyRevit/images/pushbuttondemo.png)  

- **Step 1:** Create a `.png` file, with this naming pattern:
`<00 Panel Order><00 Button Order>_<Panel Name>_PushButton_<Push Button Name>.png`

	**Example:**  
	`1005_Revit_PushButton_Get Central Path.png` defines a sub-panel under `pyRevit` named `Revit`, and a simple Push Buttons in this panel named `BIM`. The startup script will expect to find only one script with name pattern similar to Method 1, and will assign it to this push button.

- **Step 2:** All `.py` script under the home directory should have the below name pattern:
`<Push Button Name>_<Script Name>.py`.  
For the example above, `Get Central Path_action.py` will be assigned to the push button described above. Not that the The button name shown in the ribbon will be `Get Central Path` (`<Push Button Name>`) since this button only performs one action. The `<Script Name>` will be ignored.

#### Link Buttons: Creating Push Buttons that Run other Addin's Commands

This button is very similar to a PushButton except that it creates a link to a command of any other addin.  
Create a `.png` file, with the same naming pattern as Method 1, but also add `<Assembly Name>` and `<C# Class Name>` to the filename separated by `_`.

**Example:**  
`0000_RPS_PushButton_RPS_RevitPythonShell_IronPythonConsoleCommand.png`

This defines a sub-panel under `pyRevit`, named `RPS`, and a simple Push Buttons in this panel, named `RPS`. But then the startup script will use the `<Assembly Name>` and `<C# Class Name>` and will look for the referenced addin and class. If this addin has been already loaded into Revit, the startup script will assign the `<C# Class Name>` to this button. In this example, startup script will create a button that opens the 'Interactive Python Shell' from RevitPythonShell addin.

Another example of this method is `0005_RL_PushButton_Lookup_RevitLookup_CmdSnoopDb.png` that will create a button calling the 'Snoop DB' command of the [RevitLookup](https://github.com/jeremytammik/RevitLookup) addin.

Notice that this type of button does not need any external scripts. This single `.png` file has all the necessary information for this link button.


### Creating Stack of Push Buttons:

![Stack3Demo](http://eirannejad.github.io/pyRevit/images/stackthreedemo.png)

- **Step 1:** Create a `.png` file, with this naming pattern:
`<00 Panel Order><00 Button Order>_<Panel Name>_Stack3_<Stack Name>.png`  

	**Example:**  
	`1005_Selection_Stack3_Inspect.png` defines a sub-panel under `pyRevit`named `Selection`, and 3 Stacked Buttons in this panel. For a Stack3 button group, the startup script will expect to find exactly 3 scripts to be categorized under this stack. The actual stack name will be ignored since the stack doesn't have any visual representation other then the 3 buttons stacked in 3 rows.

- **Step 2:** All `.py` script under the home directory should have the below name pattern:
`<Button Group Name>_<Script Command Name>.py`.  
In this example the 3 scripts below will be used to create 3 buttons in this stack (sorted alphabetically):

	`Inspect_findLinkedElements.py`  
	`Inspect_findListOfViewsShowingElement.py`  
	`Inspect_findPaintedSurfacesOnSelected.py`  

## Adding your own tabs:

By default, the `__init__.py` script will load all the scripts inside the `pyRevit` folder provided in this repository. But the `__init__.py` script, will also look for other folders in its directory. This means that you can create other folders alongside the `__init__.py` and `pyRevit` and place your custom scripts under those. The `__init__.py` script will create a dedicated tab (with the folder name) for each of the folders that contains scripts, in Revit ribbon after `pyRevit` tab.

This is the preferred method for adding your custom scripts and categories to the pyRevit library. This way, the original pyRevit library can remain intact and always be updated from the github repository to the latest version.

## Reloading the scripts library:

![ReloadScripts](http://eirannejad.github.io/pyRevit/images/reloadScripts.png)

pyRevit commands only keep a link to the actual IronPython script file. Revit reads and runs the script file any time the user clicks on the corresponding button. This means you can edit any script while Revit is running and the next time you click on the corresponding script button, Revit will run the modified script file.

If you added scripts or panels while Revit is running, use the `reloadScripts` button from the `Settings` group to reload the changes. It'll search for the scripts and will update the buttons, disabling the missing and adding the newly found.