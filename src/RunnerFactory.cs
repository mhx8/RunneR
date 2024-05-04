using Microsoft.Extensions.DependencyInjection;

namespace RunneR;

public class RunnerFactory(
    IServiceProvider serviceProvider) : IRunnerFactory
{
    public IRunner GetRunner(
        string identifier)
    {
        return new Runner(
            serviceProvider.GetRequiredKeyedService<RunnerRegistration>(
                identifier), serviceProvider);
    }

    public IRunner<TRunData> GetRunner<TRunData>(
        string identifier)
        where TRunData : IRunData
    {
        return new Runner<TRunData>(
            serviceProvider.GetRequiredKeyedService<RunnerRegistration<TRunData>>(
                identifier), serviceProvider);
    }
}