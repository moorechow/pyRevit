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

### Most basic Revit utilities:
You can access the Document, UIDocument, and Selection elements from `revitutils` module. This module also contains other basic tools to help you with writing simple scripts.

``` python
from revitutils import doc, uidoc, selection
```

&nbsp;

### Using temporary files easily:
Scripts can create 3 different types of data files:

- **Universal files:** These files are not marked by host Revit version and could be shared between all Revit versions and instances.
These data files are saved in pyRevit appdata directory and are NOT cleaned up at Revit restarts.

***Note: Script should take care of cleaning up these data files.***

``` python
# provide a unique file id and file extension
# Method will return full path of the data file
this_script.get_universal_data_file(file_id, file_ext)
```

- **Data files** (Shared only between instances of host Revit version): These files are marked by host Revit version and could be shared between instances of host Revit version
Data files are saved in pyRevit appdata directory and are NOT cleaned up when Revit restarts.

***Note: Script should take care of cleaning up these data files.***

``` python
# provide a unique file id and file extension
# Method will return full path of the data file
this_script.get_data_file(file_id, file_ext)
```

- **Instance Data files** (Accessible only to current Revit instance):
These files are marked by host Revit version and process Id and are only available to current Revit instance. This avoids any conflicts between similar scripts running under two or more Revit instances.
Data files are saved in pyRevit appdata directory (with extension `.tmp`) and ARE cleaned up when Revit restarts.

``` python
# provide a unique file id and file extension
# Method will return full path of the data file
this_script.get_instance_data_file(file_id)


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
this_script.output.set_title('More control please!')
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

&nbsp;

### Appendix A: System Category Names:
``` python
Part Tags
MEP Fabrication Hangers
Pipe Insulation Tags
Analytical Floors
Mechanical Equipment Tags
Ramps
Cable Tray Fittings
Foundation Span Direction Symbol
Communication Device Tags
Analytical Wall Tags
Structural Connections
Planting
Ceiling Tags
Annotation Crop Boundary
Analytical Wall Foundations
Furniture Tags
Mass
Air Terminals
Pipe Accessory Tags
Security Device Tags
Window Tags
Stair Tread/Riser Numbers
MEP Fabrication Ductwork Tags
Communication Devices
Piping Systems
Panel Schedule Graphics
Detail Item Tags
Reference Lines
MEP Fabrication Containment
Analytical Spaces
Plumbing Fixtures
Structural Framing Tags
Ceilings
Section Boxes
MEP Fabrication Ductwork
Elevation Marks
Data Device Tags
Pipe Segments
Crop Boundaries
Conduit Fittings
Sprinklers
Doors
Lighting Fixture Tags
Lighting Devices
Assembly Tags
Duct Tags
Curtain Systems
Structural Rebar Tags
Parking
Ducts
Door Tags
Internal Area Load Tags
Revision Clouds
MEP Fabrication Hanger Tags
Imports in Families
Conduits
Multi-Category Tags
Analytical Isolated Foundations
Flex Pipes
Property Line Segment Tags
Curtain Panel Tags
Analytical Links
Structural Trusses
HVAC Zones
Mass Floor Tags
Electrical Spare/Space Circuits
Site
Analytical Columns
Duct Systems
Zone Tags
Duct Placeholders
Reference Planes
Cable Tray Tags
Multi-Rebar Annotations
Matchline
Specialty Equipment Tags
Duct Accessories
Duct Fitting Tags
Furniture System Tags
Callout Heads
Furniture Systems
Telephone Devices
Lines
Wires
Pipes
Structural Stiffener Tags
Topography
Casework Tags
Project Information
Wall Tags
Cable Tray Fitting Tags
Structural Internal Loads
Electrical Circuits
Analysis Display Style
Pipe Insulations
Flex Ducts
Duct Insulation Tags
Rebar Cover References
Assemblies
Structural Load Cases
Stair Support Tags
Structural Area Reinforcement
Structural Truss Tags
Analytical Slab Foundation Tags
Plan Region
Structural Framing
Electrical Fixtures
Air Terminal Tags
Data Devices
Structural Annotations
Lighting Fixtures
Duct Insulations
Span Direction Symbol
Section Line
Cable Tray Runs
Section Marks
Pipe Color Fill
Generic Models
Lighting Device Tags
Floor Tags
Sprinkler Tags
Analysis Results
Scope Boxes
Line Load Tags
Render Regions
Structural Path Reinforcement Symbols
Electrical Equipment
Stair Landing Tags
MEP Fabrication Containment Tags
Curtain Panels
Fire Alarm Devices
Analytical Braces
Displacement Path
Roads
Duct Lining Tags
Floors
Flex Duct Tags
Point Clouds
Analytical Wall Foundation Tags
Analytical Foundation Slabs
Windows
Structural Area Reinforcement Tags
Structural Path Reinforcement
Stair Run Tags
Rebar Shape
Parts
Nurse Call Device Tags
Columns
Area Load Tags
Routing Preferences
Generic Annotations
Area Tags
View Reference
Filled region
Analytical Column Tags
Structural Fabric Reinforcement
Connection Symbols
Conduit Fitting Tags
Raster Images
Structural Column Tags
Analytical Beam Tags
Adaptive Points
Grid Heads
Sections
Room Tags
Curtain Wall Mullions
Stair Tags
Structural Loads
Revision Cloud Tags
Walls
Conduit Runs
Duct Accessory Tags
Spot Slopes
Keynote Tags
Space Tags
Rebar Set Toggle
Pipe Color Fill Legends
Pipe Fittings
Structural Columns
Pipe Placeholders
Guide Grid
Grids
Fire Alarm Device Tags
Planting Tags
Callouts
Schedule Graphics
Electrical Fixture Tags
Telephone Device Tags
Structural Rebar Couplers
Cable Trays
Curtain System Tags
Structural Stiffeners
Entourage
MEP Fabrication Pipework
Internal Line Load Tags
Structural Fabric Reinforcement Symbols
Mass Tags
Analytical Node Tags
Property Tags
Structural Path Reinforcement Tags
Callout Boundary
Structural Area Reinforcement Symbols
Contour Labels
Nurse Call Devices
Areas
Materials
Roofs
Structural Fabric Areas
Structural Rebar
Reference Points
Shaft Openings
Spot Elevation Symbols
Internal Point Load Tags
Analytical Isolated Foundation Tags
Flex Pipe Tags
Duct Fittings
Cameras
Elevations
Specialty Equipment
Analytical Floor Tags
Pipe Accessories
Structural Connection Tags
Masking Region
Structural Rebar Coupler Tags
Structural Foundations
Level Heads
Duct Color Fill Legends
Analytical Beams
Curtain Grids
Levels
Brace in Plan View Symbols
Railing Tags
Structural Foundation Tags
Wire Tags
Security Devices
Site Tags
Pipe Tags
Analytical Link Tags
Spot Coordinates
Railings
Viewports
Title Blocks
Plumbing Fixture Tags
Pipe Fitting Tags
Duct Color Fill
Stair Paths
MEP Fabrication Pipework Tags
Duct Linings
Structural Beam Systems
Roof Tags
Views
Sheets
Casework
Conduit Tags
Point Load Tags
Analytical Surfaces
Structural Fabric Reinforcement Tags
Material Tags
View Titles
Mechanical Equipment
Parking Tags
Structural Beam System Tags
Analytical Brace Tags
Electrical Equipment Tags
Generic Model Tags
Switch System
Furniture
Rooms
Analytical Walls
Stairs
Text Notes
Detail Items
Spot Elevations
Analytical Nodes
Boundary Conditions
Color Fill Legends
Spaces
Dimensions
```
