using Ecommerce.Web.UI.Models;
using Ecommerce.Web.UI.Service.IService;
using Ecommerce.Web.UI.Utility;

namespace Ecommerce.Web.UI.Service
{
    public class CatalogService : ICatalogService
    {
        private readonly IBaseService _baseService;
        public CatalogService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> CheckStockForProduct(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = id,
                Url = SD.CatalogAPIBase + "/api/check_stock_for_product​?id=" + id.ToString()
            }, withBearer: false);
        }

        public async Task<ResponseDto?> GetDistinctBrandsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = null,
                Url = SD.CatalogAPIBase + "/api/brands"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> GetDistinctCategoriesAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = null,
                Url = SD.CatalogAPIBase + "/api/categories"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> GetItemById(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = id,
                Url = SD.CatalogAPIBase + "/api/items?id=" + id.ToString()
            }, withBearer: false);
        }

        public async Task<ResponseDto?> GetItems()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = null,
                Url = SD.CatalogAPIBase + "/api/items"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> GetItemsByBrand(string brand)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = brand,
                Url = SD.CatalogAPIBase + "/api​/items​/brand​?brand=" + brand.ToString()
            }, withBearer: false);
        }

        public async Task<ResponseDto?> GetItemsByCategory(string category)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = category,
                Url = SD.CatalogAPIBase + "​/api​/items​/category?category=" + category.ToString()
            }, withBearer: false);
        }

        public async Task<ResponseDto?> GetProducts()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = null,
                Url = SD.CatalogAPIBase + "/api/products="
            }, withBearer: false);
        }

        public async Task<ResponseDto?> UpdateStockForProduct(int id, UpdateStockDto updateStockDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = updateStockDto,
                Url = SD.CatalogAPIBase + "/api/update_stock_for_product​?id=" + id.ToString()
            }, withBearer: false);
        }
    }
}