using MediatR;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using Reports.Worker.Services.ApiClients;
using System.Reflection;
using ExcelRow = Reports.Worker.Models.ExcelRow;

namespace Reports.Worker.Events.ExcelDataProduced;

public class ExcelDataProducedEventHandler : INotificationHandler<ExcelDataProducedEvent>
{
    private ILogger<ExcelDataProducedEventHandler> _logger;
    private readonly IReportsApi _reportsApi;

    public ExcelDataProducedEventHandler(
        ILogger<ExcelDataProducedEventHandler> logger,
        IReportsApi reportsApi
    )
    {
        _logger = logger;
        _reportsApi = reportsApi;
    }

    public Task Handle(ExcelDataProducedEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Data == null)
        {
            _logger.LogWarning("No data to generate report");
            return Task.CompletedTask;
        }

        var path = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.xlsx");
        var f = new FileInfo(path);
        if (f.Exists) f.Delete();
        using var ep = new ExcelPackage(f);
        var worksheet = ep.Workbook.Worksheets.Add("Sheet 1");

        var headers = typeof(ExcelRow)
            .GetProperties()
            .Select(pi => (MemberInfo)pi)
            .ToArray();

        worksheet.Cells.LoadFromCollection(notification.Data, true
            , TableStyles.Dark1
            , BindingFlags.Public | BindingFlags.Instance
            , headers);

        ep.Save();

        _ = _reportsApi.ChangeStatus(notification.Id, new ChangeStatusRequest
        {
            Path = path, Status = 2
        });
        return Task.CompletedTask;
    }
}
