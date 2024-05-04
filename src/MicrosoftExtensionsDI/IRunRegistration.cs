namespace RunneR;

internal interface IRunRegistration
{
    RunOptions Options { get; }

    IRun GetRun(
        IServiceProvider serviceProvider);
}