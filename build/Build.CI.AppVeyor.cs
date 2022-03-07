using Nuke.Common.CI.AppVeyor;

[AppVeyor(
    AppVeyorImage.UbuntuLatest,
    InvokedTargets = new[] { nameof(Pack) })]
partial class Build
{
}
