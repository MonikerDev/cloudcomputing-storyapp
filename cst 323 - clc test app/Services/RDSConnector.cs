using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace cst_323___clc_test_app.Services
{
    public class RDSConnector
    {
        public static string GetRDSConnectionString()
        {
            // Hardcoded connection details
            string dbname = "";
            string username = "";
            string password = "";
            string hostname = "";
            string port = "";

            return "Data Source=" + hostname + ";Port=" + port + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }
    }
}
