set old_path=%cd%
cd %1
xcopy /D /Y /Z /E /I ..\Libraries\Actual\ArcGIS\*.* %2
rem xcopy /D /Y /Z /E /I ..\Libraries\Actual\GProfile\*.* %2
xcopy /D /Y /Z /E /I ..\Libraries\Actual\ShapeLib\*.* %2
xcopy /D /Y /Z /E /I ..\Libraries\Actual\SharpMap\*.* %2
xcopy /D /Y /Z /E /I ..\Libraries\Actual\ZedGraph\*.* %2
xcopy /D /Y /Z /E /I ..\Libraries\Actual\XSD\*.* %2
xcopy /D /Y /Z /E /I ..\Libraries\Actual\LasTools\*.* %2\LasTools
rem ArcGIS runtime
xcopy /D /Y /Z /E /I ..\Libraries\Actual\ArcGIS\arcgisruntime10.2.7 %2\arcgisruntime10.2.7

xcopy /D /Y /Z .\SolutionFiles\ClientSettings\*.* %2

set rel_path=KSPDIGT.Client.Engine\DatumMarks\Import\XmlImport

if not exist %rel_path% goto copyfromsvn

xcopy /D /Y /Z /E %rel_path%\datum.xsd %2
goto end

:copyfromsvn
rem сюда по идее не  должно заходить, все должно отрабатывать выше
echo Не найден путь %rel_path%
rem set output_dir=%2
rem set svn_export_cmd="C:\Program Files\TortoiseSVN\bin\svn.exe" export --username=teamcity --password=teamcity@  --force "svn://redmine.kspd.intech.ru/kspdigt.net.2/trunk
rem %svn_export_cmd%/Sources/Kspdigt.Net/Kspdigt/KSPDIGT.Client.Common/Helpers/DatumMarks/XmlImport/datum.xsd" %output_dir:~1,-1%

:end
cd %old_path%

