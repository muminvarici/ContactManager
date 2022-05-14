using Refit;
using Reports.Worker.Models;

namespace Reports.Worker.Services.ApiClients;

public interface IContactsApi
{
    [Get("")]
    Task<GetContactsResponse> GetContacts([Query] bool withDetails);
}

public class GetContactsResponse
{
    public bool IsSuccess { get; set; }
    public IEnumerable<ContactDto> Data { get; set; }
}
