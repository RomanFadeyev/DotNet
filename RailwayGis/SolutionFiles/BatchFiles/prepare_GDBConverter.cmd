set old_path=%cd%
cd %1

set rel_path=..\..\..\..\Deploy\Kspdigt\GdbConverter

if not exist %rel_path% goto copyfromsvn

xcopy /D /Y /Z /E %rel_path%\*.* %2
goto end

:copyfromsvn
set output_dir=%2
set svn_export_cmd="C:\Program Files\TortoiseSVN\bin\svn.exe" export --username=teamcity --password=teamcity@  --force "svn://redmine.kspd.intech.ru/kspdigt.net.2/trunk
%svn_export_cmd%/Deploy/Kspdigt/GdbConverter" %output_dir:~1,-1%

:end
cd %old_path%

