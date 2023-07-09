namespace BlazorQuery;

public static class BlazorQuery
{
    public static Mutation<T> UseMutation<T>(Func<T, Task> action, Action<MutationOptions> optionsAction)
    {
        MutationOptions options = new MutationOptions();
        optionsAction(options);

        return new Mutation<T>(action, options);
    }
}