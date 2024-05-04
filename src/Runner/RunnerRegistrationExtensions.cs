using Microsoft.Extensions.DependencyInjection;

namespace RunneR.RunneR;

internal static class RunnerRegistrationExtensions
{
    internal static bool HasPreRun(
        this IRunnerRegistration runnerRegistration,
        IServiceProvider serviceProvider)
        => serviceProvider.GetKeyedService<IPreRun>(
            runnerRegistration.ToRunnerRegistration().Identifier) != null;

    internal static bool HasPostRun(
        this IRunnerRegistration runnerRegistration,
        IServiceProvider serviceProvider)
        => serviceProvider.GetKeyedService<IPostRun>(
            runnerRegistration.ToRunnerRegistration().Identifier) != null;

    internal static IPreRun GetPreRun(
        this IRunnerRegistration runnerRegistration,
        IServiceProvider serviceProvider)
        => serviceProvider.GetRequiredKeyedService<IPreRun>(
            runnerRegistration.ToRunnerRegistration().Identifier);

    internal static IPostRun GetPostRun(
        this IRunnerRegistration runnerRegistration,
        IServiceProvider serviceProvider)
        => serviceProvider.GetRequiredKeyedService<IPostRun>(
            runnerRegistration.ToRunnerRegistration().Identifier);

    internal static IEnumerable<RunRegistration> GetRunRegistrations(
        this IRunnerRegistration runnerRegistration,
        IServiceProvider serviceProvider)
        => runnerRegistration
            .ToRunnerRegistration()
            .GetRuns(serviceProvider)
            .Where(
                run =>
                    run.Options.ShouldRun(serviceProvider));
}