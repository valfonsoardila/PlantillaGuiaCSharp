using System.Configuration;

namespace YouGym
{
    public static class ConfigConnection
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        public static string ProviderName = ConfigurationManager.ConnectionStrings["conexion"].ProviderName;
    }
}
