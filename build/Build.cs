// ReSharper disable All
using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution(GenerateProjects = true)] readonly Solution Solution;

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(_ => _
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .When(IsServerBuild, _ => _
                    .EnableContinuousIntegrationBuild()
                    .EnableDeterministic()));

            var publishConfigurations =
                from project in new[] { Solution.NukeDemo }
                from framework in project.GetTargetFrameworks()
                select (project, framework);

            DotNetPublish(_ => _
                .SetConfiguration(Configuration)
                .EnableNoBuild()
                .CombineWith(publishConfigurations, (_, c) => _
                    .SetProject(c.project)
                    .SetFramework(c.framework)));
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNet($"restore {Solution}");
        });

    [GitVersion] readonly GitVersion GitVersion;
    AbsolutePath OutputDirectory => RootDirectory / "output";

    Target Pack => _ => _
        .DependsOn(Compile)
        .Produces(OutputDirectory / "*.nupkg")
        .Executes(() =>
        {
            DotNetPack(_ => _
                .SetProject(Solution)
                .SetConfiguration(Configuration)
                .SetVersion(GitVersion.NuGetVersionV2)
                .SetOutputDirectory(OutputDirectory)
                .EnableNoBuild());
        });

    Target Logging => _ => _
        .Executes(() =>
        {
            Log.Verbose("This is a verbose message");
            Log.Debug("This is a debug message");
            Log.Information("This is an information message");
            Log.Warning("This is a warning message");
            Log.Error("This is an error message");
        });
}
