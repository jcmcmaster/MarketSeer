using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSeer
{
    public static class DBUtils
    {
        public static SqlCommand GetCommand()
        {
            SqlCommand cmd = new SqlCommand()
            {
                Connection = new SqlConnection(@"Data Source=DESKTOP-2N1GADR\SQLEXPRESS01; Initial Catalog=eve; Integrated Security=True;")
            };

            cmd.Connection.Open();

            return cmd;
        }
    }
}
