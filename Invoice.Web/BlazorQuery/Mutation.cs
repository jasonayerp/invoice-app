namespace BlazorQuery;

public class Mutation<T>
{
    private Func<T, Task> _onMutate;
    private MutationOptions _options;

    public Mutation(Func<T, Task> onMutate, MutationOptions options)
    {
        _onMutate = onMutate;
        _options = options;
    }

    public void Mutate(T obj)
    {
        _onMutate(obj);

        _options.OnSuccessInvoke();
    }
}
