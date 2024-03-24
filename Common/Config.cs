namespace CustomerDemoOne.Common
{
    public class Config
    {
        const string dataSource = @"localhost\SQLEXPRESS";
        const string dbName = "CustomerDemoOneDB";
        const string dbUserID = "sa";
        const string dbPassword = "sid@123456";

        public static string connectionString = @"Data Source=" + dataSource + "; Initial Catalog=" + dbName + "; User ID=" + dbUserID + "; Password=" + dbPassword + "; MultipleActiveResultSets=true; App=EntityFramework;Integrated Security=False;Encrypt=false;TrustServerCertificate=true;";

        //Page Size
        public static int pageSize = 2;

        //Date Formats
        public static string dateTimeFormat = "dd MMM yyyy hh:mm tt";
        public static string dateTimeFormatTT = "dd/MM/yyyy hh:mm tt";
        public static string dateFormat = "dd MMM yyyy";
        public static string reverseDateFormat = "yyyy MM dd";
        public static string timeFormat = "hh:mm tt";
        public static string searchDateFormat = "yyyy-MM-dd";
        public const string dateForJSFormat = "yyyy-MM-dd";
        public const string dateForJSFormatTwo = "dd-MM-yyyy";
        public const string dateForPDFPrint = "dd/MM/yyyy";
        public static string yearFormat = "yyyy";
    }
}
