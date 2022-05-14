namespace Contacts.Api.Models.Reports;

public class ChangeReportStatusRequest
{
    public int Status { get; set; }
    public string Path { get; set; }
}
