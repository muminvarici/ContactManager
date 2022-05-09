namespace Contacts.Api.Models.Contacts;

public class PostContactsRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Firm { get; set; }
    public AdditionalInfoDto AdditionalInfo { get; set; }
}

public class AdditionalInfoDto
{
    public int Type { get; set; }
    public string Value { get; set; }
}
