namespace Invoice.Services;

public interface IIdService
{
    int Id { get; }
    Guid NewGuid();
}

public class IdService : IIdService
{
    public int Id => 1;
    public Guid NewGuid() => Guid.NewGuid();
}
