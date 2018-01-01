---
layout: page
title: Release Notes
permalink: /releasenotes/
---

4.5 (current)
------
pyRevit Rocket mode

Major core and tool improvements

New UI Look

And many other changes

See [pyRevit 4.5]({{ site.url }}/pyRevit/pyrevit/update/2018/01/01/45final.html).


4.4
------
Revit 2018 Update and adoption of rpw

See [pyRevit 4.4 Updates]({{ site.url }}/pyRevit/pyrevit/update/2017/07/04/pyrevitfourfour.html) for a full list.


4.3
------
IronPython 2.7.7 update

pyRevit usage logging system

See [pyRevit 4.3 Updates]({{ site.url }}/pyRevit/pyrevit/update/2017/04/01/pyrevitfourthree.html) for a full list.


4.2
------
Addition of interactive Charts to output window

Performance improvements to the output window. (The back-end is a lot cleaner and faster).

pyRevit IronPython engine performance improvements.


4.1
------
Extension system has been finally added to the core and tested. See [Adding your own scripts page]({{ site.url }}/pyRevit/sharingextensions) on how to add or remove extensions.


4.0
------
pyRevit has been completely redesigned from ground up. The semantic versioning now only applies to the core features. The rest of the changes to the library are tracked by adding the latest commit hash value to the version.

Example: As of writing this text, pyRevit version is: ***4.0 : 8C6C5EA***

You can see the current full version at the top of the About window. From now on Release information will only be published for the core features.

![]({{ site.url }}/pyRevit/images/aboutwithversion.png)


3.0.0 beta (deprecated)
------
During the beta development for version 3, I had extensive discussions with Gui Talarico on how the loader and pyRevit core should be setup. These discussion led to the decision to compeltely redesign the core instead of patching the new features.

under version 4.0:x pyRevit core is an IronPython module and is way more faster, powerful, and extensible than before.

2.49.55
------
-	`Memory > Memory_copyViewportPlacement.py`: Copies the location of the selected viewport to memory. This can be later applied to other viewports on other sheets.

-	`Memory > Memory_pasteViewportPlacement.py`: Pastes a Viewport placement from memory.

-	Tooltip now shows the script Author.
-	Misc changes

2.47.51
------
-	`Revisions > Revisions_listAllSheetsNotShowingRevisionCloudsOnViews.py`: Sometimes when a revision cloud is placed inside a view (instead of a sheet) and the view is placed on a sheet, the revision schedule on the sheet does not get updated with the revision number of the cloud inside the sheeted view. This script verifies that the sheet revision schedule is actually listing all the revisions shown inside the views of that sheet.

-   Misc changes

2.46.51
------
-   `Analyse > Analyse_calculateAverageArea.py`: Find all Rooms//Areas//Spaces with identical names and calcualts the average area of that space type.

-   Misc changes

2.45.51
------
-   Revised `findReferencingSheets` to find referencing sheets for schedules as well.

-   `Select > selectAllMirroredDoors`: Selects all mirrored doors in the model.

-   `Views > removeUnderlayFromSelectedViews`: Removes Underlay from selected views.

-   Misc changes

2.43.46
------
-   `WorkSharing > listSheetsWithElementsOwnedByMe`: Lists all sheets containing elements currently "owned" by the user. "Owned" elements are the elements by the user since the last synchronize and release.

-   `WorkSharing > selectLastEditedByMeInCurrentView`: Uses the Worksharing tooltip to find out the element "last edited" by the user in the current view. If current view is a sheet, the tools searches all the views placed on this sheet as well. "Last Edited" elements are elements last edited by the user, and are different from borrowed elements.

-   `WorkSharing > selectOwnedByMeInCurrentView`: Uses the Worksharing tooltip to find out the element "owned" by the user in the current view. If current view is a sheet, the tools searches all the views placed on this sheet as well. "Owned" elements are the elements edited by the user since the last synchronize and release.

-   Added extended tooltip notes to buttons listing class and assembly name.
-   Minor cleanups


2.40.4
------
-   `Settings > What's New`: Added a button to list all the tools added from now on.

-   `Sheets > setCropRegionToSelectedShape`: Draw the desired crop boundary as a polygon on your sheet (using detail lines). Then select the bounday and the destination viewport and run the script. This script will apply the drafted boundary to the view of the selected viewport.

-   Minor cleanups


Earlier versions
------
pyRevit was my own private tool for almost two years and honestly I never felt the need to write version changes for myself thus none exists :D
