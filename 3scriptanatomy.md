---
layout: page
title: Anatomy of a pyRevit script
permalink: /anatomyofpyrevitscript/
---

## Anatomy of a pyRevit script:

This is a quick look at a typical pyRevit script and the utilities that are available to it.

&nbsp;

### Basic script parameters:
``` python
"""You can place the docstring (tooltip) at the top of the script file.
This serves both as python docstring and also button tooltip in pyRevit.
You should use triple quotes for standard python docstrings."""
```

&nbsp;

You can also explicitly define the tooltip for this script file,
independent of the docstring defined at the top by defining `__doc__` parameter.

``` python
__doc__ = 'This is the text for the button tooltip associated with this script.'
```

&nbsp;

If you'd like the UI button to have a custom name different from the script name, you can define this variable.
For example, the `__title__` parameter is defined as shown below for this script.


``` python
__title__ = 'Sample\\nCommand'
```

&nbsp;

You can define the script author as shown below. This will show up on the button tooltip.

``` python
__author__ = 'Ehsan Iran-Nejad'
```

&nbsp;

### Logging:
For all logging, the 'scriptutils' module defines the default logger for each script. Here is how to use it:

``` python
from scriptutils import logger
logger.info('Test Log Level :ok_hand_sign:')

logger.warning('Test Log Level')

logger.critical('Test Log Level')
```

Critical and warning messages are printed in colour for clarity.


Another logging function is available for logging DEBUG messages. Normally these messages are not printed.
you can hold CTRL and click on a command button to put that command in DEBUG mode and see all its debug messages

``` python 
logger.debug('Yesss! Here is the debug message')
```

&nbsp;

### Shift-Clicking: Script Configuration:
You can create a script called `config.py` in your button bundle.
SHIFT-clicking on a ui button will run the configuration script.
Try Shift clicking on the Match tool in pyRevit > Modify panel and see the configuration window.

If you don't define the configuration script, you can check the value of `__shiftclick__` in your scripts
to change script behaviour

``` python
if __shiftclick__:
    do_task_A()
else:
    do_task_B()
```

&nbsp;

### Ctrl-Clicking: Debug Mode:
CTRL-clicking on a ui button will run the script in DEBUG mode and will allow the script to print all debug messages. Try CTRL Clicking on this button to see debug messages.

You can check the value of `__forceddebugmode__` variable to see if the script is running in Debug mode to change script behaviour if neccessary

``` python
if __forceddebugmode__:
	do_task_A()
else:
	do_task_B()
```

&nbsp;

### Alt-Clicking: Show Script file in Explorer:
ALT-clicking on a ui button will show the associated script file in windows explorer.

&nbsp;

### Script Information:
`scriptutils` module also provides a class to access the running script information and utilities:

``` python
from scriptutils import this_script

# script name
this_script.info.name

# script ui title (value set by __title__)
this_script.info.ui_title
# script unique name, generally used to create IExternalCommand class names
this_script.info.unique_name
# script unique name, generally used to create IExternalCommandAvailability class names
this_script.info.unique_avail_name
# script's command context (set by __context__)
this_script.info.cmd_context

# script's tooltip (set by script docstring or __doc__)
this_script.info.doc_string
# script's author (set by __author__)
this_script.info.author

# script file address
this_script.info.script_file
# script configuration file address
this_script.info.config_script_file
# script default icon file
this_script.info.icon_file

# script's library path if the script bundle includes a library
this_script.info.library_path

# Accessing the running pyRevit version
this_script.pyrevit_version
```

&nbsp;

### Custom User Configuration for Scripts:
Each script can save and load configuration pyRevit's user configuration file:

``` python
from scriptutils import this_script

# set a new config parameter: firstparam
this_script.config.firstparam = True

# saving configurations
this_script.save_config()

# read the config parameter value
if this_script.config.firstparam:
    do_task_A()
```


&nbsp;

### Most basic Revit utilities:
You can access the Document, UIDocument, and Selection elements from `revitutils` module. This module also contains other basic tools to help you with writing simple scripts.

``` python
from revitutils import doc, uidoc, selection
```

&nbsp;

### Using temporary files easily:
Scripts can create 3 different types of data files:

- **Universal files:** These files are not marked by host Revit version and could be shared between all Revit versions and instances.
These data files are saved in pyRevit appdata directory and are NOT cleaned up at Revit restarts. Script should manage cleaning up these data files.

``` python
# provide a unique file id and file extension
# Method will return full path of the data file
this_script.get_universal_data_file(file_id, file_ext)
```

- **Data files** (Shared only between instances of host Revit version): These files are marked by host Revit version and could be shared between instances of host Revit version
Data files are saved in pyRevit appdata directory and are NOT cleaned up at Revit restarts.
Script should manage cleaning up these data files.

``` python
# provide a unique file id and file extension
# Method will return full path of the data file
this_script.get_data_file(file_id, file_ext)
```

- **Instance Data files** (Accessible only to current Revit instance):
These files are marked by host Revit version and process Id and are only available to current Revit instance.
Data files are saved in pyRevit appdata directory and ARE cleaned up at Revit restarts.

``` python
# provide a unique file id and file extension
# Method will return full path of the data file
this_script.get_instance_data_file(file_id, file_ext)


# this is the standard instance data file that is setup by default for this script
this_script.instance_data_filename
```

&nbsp;

### Controlling Output Window:
Each script can control its own output window:

``` python
from scriptutils import this_script

this_script.output.set_height(600)
this_script.output.get_title()
this_script.output.set_title('Beautiful title')
```

&nbsp;

### Misc Parameters:
``` python
# Revit UIApplication is accessable through:
__revit__

# Command data provided to this command by Revit is accessable through:
__commandData__

# and UI Controlled application is:
__uiControlledApplication__
```
