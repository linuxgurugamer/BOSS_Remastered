@echo on
set H=R:\KSP_1.1.4_dev
echo %H%

copy /y bin\Debug\BOSS.dll GameData\BOSS\Plugins
copy  /y BOSSContinued.version GameData\BOSS\BOSSContinued.version

xcopy /Y /E GameData\BOSS %H%\GameData\BOSS

