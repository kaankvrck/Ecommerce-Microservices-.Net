using Ecommerce.Web.UI.Models;

namespace Ecommerce.Web.UI.Service.IService
{
    public interface ICatalogService
    {
        Task<ResponseDto?> GetItems();
        Task<ResponseDto?> GetItemById(int id);
        Task<ResponseDto?> GetDistinctCategoriesAsync();
        Task<ResponseDto?> GetDistinctBrandsAsync();
        Task<ResponseDto?> GetItemsByCategory(string category);
        Task<ResponseDto?> GetItemsByBrand(string brand);
        Task<ResponseDto?> CheckStockForProduct(int id);
        Task<ResponseDto?> UpdateStockForProduct(int id, UpdateStockDto updateStockDto);
        Task<ResponseDto?> GetProducts();
    }
}
