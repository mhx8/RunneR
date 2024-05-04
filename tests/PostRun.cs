namespace RunneR.TestBench;

public class PostRun : IPostRun
{
    public Task<Result> RunAsync()
    {
        Console.WriteLine("PostRun is running...");
        return Task.FromResult<Result>(new Success());
    }
}