namespace Invoice.Domains.Common.Models;

public class AppClientSettingModel
{
    public Guid Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
