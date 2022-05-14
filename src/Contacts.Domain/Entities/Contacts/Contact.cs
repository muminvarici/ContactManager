using System.Text.Json.Serialization;

namespace Contacts.Domain.Entities.Contacts;

public class Contact : ModelBase
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Firm { get; set; }
    public List<AdditionalInfo> AdditionalInfo { get; set; }
}

public class AdditionalInfo
{
    [JsonPropertyName("UUID")]
    public string Id { get; set; }
    public AdditionalInfoType Type { get; set; }
    public string Value { get; set; }
}

public enum AdditionalInfoType
{
    None = 0,
    Phone = 1,
    Email = 2,
    Location = 3
}
