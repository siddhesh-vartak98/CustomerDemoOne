using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CustomerDemoOne.Common
{
    public class CommonFunctions
    {
        public static DateTime ConvertToIST(DateTime utcDateTime)
        {
            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, istTimeZone);
        }

        public static string getExceptionMessage(Exception ex)
        {
            string message = string.Empty;

            switch (ex)
            {
                case SqlException sqlExp:
                    message = sqlExp.Message;
                    break;

                case ArgumentNullException argNullEx:
                    message = argNullEx.Message;
                    break;

                case ArgumentOutOfRangeException argOutOfRangeEx:
                    message = argOutOfRangeEx.Message;
                    break;

                case ArgumentException argExp:
                    message = argExp.Message;
                    break;

                case InvalidOperationException invalidOpEx:
                    message = invalidOpEx.Message;
                    break;

                case FileNotFoundException fileNotFdEx:
                    message = fileNotFdEx.Message;
                    break;

                case IOException ioEx:
                    message = ioEx.Message;
                    break;

                case ValidationException validtnEx:
                    message = validtnEx.Message;
                    break;

                case DbUpdateException dbUpdateEx:
                    message = dbUpdateEx.Message;
                    break;

                case TimeoutException timeoutEx:
                    message = timeoutEx.Message;
                    break;

                case HttpRequestException httpReqEx:
                    message = httpReqEx.Message;
                    break;

                default:
                    message = ex.Message;
                    break;

            }

            return message;
        }
    }
}
