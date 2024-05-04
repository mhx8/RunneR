using Microsoft.Extensions.DependencyInjection;
using RunneR;
using RunneR.TestBench;

ServiceCollection services = [];
services
    .AddRunneR(
        builder =>
        {
            builder.AddRunner(
                "TestRun",
                runBuilder =>
                    runBuilder.AddRun<TestRun1>());

            builder.AddRunner(
                "TestRunInParallel",
                runBuilder =>
                {
                    runBuilder
                        .AddRun<TestRun1>(
                            options =>
                                options.ShouldRun = _ => true)
                        .AddRun<TestRun2>();
                }
            ).ConfigureOptions(
                configure =>
                    configure.RunAction = RunAction.RunInParallel);

            builder.AddRunner(
                "TestRunInSequence",
                runBuilder =>
                {
                    runBuilder
                        .AddRun<TestRun1>(
                            options =>
                                options.RunSequence = 1)
                        .AddRun<TestRun2>(
                            options => options.RunSequence = 2)
                        .AddRun<TestRun3>(
                            options => options.RunSequence = 2);
                }
            ).ConfigureOptions(
                configure =>
                    configure.RunAction = RunAction.RunInSequence);

            builder.AddRunner(
                    "TestRunWithPreRun",
                    runBuilder =>
                        runBuilder
                            .AddRun<TestRun1>()
                            .AddRun<TestRun2>()
                            .AddRun<TestRun3>())
                .AddPreRun<PreRun>(services);

            builder.AddRunner(
                    "TestRunWithPostRun",
                    runBuilder =>
                        runBuilder
                            .AddRun<TestRun1>()
                            .AddRun<TestRun2>())
                .AddPostRun<PostRun>(services);

            builder.AddRunner(
                    "TestRunWithPreAndPostRun",
                    runBuilder =>
                        runBuilder
                            .AddRun<TestRun1>()
                            .AddRun<TestRun2>())
                .AddPreRun<PreRun>(services)
                .AddPostRun<PostRun>(services);

            // builder.AddRunner<Data>(
            //     "TestRun3WithData",
            //     runBuilder =>
            //         runBuilder.AddRun<TestRun3WithData, Data>());

            builder.AddRunner(
                    "TestRunWithRetry",
                    runBuilder =>
                        runBuilder.AddRun<TestRun1>(
                            options => options.Retryable = true))
                .ConfigureOptions(
                    options => options.RetryCount = 3);
        });


// Build the ServiceProvider
ServiceProvider serviceProvider = services.BuildServiceProvider();
IRunnerFactory factory = serviceProvider.GetRequiredService<IRunnerFactory>();
IRunner definition = factory.GetRunner("TestRunWithPreAndPostRun");
await definition.RunAsync();

// IRunner<Data> definition3 = factory.GetRunner<Data>("TestRun3WithData");
// await definition3.RunAsync(new Data());