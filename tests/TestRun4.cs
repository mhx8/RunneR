namespace RunneR.TestBench;

public class TestRun4 : IRun
{
    public Task<Result> RunAsync()
    {
        Console.WriteLine("TestRun4 is running...");
        return Task.FromResult<Result>(new Success());
    }
}