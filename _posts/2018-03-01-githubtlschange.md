---
layout: post
title:  "Installer Error and Github Security Changes"
date:   2018-03-01 09:30:00 -0900
categories: pyrevit installer
comments: true
---

Hey All,

Github has recently finalized a [security change](https://blog.github.com/2018-02-23-weak-cryptographic-standards-removed/) that affects the pyRevit installer among many other Windows apps that connect to Github through WinHTTP (e.g. GitKraken)

[Please got to this page](https://support.microsoft.com/en-us/help/3140245/update-to-enable-tls-1-1-and-tls-1-2-as-a-default-secure-protocols-in) and download the EasyFix installer and install. This is a fix from Microsoft himself. Also please check with your IT before installing this to make sure it doesn't break anything else.

After the installation, the pyRevit installer should behave as usual.

:rolling-eyes-emoji: