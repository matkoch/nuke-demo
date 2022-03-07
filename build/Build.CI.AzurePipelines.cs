using Nuke.Common.CI.AzurePipelines;

[AzurePipelines(
    AzurePipelinesImage.UbuntuLatest,
    InvokedTargets = new[] { nameof(Pack) },
    NonEntryTargets = new[] { nameof(Restore), nameof(Compile) })]
partial class Build
{
}
