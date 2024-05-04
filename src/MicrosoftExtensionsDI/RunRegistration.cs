namespace RunneR;

internal class RunRegistration(
    Func<IServiceProvider, IRun> factory,
    RunOptions options) : IRunRegistration
{
    public RunOptions Options { get; } = options;

    public IRun GetRun(
        IServiceProvider serviceProvider)
        => factory(serviceProvider);
}