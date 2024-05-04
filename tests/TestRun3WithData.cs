namespace RunneR.TestBench;

public class TestRun3WithData : IRun<Data>
{
    public Task<Result> RunAsync(Data data)
    {
        Console.WriteLine("TestRun3 with data is running...");
        return Task.FromResult<Result>(new Success());
    }
}