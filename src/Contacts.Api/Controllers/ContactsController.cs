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

    /// <summary>
    /// List All Contacts
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ListAllContactsQueryResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var response = await _sender.Send(new ListAllContactsQuery());
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post(PostContactsRequest request)
    {
        var response = await _sender.Send(request.Adapt<CreateContactCommand>());
        return Ok(response);
    }
    /// <summary>
    /// Create Additional Info
    /// </summary>
    [HttpPut("{id}/additional-info")]
    [ProducesResponseType(typeof(CreateAdditionalInfoCommandResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Put([FromRoute] string id, [FromBody] CreateAdditionalInfoRequest request)
    {
        var command = request.Adapt<CreateAdditionalInfoCommand>();
        command.Id = id;
        var response = await _sender.Send(command);
        return Ok(response);
    }
    /// <summary>
    /// Delete Contact
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(DeleteContactCommandResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var response = await _sender.Send(new DeleteContactCommand
        {
            Id = id
        });
        return Ok(response);
    }
}
