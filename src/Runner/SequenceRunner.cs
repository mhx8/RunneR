namespace RunneR.RunneR;

internal sealed class SequenceRunner(
    IRunnerRegistration runnerRegistration,
    IServiceProvider serviceProvider) : IRunner
{
    public async Task<Result> RunAsync()
    {
        IEnumerable<RunRegistration> runs = runnerRegistration.GetRunRegistrations(serviceProvider);
        IEnumerable<IGrouping<int, RunRegistration>> groupedRuns =
            runs.GroupBy(
                run => run.Options.RunSequence ?? 0);

        Result[] results = Array.Empty<Result>();
        foreach (IGrouping<int, RunRegistration> runGroup in groupedRuns)
        {
            if (results.OfType<Failure>().Any())
            {
                break;
            }

            List<Task<Result>> tasks = [];
            foreach (IRun run in runGroup
                         .Select(
                             run =>
                                 run.GetRun(
                                     serviceProvider)))
            {
                if (runnerRegistration.HasPreRun(serviceProvider))
                {
                    tasks.Add(
                        runnerRegistration
                            .GetPreRun(serviceProvider)
                            .RunAsync());
                }

                tasks.Add(run.RunAsync());

                if (runnerRegistration.HasPostRun(serviceProvider))
                {
                    tasks.Add(
                        runnerRegistration
                            .GetPostRun(serviceProvider)
                            .RunAsync());
                }
            }

            results = results.Concat(
                await Task.WhenAll(tasks)).ToArray();
        }


        List<string> failureMessages = RunnerUtils.GetFailureMessages(results);
        return failureMessages.Count > 0 ? RunnerUtils.Failure(failureMessages) : new Success();
    }
}