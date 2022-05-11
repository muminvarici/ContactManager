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

    /// <summary>
    /// Get Contact
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetContactQueryResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var response = await _sender.Send(new GetContactQuery()
        {
            Id = id
        });
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post(PostContactsRequest request)
    {
        var response = await _sender.Send(request.Adapt<CreateContactCommand>());
        return Ok(response);
    }
}
