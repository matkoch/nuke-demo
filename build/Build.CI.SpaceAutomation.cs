using Nuke.Common.CI.SpaceAutomation;

[SpaceAutomation(
    "continuous",
    "mcr.microsoft.com/dotnet/sdk:6.0",
    InvokedTargets = new[] { nameof(Pack) })]
partial class Build
{
}
