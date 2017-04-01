---
layout: post
title:  "pyRevit v4.3"
date:   2017-04-01 08:00:00 -0900
categories: pyrevit update
comments: true
---

Okay. We're officially on 4.3 and here is a list of updates.

&nbsp;

![]({{ site.url }}/pyRevit/images/pyrevitnewipy277.png)

&nbsp;

![]({{ site.url }}/pyRevit/images/pyrevitnewipy277output.png)

&nbsp;

### Quick links:
- [New IronPython 2.7.7](#new-ironpython-277)
- [pyRevit Usage logging system](#pyrevit-usage-logging-system)
- [Other misc changes](#other-misc-changes)

&nbsp;

## New IronPython 2.7.7

pyRevit is using the IronPython 2.7.7 now. There has been a lot of fixes and improvements to this IronPython version. The unicode improvements is what I'm happy about the most.

&nbsp;

## pyRevit Usage logging system

By popular demand, this feature is back but has been completely redesigned for flexibility and extensibility.

For those of you that hadn't use this feature on pyRevit 3, the usage logging system was used to log every usage of any of the pyRevit tools to a log file so we could calculate the amount of time saved during a time span. I specifically used this to show the management at my company that investing a little on custom tools is useful and saves us a couple of hours per person per week, which is a lot. It also makes users happier since now they have a way of circumventing Revit limitations.

The usage logging system is back. It collects information about the executed pyRevit commands and logs this info to the destinations set by the user in pyRevit settings.

Let's take a deeper look at the log record. It's composed in JSON format and looks like this:

&nbsp;

![]({{ site.url }}/pyRevit/images/usagelogjsonrecord.png)

&nbsp;

As you can see there are a lot of info about the command and the environment it is being executed under (Revit version, pyRevit version, session id,...). Most of the fields listed here are self-explanatory but I'd like to talk about two of the most important ones. The `resultcode` and `commandresults` fields.

### resultcode

This is an integer field and contains the result code that was returned by the pyRevit command executor. As of now, here is a list of pre-defined result codes:

``` python
RESULT_DICT = {0: 'Succeeded',
               1: 'SysExited',
               2: 'ExecutionException',
               3: 'CompileException',
               9: 'UnknownException'}
```

- `SysExited` means that the pyRevit script used `sys.exit()` to exit from the command.
- `ExecutionException` is when an exception occurs at command runtime.
- and `CompileException` is errors in compiling code e.g. syntax errors.

### command results

In order to make this logging system more extensible, I added a dictionary to the log record that pyRevit commands can use to store custom parameters and values. You might decide to calculate the amount of time-saved inside each script and then report if to the usage logging system so later it can be used to calculate the total time saved by all your tools.

For example, the pyRevit Reload command uses this to return the unique identifier of the newly loaded session to the usage logging system. This way the admin that is looking into the usage logs, can understand that this reload command execution, loaded pyRevit into a new session with the provided session id:

&nbsp;

![]({{ site.url }}/pyRevit/images/usagelogcommandrecord.png)

&nbsp;

This results dictionary is accessible through the `scriptutils` module. Here is how the Reload command is logging the new session id:

``` python
from scriptutils import this_script

# this_script.results is the results dictionary
# newsession is the name of the custom parameter to be added to results
this_script.results.newsession = get_session_uuid()
```

As you can see, to make this really easy, I have modified the results dictionary so you can type in the name of your custom return parameter as if it is a pre-existing parameter and assign a value to it.

### Usage logging system configurations

To setup the usage logging system, go to the pyRevit settings and under the `Usage Logging` expander, flick the master switch.

&nbsp;

![]({{ site.url }}/pyRevit/images/usagelogsettings.png)

&nbsp;

As you can see the log system can write to two different destinations:

- **Logging to file:** logger will write all usage records to log files in the provided destination folder. The log files are written in JSON format and have `.json` extension. pyRevit automatically names the log files based on the Revit version, username and session id so multiple users can log to the same destination:

![]({{ site.url }}/pyRevit/images/usagelogfiles.png)

&nbsp;

- **Logging to server:** logger will POST the usage records to the provided server url. I'm using [requestb.in](https://requestb.in) website in the example below. See how the server url is set in the configuration window and an example of a usage record, posted to the web server:

![]({{ site.url }}/pyRevit/images/usagelogsettingsserverurl.png)

![]({{ site.url }}/pyRevit/images/usagelogserverrecord.png)

 
### Viewer for usage logs

And for you sysadmins who want to review the usage logs, there is also a log viewer tool under the newly created pyRevit Development extension (activate it in the Extension manager).

This tool helps you browse through log files more easily, colour-codes the log entries based on the result code, shows the human-readable result codes instead of the actual integer values, and provides smart filtering and search. It also allows for parsing log files in a directory different than the currently set file logging directory.

![]({{ site.url }}/pyRevit/images/usagelogviewercommandresults.png)

![]({{ site.url }}/pyRevit/images/usagelogviewerfilter.png)

Each record, has a context menu that you can use to filter by session or copy information from that log record to clipboard:

![]({{ site.url }}/pyRevit/images/usagerecordcontextmenu.png)


## Other misc changes

There are a whole bunch of other changes in this revision. Here is a list:

 - Improvements to `revitutils`: I'll write another post about this as there are a lot of changes and improvements
 - pyRevit has a new core extension called `pyRevitDev`. All the work-in-progress and test tools have been moved into this extension. It is disabled by default since most of you don't need these tools but you can always activate it from the Extension manager.
 - The unittests module has also been cleaned up for a better test output.
 - The pyRevit executor and core has been cleaned up a lot.
 - I have converted all the explicit `str` to `unicode` in pyRevit code for unicode consistency.
 - Misc upgrades to tools. One is the `Wipe model components` tool that I'll write about in a different post.
 - New environment variables that allow the usage logging system to read the most current logging configuration from the current pyRevit session. The list now also has a context menu to copy the env var value to clipboard:

![]({{ site.url }}/pyRevit/images/newenvvars.png)
