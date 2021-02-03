using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSIGNMENT3.DBHandler
{
    /// <summary>
    /// Base class Database Handling
    /// </summary>
    public class BaseDBHandler
    {
        protected const string connectionStr = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=blogland;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        protected SqlConnection con;
        protected SqlCommand cmd;
        protected SqlDataReader dr;

        public BaseDBHandler()
        {
            con = new SqlConnection(connectionStr);
        }
    }
}


// CREATE DATABASE blogland