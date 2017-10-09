@echo on
set H=R:\KSP_1.3.1_dev
echo %H%

copy /y bin\Debug\BOSS.dll GameData\BOSS\Plugins
copy  /y BOSSContinued.version GameData\BOSS\BOSSContinued.version

xcopy /Y /E /I GameData\BOSS %H%\GameData\BOSS

