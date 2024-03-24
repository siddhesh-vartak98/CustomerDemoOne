namespace CustomerDemoOne.Common
{
    public class CommonFunctions
    {
        public static DateTime ConvertToIST(DateTime utcDateTime)
        {
            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, istTimeZone);
        }
    }
}
