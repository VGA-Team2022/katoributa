rem //	DesignerSettingsBatch

@echo on
cd %~dp0
set root=%~dp0
set main=UnityProjectRoot
set project=UnityArtProject

rem ファイルの参照とコピー
xcopy /e /y %root%%project%\Assets\Master %root%%main%\Assets\Model\Master

pause