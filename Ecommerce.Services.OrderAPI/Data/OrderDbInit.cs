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
                    name = "John",
                    surname = "Nhoj",
                    phonenumber = "1234567890",
                    email = "1234567890@ege.edu",
                    address = "Istanbul TR 34000",
                    totalprice = 1452.54m,
                    statusid = 0,
                    createddate = TimeHelper.GetCurrentTurkeyTime(),
                    createdby = 1
                },
                new Order()
                {
                    name = "Tom",
                    surname = "Mot",
                    phonenumber = "1234562890",
                    email = "1265567890@ege.edu",
                    address = "Izmir TR 34000",
                    totalprice = 1982,
                    statusid = 0,
                    createddate = TimeHelper.GetCurrentTurkeyTime(),
                    createdby = 1
                }
            };

            context.tb_order.AddRange(orders);
            context.SaveChanges();



        }
    }
}
