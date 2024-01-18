using Ecommerce.Web.UI.Models;

namespace Ecommerce.Web.UI.Service.IService
{
    public interface IOrderService
    {
        Task<ResponseDto?> CreateOrderAsync(CreateOrderRequestDto createOrderRequestDto);
    }
}
