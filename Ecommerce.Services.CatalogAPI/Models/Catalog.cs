namespace Ecommerce.Services.CatalogAPI.Models
{
    public class Catalog
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string? Descripton { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public byte[] Photo { get; set; }
    }
}
