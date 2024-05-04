namespace RunneR;

public interface IRunner<in TRunData>
    where TRunData : IRunData
{
    Task<Result> RunAsync(
        TRunData data);
}