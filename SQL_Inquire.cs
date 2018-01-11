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
    public class SQL_Inquire
    {
        public List<Model.Data> GetData(Model.Data Data)
        {
            string sql = "Select * ";
            string Where = "Where";
            if (Data.OrderId != null)
            {
                Where += " a.OrderID = " + Data.OrderId;
            }
            if (Data.CustName != null)
            {
                Where += " and CustName = 'Customer IBVRG'";
            }
            if (!Data.EmpName.Equals("null"))
            {
                Where += " and a.EmployeeID = " + Data.EmpName;
            }
            if (!Data.CpyName.Equals("null"))
            {
                Where += " and d.ShipperID = " + Data.CpyName;
            }
            if (Data.OrderDate != null)
            {
                Where += " and OrderDate = '" + Data.OrderDate;

            }
            if (Data.RequiredDate != null)
            {
                Where += " and RequiredDate = '" + Data.RequiredDate;
            }
            if (Data.ShippedDate != null)
            {
                Where += " and ShippedDate = '" + Data.ShippedDate.Substring(0, 10) + "'";
            }
            if (Where.Equals("Where "))
            {
                Where = "";
            }
            if((Where.IndexOf("Where and")) > -1)
            {
                Where = Where.Replace("Where and", "Where ");
            }

            DataTable dt = new DataTable();
            sql = sql + "from Sales.Orders a inner join Sales.Customers b " +
                                  "on b.CustomerID = a.CustomerID " +
                                  "inner join HR.Employees c " +
                                  "on c.EmployeeID = a.EmployeeID " +
                                  "inner join Sales.Shippers d " +
                                  "on d.ShipperID = a.ShipperID " +
                                  Where;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            List<Model.Data> AddData = new List<Model.Data>();
            AddData = SearchData(dt);            
            return AddData;
        }
        private List<Model.Data> SearchData(DataTable dt)
        {
            List<Model.Data> result = new List<Model.Data>();
            foreach (DataRow row in dt.Rows)
            {
                if (row["ShippedDate"].ToString() == "")
                {
                    result.Add(
                        new Model.Data
                        {
                            OrderId = row["OrderID"].ToString(),
                            CustName = row["CustName"].ToString(),
                            EmpName = row["LastName"].ToString() + row["FirstName"].ToString(),
                            CpyName = row["CpyName"].ToString(),

                            OrderDate = Convert.ToDateTime(row["OrderDate"]).ToString("yyyy-MM-dd"),
                            RequiredDate = Convert.ToDateTime(row["RequiredDate"]).ToString("yyyy-MM-dd"),
                            ShippedDate = "無",

                            ShipAddress = row["ShipAddress"].ToString(),
                            ShipCity = row["ShipCity"].ToString(),
                            ShipCountry = row["ShipCountry"].ToString(),
                            ShipPostalCode = row["ShipPostalCode"].ToString(),
                            ShipRegion = row["ShipRegion"].ToString()
                        });
                }
                else if (row["ShipRegion"].ToString() == "")
                {
                    result.Add(
                        new Model.Data
                        {
                            OrderId = row["OrderID"].ToString(),
                            CustName = row["CustName"].ToString(),
                            EmpName = row["LastName"].ToString() + row["FirstName"].ToString(),
                            CpyName = row["CpyName"].ToString(),

                            OrderDate = Convert.ToDateTime(row["OrderDate"]).ToString("yyyy-MM-dd"),
                            RequiredDate = Convert.ToDateTime(row["RequiredDate"]).ToString("yyyy-MM-dd"),
                            ShippedDate = Convert.ToDateTime(row["ShippedDate"]).ToString("yyyy-MM-dd"),

                            ShipAddress = row["ShipAddress"].ToString(),
                            ShipCity = row["ShipCity"].ToString(),
                            ShipCountry = row["ShipCountry"].ToString(),
                            ShipPostalCode = row["ShipPostalCode"].ToString(),
                            ShipRegion = "無"
                        });
                }else
                result.Add(
                    new Model.Data
                    {
                        OrderId = row["OrderID"].ToString(),
                        CustName = row["CustName"].ToString(),
                        EmpName = row["LastName"].ToString() + row["FirstName"].ToString(),
                        CpyName = row["CpyName"].ToString(),

                        OrderDate = Convert.ToDateTime(row["OrderDate"]).ToString("yyyy-MM-dd"),
                        RequiredDate = Convert.ToDateTime(row["RequiredDate"]).ToString("yyyy-MM-dd"),
                        ShippedDate = Convert.ToDateTime(row["ShippedDate"]).ToString("yyyy-MM-dd"),

                        ShipAddress = row["ShipAddress"].ToString(),
                        ShipCity = row["ShipCity"].ToString(),
                        ShipCountry = row["ShipCountry"].ToString(),
                        ShipPostalCode = row["ShipPostalCode"].ToString(),
                        ShipRegion = row["ShipRegion"].ToString()
                    });
            }
            return result;
        }
    }
}
