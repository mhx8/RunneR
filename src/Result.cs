namespace RunneR;

public abstract record Result;

public record Success : Result;

public record Failure(
    string Message) : Result;