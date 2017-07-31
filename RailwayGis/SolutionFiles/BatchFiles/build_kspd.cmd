chcp 1251
set old_path=%cd%
cd ..\..\

mkdir __Installation

mkdir __Installation\Server
del  /F /S /Q __Installation\Server

mkdir __Installation\OraClient
del  /F /S /Q __Installation\OraClient

mkdir __Installation\Server\Query
del  /F /S /Q __Installation\Server\Query

mkdir __Installation\License
del  /F /S /Q __Installation\License

mkdir __Installation\Client
del  /F /S /Q __Installation\Client

mkdir __Installation\ClientMain
del  /F /S /Q __Installation\ClientMain

mkdir __Installation\Utils
del  /F /S /Q __Installation\Utils

mkdir __Installation\Client\АСУ-П
del  /F /S /Q __Installation\Client\АСУ-П

mkdir __Installation\Client\АСУ-П\QueryViewer
del  /F /S /Q __Installation\Client\АСУ-П\QueryViewer

mkdir __Installation\Client\АСУ-П\ShowData
del  /F /S /Q __Installation\Client\АСУ-П\ShowData

mkdir __Installation\Client\LasTools
del  /F /S /Q __Installation\Client\LasTools

mkdir "__Installation\Client\Software OpenGL"
del  /F /S /Q "__Installation\Client\Software OpenGL"

mkdir __Installation\UpdateService
del  /F /S /Q __Installation\UpdateService

mkdir __Installation\Updater
del  /F /S /Q __Installation\Updater

mkdir __Installation\DatumMark
del  /F /S /Q __Installation\DatumMark

mkdir __Installation\FLCChecker
del  /F /S /Q __Installation\FLCChecker

mkdir __Installation\PointCloudService
del  /F /S /Q __Installation\PointCloudService

mkdir __Installation\Loader
del  /F /S /Q __Installation\Loader


rem goto GDBConverter

rem #################################################################################################################################################
rem Собираем основной проект

call .\SolutionFiles\BatchFiles\empty_licx.cmd
call "%VS100COMNTOOLS%\..\..\VC\bin\vcvars32.bat"
set vBuildTarget=Release
set vBuildPlatform=AnyCPU
set vFolderx86=x86
set vTargets=/t:Clean;Rebuild
set vProperties=/p:configuration=%vBuildTarget%	/p:Platform="%vBuildPlatform%" 

rem Вычищаем весь потенциальный мусор (Clean не всегда чистит до конца)
rd /S /Q KSPDIGT.Client.Proc.Adm\bin\%vFolderx86%\%vBuildTarget%
rd /S /Q KSPDIGT.Client.Proc.Main\bin\%vBuildTarget%
rd /S /Q KSPDIGT.Server.Service\bin\%vFolderx86%\%vBuildTarget%
rd /S /Q KSPDIGT.Client.Proc.DatumMark\bin\%vBuildTarget%
rd /S /Q KSPDIGT.Server.Service\bin\%vFolderx86%\%vBuildTarget%
rd /S /Q KSPDIGT.Utils\bin\%vBuildTarget%
rd /S /Q KSPDIGT.FLC.Checker\bin\%vFolderx86%\%vBuildTarget%
rd /S /Q KSPDIGT.PointCloudServer.Service\bin\%vBuildTarget%
rd /S /Q KSPDIGT.License.Service\bin\%vBuildTarget%
rd /S /Q KSPDIGT.ArcGIS.Loader\bin\%vFolderx86%\%vBuildTarget%

rem Запускаем сборку
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe KSPDIGT.v2.sln %vTargets% %vProperties% /maxcpucount /noconsolelogger /fileLogger /fileloggerparameters:LogFile=__Installation\msbuild.log;Verbosity=n;Encoding=UTF-8

xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt KSPDIGT.Client.Proc.Adm\bin\%vFolderx86%\%vBuildTarget%\*.* __Installation\Client
xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt KSPDIGT.Client.Proc.Main\bin\%vBuildTarget%\*.* __Installation\ClientMain
xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt KSPDIGT.Viewer\bin\%vFolderx86%\%vBuildTarget%\*.* __Installation\Client
xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt KSPDIGT.Viewer\bin\%vBuildTarget%\*.* __Installation\ClientMain
xcopy /D /Y /Z /E /I SolutionFiles\CommonFiles\160620_Symbol_CSSD_RWI_v2.ttf  __Installation\Client
xcopy /D /Y /Z /E /I SolutionFiles\CommonFiles\GOST_A.TTF __Installation\Client
xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt KSPDIGT.Client.Proc.DatumMark\bin\%vBuildTarget%\*.* __Installation\DatumMark
xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt KSPDIGT.Server.Service\bin\%vFolderx86%\%vBuildTarget%\*.* __Installation\Server
xcopy /D /Y /Z /I KSPDIGT.Server.Service\Query\*.* __Installation\Server\Query
xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt KSPDIGT.Utils\bin\%vBuildTarget%\*.* __Installation\Utils
xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt KSPDIGT.FLC.Checker\bin\%vFolderx86%\%vBuildTarget%\*.* __Installation\FLCChecker
xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt KSPDIGT.PointCloudServer.Service\bin\%vBuildTarget%\*.* __Installation\PointCloudService
xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt KSPDIGT.License.Service\bin\%vBuildTarget%\*.* __Installation\License
xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt KSPDIGT.ArcGIS.Loader\bin\%vFolderx86%\%vBuildTarget%\*.* __Installation\Loader

echo KSPDIGT.ArcGIS.Loader.exe /s="example.gdb" /d="SERVER=devsrv;INSTANCE=5151;USER=mf_mkr;PASSWORD=mf_mkr;VERSION=sde.DEFAULT" /c=".\Versions\2015.04.09\2015.KSPDIGT.ArcGIS.Loader.cfg.xml" /oc /et /i  > __Installation\Loader\start_loader.cmd 

rem Следующая строка нужна для того, что бы были скопированы все dll(пока что решение такое)
rem xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt  ..\Libraries\Actual\DevX\Framework\*.* __Installation\Client
rem xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt  ..\Libraries\Actual\DevX\Framework\*.* __Installation\ClientMain
rem xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt  ..\Libraries\Actual\DevX\Framework\*.* __Installation\DatumMark
rem xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt  ..\Libraries\Actual\DevX\Framework\*.* __Installation\Utils
rem xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt  ..\Libraries\Actual\DevX\Framework\*.* __Installation\License
rem xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt  ..\Libraries\Actual\DevX\Framework\*.* __Installation\FLCChecker
rem xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt  ..\Libraries\Actual\DevX\Framework\*.* __Installation\PointCloudService

xcopy /D /Y /Z /I KSPDIGT.Update.WinService\bin\%vBuildTarget%\*.* __Installation\UpdateService
xcopy /D /Y /Z /I KSPDIGT.Update.Updater\bin\%vBuildTarget%\*.* __Installation\Updater

xcopy /D /Y /Z /E /I ..\Libraries\Actual\OraClient\*.* __Installation\OraClient
xcopy /D /Y /Z /E /I ..\Libraries\Actual\Mesa\*.* __Installation\Mesa
xcopy /D /Y /Z /E /I ..\Libraries\Actual\GProfile\*.* __Installation\GProfile

xcopy /D /Y /Z /E /I SolutionFiles\ServerSettings\KSPDIGT.Server.config __Installation\Server
xcopy /D /Y /Z /E /I SolutionFiles\CommonFiles\160620_Symbol_CSSD_RWI_v2.ttf __Installation\Server
xcopy /D /Y /Z /E /I SolutionFiles\CommonFiles\GOST_A.TTF __Installation\Server
xcopy /D /Y /Z /E /I ..\Libraries\Actual\Mxd\sample.mxd __Installation\Server\Query

xcopy /D /Y /Z /E /I ..\Libraries\Actual\QueryViewer\*.* __Installation\Client\АСУ-П\QueryViewer
xcopy /D /Y /Z /E /I ..\Libraries\Actual\ShowData\*.* __Installation\Client\АСУ-П\ShowData
xcopy /D /Y /Z /E /I ..\Libraries\Actual\LasTools\*.* __Installation\Client\LasTools
xcopy /D /Y /Z /E /I ..\Libraries\Actual\Mesa\x86\*.dll "__Installation\Client\Software OpenGL"

xcopy /D /Y /Z /E /I ..\Libraries\Actual\QueryViewer\*.* __Installation\ClientMain\АСУ-П\QueryViewer
xcopy /D /Y /Z /E /I ..\Libraries\Actual\ShowData\*.* __Installation\ClientMain\АСУ-П\ShowData
xcopy /D /Y /Z /E /I ..\Libraries\Actual\LasTools\*.* __Installation\ClientMain\LasTools

echo rem Запуск > __Installation\run_kspd.cmd 
echo start /min Server\KSPDIGT.Server.Service.exe /noservice >> __Installation\run_kspd.cmd
echo start /min License\KSPDIGT.License.Service.exe /noservice >> __Installation\run_kspd.cmd
echo start Client\KSPDIGT.Client.exe >> __Installation\run_kspd.cmd

echo %SystemRoot%\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe KSPDIGT.Viewer.dll /codebase /tlb /silent > __Installation\Client\reg_com.cmd


:GDBConverter
rem #################################################################################################################################################
rem собираем GDBConverter

mkdir __Installation\GDBConverter
del  /F /S /Q __Installation\GDBConverter

set vBuildTarget=Release
set vBuildPlatform=Any CPU
set vTargets=/t:Clean;Rebuild
set vProperties=/p:configuration=%vBuildTarget%;Platform="%vBuildPlatform%"

%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe KSPDIGT.GDBConverter.sln %vTargets% %vProperties% /maxcpucount /noconsolelogger /fileLogger /fileloggerparameters:LogFile=__Installation\msbuild.log;Append;Verbosity=n;Encoding=UTF-8;
rem call .\SolutionFiles\BatchFiles\prepare_GDBConverter.cmd KSPDIGT.GDBConverter bin\%vBuildTarget%\
rem xcopy /D /Y /Z /E /I /EXCLUDE:SolutionFiles\BatchFiles\build_kspd_excluded_files.txt  ..\Libraries\Actual\DevX\Framework\*.* __Installation\GDBConverter
xcopy /D /Y /Z /E /I KSPDIGT.GDBConverter\bin\%vBuildTarget%\*.* __Installation\GDBConverter
                                                                                                       
rem start notepad .\__Installation\msbuild.log 


rem очиска от pdb и xml файлов отладки. сделано так, что не бы удалялись xml которые нам нужны
for /D %%c in (__Installation\*.*) do call .\SolutionFiles\BatchFiles\remove_debug_files.cmd %%c

cd %old_path%