using Microsoft.Extensions.DependencyInjection;

namespace RunneR;

public static class RunnerRegistrationExtensions
{
    public static IRunnerRegistration ConfigureOptions(
        this IRunnerRegistration registration,
        Action<RunnerOptions> configureOptions)
    {
        RunnerRegistration registrationImplementation = registration.ToRunnerRegistration();
        RunnerOptions options = new();
        configureOptions(options);
        registrationImplementation.Options = options;
        return registration;
    }

    public static IRunnerRegistration AddPreRun<TPreRun>(
        this IRunnerRegistration registration,
        IServiceCollection serviceCollection)
        where TPreRun : IPreRun
    {
        RunnerRegistration registrationImplementation = registration.ToRunnerRegistration();
        serviceCollection.AddKeyedTransient(
            typeof(IPreRun),
            registrationImplementation.Identifier,
            typeof(TPreRun));
        
        return registration;
    }
    
    public static IRunnerRegistration AddPostRun<TPostRun>(
        this IRunnerRegistration registration,
        IServiceCollection serviceCollection)
        where TPostRun : IPostRun
    {
        RunnerRegistration registrationImplementation = registration.ToRunnerRegistration();
        serviceCollection.AddKeyedTransient(
            typeof(IPostRun),
            registrationImplementation.Identifier,
            typeof(TPostRun));
        
        return registration;
    }

    internal static RunnerRegistration ToRunnerRegistration(
        this IRunnerRegistration registration)
        => (RunnerRegistration)registration;
}