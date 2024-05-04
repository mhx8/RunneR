using Microsoft.Extensions.DependencyInjection;

namespace RunneR;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRunneR(
        this IServiceCollection services,
        Action<RunnerBuilder> builder)
    {
        services.AddSingleton<IRunnerFactory, RunnerFactory>();

        builder(new RunnerBuilder(services));
        return services;
    }
}