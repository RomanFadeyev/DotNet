set svn="C:\Program Files\TortoiseSVN\bin\svn.exe"
set outpath=C:\OutInstallation
set outsvn=%outpath%\svn
set installpath=%outpath%\install
rem ------------------------------------------------------------------------------
%svn% export --username=teamcity --password=teamcity@  --force "svn://redmine.kspd.intech.ru/kspdigt.net.2/trunk" %outsvn%
IF EXIST C:\Taganrog\update xcopy /D /Y /Z /E /I C:\Taganrog\update\*.*  %outsvn%\
cd /D %outsvn%\Sources\Kspdigt.Net\Kspdigt\SolutionFiles\BatchFiles
call build_kspd.cmd

cd %outsvn%\Sources\Kspdigt.Net\InstallationProject
call CompileSetup.cmd SetupKSPDClient.iss "..\Kspdigt"
call CompileSetup.cmd SetupKSPDServer.iss "..\Kspdigt"
call CompileSetup.cmd SetupKSPDClientDatumMark.iss "..\Kspdigt"
call CompileSetup.cmd SetupKSPDServerDatumMark.iss "..\Kspdigt"
call CompileSetup.cmd SetupKSPDLicense.iss "..\Kspdigt"
call CompileSetup.cmd SetupKSPDFLCChecker.iss "..\Kspdigt"
call CompileSetup.cmd SetupKSPDUtils.iss "..\Kspdigt"
call CompileSetup.cmd SetupKSPDGDBConverter.iss "..\Kspdigt"
call CompileSetup.cmd SetupKSPDLoader.iss "..\Kspdigt"





