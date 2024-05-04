namespace RunneR;

public class RunnerOptions
{
    public RunAction? RunAction { get; set; }

    public FailureAction? FailureAction { get; set; }

    public int RetryCount { get; set; }
}