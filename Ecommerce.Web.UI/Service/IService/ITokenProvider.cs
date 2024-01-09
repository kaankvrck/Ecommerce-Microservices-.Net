namespace Ecommerce.Web.UI.Service.IService
{
    public interface ITokenProvider
    {

        void SetToken(string token);
        string? GetToken();
        void ClearToken();
    }
}
