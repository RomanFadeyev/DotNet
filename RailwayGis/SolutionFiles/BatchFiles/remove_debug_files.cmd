set old_path=%cd%
cd %1
for /F %%c in ('findstr /S /I /M "</doc>" *.xml') do del /F /S /Q %%c
del /F /S /Q *.pdb
cd %old_path%

