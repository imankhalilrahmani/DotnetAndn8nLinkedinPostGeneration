namespace AiLinkedinBridge.Models;


public class GeneratePostRequest
{
    public string Topic { get; set; } = string.Empty;
    public string GeminiApiKey { get; set; } = string.Empty;
    public string GoogleSheetId { get; set; } = string.Empty;
}