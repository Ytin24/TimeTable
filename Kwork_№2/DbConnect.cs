using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwork__2
{
    public static class DbConnect
    {
        public static string HostDb { get; set; }
        public static string PortDb { get; set; }
        public static string NameDb { get; set; }
        public static string LoginDb { get; set; }
        public static string PasswordDb { get; set; }
        public static SqlConnection connection { get; set; }

    }
}
