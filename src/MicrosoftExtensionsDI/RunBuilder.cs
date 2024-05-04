using Microsoft.Extensions.DependencyInjection;

namespace RunneR;

public class RunBuilder(
    IServiceCollection serviceCollection,
    string identifier)
{
    public RunBuilder AddRun<TType>(
        Action<RunOptions>? runOptions = null)
        where TType : class, IRun
    {
        RunOptions options = new();
        runOptions?.Invoke(options);
        IRunRegistration registration = new RunRegistration(
            factory =>
                factory.GetKeyedServices<IRun>(identifier)
                    .First(
                        run => run.GetType() == typeof(TType)),
            options);

        serviceCollection.AddKeyedTransient(
            typeof(IRun),
            identifier,
            typeof(TType));

        serviceCollection.AddKeyedSingleton(
            identifier,
            registration);

        return this;
    }

    public RunBuilder AddRun<TType, TRunData>(
        Action<RunOptions>? runOptions = null)
        where TType : class, IRun<TRunData>
        where TRunData : IRunData
    {
        RunOptions options = new();
        runOptions?.Invoke(options);
        RunRegistration<TRunData> registration = new(
            factory =>
                factory.GetKeyedServices<IRun<TRunData>>(identifier)
                    .First(
                        run => run.GetType() == typeof(TType)),
            options);

        serviceCollection.AddKeyedTransient(
            typeof(IRun<TRunData>),
            identifier,
            typeof(TType));

        serviceCollection.AddKeyedSingleton(
            identifier,
            registration);

        return this;
    }
}