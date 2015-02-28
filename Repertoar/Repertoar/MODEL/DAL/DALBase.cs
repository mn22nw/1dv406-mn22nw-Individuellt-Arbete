using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Repertoar.MODEL;
using System.Data.SqlClient;

namespace Repertoar.MODEL.DAL
{
    public abstract class DALBase
    {
        private static readonly string _connectionString;

        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        static DALBase()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["WP13_mn22nw_IAConnectionString"].ConnectionString;
        }
    }
}