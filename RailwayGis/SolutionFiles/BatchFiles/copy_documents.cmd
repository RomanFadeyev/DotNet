chcp 866
set output_dir=%1
set svn_export_cmd="C:\Program Files\TortoiseSVN\bin\svn.exe" export --username=teamcity --password=teamcity@  --force "svn://redmine.kspd.intech.ru/kspdigt.net.2/trunk

mkdir %output_dir%\Database
mkdir %output_dir%\Database\SDB
mkdir %output_dir%\Database\GDB
mkdir %output_dir%\Документация

%svn_export_cmd%/Sources/Kspdigt.Database/sdb/Scripts" %output_dir%\Database\SDB\Scripts
%svn_export_cmd%/Sources/Kspdigt.Database/sdb/create_db.cmd" %output_dir%\Database\SDB
%svn_export_cmd%/Sources/Kspdigt.Database/sdb/create_db.mak" %output_dir%\Database\SDB
%svn_export_cmd%/Sources/Kspdigt.Database/sdb/ssql.exe" %output_dir%\Database\SDB
%svn_export_cmd%/Sources/Kspdigt.Database/sdb/KSPDBaseVersion.exe" %output_dir%\Database\SDB
%svn_export_cmd%/Sources/Kspdigt.Database/sdb/Oracle.ManagedDataAccess.dll" %output_dir%\Database\SDB

%svn_export_cmd%/Sources/Kspdigt.Database/gdb/create_mf.cmd" %output_dir%\Database\GDB
%svn_export_cmd%/Sources/Kspdigt.Database/gdb/create_mf.mak" %output_dir%\Database\GDB
%svn_export_cmd%/Sources/Kspdigt.Database/gdb/main.sql" %output_dir%\Database\GDB
%svn_export_cmd%/Sources/Kspdigt.Database/gdb/ssql.exe" %output_dir%\Database\GDB

%svn_export_cmd%/Sources/Kspdigt.Database/gdb/kspd_workspace_2.0_2015_04_09.xml" %output_dir%\Database\GDB
%svn_export_cmd%/Sources/Kspdigt.Database/gdb/kspd_workspace_geo_2.0_2015_04_09.xml" %output_dir%\Database\GDB
%svn_export_cmd%/Sources/Kspdigt.Database/gdb/%%D0%%97%%D0%%B0%%D0%%B3%%D1%%80%%D1%%83%%D0%%B7%%D0%%BA%%D0%%B0%%20%%D0%%B4%%D0%%B0%%D0%%BD%%D0%%BD%%D1%%8B%%D1%%85%%20%%D0%%B2%%20%%D0%%BF%%D1%%80%%D0%%BE%%D1%%81%%D1%%82%%D1%%80%%D0%%B0%%D0%%BD%%D1%%81%%D1%%82%%D0%%B2%%D0%%B5%%D0%%BD%%D0%%BD%%D1%%83%%D1%%8E%%20%%D0%%91%%D0%%94.doc" %output_dir%\Database\GDB
%svn_export_cmd%/%%D0%%A2%%D0%%B5%%D1%%85%%D0%%BD%%D0%%B8%%D1%%87%%D0%%B5%%D1%%81%%D0%%BA%%D0%%B8%%D0%%B5%%20%%D1%%82%%D1%%80%%D0%%B5%%D0%%B1%%D0%%BE%%D0%%B2%%D0%%B0%%D0%%BD%%D0%%B8%%D1%%8F%%2F%%D0%%A2%%D0%%B5%%D1%%85%%D0%%BD%%D0%%B8%%D1%%87%%D0%%B5%%D1%%81%%D0%%BA%%D0%%B8%%D0%%B5%%20%%D0%%BF%%D1%%80%%D0%%BE%%D0%%B5%%D0%%BA%%D1%%82%%D1%%8B%%2F%%D0%%A1%%D1%%83%%D1%%89%%D0%%BD%%D0%%BE%%D1%%81%%D1%%82%%D0%%B8%%2F%%D0%%91%%D0%%94%%20%%D0%%B3%%D0%%B5%%D0%%BE%%D0%%B8%%D0%%BD%%D1%%84%%D0%%BE%%D1%%80%%D0%%BC%%D0%%B0%%D1%%86%%D0%%B8%%D0%%BE%%D0%%BD%%D0%%BD%%D0%%B0%%D1%%8F.doc" %output_dir%\Database\GDB

%svn_export_cmd%/%%D0%%94%%D0%%BE%%D0%%BA%%D1%%83%%D0%%BC%%D0%%B5%%D0%%BD%%D1%%82%%D0%%B0%%D1%%86%%D0%%B8%%D1%%8F" %output_dir%\0922~1


