dotnet publish -c Release --self-contained true -r win10-x86
dotnet publish -c Release --self-contained true -r win10-x64

dotnet publish -c Release --self-contained true -r debian-x64
dotnet publish -c Release --self-contained true -r fedora-x64

dotnet publish -c Release --self-contained true -r opensuse-x64
dotnet publish -c Release --self-contained true -r rhel.7.4-x64
dotnet publish -c Release --self-contained true -r ubuntu.18.04-x64

dotnet publish -c Release --self-contained true -r osx.10.13-x64


7z a dist\sultan-win86.zip bin\Release\netcoreapp2.1\win10-x86\publish\*
7z a dist\sultan-win64.zip bin\Release\netcoreapp2.1\win10-x64\publish\*

7z a dist\sultan-deb64.zip bin\Release\netcoreapp2.1\debian-x64\publish\*
7z a dist\sultan-fed64.zip bin\Release\netcoreapp2.1\fedora-x64\publish\*

7z a dist\sultan-suse64.zip bin\Release\netcoreapp2.1\opensuse-x64\publish\*
7z a dist\sultan-rhel64.zip bin\Release\netcoreapp2.1\rhel.7.4-x64\publish\*
7z a dist\sultan-ubun64.zip bin\Release\netcoreapp2.1\ubuntu.18.04-x64\publish\*

7z a dist\sultan-osx64.zip bin\Release\netcoreapp2.1\osx10.13-x64\publish\*