using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.ConsoleApp
{
    public class AppSettings
    {
        public LoginCredentialsConfiguration? LoginCredentials { get; set; }
        public DatabaseConfiguration? DataBase { get; set; }
        public GeneralConfiguration? General { get; set; }
    }

    //
    public class LoginCredentialsConfiguration
    {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? TenantId { get; set; }
    }

    //
    public class DatabaseConfiguration
    {
        public string? PathToDatabases { get; set; }
    }

    //
    public class GeneralConfiguration
    {
        public string? PathToLogFiles { get; set; }
    }

}
