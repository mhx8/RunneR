namespace RunneR;

internal class RunnerRegistration(
    string identifier,
    Func<IServiceProvider, IEnumerable<RunRegistration>> factory) : IRunnerRegistration
{
    internal string Identifier { get; } = identifier;

    internal IEnumerable<RunRegistration> GetRuns(
        IServiceProvider serviceProvider)
        => factory(serviceProvider);

    internal RunnerOptions? Options { get; set; }
}