using Refit;

namespace Reports.Worker.Services.ApiClients;

public interface IReportsApi
{
    [Put("/{id}")]
    Task<object> ChangeStatus([Query] string id, [Body] ChangeStatusRequest request);
}

public class ChangeStatusRequest
{
    public string Path { get; set; }
    public int Status { get; set; }
}
