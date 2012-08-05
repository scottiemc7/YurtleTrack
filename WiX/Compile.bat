@ECHO OFF
SET BIN=C:\Program Files (x86)\Windows Installer XML v3.5\bin
SET Platform=%2
SET YurtleVersion=%1

ECHO Building For Platform: '%Platform%' Version: '%YurtleVersion%'
"%Bin%\candle.exe" -nologo YurtleTrack.wxs -out YurtleTrack.wixobj -ext WixUIExtension  -ext WixNetFxExtension
"%Bin%\light.exe" -nologo "YurtleTrack.wixobj" -out "YurtleTrack_%YurtleVersion%_%Platform%.msi"  -ext WixUIExtension  -ext WixNetFxExtension
move "YurtleTrack_%YurtleVersion%_%Platform%.msi" "..\Installers"