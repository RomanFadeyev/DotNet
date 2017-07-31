set sed="C:\Program Files (x86)\GnuWin32\bin\sed.exe"
set KspdigtPath=..\..\..
set fromversion=10.4.0.0
set toversion=10.0.0.0

pushd 
cd /D %KspdigtPath%
for /R %%c in (*.csproj) do attrib -R "%%c" && %sed% -i -e "s/Version=%fromversion%/Version=%toversion%/g" "%%c"
del sed*.*
popd 

