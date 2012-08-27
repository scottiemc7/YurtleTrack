for /F "tokens=4" %%F in ('filever.exe /B /A /D D:\Visual Studio Projects\YurtleTrack\YurtleTrack\bin\Debug\YurtleTrack.dll') do (
  set VERSION=%%F
)
echo The version is %VERSION%