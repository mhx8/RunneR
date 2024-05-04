namespace RunneR.TestBench;

public class TestRun2 : IRun
{
    public Task<Result> RunAsync()
    {
        Console.WriteLine("TestRun2 is running...");
        return Task.FromResult<Result>(new Success());
    }
}