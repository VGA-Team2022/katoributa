@echo on
cd %~dp0
set root=%~dp0
set main=UnityProjectRoot
set project=UnityArtProject
set plan=UnityPlannerProject

xcopy /e /y %root%%project%\Assets\Master %root%%main%\Assets\Model\Master
xcopy /e /y %root%%plan%\Assets\SceneJoint %root%%main%\Assets\Scenes\ScenesData

pause