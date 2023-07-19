namespace Invoice.Api.Data.Documents;

public class AppClientDocument
{
    public Guid Id { get; set; }
    public string ClientId { get; set; }
    
    public List<AppClientSettingDocument> Settings { get; set; } = new List<AppClientSettingDocument>();
}
