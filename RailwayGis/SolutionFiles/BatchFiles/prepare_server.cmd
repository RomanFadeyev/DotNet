set old_path=%cd%
cd %1
xcopy /D /Y /Z /E /I ..\Libraries\Actual\ArcGIS\*.* %2
xcopy /D /Y /Z .\SolutionFiles\BatchFiles\service_drm_install.cmd %2
xcopy /D /Y /Z .\SolutionFiles\BatchFiles\service_install.cmd %2
xcopy /D /Y /Z .\SolutionFiles\BatchFiles\service_uninstall.cmd %2

rem set outputdirNorm=%2
rem set outputdirNorm=%outputdirNorm:"=%
rem xcopy /F /R /Y /D /C "%outputdirNorm%\Deploy\*.*" %2
rem xcopy /D /Y /Z .\SolutionFiles\ServerSettings\mxd.xml %2
rem xcopy /D /Y /Z .\SolutionFiles\ServerSettings\Roles.config %2
cd %old_path%

