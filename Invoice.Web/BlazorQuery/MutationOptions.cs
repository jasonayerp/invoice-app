namespace BlazorQuery;

public class MutationOptions
{
    private Func<Task> _onSuccess;

    public void OnSuccess(Func<Task> onSuccess)
    {
        _onSuccess = onSuccess;
    }

    public void OnSuccessInvoke()
    {
        _onSuccess();
    }
}
