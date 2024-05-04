namespace RunneR;

public class RunRegistration<TRunData>(
    Func<IServiceProvider, IRun<TRunData>> factory,
    RunOptions options)
    where TRunData : IRunData
{
    public RunOptions Options { get; set; } = options;

    public IRun<TRunData> GetRun(
        IServiceProvider serviceProvider)
        => factory(serviceProvider);
}