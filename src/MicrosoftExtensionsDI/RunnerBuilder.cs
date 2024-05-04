using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RunneR.RunneR;

namespace RunneR;

public class RunnerBuilder(
    IServiceCollection serviceCollection)
{
    public IRunnerRegistration AddRunner(
        Action<RunBuilder> runBuilder)
        => AddRunner(IdentifierConstants.Default, runBuilder);

    public IRunnerRegistration AddRunner(
        string identifier,
        Action<RunBuilder> runBuilder)
    {
        RunnerRegistration runnerRegistration = new(
            identifier,
            factory => factory.GetKeyedServices<RunRegistration>(identifier));

        serviceCollection.AddKeyedSingleton(
            identifier,
            runnerRegistration);

        runBuilder(
            new RunBuilder(
                serviceCollection,
                identifier));

        return runnerRegistration;
    }

    public RunnerRegistration<TRunData> AddRunner<TRunData>(
        Action<RunBuilder> runBuilder)
        where TRunData : IRunData
        => AddRunner<TRunData>(IdentifierConstants.Default, runBuilder);

    public RunnerRegistration<TRunData> AddRunner<TRunData>(
        string identifier,
        Action<RunBuilder> runBuilder)
        where TRunData : IRunData
    {
        serviceCollection.TryAddTransient<IRunner, Runner>();

        RunnerRegistration<TRunData> runnerRegistration = new(
            identifier,
            factory => factory.GetKeyedServices<RunRegistration<TRunData>>(identifier));

        serviceCollection.AddKeyedSingleton(
            identifier,
            runnerRegistration);

        runBuilder(
            new RunBuilder(
                serviceCollection,
                identifier));

        return runnerRegistration;
    }
}