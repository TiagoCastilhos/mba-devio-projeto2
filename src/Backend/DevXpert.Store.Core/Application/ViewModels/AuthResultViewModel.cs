namespace DevXpert.Store.Core.Application.ViewModels;

public class AuthResultViewModel
{
    public bool Success { get; set; }
    public string Token { get; set; }
    public List<string> Errors { get; set; }
}