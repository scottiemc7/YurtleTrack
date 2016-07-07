@ECHO OFF
SET BIN=C:\Program Files (x86)\WiX Toolset v3.10\bin
SET YurtleVersion=%1

SET Platform=x64
ECHO Building For Platform: '%Platform%' Version: '%YurtleVersion%'
"%Bin%\candle.exe" -nologo YurtleTrack.wxs -out YurtleTrack.wixobj -ext WixUIExtension  -ext WixNetFxExtension -dVersion=%YurtleVersion%
"%Bin%\light.exe" -nologo "YurtleTrack.wixobj" -out "YurtleTrack_%YurtleVersion%_%Platform%.msi"  -ext WixUIExtension  -ext WixNetFxExtension
move "YurtleTrack_%YurtleVersion%_%Platform%.msi" "..\Installers"

SET Platform=x86
ECHO Building For Platform: '%Platform%' Version: '%YurtleVersion%'
"%Bin%\candle.exe" -nologo YurtleTrack.wxs -out YurtleTrack.wixobj -ext WixUIExtension  -ext WixNetFxExtension -dVersion=%YurtleVersion%
"%Bin%\light.exe" -nologo "YurtleTrack.wixobj" -out "YurtleTrack_%YurtleVersion%_%Platform%.msi"  -ext WixUIExtension  -ext WixNetFxExtension
move "YurtleTrack_%YurtleVersion%_%Platform%.msi" "..\Installers"