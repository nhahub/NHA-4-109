using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.DTOs.Admin;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin")] // entire controller — no action here should ever be reachable by Owner/Tenant
    [ApiController]
    [Route("api/admin/ml")]
    public class AdminMlController : ControllerBase
    {
        private readonly IMlApiClient _mlApiClient;

        public AdminMlController(IMlApiClient mlApiClient)
        {
            _mlApiClient = mlApiClient;
        }

        [HttpPost("scrape")]
        public async Task<IActionResult> StartScrape(StartScrapeDTO dto)
        {
            var result = await _mlApiClient.StartScrapeAsync(dto.SiteName, dto.TargetUrl, dto.MaxPages);
            return Ok(result);
        }

        [HttpGet("scrape/status/{jobId}")]
        public async Task<IActionResult> GetScrapeStatus(Guid jobId)
        {
            var result = await _mlApiClient.GetScrapeStatusAsync(jobId);
            return Ok(result);
        }

        [HttpPost("train-model")]
        public async Task<IActionResult> TriggerTraining(TrainModelDTO dto)
        {
            await _mlApiClient.TriggerTrainingAsync(dto.ModelType, dto.ForceRetrain);
            return Ok(new { message = "Training triggered" });
        }
    }
}