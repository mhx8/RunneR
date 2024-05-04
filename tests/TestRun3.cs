namespace RunneR.TestBench;

public class TestRun3 : IRun
{
    public Task<Result> RunAsync()
    {
        Console.WriteLine("TestRun3 is running...");
        return Task.FromResult<Result>(new Success());
    }
}