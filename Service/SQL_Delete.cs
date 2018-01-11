using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Service
{
    public class SQL_Delete
    {
        public void Delete(string OrderId)
        {
            string sql = "delete from Sales.OrderDetails where OrderID = '"+OrderId+"'";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                sql = "delete from Sales.Orders where OrderID = '"+OrderId+"'";
                cmd = new SqlCommand(sql, conn);
                sqlAdapter = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
