using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSeer
{
    public static class UniverseFactory
    {
        public static int GetRegionID(string regionName)
        {
            using (SqlCommand cmd = DBUtils.GetCommand())
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@regionName", SqlDbType.VarChar) { Value = regionName });
                cmd.CommandText = @"
                    select top 1 regionID
                      from mapRegions
                     where regionName LIKE '%' + @regionName + '%'
                    ;
                ";

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static string GetStationName(long locationID)
        {
            using (SqlCommand cmd = DBUtils.GetCommand())
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@locationID", SqlDbType.BigInt) { Value = Convert.ToInt64(locationID) });
                cmd.CommandText = @"
                    select top 1 stationName
                      from staStations
                     where stationID = @locationID
                    ;
                ";

                var result = cmd.ExecuteScalar();

                if (result != null)
                    return result.ToString();

                return "STATION NOT FOUND";
            }
        }
    }
}
