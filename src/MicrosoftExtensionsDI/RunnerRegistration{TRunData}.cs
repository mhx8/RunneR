namespace RunneR;

public class RunnerRegistration<TRunData>(
    string identifier,
    Func<IServiceProvider, IEnumerable<RunRegistration<TRunData>>> factory)
    where TRunData : IRunData
{
    public string Identifier { get; } = identifier;

    public IEnumerable<RunRegistration<TRunData>> GetRuns(
        IServiceProvider serviceProvider)
        => factory(serviceProvider);

    public RunnerOptions? Options { get; set; }
}