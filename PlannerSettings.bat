@echo on
cd %~dp0
set root=%~dp0
set main=UnityProjectRoot
set project=UnityPlannerProject

xcopy /e /y %root%%main%\Assets %root%%project%\Assets
xcopy /e /y %root%%main%\Packages %root%%project%\Packages
xcopy /e /y %root%%main%\ProjectSettings %root%%project%\ProjectSettings

pause