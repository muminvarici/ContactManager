using MediatR;
using Reports.Worker.Models;

namespace Reports.Worker.Events.ExcelDataProduced;

public class ExcelDataProducedEvent : INotification
{
    public IEnumerable<ExcelRow> Data { get; set; }
    public string Id { get; set; }
}
