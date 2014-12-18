using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLog.Monitor
{
    class Settings
    {
        static Settings()
        {
            Server = "mongodb://localhost";
            Collections =new List<string>();
            Collections.Add("AppName5Log");
        }
        public static string Server { get; set; }
        public static List<string> Collections { get; set; } 
       
    }
}
