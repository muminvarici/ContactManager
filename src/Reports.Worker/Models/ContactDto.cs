using System.Text.Json.Serialization;

namespace Reports.Worker.Models;

public class ContactDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Firm { get; set; }
    public List<AdditionalInfoDto> AdditionalInfo { get; set; }
}

public class AdditionalInfoDto
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
