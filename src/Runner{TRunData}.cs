namespace RunneR;

public class Runner<TRunData>(
    RunnerRegistration<TRunData> registration,
    IServiceProvider serviceProvider) : IRunner<TRunData>
    where TRunData : IRunData
{
    public async Task<Result> RunAsync(
        TRunData data)
    {
        if (registration.Options?.RunAction == RunAction.RunInParallel)
        {
            return await RunParallelInternalAsync(data);
        }

        return await RunInternalAsync(data);
    }

    private async Task<Result> RunInternalAsync(
        TRunData data)
    {
        foreach (RunRegistration<TRunData> run in registration
                     .GetRuns(serviceProvider)
                     .Where(
                         run =>
                             run.Options.ShouldRun(serviceProvider)))
        {
            Result result = await run
                .GetRun(serviceProvider)
                .RunAsync(data);
            if (result is Failure)
            {
                return result;
            }
        }

        return new Success();
    }

    private async Task<Result> RunParallelInternalAsync(
        TRunData data)
    {
        IEnumerable<Task<Result>> tasks = registration
            .GetRuns(serviceProvider)
            .Where(
                run =>
                    run.Options.ShouldRun(serviceProvider))
            .Select(
                run =>
                    run.GetRun(serviceProvider)
                        .RunAsync(data));
        Result[] results = await Task.WhenAll(tasks);

        List<string> failureMessages = results
            .OfType<Failure>()
            .Select(
                failure =>
                    failure.Message)
            .ToList();

        if (failureMessages.Count > 0)
        {
            string aggregatedMessage = string.Join(", ", failureMessages);
            return new Failure($"One or more runs failed: {aggregatedMessage}");
        }

        return new Success();
    }
}