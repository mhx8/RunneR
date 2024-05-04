namespace RunneR.RunneR;

internal sealed class ParallelRunner(
    IRunnerRegistration runnerRegistration,
    IServiceProvider serviceProvider) : IRunner
{
    public async Task<Result> RunAsync()
    {
        List<Task<Result>> tasks = [];
        foreach (RunRegistration runRegistration in runnerRegistration.GetRunRegistrations(serviceProvider))
        {
            if (runnerRegistration.HasPreRun(serviceProvider))
            {
                tasks.Add(runnerRegistration.GetPreRun(serviceProvider).RunAsync());
            }

            tasks.Add(runRegistration.GetRun(serviceProvider).RunAsync());

            if (runnerRegistration.HasPostRun(serviceProvider))
            {
                tasks.Add(runnerRegistration.GetPostRun(serviceProvider).RunAsync());
            }
        }

        Result[] results = await Task.WhenAll(tasks);

        List<string> failureMessages = RunnerUtils.GetFailureMessages(results);
        return failureMessages.Count > 0 ? RunnerUtils.Failure(failureMessages) : new Success();
    }
}