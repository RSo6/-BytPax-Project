namespace BytPax.Models;

public class ErrorViewModel
{
    public int Id { get; set; }
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}