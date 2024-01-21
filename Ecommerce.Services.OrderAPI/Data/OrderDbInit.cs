using Ecommerce.Services.OrderAPI.Common;
using Ecommerce.Services.OrderAPI.Models;

namespace Ecommerce.Services.OrderAPI.Data
{
    // Dummy data initilizer !!!!
    public class OrderDbInit
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            SeedData(scope.ServiceProvider.GetService<OrderDbContext>());
            SeedOrderStatus(scope.ServiceProvider.GetService<OrderDbContext>());
        }

        private static void SeedData(OrderDbContext? context)
        {
            if (context.tb_order.Any())
            {
                Console.WriteLine("Already have data for test");
                return;
            }

            var orders = new List<Order>()
            {
                new Order()
                {
                    customerid = "1",
                    name = "John",
                    phonenumber = "1234567890",
                    email = "1234567890@ege.edu",
                    address = "Istanbul TR 34000",
                    totalprice = 1452.54m,
                    statusid = 0,
                    createddate = DateTime.SpecifyKind(TimeHelper.GetCurrentTurkeyTime(), DateTimeKind.Utc),
                    createdby = "1"
                },
                new Order()
                {
                    customerid = "2",
                    name = "Tom",
                    phonenumber = "1234562890",
                    email = "1265567890@ege.edu",
                    address = "Izmir TR 34000",
                    totalprice = 1982,
                    statusid = 0,
                    createddate = DateTime.SpecifyKind(TimeHelper.GetCurrentTurkeyTime(), DateTimeKind.Utc),
                    createdby = "2"
                }
            };

            context.tb_order.AddRange(orders);
            context.SaveChanges();
        }

        private static void SeedOrderStatus(OrderDbContext? context)
        {
            if (context.tb_order_status.Any())
            {
                Console.WriteLine("Already have order status data");
                return;
            }

            var orderStatus = new List<OrderStatus>()
            {
                new OrderStatus()
                {
                    id = 1,
                    description =  "Oluşturuldu",
                    createddate = DateTime.SpecifyKind(TimeHelper.GetCurrentTurkeyTime(), DateTimeKind.Utc),
                    createdby = 1,
                    isdeleted = false
                },
                new OrderStatus()
                {
                    id = 2,
                    description =  "Hazırlanıyor",
                    createddate = DateTime.SpecifyKind(TimeHelper.GetCurrentTurkeyTime(), DateTimeKind.Utc),
                    createdby = 1,
                    isdeleted = false
                },
                new OrderStatus()
                {
                    id = 3,
                    description =  "Tamamlandı",
                    createddate = DateTime.SpecifyKind(TimeHelper.GetCurrentTurkeyTime(), DateTimeKind.Utc),
                    createdby = 1,
                    isdeleted = false
                },
                new OrderStatus()
                {
                    id = 4,
                    description =  "İptal Edildi",
                    createddate = DateTime.SpecifyKind(TimeHelper.GetCurrentTurkeyTime(), DateTimeKind.Utc),
                    createdby = 1,
                    isdeleted = false
                }
            };
            context.tb_order_status.AddRange(orderStatus);
            context.SaveChanges();
        }

    }
}
