---
layout: post
title:  "BasicFileInfo & PartAtom"
date:   2019-01-19 09:00:00 -0900
categories: pyrevit updates
comments: true
---

# BasicFileInfo Stream

As many of you might know, Revit files are [Structured Storage](https://en.wikipedia.org/wiki/COM_Structured_Storage) files (which is basically like a bundled file system) and have multiple data streams (files?) written inside this bundle.

You can use an application like [SSView](http://www.mitec.cz/ssv.html) to peek into this file structure

![]({{ site.url }}/pyRevit/images/ssview.png)

&nbsp;

## Stream Format

The metadata about a file is stored in the `BasicFileInfo` stream. Over the years, TheBuildingCoder has written many ([This](https://thebuildingcoder.typepad.com/blog/2013/01/basic-file-info-and-rvt-file-version.html), [This](https://thebuildingcoder.typepad.com/blog/2018/04/standalone-basicfileinfo-and-extractpartatom-method.html), [This](https://thebuildingcoder.typepad.com/blog/2014/08/document-version-guid-and-number-of-saves.html), and [This](https://thebuildingcoder.typepad.com/blog/2017/06/determining-rvt-file-version-using-python.html) to link a few) articles on how to extract info from this stream.

But let's talk about this stream in a little more detail and know how to use `pyrevit` cli tool and `Get RVT info` tool in pyRevit to extract this info easily.

The BasicFileInfo is a [UTF-16 Little-Endian](https://en.wikipedia.org/wiki/UTF-16) stream and is formatted (as of Revit 2019.2) as shown here:

```
Worksharing: Not enabled
Username:
Central Model Path:
Format: 2019
Build: 20180806_1515(x64)
Last Save Path: C:\Users\eirannejad\Desktop\Project1.rvt
Open Workset Default: 3
Project Spark File: 0
Central Model Identity: 00000000-0000-0000-0000-000000000000
Locale when saved: ENU
All Local Changes Saved To Central: 0
Central model's version number corresponding to the last reload latest: 4
Central model's episode GUID corresponding to the last reload latest: 2ecc6fa1-2960-4473-9fd9-0abce22022fc
Unique Document GUID: 2ecc6fa1-2960-4473-9fd9-0abce22022fc
Unique Document Increments: 4
Model Identity: 00000000-0000-0000-0000-000000000000
IsSingleUserCloudModel: False
Author: Autodesk Revit
```

These are the changes to this format in 2019 going forward so watch for them if you're reading older Revit files:

```
# added
Format: 2019
IsSingleUserCloudModel: False
Author: Autodesk Revit

# modified
Revit Build: Autodesk Revit 2016 (Build: 20161004_0715(x64)) --> Build: 20180806_1515(x64)
```

&nbsp;

## Extracting the Stream

This UTF-16 stream (every two bytes represents one character) is usually stored after an ASCII stream (each single byte is a character). Both these streams are written with Windows-Style line-ending (`\r\n` with hex codes `0x0D 0x0A` read [Carriage-Return/Line-Feed](https://stackoverflow.com/questions/15433188/r-n-r-and-n-what-is-the-difference-between-them)). An ASCII line ending (`0x0D 0x0A` vs `0x0D 0x00 0x0A 0x00` in UTF-16) separates these two sections:

&nbsp;

![]({{ site.url }}/pyRevit/images/ascii-utf.png)

&nbsp;

So the stream needs to be read in binary, split by `0x0D 0x0A` bytes, and the last section be converted into the UTF-16 format:

```
... 0x00 0x76 0x00 0x69 0x00 0x74 0x00      # ASCII stream
    split by: 0x0D 0x0A                     # End of ASCII stream
0x57 0x00 0x6F 0x00 0x72 0x00 0x6B...       # UTF-16 stream
... 0x0D 0x00 0x0A 0x00                     # End of UTF-16 stream
```

In addition, the string representation of boolean values are written in ASCII again inside this UTF-16 stream. So a binary search and replace is necessary.

&nbsp;

![]({{ site.url }}/pyRevit/images/ascii-false.png)

&nbsp;

Here is a snippet from pyRevitLabs that performs this search and replace:

```csharp
using pyRevitLabs.Common;

// replace False with UTF-16 LE False
utf16Stream = CommonUtils.ReplaceBytes(
    extractedStream,
    //           F     a     l     s     e     null
    new byte[] { 0x46, 0x61, 0x6C, 0x73, 0x65, 0x00 },
    //           F           a           l           s           e
    new byte[] { 0x46, 0x00, 0x61, 0x00, 0x6C, 0x00, 0x73, 0x00, 0x65, 0x00}
    );

```

Now the BasicFileInfo stream is cleaned up and ready to be converted to UTF-16:

```csharp
using System.Text;

var baseInfoString = Encoding.GetEncoding("UTF-16").GetString(utf16Stream);
```

&nbsp;

# PartAtom Stream

In addition to the BasicFileInfo stream, Revit family files include a PartAtom stream written in ASCII. This stream is an xml document with specific `urn:schemas-autodesk-com:partatom` namespace:

``` xml
<?xml version="1.0" encoding="UTF-8"?>
<entry xmlns="http://www.w3.org/2005/Atom" xmlns:A="urn:schemas-autodesk-com:partatom">
    <title># Tag_Keynote</title>
    <id></id>
    <updated>2018-12-27T16:24:17Z</updated>
    <A:taxonomy>
        <term>adsk:revit</term><label>Autodesk Revit</label>
    </A:taxonomy>
    <A:taxonomy>
        <term>adsk:revit:grouping</term><label>Autodesk Revit Grouping</label>
    </A:taxonomy>
    <category>
        <term>Keynote Tags</term>
        <scheme>adsk:revit:grouping</scheme>
    </category>
    <link rel="design-2d" type="application/rfa" href=".">
    <A:design-file>
        <A:title># Tag_Keynote.rfa</A:title>
        <A:product>Revit</A:product>
        <A:product-version>2019</A:product-version>
        <A:updated>2018-12-27T16:24:17Z</A:updated>
    </A:design-file>
    </link>
    <A:features>
        <A:feature>
            <A:title>Family Parameters</A:title>
            <A:group>
                <A:title>Other</A:title>
                <Rotate_with_component displayName="Rotate with component"
                                       type="system"
                                       typeOfParameter="Yes/No">
                    No
                </Rotate_with_component>
            </A:group>
        </A:feature>
    </A:features>
    <A:family type="user">
        <A:variationCount>2</A:variationCount>
        <A:part type="user">
            <title># Text</title>
            <TYP. type="custom" typeOfParameter="Yes/No">No</TYP.>
            <Placeholder type="custom" typeOfParameter="Yes/No">Yes</Placeholder>
            <Keynote_Text displayName="Keynote Text" type="custom" typeOfParameter="Yes/No">Yes</Keynote_Text>
            <Keynote_Number displayName="Keynote Number" type="custom" typeOfParameter="Yes/No">No</Keynote_Number>
            <Hexagon type="custom" typeOfParameter="Yes/No">No</Hexagon>
        </A:part>
        <A:part type="user">
            <title># Symbol</title>
            <TYP. type="custom" typeOfParameter="Yes/No">No</TYP.>
            <Placeholder type="custom" typeOfParameter="Yes/No">Yes</Placeholder>
            <Keynote_Text displayName="Keynote Text" type="custom" typeOfParameter="Yes/No">No</Keynote_Text>
            <Keynote_Number displayName="Keynote Number" type="custom" typeOfParameter="Yes/No">Yes</Keynote_Number>
            <Hexagon type="custom" typeOfParameter="Yes/No">Yes</Hexagon>
        </A:part>
    </A:family>
</entry>
```

This stream contains many useful information about the family type, its parameters, and parameter values. Since this is a standard xml document, the stream is much easier to read and extract info from. Here is a C# snippet:

```csharp
using System.Xml;

// load xml document
var doc = new XmlDocument();
doc.LoadXml(rawPartAtom);
XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);

// make sure to add the namespaces
nsmgr.AddNamespace("rfa", @"http://www.w3.org/2005/Atom");
nsmgr.AddNamespace("A", @"urn:schemas-autodesk-com:partatom");

// extract family category
string CategoryName;
var catElements = doc.SelectNodes("//rfa:entry/rfa:category/rfa:term", nsmgr);
if (catElements.Count > 0) {
    CategoryName = catElements[catElements.Count - 1].InnerText;
}
```

&nbsp;

# Extracting Info

In pyRevit, there are two ways (well 3 really) to read this info:

## Get RVT Info tool

You can use this tool in pyRevit's Project panel to read information about any Revit file (rvt, rtf, rfa). One important step is that pyRevit will lookup the build number stored in this file info against its database and provides the exact version of Revit used to write this file:

![]({{ site.url }}/pyRevit/images/getrvtinfo.png)

Here are the results for the example file. Currently the most important info is printed out but more info can be added easily in the future:

![]({{ site.url }}/pyRevit/images/getrvtinfo-results.png)

You can also hold CTRL while clicking on the tool to see all the DEBUG results which includes full extracted stream and binary data:

![]({{ site.url }}/pyRevit/images/getrvtinfo-debugresults.png)

&nbsp;

## pyRevit CLI

The other method is to use the pyrevit cli tool to lookup this info:


```powershell
pyrevit revits fileinfo "C:\Users\eirannejad\Desktop\Test BasicFileInfo.rvt"
```

The output is identical to the pyRevit gui tool:

```
Created in: Autodesk Revit 2019.2 (Update) (20181217_1515(x64))
Workshared: No
Last Saved Path: C:\Users\eirannejad\Desktop\Test BasicFileInfo.rvt
Document Id: f86eb316-c79a-419f-98ba-67031c6ab92b
Open Workset Settings: LastViewed
Document Increment: 1
```

You can also provide a directory path instead of a single file, and the cli tool will look for all Revit files in that subdirectory recursively and will print out the results. Use the `--csv` option and provide a file path so all this info is exported to a data file properly. You can use this pyrevit cli feature to periodically collect version information about your Revit files.


```powershell
pyrevit revits fileinfo "\\Filestore\Projects" --csv="C:\Users\eirannejad\Desktop\test.csv"
```

![]({{ site.url }}/pyRevit/images/csv-fileinfo.png)

&nbsp;

## pyRevitLabs

If you are writing your own applications, you can also use the `pyRevitLabs.TargetApps.Revit` library to get the model info. Use the [`RevitModelFile` class from this namespace](https://github.com/eirannejad/pyRevit/dev/pyRevitLabs/pyRevitLabs.TargetApps.Revit/RevitController.cs).

```csharp
// add reference to pyRevitLabs.TargetApps.Revit.dll
using pyRevitLabs.TargetApps.Revit;

try {
    var model = new RevitModelFile(rvtFilePath);

    // print model info in pyrevit cli tool
    Console.WriteLine(string.Format("Workshared: {0}", model.IsWorkshared ? "Yes" : "No"));
    Console.WriteLine(string.Format("Central Model Path: {0}", model.CentralModelPath));
    Console.WriteLine(string.Format("Last Saved Path: {0}", model.LastSavedPath));
    Console.WriteLine(string.Format("Document Id: {0}", model.UniqueId));
    Console.WriteLine(string.Format("Open Workset Settings: {0}", model.OpenWorksetConfig));
    Console.WriteLine(string.Format("Document Increment: {0}", model.DocumentIncrement));

    if (model.IsFamily) {
        Console.WriteLine("Model is a Revit Family!");
        Console.WriteLine(string.Format("Category Name: {0}", model.CategoryName));
        Console.WriteLine(string.Format("Host Category Name: {0}", model.HostCategoryName));
    }
} catch {
    // not a revit file
}

```

or in IronPython:

```python
# add reference to pyRevitLabs.TargetApps.Revit.dll
import clr
clr.AddReference('pyRevitLabs.TargetApps.Revit')

import pyRevitLabs.TargetApps.Revit as revit

try:
    model = revit.RevitModelFile(rvtFilePath)

    # print model info in pyrevit cli tool
    print("Workshared: %s" % "Yes" if model.IsWorkshared else "No")
    print("Central Model Path: %s" % model.CentralModelPath)
    print("Last Saved Path: %s" % model.LastSavedPath)
    print("Document Id: %s" % model.UniqueId)
    print("Open Workset Settings: %s" % model.OpenWorksetConfig)
    print("Document Increment: %s" % model.DocumentIncrement)

    if model.IsFamily:
        print("Model is a Revit Family!")
        print("Category Name: %s" % model.CategoryName)
        print("Host Category Name: %s" % model.HostCategoryName)
except:
    # not a revit file
```