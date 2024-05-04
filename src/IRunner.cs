namespace RunneR;

public interface IRunner
{
    Task<Result> RunAsync();
}