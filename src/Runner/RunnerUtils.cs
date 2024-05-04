namespace RunneR.RunneR;

internal static class RunnerUtils
{
    internal static List<string> GetFailureMessages(
        IEnumerable<Result> results)
        => results
            .OfType<Failure>()
            .Select(
                failure =>
                    failure.Message)
            .ToList();


    internal static Failure Failure(
        IEnumerable<string> failureMessages)
    {
        string aggregatedMessage = string.Join(", ", failureMessages);
        return new Failure($"One or more runs failed: {aggregatedMessage}");
    }
}