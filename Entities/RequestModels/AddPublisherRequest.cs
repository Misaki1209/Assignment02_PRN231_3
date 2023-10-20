namespace Entities.RequestModels;

public class AddPublisherRequest
{
    public string PublisherName { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}