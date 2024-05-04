using Microsoft.Extensions.DependencyInjection;
using RunneR.RunneR;

namespace RunneR;

internal sealed class Runner(
    IRunnerRegistration registration,
    IServiceProvider serviceProvider) : IRunner
{
    public async Task<Result> RunAsync()
    {
        RunAction? runAction =
            registration.ToRunnerRegistration().Options?.RunAction;

        return runAction switch
        {
            RunAction.RunInParallel => await new ParallelRunner(registration, serviceProvider)
                .RunAsync(),
            RunAction.RunInSequence => await new SequenceRunner(registration, serviceProvider)
                .RunAsync(),
            RunAction.RunSynchronously or null => await new SynchronousRunner(registration, serviceProvider)
                .RunAsync(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}