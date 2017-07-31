set old_path=%cd%
cd %1
xcopy /D /Y /Z .\SolutionFiles\BatchFiles\license_service_install.cmd %2
xcopy /D /Y /Z .\SolutionFiles\BatchFiles\license_service_uninstall.cmd %2
cd %old_path%

