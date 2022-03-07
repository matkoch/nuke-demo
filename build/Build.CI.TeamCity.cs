using Nuke.Common.CI.TeamCity;

[TeamCity(
    VcsTriggeredTargets = new[] { nameof(Pack) },
    NonEntryTargets = new[] { nameof(Restore), nameof(Compile) })]
partial class Build
{
}
