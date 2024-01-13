using Ecommerce.Services.CatalogAPI.Models;

namespace Ecommerce.Services.CatalogAPI.Data
{
    // Dummy data initilizer !
    public class CatalogDbInit
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            SeedData(scope.ServiceProvider.GetService<CatalogDbContext>());
        }

        private static void SeedData(CatalogDbContext? context)
        {
            if (context.tb_catalog.Any())
            {
                Console.WriteLine("Already have data for test");
                return;
            }

            //TODO: Image paths should be changed
            var catalogItems = new List<Catalog>
            {
                new Catalog
                {
                    productid = 1,
                    name = "Ocean Extramag",
                    category = "Mineral",
                    brand = "Orzax Ocean",
                    description = "Ocean ExtraMag’ın her tabletinde magnezyum malat, magnezyum bisglisinat ve magnezyum sitrattan gelen 200 mg elementel magnezyum bulunur. Renklendirici içermez. ÜRÜN İÇERİĞİ: Magnezyum 200 mg MENŞEİ: Türkiye",
                    stock = 10,
                    price = 335.00M,
                    image = ".\\Ecommerce.Web.UI\\wwwroot\\img\\shop_01.jpg"
                },
                new Catalog
                {
                    productid = 2,
                    name = "Magnezyum Bisglisinat",
                    category = "Mineral",
                    description = "VeNatura Magnezyum Bisglisinat, Sitrat ve P-5-P (Vitamin B6)Takviye Edici Gıda 60 Kapsül Her yeni gün, yeni bir başlangıçtır. İçindekiler: Magnezyum bisglisinat, Sitrik asidin magnezyum tuzları, Kapsül kabuğu (Hidroksipropil metil selüloz), Kıvam arttırıcı (Yağ asitlerinin magnezyum tuzları), Piridoksal 5?-fosfat Net Miktar: 60 Kapsül Menşe Ülke: Türkiye (Magnezyum’un menşe ülkesi Almanya, Vitamin B6’nın menşe ülkesi Japonya’dır.) Takviye Edici Gıda Onay Numarası: 010779-26.05.2021 Tavsiye Edilen Günlük Alım Dozu: Günde 1 kapsül alınması önerilir. 11 yaş ve üzeri bireylerin kullanımına yöneliktir. Saklama Koşulları: 25°C’nin altında serin ve kuru yerlerde muhafaza ediniz. Etken Madde Miktar (1 kapsül) % BRD Magnezyum* 140 mg 37,33 Vitamin B6 (piridoksin) 5 mg 357,14 *475 mg magnezyum bisglisinattan gelen 95 mg elementel magnezyum ve 300 mg magnezyum sitrattan gelen 45 mg elementel magnezyum.",
                    brand = "Venatura",
                    price = 145.50M,
                    stock = 100,
                    image = ".\\Ecommerce.Web.UI\\wwwroot\\img\\shop_02.jpg"
                },
                new Catalog
                {
                    productid = 3,
                    name = "Ocean D3k2 Damla",
                    category = "Vitamin",
                    description = "Ocean d3 k2 damla 20 ml Genel uyarı: Sağlık bakanlığına bağlı Türkiye İlaç ve Tıbbi Cihaz Kurumunun aldığı karar gereği, İnternet üzerinden satışı yapılan ürünlere ilişkin reklam ilan ve açıklamalarda endikasyon belirtilmesi yasaklanmıştır.İnternet Sitemiz üzerinden satılan ürünlerle ilgili özellikle tedavi edilmesi gereken rahatsızlıkları tedavi ettiği ya da tedavisine yardımcı olduğu ve/veya ilaç niteliğinde olduğu şeklinde beyanlara ve ürün sayfalarında ürün açıklamalarına yer verilmemektedir. İlginiz ve anlayışınız için teşekkür ederiz.",
                    brand = "Orzax Ocean",
                    price = 195.99M,
                    stock = 105,
                    image = ".\\Ecommerce.Web.UI\\wwwroot\\img\\shop_03.jpg"
                },
                new Catalog
                {
                    productid = 4,
                    name = "Venatura - Vitamin D3 K2",
                    category = "Vitamin",
                    description = "Venatura Vitamin D3 K2 20 ml Damla Takviye edici gıda Kullanım Şekli: Günde bir defa 1 damla kullanılır. Ürün Bileşimi: İçindekiler:Zeytinyağı, Menaquinon 7 karışımı (Zeytinyağı ve Menaquinon 7), Kolekasiferol, Antioksidan:Tokoferolce zengin ekstrakt u200b Etken madde Miktar (1 damlada) % BRDVitamin D 25 u00b5g (1000 IU) 500Vitamin K 20 u00b5g 26,66",
                    brand = "Venatura",
                    price = 218.20M,
                    stock = 200,
                    image = ".\\Ecommerce.Web.UI\\wwwroot\\img\\shop_04.jpg"
                },
                new Catalog
                {
                    productid = 5,
                    name = "Solgar Vitamin D3",
                    category = "Vitamin",
                    description = "Solgar vitamin d3 formunda d vitamini içeren gıda takviyesidir. Vücutta üretilebildiği gibi gün ışığı ve besinler aracılığıyla da elde edilebilen d vitamini, yağda çözünebilen bir vitamin türüdür. Solgar d3 vitamini (kolekalsiferol) kaynağını balık karaciğer yağından elde etmektedir.solgar vitamin d3 1000 ıu 100 kapsülün özellikleri:- servis başı 25 mch (1000 ıu) d3 vitamini - cam şişe içerisinde 100 kapsül bulunmaktadır - kapsül kaynağı: Sığır jelatinisolgar d vitamini 1000 ıu 100 adet kullanımı-Günde 1 softjel, tercihen yemeklerden sonra kullanılması önerilmektedir.",
                    brand = "Solgar",
                    price = 335.01M,
                    stock = 5,
                    image = ".\\Ecommerce.Web.UI\\wwwroot\\img\\shop_05.jpg"
                }
            };

            //'tb_catalog' is the database context
            context.tb_catalog.AddRange(catalogItems);
            context.SaveChanges();
        }
    }
}