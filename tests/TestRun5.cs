namespace RunneR.TestBench;

public class TestRun5 : IRun
{
    public Task<Result> RunAsync()
    {
        Console.WriteLine("TestRun5 is running...");
        return Task.FromResult<Result>(new Success());
    }
}