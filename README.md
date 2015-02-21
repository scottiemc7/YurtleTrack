#YurtleTrack
A [TortoiseSVN](http://tortoisesvn.net/ "TortoiseSVN") issue tracker plug-in for integration with [JetBrains YouTrack 4.0](http://www.jetbrains.com/youtrack/ "YouTrack")  

    Scott McClure
    scottie_DOT_mcclure@gmail.com

## INSTALLATION:

    System Requirements:  
    .NET Framework 4.5 - http://www.microsoft.com/en-us/download/details.aspx?id=30653
    
Two installers are included in the `Installers` directory, one compatible with the *x86* version and one compatible with the *x64* version of TortiseSVN.
	
## SETUP:
***
*For instructions on how to set up the plugin per project, instead of per user see [Integration with Bug Tracking Systems / Issue Trackers](http://tortoisesvn.net/docs/release/TortoiseSVN_en/tsvn-dug-bugtracker.html "TortoiseSVN"). The GUID for both the x86 and x64 versions of the plugin is:* **{0044f6c0-b999-11e1-afa6-0800200c9a66}**  
***
`TortiseSVN->Settings->Issue Tracker Integration->Add` 

![Settings 1 png](https://github.com/scottiemc7/YurtleTrack/raw/master/README_img/Settings1.png "Issue Tracker Integration->Add")  
**Working Copy Path** - Set the working copy path. Every commit under this root path will have YurtleTrack integration available.  
**Provider** - Select YurtleTrack.  
**Parameters** - Click **Options** to set. You shouldn't have to edit this by hand (unless you really want to).  

`Options`  

![Options png](https://github.com/scottiemc7/YurtleTrack/raw/master/README_img/Settings2.png "Options")  
**YouTrack URL** - The URL of your YouTrack instance.  
**User Name** - Your YouTrack user name.  
**Password** - Your YouTrack password.    

You should now see a new `Choose Issue(s)` button when you are attempting to commit changes from a directory under your working copy path  
![Commit png](https://github.com/scottiemc7/YurtleTrack/raw/master/README_img/Commit.png "Commit")

## LICENSE:
#### The MIT License (MIT)

Copyright (c) 2015 Scott R McClure

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
