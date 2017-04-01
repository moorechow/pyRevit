---
layout: post
title:  "revitutils loves RPW"
date:   2017-04-01 09:00:00 -0900
categories: pyrevit update
comments: true
---

I've added some stuff to the `revitutils` module but there is something that I need to talk to you about before you start using these features:

### Fact 1

Currently there is a very useful, powerfull, and easy to use python module to interact with Revit API: [RevitPythonWrapper](http://revitpythonwrapper.readthedocs.io/en/latest/) by [Gui Talarico](https://github.com/gtalarico).

You can install this module using the pyRevit extension manager. This module also works in Dynamo environment so it'll be a good time investment for the user since it could be used in both Revit and Dynamo without any code changes.

### Fact 2

pyRevit includes `revitutils` in its core libraries. The primary purpose of `revitutils` module is to provide the required functionality for the standard pyRevit scripts.


### The thing that I wanted to talk to you about:

I have started work to incorporate `RevitPythonWrapper` into pyRevit. Yes it is a dependency but I already have other dependencies (`Charts.js` and the `markdown` module for the output window) and `RevitPythonWrapper` is a very well-designed platform and I intend to use it more and more and have started work to contribute to it.

Very soon, `RevitPythonWrapper` will be a standard part of pyRevit and without any changes to your code, it won't be a required dependency for your tools anymore. I think this is great news.

I'm moving some of the stuff in `revitutils` to `RevitPythonWrapper` (Don't worry I won't break your code just now). I'll be a lot more strict about stuff that goes into `revitutils`. If the idea is generic and it could be incorporated into `RevitPythonWrapper` I will do that.

`revitutils` will be for the extra functionality that is heavily tied to the pyRevit itself. For example `revitutils` has a sub-module called `patmaker` that is the backend for the `Make Pattern` tool in pyRevit. This is a very specific module and it kinda is tied to pyRevit. `revitutils` will be home to these type of functionality.

For you python programmers and Revit radicals out there, I encourage you to invest time, implement your functionality ideas, and improve upon `RevitPythonWrapper`. It is a very well-designed platform and I intend to use it more and more.
