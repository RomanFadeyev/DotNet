@echo off
for /f "tokens=2 delims=()" %%c in ('findstr /r ".*AssemblyVersion(".*").*" ..\..\Version.cs') do (echo %%~c)
