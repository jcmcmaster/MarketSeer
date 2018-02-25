using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSeer
{
    public static class ItemFactory
    {
        public static int GetItemID(string itemName)
        {
            using (SqlCommand cmd = DBUtils.GetCommand())
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@itemName", SqlDbType.VarChar) { Value = itemName });
                cmd.CommandText = @"
                    select top 1 typeID
                      from invTypes
                     where typeName = @itemName
                    ;
                ";

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
