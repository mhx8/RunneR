namespace RunneR.TestBench;

public class PreRun : IPreRun
{
    public Task<Result> RunAsync()
    {
        Console.WriteLine("PreRun is running...");
        return Task.FromResult<Result>(new Success());
    }
}