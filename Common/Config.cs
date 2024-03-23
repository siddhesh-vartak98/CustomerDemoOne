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
    }
}
