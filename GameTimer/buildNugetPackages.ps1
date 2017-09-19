rm *.nupkg
nuget pack .\GameTimer.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget push *.nupkg -Source https://www.nuget.org/api/v2/package