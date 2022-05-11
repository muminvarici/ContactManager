using Contacts.Api.Models.Contacts;
using Contacts.Application.Commands.Contacts.CreateAdditionalInfo;
using Contacts.Application.Commands.Contacts.CreateContact;
using Contacts.Application.Commands.Contacts.DeleteAdditionalInfo;
using Contacts.Application.Commands.Contacts.DeleteContact;
using Contacts.Application.Commands.Reports.CreateReport;
using Contacts.Application.Queries.Contacts;
using Contacts.Application.Queries.Contacts.GetContact;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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


    /// <summary>
    /// Create Contacts
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CreateContactCommandResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Post(CreateContactsRequest request)
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
    /// Delete Additional Info
    /// </summary>
    [HttpDelete("{id}/additional-info/{infoId}")]
    [ProducesResponseType(typeof(DeleteAdditionalInfoCommandResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] string id, [FromRoute] string infoId)
    {
        var response = await _sender.Send(new DeleteAdditionalInfoCommand
        {
            Id = id, InfoId = infoId
        });
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
