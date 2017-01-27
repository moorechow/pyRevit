---
layout: post
title:  "Increment/Decrement Sheet Number"
date:   2017-01-26 14:00:00 -0900
categories: pyrevit update
comments: true
---

re: [Issue #149](https://github.com/eirannejad/pyRevit/issues/149) reported by [thazell](https://github.com/thazell)

**Renamed and updated:**

- Shift Selected Sheets One Up > `Decrement Selected Sheet Numbers`
- Shift Selected Sheets One Up > `Increment Selected Sheet Numbers`

There were two scripts under the `Sheets` pulldown that would increment and decrement the sheet number of the selected sheets. I had written these scripts a long time ago for personal use and really didn't get the chance to revisit and make them smarter. They're designed to rename sheets that are numbered in `A100` format so they'd produce an error with sheets formatted as `A100a`, `S-100`, or similar.

[thazell](https://github.com/thazell) created the issue #149 to address this shortcoming. I had a chance this afternoon to create/find a proper increment/decrement function for strings and use it to increment and decrement sheet numbers in a smarter fashion. This new function has been added to the `coreutils` module and is available to scripts as well.

Here is a couple of examples of how it would increment/decrement sheet numbers:

| **Decremented <** | **Original** | **> Incremented** |
|:------------------:|:------------:|:------------------:|
| `A099`             |  `A100`      | `A101`             |
| `A099z`            |  `A100a`     | `A100b`            |
| `S-101Y`           |  `S-101Z`    | `S-102A`           |
| `M111-ZZ`          |  `M112-AA`   | `M112-AB`          |



