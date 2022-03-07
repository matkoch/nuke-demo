using Nuke.Common.CI.GitHubActions;

[GitHubActions(
    "continuous",
    GitHubActionsImage.UbuntuLatest,
    On = new[] { GitHubActionsTrigger.Push },
    EnableGitHubContext = true,
    InvokedTargets = new[] { nameof(Pack) })]
partial class Build
{
}
