namespace RunneR;

public class RunOptions
{
    public Func<IServiceProvider, bool> ShouldRun { get; set; } = _ => true;

    public int? RunSequence { get; set; }

    public bool Retryable { get; set; }

    // If not set, count will be inherited from RunnerOptions
    public int? RetryCount { get; set; }
}