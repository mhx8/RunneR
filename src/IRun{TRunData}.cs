namespace RunneR;

public interface IRun<in TRunData>
    where TRunData : IRunData
{
    Task<Result> RunAsync(
        TRunData data);
}