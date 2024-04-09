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
            string dbname = "cc-clc";
            string username = "moniker";
            string password = "Dickcock69";
            string hostname = "saclc.mysql.database.azure.com";
            string port = "3306";

            return "Data Source=" + hostname + ";Port=" + port + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }
    }
}
