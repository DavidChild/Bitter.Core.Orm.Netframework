del *.nupkg
nuget pack Bitter.Base.Netframework.csproj
nuget push *.nupkg -s http://192.168.100.134:8089 123456
pause