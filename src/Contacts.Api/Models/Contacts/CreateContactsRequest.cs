namespace Contacts.Api.Models.Contacts;

public class CreateContactsRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Firm { get; set; }
    public List<AdditionalInfoDto> AdditionalInfo { get; set; }
}
