---
layout: post
title:  "pyRevitLabs Merge"
date:   2019-01-12 09:00:00 -0910
categories: pyrevit updates
comments: true
---

Long time ago, when I started the effort to revise some core functionality in pyRevit and increase code sharing and performance, I decided to start a private repository named `pyRevitLabs`. As you might know this is a C# only repo that build many of the libraries that are shared between pyRevit and the CLI tool. The repo was finally made public this year with the release of the CLI tool. Now that the pyRevitLabs is matured, it is time for me to finally merge pyRevitLabs into pyRevit.

Previously pyRevit was being installed as a git repository and was already a fairly large repo. It did not make sense to add more content to it and increase the install size. But now, the modern pyRevit is being deployed as a package (no git repo) the size is much smaller and I have granular control over what content to include in each package. It's faily easy now to add the pyRevitLabs to pyRevit repo `/dev` directory. 

After the merge, both pyRevit and pyRevit CLI installers will be available at the same link which is a lot easier for end-users.

If you have a clone of the pyRevitLabs please watch for this change soon. The `pyrevit help` link in the older CLI versions will break obviously (`pyrevit --help` still works) but it will be replaced with a more forward-dependable link in the new version.

I'll update you again when merge is complete.