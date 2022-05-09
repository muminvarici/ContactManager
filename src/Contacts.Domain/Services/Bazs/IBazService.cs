using Refit;
using Contacts.Domain.Services.Bazs.Response;
using Contacts.Domain.Services.Bazs.Request;

namespace Contacts.Domain.Services.Bazs;

public interface IBazService
{
    [Get("/bazs/{id}")]
    Task<BazServiceResponse> Get(int id);

    [Post("/bazs")]
    Task<BazServiceResponse> Create([Body] CreateBazServiceRequest request);
}
