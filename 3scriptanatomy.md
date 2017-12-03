---
layout: page
title: Anatomy of a pyRevit script
permalink: /anatomyofpyrevitscript/
---

## Anatomy of a pyRevit script:

This is a quick look at a typical pyRevit script and the utilities that are available to it.

&nbsp;

### Contents:

- [Basic script parameters](#basic-script-parameters)
- [Logging](#logging)
- Modifier Keys:
	- [Shift-Clicking](#shift-clicking-script-configuration)
	- [Ctrl-Clicking](#ctrl-clicking-debug-mode)
	- [Alt-Clicking](#alt-clicking-show-script-file-in-explorer)
- [Command Availability](#command-availability)
- [Script Information](#script-information)
- [Custom User Configuration for Scripts](#custom-user-configuration-for-scripts)
- [Basic Revit utilities](#most-basic-revit-utilities)
- [Temporary files](#using-temporary-files-easily)
- [Controlling Output Window](#controlling-output-window)
- [Misc Parameters](#misc-parameters)
- [Appendix A: System Categories](#appendix-a-system-category-names)


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
For all logging, the `scriptutils` module defines the default logger for each script. Here is how to use it:

``` python
from scriptutils import logger
logger.info('Test Log Level :ok_hand_sign:')

logger.warning('Test Log Level')

logger.critical('Test Log Level')
```

Critical and warning messages are printed in color for clarity.


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
CTRL-clicking on a ui button will run the script in DEBUG mode and will allow the script to print all debug messages.

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

### Command Availability:
Revit commands use standard `IExternalCommandAvailability` class to let Revit know if they are available in different situations. For example, if a command needs to work on currently selected elements, it can tell Revit to deactivate the button unless the user has selected some elements.

In pyRevit, command availability is set through the `__context__` variable. Currently, pyRevit support two types of command availability types.

- `__context__ = 'Selection'`<br/>(Tool activates when at least one element is selected)
- `__context__ = 'zerodoc'`<br/>(Tools are active even when there are not documents open in Revit)
- `__context__ = '<Element Category>'`<br/>(Tool activates when all selected elements are of the given category)


**Element Categories:**

`<Element Category>` can be any of the standard Revit element categories. Here are a few examples:

``` python
# Tool activates when all selected elements are of the given category

__context__ = 'Doors'
__context__ = 'Walls'
__context__ = 'Floors'
__context__ = 'Space Tags'
```

See [Appendix A](#appendix-a-system-category-names) for a full list of system categories. You can use the `List` tool under `pyRevit > Spy` and list the standard categories.

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

### Using temporary files easily:
Scripts can create 3 different types of data files:

- **Universal files:** These files are not marked by host Revit version and could be shared between all Revit versions and instances.
These data files are saved in pyRevit's appdata directory and are NOT cleaned up at Revit restarts.

***Note: Script should take care of cleaning up these data files.***

``` python
# provide a unique file id and file extension
# Method will return full path of the data file
this_script.get_universal_data_file(file_id, file_ext)
```

- **Data files** (Shared only between instances of host Revit version): These files are marked by host Revit version and could be shared between instances of host Revit version
Data files are saved in pyRevit's appdata directory and are NOT cleaned up when Revit restarts.

***Note: Script should take care of cleaning up these data files.***

``` python
# provide a unique file id and file extension
# Method will return full path of the data file
this_script.get_data_file(file_id, file_ext)
```

- **Instance Data files** (Accessible only to current Revit instance):
These files are marked by host Revit version and process Id and are only available to current Revit instance. This avoids any conflicts between similar scripts running under two or more Revit instances.
Data files are saved in pyRevit's appdata directory (with extension `.tmp`) and ARE cleaned up when Revit restarts.

``` python
# provide a unique file id and file extension
# Method will return full path of the data file
this_script.get_instance_data_file(file_id)


# this is the standard instance data file that is setup by default for this script
this_script.instance_data_filename
```

- **Document Data files** (Shared only between instances of host Revit version): These files are marked by host Revit version and name of Active Project and could be shared between instances of host Revit version.
Data files are saved in pyRevit's appdata directory and are NOT cleaned up when Revit restarts.

***Note: Script should take care of cleaning up these data files.***

``` python
# provide a unique file id and file extension
# Method will return full path of the data file
this_script.get_document_data_file(file_id, file_ext)

# You can also pass a document object to get a data file for that
# document (use document name in file naming)
this_script.get_document_data_file(file_id, file_ext, doc)

```

&nbsp;

### Controlling Output Window:
Each script can control its own output window:

``` python
from scriptutils import this_script

this_script.output.set_height(600)
this_script.output.get_title()
this_script.output.set_title('More control please!')
```

&nbsp;

### Misc Parameters:
``` python
# Revit UIApplication is accessible through:
__revit__

# Command data provided to this command by Revit is accessible through:
__commandData__

# and UI Controlled application is:
__uiControlledApplication__
```

&nbsp;

### Appendix A: System Category Names:
```
Adaptive Points
Air Terminal Tags
Air Terminals
Analysis Display Style
Analysis Results
Analytical Beam Tags
Analytical Beams
Analytical Brace Tags
Analytical Braces
Analytical Column Tags
Analytical Columns
Analytical Floor Tags
Analytical Floors
Analytical Foundation Slabs
Analytical Isolated Foundation Tags
Analytical Isolated Foundations
Analytical Link Tags
Analytical Links
Analytical Node Tags
Analytical Nodes
Analytical Slab Foundation Tags
Analytical Spaces
Analytical Surfaces
Analytical Wall Foundation Tags
Analytical Wall Foundations
Analytical Wall Tags
Analytical Walls
Annotation Crop Boundary
Area Load Tags
Area Tags
Areas
Assemblies
Assembly Tags
Boundary Conditions
Brace in Plan View Symbols
Cable Tray Fitting Tags
Cable Tray Fittings
Cable Tray Runs
Cable Tray Tags
Cable Trays
Callout Boundary
Callout Heads
Callouts
Cameras
Casework
Casework Tags
Ceiling Tags
Ceilings
Color Fill Legends
Columns
Communication Device Tags
Communication Devices
Conduit Fitting Tags
Conduit Fittings
Conduit Runs
Conduit Tags
Conduits
Connection Symbols
Contour Labels
Crop Boundaries
Curtain Grids
Curtain Panel Tags
Curtain Panels
Curtain System Tags
Curtain Systems
Curtain Wall Mullions
Data Device Tags
Data Devices
Detail Item Tags
Detail Items
Dimensions
Displacement Path
Door Tags
Doors
Duct Accessories
Duct Accessory Tags
Duct Color Fill
Duct Color Fill Legends
Duct Fitting Tags
Duct Fittings
Duct Insulation Tags
Duct Insulations
Duct Lining Tags
Duct Linings
Duct Placeholders
Duct Systems
Duct Tags
Ducts
Electrical Circuits
Electrical Equipment
Electrical Equipment Tags
Electrical Fixture Tags
Electrical Fixtures
Electrical Spare/Space Circuits
Elevation Marks
Elevations
Entourage
Filled region
Fire Alarm Device Tags
Fire Alarm Devices
Flex Duct Tags
Flex Ducts
Flex Pipe Tags
Flex Pipes
Floor Tags
Floors
Foundation Span Direction Symbol
Furniture
Furniture System Tags
Furniture Systems
Furniture Tags
Generic Annotations
Generic Model Tags
Generic Models
Grid Heads
Grids
Guide Grid
HVAC Zones
Imports in Families
Internal Area Load Tags
Internal Line Load Tags
Internal Point Load Tags
Keynote Tags
Level Heads
Levels
Lighting Device Tags
Lighting Devices
Lighting Fixture Tags
Lighting Fixtures
Line Load Tags
Lines
Masking Region
Mass
Mass Floor Tags
Mass Tags
Matchline
Material Tags
Materials
Mechanical Equipment
Mechanical Equipment Tags
MEP Fabrication Containment
MEP Fabrication Containment Tags
MEP Fabrication Ductwork
MEP Fabrication Ductwork Tags
MEP Fabrication Hanger Tags
MEP Fabrication Hangers
MEP Fabrication Pipework
MEP Fabrication Pipework Tags
Multi-Category Tags
Multi-Rebar Annotations
Nurse Call Device Tags
Nurse Call Devices
Panel Schedule Graphics
Parking
Parking Tags
Part Tags
Parts
Pipe Accessories
Pipe Accessory Tags
Pipe Color Fill
Pipe Color Fill Legends
Pipe Fitting Tags
Pipe Fittings
Pipe Insulation Tags
Pipe Insulations
Pipe Placeholders
Pipe Segments
Pipe Tags
Pipes
Piping Systems
Plan Region
Planting
Planting Tags
Plumbing Fixture Tags
Plumbing Fixtures
Point Clouds
Point Load Tags
Project Information
Property Line Segment Tags
Property Tags
Railing Tags
Railings
Ramps
Raster Images
Rebar Cover References
Rebar Set Toggle
Rebar Shape
Reference Lines
Reference Planes
Reference Points
Render Regions
Revision Cloud Tags
Revision Clouds
Roads
Roof Tags
Roofs
Room Tags
Rooms
Routing Preferences
Schedule Graphics
Scope Boxes
Section Boxes
Section Line
Section Marks
Sections
Security Device Tags
Security Devices
Shaft Openings
Sheets
Site
Site Tags
Space Tags
Spaces
Span Direction Symbol
Specialty Equipment
Specialty Equipment Tags
Spot Coordinates
Spot Elevation Symbols
Spot Elevations
Spot Slopes
Sprinkler Tags
Sprinklers
Stair Landing Tags
Stair Paths
Stair Run Tags
Stair Support Tags
Stair Tags
Stair Tread/Riser Numbers
Stairs
Structural Annotations
Structural Area Reinforcement
Structural Area Reinforcement Symbols
Structural Area Reinforcement Tags
Structural Beam System Tags
Structural Beam Systems
Structural Column Tags
Structural Columns
Structural Connection Tags
Structural Connections
Structural Fabric Areas
Structural Fabric Reinforcement
Structural Fabric Reinforcement Symbols
Structural Fabric Reinforcement Tags
Structural Foundation Tags
Structural Foundations
Structural Framing
Structural Framing Tags
Structural Internal Loads
Structural Load Cases
Structural Loads
Structural Path Reinforcement
Structural Path Reinforcement Symbols
Structural Path Reinforcement Tags
Structural Rebar
Structural Rebar Coupler Tags
Structural Rebar Couplers
Structural Rebar Tags
Structural Stiffener Tags
Structural Stiffeners
Structural Truss Tags
Structural Trusses
Switch System
Telephone Device Tags
Telephone Devices
Text Notes
Title Blocks
Topography
View Reference
View Titles
Viewports
Views
Wall Tags
Walls
Window Tags
Windows
Wire Tags
Wires
Zone Tags
```
