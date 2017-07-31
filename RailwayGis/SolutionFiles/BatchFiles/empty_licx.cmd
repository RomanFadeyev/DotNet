for /R %%c in (*.licx) do attrib -R "%%c"
for /R %%c in (*.licx) do @echo off  > %%c