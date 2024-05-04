namespace RunneR;

public interface IRunnerFactory
{
    IRunner GetRunner(
        string identifier);

    IRunner<TRunData> GetRunner<TRunData>(
        string identifier)
        where TRunData : IRunData;
}