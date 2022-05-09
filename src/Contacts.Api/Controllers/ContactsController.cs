using Contacts.Api.Models.Contacts;
using Contacts.Application.Commands.Contacts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly ISender _sender;

    public ContactsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Post(PostContactsRequest request)
    {
        var response = await _sender.Send(request.Adapt<CreateContactCommand>());
        return Ok(response);
    }
}
