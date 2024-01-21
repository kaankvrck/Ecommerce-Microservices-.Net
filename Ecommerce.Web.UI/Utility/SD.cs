namespace Ecommerce.Web.UI.Utility
{
    public class SD
    {
        public static string CustomerAPIBase { get; set; }
        public static string OrderAPIBase { get; set; }
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public const string TokenCookie = "JWTToken";
        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
    }
}
