
@echo off

set RELEASEDIR=d:\Users\jbb\release
set ZIP="c:\Program Files\7-zip\7z.exe"

copy /y bin\Release\BOSS.dll GameData\BOSS\Plugins
copy  /y BOSSContinued.version GameData\BOSS\BOSSContinued.version


set VERSIONFILE=BOSSContinued.version
rem The following requires the JQ program, available here: https://stedolan.github.io/jq/download/
c:\local\jq-win64  ".VERSION.MAJOR" %VERSIONFILE% >tmpfile
set /P major=<tmpfile

c:\local\jq-win64  ".VERSION.MINOR"  %VERSIONFILE% >tmpfile
set /P minor=<tmpfile

c:\local\jq-win64  ".VERSION.PATCH"  %VERSIONFILE% >tmpfile
set /P patch=<tmpfile

c:\local\jq-win64  ".VERSION.BUILD"  %VERSIONFILE% >tmpfile
set /P build=<tmpfile
del tmpfile
set VERSION=%major%.%minor%.%patch%
if "%build%" NEQ "0"  set VERSION=%VERSION%.%build%


echo %VERSION%
pause



xcopy /Y /E GameData\BOSS Gamedata\BOSS
copy ..\MiniAVC.dll Gamedata\BOSS
copy ReadmeAndLicense.txt Gamedata\BOSS


set FILE="%RELEASEDIR%\BOSS-%VERSION%.zip"
IF EXIST %FILE% del /F %FILE%
%ZIP% a -tzip %FILE% GameData\BOSS
