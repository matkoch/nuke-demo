using Nuke.Common;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    Target Restore => _ => _
        .Executes(() =>
        {
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
        });
}
