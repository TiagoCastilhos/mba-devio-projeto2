namespace DevXpert.Store.Core.Application.App
{
    public interface IAppIdentityUser
    {
        string GetUsername();
        Guid GetUserId();
        bool IsAuthenticated();
        string GetUserRole();
        string GetRemoteIpAddress();
        string GetLocalIpAddress();
    }
}
