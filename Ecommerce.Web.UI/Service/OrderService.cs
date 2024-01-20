using Ecommerce.Web.UI.Models;
using Ecommerce.Web.UI.Service.IService;
using Ecommerce.Web.UI.Utility;

namespace Ecommerce.Web.UI.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;
        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateOrderAsync(CreateOrderRequestDto createOrderRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = createOrderRequestDto,
                Url = SD.OrderAPIBase + "/api/orders/CreateOrder"
            });
        }

        public async Task<ResponseDto?> GetMyOrders(string customerID)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = customerID,
                Url = SD.OrderAPIBase + "/api/orders/GetMyOrders?customerId=" + customerID
            });
        }
    }
}
