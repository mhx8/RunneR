namespace RunneR.RunneR;

internal sealed class SynchronousRunner(
    IRunnerRegistration runnerRegistration,
    IServiceProvider serviceProvider) : IRunner
{
    public async Task<Result> RunAsync()
    {
        foreach (RunRegistration runRegistration in runnerRegistration
                     .GetRunRegistrations(serviceProvider))
        {
            if (runnerRegistration.HasPreRun(serviceProvider))
            {
                await runnerRegistration.GetPreRun(serviceProvider).RunAsync();
            }

            IRun run = runRegistration.GetRun(serviceProvider);
            Result result = await run.RunAsync();
            if (runnerRegistration.HasPostRun(serviceProvider))
            {
                await runnerRegistration
                    .GetPostRun(serviceProvider)
                    .RunAsync();
            }

            if (result is Failure)
            {
                List<string> failureMessages = RunnerUtils.GetFailureMessages([result]);
                return RunnerUtils.Failure(failureMessages);
            }
        }

        return new Success();
    }
}