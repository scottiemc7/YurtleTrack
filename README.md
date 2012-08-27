#YurtleTrack
A [TortoiseSVN](http://tortoisesvn.net/ "TortoiseSVN") issue tracker plug-in for integration with [JetBrains YouTrack 4.0](http://www.jetbrains.com/youtrack/ "YouTrack")  

    Scott McClure
    scottie_DOT_mcclure@gmail.com

## INSTALLATION:
Two installers are included in the `Installers` directory, one compatible with the *x86* version and one compatible with the *x64* version of TortiseSVN.

    System Requirements:
	    .NET Framework 4.0
	
## SETUP:

*For instructions on how to set up the plugin per project, instead of per user see [Integration with Bug Tracking Systems / Issue Trackers](http://tortoisesvn.net/docs/release/TortoiseSVN_en/tsvn-dug-bugtracker.html/ "TortoiseSVN"). The GUID for both the x86 and x64 versions of the plugin is:* **{0044f6c0-b999-11e1-afa6-0800200c9a66}**  
   
   
*    `TortiseSVN->Settings->Issue Tracker Integration->Add`  
![Settings 1 png](https://github.com/scottiemc7/YurtleTrack/raw/master/README_img/Settings1.png "Issue Tracker Integration->Add")
    *    Select YurtleTrack
    *    Set the `Working Copy Path` to the directory under which you would like to have this plug-in be active
    *    Click `Options` to set the base URL of your YouTrack server  
![Options png](https://github.com/scottiemc7/YurtleTrack/raw/master/README_img/Settings2.png "Options")

*    You should now see a new `Choose Issue(s)` button when you are attempting to commit changes from a directory under your `Working Copy Path`  
![Commit png](https://github.com/scottiemc7/YurtleTrack/raw/master/README_img/Commit.png "Commit")

## LICENSE:
#### Beer-Ware License

You can do whatever you want with this code. If we meet some day, and you think
this is great (or even just OK) you can buy me a beer in return.