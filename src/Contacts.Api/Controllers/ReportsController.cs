using Contacts.Api.Models.Reports;
using Contacts.Application.Commands.Reports.ChangeReportStatus;
using Contacts.Application.Queries.Reports.GetReport;
using Contacts.Application.Queries.Reports.GetReports;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Reports.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly ISender _sender;

    public ReportsController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Get Report
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FileContentResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var response = await _sender.Send(new GetReportQuery
        {
            Id = id
        });
        if (!response.IsSuccess) return NotFound();

        return File(new MemoryStream(response.Data.ToArray()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", response.FileName);
    }

    /// <summary>
    /// List All Reports
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ListAllReportsQueryResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var response = await _sender.Send(new ListAllReportsQuery());
        return Ok(response);
    }

    /// <summary>
    /// Change report status
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ChangeReportStatusCommandResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Put([FromRoute] string id, [FromBody] ChangeReportStatusRequest request)
    {
        var command = request.Adapt<ChangeReportStatusCommand>();
        command.Id = id;
        var response = await _sender.Send(command);
        return Ok(response);
    }
}
