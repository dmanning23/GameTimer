nuget pack .\GameTimer.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget push *.nupkg