using Newtonsoft.Json;

namespace Invoice.Web.Core;

[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public class CollectionResult<T>
{
    [JsonProperty]
    public bool IsSuccess { get; set; }
    [JsonProperty]
    public ICollection<T>? Data { get; set; }
}
