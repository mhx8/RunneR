namespace RunneR.TestBench;

public class TestRun1 : IRun
{
    public Task<Result> RunAsync()
    {
        Console.WriteLine("TestRun1 is running...");
        return Task.FromResult<Result>(new Success());
    }
}