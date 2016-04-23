nuget pack .\GameTimer.Android\GameTimer.Android.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget pack .\GameTimer.DesktopGL\GameTimer.DesktopGL.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget pack .\GameTimer.iOS\GameTimer.iOS.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget pack .\GameTimer.WindowsUniversal\GameTimer.WindowsUniversal.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget push *.nupkg