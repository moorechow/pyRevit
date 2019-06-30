---
layout: post
title:  "ProjectInformation"
date:   2019-06-30 8:30:00 -0800
categories: pyrevit updates
comments: true
image: /apple-touch-icon-180x180.png
---

This is the second article in a series of articles about extracting metadata from Revit model files. In this article, we will discuss extracting Project Information data especially the custom project parameters defined under this category (e.g. Revit template version)

&nbsp;

See the previous Articles here:
- [BasicFileInfo & PartAtom]({{ site.baseurl }}{% post_url 2019-01-19-basicfileinfo %})

&nbsp;

# ProjectInformation Stream

As mentioned before, Revit files are [Structured Storage](https://en.wikipedia.org/wiki/COM_Structured_Storage) files (which is basically like a bundled file system). You can use an application like [SSView](http://www.mitec.cz/ssv.html) to peek into this file structure

In the previous article we discussed extracting information from the `BasicFileInfo` and `PartAtom` streams. Another useful stream is named `ProjectInformation` and contains the Projetc Information data and custom project parameters.

&nbsp;

![]({{ site.baseurl }}/images/ssviewpinfo.png)

&nbsp;

## Stream Format

The `ProjectInformation` stream is a blob of pk-zip archive. We can save this stream using SSView to a separate file and open it with 7zip. You will find a single xml file inside this archive when extracted.

&nbsp;

![]({{ site.baseurl }}/images/pinfoaszip.png)

&nbsp;

Let's peek into this xml file. The structure is very similar to the PartAtom stream discussed in the previous article:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<entry xmlns="http://www.w3.org/2005/Atom" xmlns:A="urn:schemas-autodesk-com:partatom">
    <title>Project1</title>
    <updated>2019-06-30T11:52:15Z</updated>
    <A:taxonomy>
        <term>adsk:revit</term><label>Autodesk Revit</label>
    </A:taxonomy>
    <A:taxonomy>
        <term>adsk:revit:grouping</term><label>Autodesk Revit Grouping</label>
    </A:taxonomy>
    <link rel="design-2d" type="application/rvt" href=".">
    <A:design-file>
        <A:title>Project1.rvt</A:title>
        <A:product>Revit</A:product>
        <A:product-version>2019</A:product-version>
        <A:updated>2019-06-30T11:52:15Z</A:updated>
    </A:design-file>
    </link>
    <A:features>
        <A:feature>
            <A:title>Project Information</A:title>
            <A:group>
                <A:title>Text</A:title>
                <Custom_Project_Parameter displayName="Custom Project Parameter" type="custom" typeOfParameter="Text">
                    Custom Value</Custom_Project_Parameter>
            </A:group>
            <A:group>
                <A:title>Identity Data</A:title>
                <Organization_Name displayName="Organization Name" type="system" typeOfParameter="Text">Value
                </Organization_Name>
                <Organization_Description displayName="Organization Description" type="system" typeOfParameter="Text">
                    Value</Organization_Description>
                <Building_Name displayName="Building Name" type="system" typeOfParameter="Text">Value</Building_Name>
                <Author type="system" typeOfParameter="Text">Value</Author>
            </A:group>
            <A:group>
                <A:title>Energy Analysis</A:title>
            </A:group>
            <A:group>
                <A:title>Other</A:title>
                <Project_Issue_Date displayName="Project Issue Date" type="system" typeOfParameter="Text">Value
                </Project_Issue_Date>
                <Project_Status displayName="Project Status" type="system" typeOfParameter="Text">Value</Project_Status>
                <Client_Name displayName="Client Name" type="system" typeOfParameter="Text">Value</Client_Name>
                <Project_Address displayName="Project Address" type="system" typeOfParameter="Multiline Text">Multiline
                    Value
                    Multiline Value
                    Multiline Value</Project_Address>
                <Project_Name displayName="Project Name" type="system" typeOfParameter="Text">Value</Project_Name>
                <Project_Number displayName="Project Number" type="system" typeOfParameter="Text">Value</Project_Number>
            </A:group>
        </A:feature>
    </A:features>
</entry>
```

&nbsp;

Notice that the project information key:values are grouped, similar to how the are grouped in Revit user interface:

```xml
<A:group>
    <A:title>Identity Data</A:title>
</A:group>
```

&nbsp;

Inside each group, there are one or more custom xml elements that correspond to the project information parameter:

```xml
<Organization_Description displayName="Organization Description"
                          type="system"
                          typeOfParameter="Text">
    Value
</Organization_Description>
```

&nbsp;

To be honest I do not know why these elements have custom names e.g. `Organization_Description`. But the actual pretty name of the parameter is set on the `displayName` attribute. The value, per xml standard, is the inner text of the element and could be multi-line as well:


```xml
<Project_Address displayName="Project Address"
                 type="system"
                 typeOfParameter="Multiline Text">
    Multiline Value
    Multiline Value
    Multiline Value
</Project_Address>
```

&nbsp;

# Extracting Info

We can automate this process in code. These excerpts are from the pyRevitLabs code library that supports pyRevit and pyRevit CLI:

```csharp
// extract ProjectInformation (Revit Project Files)
// ProjectInformation is a PK Zip stream
var rawProjectInformationData = CommonUtils.GetStructuredStorageStream(FilePath, "ProjectInformation");

Stream zipData = new MemoryStream(rawProjectInformationData);
var zipFile = new ZipArchive(zipData);
foreach (var entry in zipFile.Entries) {
    if (entry.FullName.ToLower().EndsWith(".project.xml")) {
        using (Stream projectInfoXamlData = entry.Open()) {
            var projectInfoXmlData = new StreamReader(projectInfoXamlData).ReadToEnd();
            ProcessProjectInfo(projectInfoXmlData);
        }
    }
}
```

```csharp
// parse the xml file and extract the data into a dictionary of key, values
var doc = new XmlDocument();

doc.LoadXml(rawProjectInfoData);
XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
nsmgr.AddNamespace("rfa", @"http://www.w3.org/2005/Atom");
nsmgr.AddNamespace("A", @"urn:schemas-autodesk-com:partatom");

// extract project parameters
var projectInfoDict = new Dictionary<string, string>();
XmlNodeList propertyGroups = doc.SelectNodes("//rfa:entry/A:features/A:feature/A:group", nsmgr);
foreach(XmlElement properyGroup in propertyGroups) {
    foreach(XmlElement child in properyGroup.ChildNodes) {
        if (child.HasAttribute("displayName")) {
            string propertyName = child.GetAttribute("displayName");
            string propertyValue = child.InnerText;
            projectInfoDict.Add(propertyName, propertyValue);
        }
    }
}
```

&nbsp;

## pyRevit CLI

The new pyRevit CLI (>0.13.0) automates this process by reading the stream, passing it to `ZipArchive`, extracting the xml file and parsing the xml stream for the project information data:

```
$ pyrevit revits fileinfo Project1.rvt

Created in: Autodesk Revit 2019.2 (Update) (20181217_1515(x64))
Workshared: No
Last Saved Path: C:\Users\LeoW10\Desktop\Project1.rvt
Document Id: 5cafc5e6-644c-40c6-9e6d-df04b4469fd5
Open Workset Settings: LastViewed
Document Increment: 1
Project Information (Properties):
        Building Name = Value
        Client Name = Value
        Custom Project Parameter = Custom Value
        Organization Description = Value
        Organization Name = Value
        Project Address = Multiline Value\r\nMultiline Value\r\nMultiline Value
        Project Issue Date = Value
        Project Name = Value
        Project Number = Value
        Project Status = Value
```


As before, you can also export this data to csv. This process can be used to automatically collect information about your revit files.

```
$ pyrevit revits fileinfo Project1.rvt --csv=data.csv
```

```
"{""Building Name"":""Value"",""Client Name"":""Value"",""Custom Project Parameter"":""Custom Value"",""Organization Description"":""Value"",""Organization Name"":""Value"",""Project Address"":""Multiline Value\r\nMultiline Value\r\nMultiline Value"",""Project Issue Date"":""Value"",""Project Name"":""Value"",""Project Number"":""Value"",""Project Status"":""Value""}"
```

The project information key:value output will be written as a json object under the `projectinfo` field. When parsing the csv file in your own code, you can pass this value to a json loader from string (e.g. `json.loads` in python) and get the json data object as result.