namespace Ecommerce.Services.OrderAPI.Common
{
    public static class TimeHelper
    {
        private static readonly TimeZoneInfo TurkeyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Istanbul");

        public static DateTime GetCurrentTurkeyTime()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TurkeyTimeZone);
        }
    }
}
