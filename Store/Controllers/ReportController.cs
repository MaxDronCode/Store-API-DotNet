using Microsoft.AspNetCore.Mvc;
using Store.Api.Models;
using Store.Service.Services;

namespace Store.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("income-report")]
    public async Task<IActionResult> GetIncomeReport()
    {
        var reportData = await _reportService.GetIncomeReport();

        var response = reportData.Select(r => new IncomeReportDto
        {
            ProductCode = r.ProductCode,
            ProductName = r.ProductName,
            TotalSales = r.TotalSales
        });

        return Ok(response);
    }
}