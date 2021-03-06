var target          = Argument("target", "Default");
var configuration   = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var packPath            = Directory("./src/LearningCSharp.Service");
var buildArtifacts      = Directory("./artifacts/packages");

var isAppVeyor          = AppVeyor.IsRunningOnAppVeyor;
var isWindows           = IsRunningOnWindows();
var netcore             = "netcoreapp1.1";
var netstandard         = "netstandard1.6";


///////////////////////////////////////////////////////////////////////////////
// Clean
///////////////////////////////////////////////////////////////////////////////
Task("Clean")
    .Does(() =>
{
    CleanDirectories(new DirectoryPath[] { buildArtifacts });
});

///////////////////////////////////////////////////////////////////////////////
// Restore
///////////////////////////////////////////////////////////////////////////////
Task("Restore")
    .Does(() =>
{
    var settings = new DotNetCoreRestoreSettings
    {
        Sources = new [] { "https://api.nuget.org/v3/index.json" }
    };

	var projects = GetFiles("./**/*.csproj");

	foreach(var project in projects)
	{
	    DotNetCoreRestore(project.GetDirectory().FullPath, settings);
    }
});

///////////////////////////////////////////////////////////////////////////////
// Build
///////////////////////////////////////////////////////////////////////////////
Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() =>
{
    var settings = new DotNetCoreBuildSettings 
    {
        Configuration = configuration
    };

    // main build (Windows local and Appveyor)
    // build for all targets
    if (isWindows)
    {
        DotNetCoreBuild(Directory("./src/LearningCSharp.Service"), settings);
        DotNetCoreBuild(Directory("./test/LearningCSharp.Test"), settings);

        if (!isAppVeyor)
        {
            //DotNetCoreBuild(Directory("./src/LearningCSharp.Service"), settings);     
        }
    }
    // local mac / travis
    // don't build for .net framework
    else
    {
        //settings.Framework = netstandard;
        //DotNetCoreBuild(Directory("./src/LearningCSharp.Service"), settings);
        
        settings.Framework = netcore;
        DotNetCoreBuild(Directory("./src/LearningCSharp.Service"), settings);
        DotNetCoreBuild(Directory("./test/LearningCSharp.Test"), settings);
    }
});

///////////////////////////////////////////////////////////////////////////////
// Test
///////////////////////////////////////////////////////////////////////////////
Task("Test")
    .IsDependentOn("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var settings = new DotNetCoreTestSettings
    {
        Configuration = configuration
    };

    if (!isWindows)
    {
        Information("Not running on Windows - skipping tests for full .NET Framework");
        settings.Framework = netcore;
    }

    var projects = GetFiles("./test/**/*.csproj");
    foreach(var project in projects)
    {
        DotNetCoreTest(project.FullPath, settings);
    }
});

///////////////////////////////////////////////////////////////////////////////
// Pack
///////////////////////////////////////////////////////////////////////////////
Task("Pack")
    .IsDependentOn("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    if (!isWindows)
    {
        Information("Not running on Windows - skipping pack");
        return;
    }

    var settings = new DotNetCorePackSettings
    {
        Configuration = configuration,
        OutputDirectory = buildArtifacts,
    };

    // add build suffix for CI builds
    if(isAppVeyor)
    {
        settings.VersionSuffix = "build" + AppVeyor.Environment.Build.Number.ToString().PadLeft(5,'0');
    }

    DotNetCorePack(packPath, settings);
});


Task("Default")
  .IsDependentOn("Build")
  .IsDependentOn("Test");
  //.IsDependentOn("Pack");

RunTarget(target);
