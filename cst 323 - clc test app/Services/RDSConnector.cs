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
            string dbname = "ebdb";
            string username = "monikerdev";
            string password = "peepeepoopoo";
            string hostname = "awseb-e-3hbvunhemt-stack-awsebrdsdatabase-edy39oafwuj1.cvsy6q668z9t.us-east-1.rds.amazonaws.com";
            string port = "3306";

            return "Data Source=" + hostname + ";Port=" + port + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }
    }
}
