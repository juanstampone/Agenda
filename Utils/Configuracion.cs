using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Configuracion
    {
        private const string Server = "localhost\\SQLEXPRESS";
        private const string DBName = "agenda";
        public static string GetConnectionString()
        {
            return string.Concat(
                "Data Source=",
                Server,
                ";",
                "Initial Catalog=",
                DBName,
                ";Integrated Security=true;"
            );
        }
    }
}
