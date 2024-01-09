using Ecommerce.Web.UI.Models;

namespace Ecommerce.Web.UI.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
