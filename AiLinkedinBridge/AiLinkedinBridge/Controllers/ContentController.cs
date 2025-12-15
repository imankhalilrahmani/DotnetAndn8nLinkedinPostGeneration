using AiLinkedinBridge.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace AiLinkedinBridge.Controllers;

[ApiController]
[Route("api/content")]
public class ContentController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ContentController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    [HttpPost("linkedin")]
    public async Task<IActionResult> GenerateLinkedinPost(
    [FromBody] GeneratePostRequest request)
    {
        try
        {
            var webhookUrl = _configuration["N8nWebhookUrl"]
                ?? "http://localhost:5678/webhook/generate-linkedin-post";

            var response = await _httpClient.PostAsJsonAsync(webhookUrl, request);

            // فقط جواب خام n8n
            var content = await response.Content.ReadAsStringAsync();

            // Debug: لاگ کردن کامل جواب
            Console.WriteLine("n8n Response:");
            Console.WriteLine(content);

            // برگشت جواب بدون هیچ تغییر
            return new ContentResult
            {
                Content = content,
                ContentType = "application/json",
                StatusCode = (int)response.StatusCode
            };
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
