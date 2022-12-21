@echo on
cd %~dp0
set root=%~dp0
set main=UnityProjectRoot
set project=UnityArtProject

xcopy /e /y %root%%project%\Assets\Master %root%%main%\Assets\Model\Master

pause