namespace RunneR.RunneR;

internal static class RetryUtils
{
    internal static Task<Result> WithRetryAsync(
        this IRunRegistration runRegistration,
        IServiceProvider serviceProvider)
    {
        IRun run = runRegistration.GetRun(serviceProvider);

        
        Exception? lastException = null;
        for (int i = 0; i < runRegistration.Options.RetryCount; i++)
        {
            try
            {

            }
            catch (Exception exception)
            {
                lastException = exception;
            }
        }

        if (lastException != null)
        {
            
        }

        return Task.FromResult<Result>(new Success());
    }
}