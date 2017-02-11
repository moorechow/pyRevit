---
layout: post
title:  "Viva progress bars"
date:   2017-02-11 13:00:00 -0900
categories: pyrevit update output
comments: true
---

The output window now has a progress bar! Yaay!

(It's the green line at the bottom of the output window)

&nbsp;

![]({{ site.url }}/pyRevit/images/linkify.png)

&nbsp;

Use it like this:

``` python
from scriptutils import this_script

# update_progress( current_value, max_value )
# so this means 50% progress
# values can be any int or float. The function will do
# the math and will calculate the percentage of completion.
this_script.output.update_progress(50, 100)
```


So let's say your script is deleting some views. Using this method the script can update the progress bar as it is removing the views:

``` python
from scriptutils import this_script

view_ids = get_views_to_be_deleted()

view_count = len(view_ids)

with Transaction(doc, 'delete views') as t:
	t.Start()

	for index, view_id in enumerate(view_ids):
		doc.Delete(view_id)
		this_script.output.update_progress(index, view_count)
		
	t.Commit()

```

Awesome isn't it?