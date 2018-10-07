---
layout: post
title:  "pyRevit Distribution Model"
date:   2018-10-07 09:30:00 -0900
categories: pyrevit updates
comments: true
---

### The Challenge

Some of you have had a lot of issues getting pyRevit installed on your systems and keeping it updates. In the past 3 months, I've done a lot of work to redesign a better way to distribute pyRevit to you.

### The New Distribution Model

Prior to 4.6, pyRevit used git as the main distribution method. When I started working on the original pyRevit 4 years ago, I didn't have much experience with creating installers and the proper distribution models. I also loved the idea of using git to distribute the tools (I still do) but git is hard on windows environment and there is a lot of challenges associated with it.

pyRevit repository also grew overtime (almost 600MB now) and it doesn't make sense to push all these content to users that are not planning to use any of the git features.

Going forward, pyRevit uses new distribution model. The goals are:

- Make it easy for normal users to get pyRevit installed (no git)

- Make it robust for power-users to manage pyRevit and deploy to their users in their company (pyRevit CLI tool. Using or not using git repositories is completely optional in this method)

In this video I'm explaining how the new distribution model works:

<div style='position: relative; width: 100%; height: 0px; padding-bottom: 60%;'>
<iframe style='position: absolute; left: 0px; top: 0px; width: 100%; height: 100%' src="https://www.youtube.com/embed/VZdgrh_US68" frameborder="0" allowfullscreen></iframe>
</div>