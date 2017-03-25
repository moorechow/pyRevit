---
layout: post
title:  "Fancy forms for your scripts"
date:   2017-03-25 08:00:00 -0900
categories: pyrevit update
comments: true
---

Recently, I started adding more standard forms to the `scriptutils.userinput` module so simple scripts don't have to deal with creating forms to ask for simple information. Currently I have developed two standard forms that I'm using on two of the scripts.


### SelectFromList

First is `SelectFromList`. A script can add a series of options to the form that users can select from. There is also a built-in filter option that filters the list. Here is how it looks and a quick intro on how it works:

``` python
from scriptutils.userinput import SelectFromList

options = ['Option 1', 'Option 2' , 'Option 3']

# shows the form and returns the selected options
selected_options = SelectFromList.show(options,
			title='Custom Form Title',
			width=300,
			height=500,
			multiselect=True)

# you can also pass a list of objects
# the form will show the str(object) of the objects in the list
class ActionOption:
    def __init__(self, option_name):
    	self.option = option_name

    def __str__(self):
        return self.option

options = [ActionOption('Option 1'), ActionOption('Option 2')]
selected_options = SelectFromList.show(options)

```

Currently, the `Load Family Types` tool uses this standard form:

&nbsp;

![]({{ site.url }}/pyRevit/images/SelectFromList.png)

&nbsp;


### SelectFromCheckBoxes

This also works very similarly to the `SelectFromList`. The only difference is that since it deals with smarter choices, it needs a list of objects that define `name` and `state` parameters. `name` will be the name of the checkbox and `state` will be the state of the checkbox (`True` or `False`)

``` python
class CheckBoxOption:
    def __init__(self, name, default_state=False):
        self.name = name
        self.state = default_state

	# define the __nonzero__ method so you can use your objects in an 
	# if statement. e.g. if checkbox_option:
    def __nonzero__(self):
        return self.state

	# __bool__ is same as __nonzero__ but is for python 3 compatibility
    def __bool__(self):
        return self.state

options = [CheckBoxOption('Option 1'), CheckBoxOption('Option 2')]
all_checkboxes = SelectFromCheckBoxes.show(options)

# now you can check the state of checkboxes in your program
for checkbox in all_checkboxes:
	if checkbox:
		# do stuff

```

I'm revising the `Wipe Model Components` to use this form. It helps the tool to be extensible, allowing all sorts of smart options based on the current model. You'll see this update in the next revision.

&nbsp;

![]({{ site.url }}/pyRevit/images/SelectFromCheckBoxes1.png)

&nbsp;

![]({{ site.url }}/pyRevit/images/SelectFromCheckBoxes2.png)
