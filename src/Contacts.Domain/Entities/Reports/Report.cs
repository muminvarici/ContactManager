namespace Contacts.Domain.Entities.Reports;

public class Report : ModelBase
{
    public string Path { get; set; }
    public ReportStatus Status { get; set; }
}
