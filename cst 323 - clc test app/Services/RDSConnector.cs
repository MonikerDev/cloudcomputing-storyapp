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
            string username = "gothamcity";
            string password = "Morgenstern445";
            string hostname = "awseb-e-i7ya9s27dy-stack-awsebrdsdatabase-m6cucfumckxb.criomeosk4tn.us-east-1.rds.amazonaws.com";
            string port = "3306";

            return "Data Source=" + hostname + ";Port=" + port + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }
    }
}
