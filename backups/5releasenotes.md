---
layout: page
title: Release Notes
permalink: /releasenotes/
---

2.49.55 (current)
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



