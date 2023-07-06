namespace Invoice.Mvc;

public class Error
{
    public Guid ErrorId { get; set; } = Guid.NewGuid();
    public int ErrorCode { get; set; } = 500;
    public string Message { get; set; } = "Internal Server Error";
    public ICollection<ErrorDetail> Errors { get; set; } = new HashSet<ErrorDetail>();
}
