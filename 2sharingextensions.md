---
layout: page
title: Sharing Extensions
permalink: /sharingextensions/
---

## Adding extensions from others:
Use the Extensions tool under pyRevit panel and install third-party extensions as you wish.

![]({{ site.url }}/pyRevit/images/extensions45.png)

## Sharing your extensions with others:
Create your extension first obviously, publish it to Github, Bitbucket, or other online git cloud.

Then send me an email with the link and I'll add that to the list of standard extensions in pyRevit repository. Here are the information that I'll need from you:

- Extension Name
- Extension Git Repository Link
- Single-line description
- Author's Name
- Author's online profile url
- Website url (Optional)
- Extension icon/logo url (Optional, PNG file)
- Is it compatible with Rocket-Mode?
- Does it have any dependencies?


This information will be stored in [this json file](https://github.com/eirannejad/pyRevit/blob/master/extensions/extensions.json) and is used by the extension manager or pyRevit CLI to find information about your extension and install on user machine.