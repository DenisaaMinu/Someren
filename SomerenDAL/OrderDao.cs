using SomerenModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace SomerenDAL
{
    public class OrderDao : BaseDao
    {
        public List<Order> GetAllOrders()
        {
            string query = "SELECT orderId, drinkId, studentId, amount, price  " +
                           "FROM [ORDER]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Order> ReadTables(DataTable dataTable)
        {
            List<Order> orders = new List<Order>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Order order = new Order()
                {
                    Id = (int)dr["orderId"],
                    DrinkId = (int)dr["drinkId"],
                    StudentId = (int)dr["studentId"],
                    Amount = (int)dr["amount"],
                    Price = (decimal)dr["price"],
                    Date = (DateTime)dr["date"]
                };
            }

            return orders;
        }

        public void PlaceOrder(int drinkId, int studentId, int amount, double price, DateTime date)
        {
            string query = "INSERT INTO [ORDER] (drinkId, studentId, amount, price, date) " +
                           "VALUES (@DrinkId, @StudentId, @Amount, @Price, @Date)";

            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@DrinkId", drinkId),
                new SqlParameter("@StudentId", studentId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@Price", price),
                new SqlParameter("@Date", date)
            };

            ExecuteEditQuery(query, sqlParameters);
        }

        public void UpdateDrinkSupplies(int drinkId, int amount)
        {
            string query = "UPDATE [DRINK] SET numberOfDrinksSold = @amount " +
                           "WHERE drinkId = @drinkId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@drinkId", drinkId),
                new SqlParameter("@amount", amount)
            };

            ExecuteEditQuery(query, parameters);
        }

        public RevenueReport GetRevenueReport(DateTime startDate, DateTime endDate)
        {
            string query = "SELECT SUM(amount) AS Sales, SUM(amount * price), COUNT(DISTINCT StudentId) " +
                           "FROM[ORDER] AS O JOIN DRINK AS D ON O.drinkId = D.drinkId WHERE CAST([date] AS DATE) BETWEEN @StartDate AND @EndDate;";

            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };

            DataTable resultTable = ExecuteSelectQuery(query, parameters);
            RevenueReport revenueReport = new RevenueReport();

            if (resultTable.Rows.Count > 0 && resultTable.Rows[0][0] != DBNull.Value)
            {
                revenueReport.Sales =  Convert.ToInt32(resultTable.Rows[0][0]);
                revenueReport.Turnover = Convert.ToInt32(resultTable.Rows[0][1]);
                revenueReport.NumberOfCustomers = Convert.ToInt32(resultTable.Rows[0][2]);

                return revenueReport;
            }

            return null;
        }
    }
}
