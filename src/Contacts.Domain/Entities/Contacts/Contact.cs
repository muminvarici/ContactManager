namespace Contacts.Domain.Entities.Contacts;

public class Contact : ModelBase
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Firm { get; set; }
    public AdditionalInfo AdditionalInfo { get; set; }
}

public class AdditionalInfo
{
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
