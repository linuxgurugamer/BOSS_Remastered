

copy /y bin\Release\BOSS.dll GameData\BOSS\Plugins
copy  /y BOSSContinued.version GameData\BOSS\BOSSContinued.version




@echo off
set DEFHOMEDRIVE=d:
set DEFHOMEDIR=%DEFHOMEDRIVE%%HOMEPATH%
set HOMEDIR=
set HOMEDRIVE=%CD:~0,2%

set RELEASEDIR=d:\Users\jbb\release
set ZIP="c:\Program Files\7-zip\7z.exe"
echo Default homedir: %DEFHOMEDIR%

rem set /p HOMEDIR= "Enter Home directory, or <CR> for default: "

if "%HOMEDIR%" == "" (
set HOMEDIR=%DEFHOMEDIR%
)
echo %HOMEDIR%

SET _test=%HOMEDIR:~1,1%
if "%_test%" == ":" (
set HOMEDRIVE=%HOMEDIR:~0,2%
)

type BOSSContinued.version
set /p VERSION= "Enter version: "


rmdir /s /q %HOMEDIR%\install\Gamedata\BOSS

xcopy /Y /E GameData\BOSS %HOMEDIR%\install\Gamedata\BOSS
copy ..\MiniAVC.dll %HOMEDIR%\install\Gamedata\BOSS
copy ReadmeAndLicense.txt %HOMEDIR%\install\Gamedata\BOSS

%HOMEDRIVE%
cd %HOMEDIR%\install

set FILE="%RELEASEDIR%\BOSS-%VERSION%.zip"
IF EXIST %FILE% del /F %FILE%
%ZIP% a -tzip %FILE% GameData\BOSS
