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

        public int GetTotalDrinksSold(DateTime startDate, DateTime endDate)
        {
            string query = "SELECT SUM(amount) FROM [ORDER] " +
                           "WHERE CAST([date] AS DATE) BETWEEN @StartDate AND @EndDate;";

            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };

            DataTable resultTable = ExecuteSelectQuery(query, parameters);

            if (resultTable.Rows.Count > 0 && resultTable.Rows[0][0] != DBNull.Value)
            {
                int numberOfDrinksSold = Convert.ToInt32(resultTable.Rows[0][0]);
                return numberOfDrinksSold;
            }

            return 0;
        }

        public decimal GetTurnover(DateTime startDate, DateTime endDate)
        {
            string query = "SELECT SUM(amount * price) FROM [ORDER] " +
                           "WHERE CAST([date] AS DATE) BETWEEN @StartDate AND @EndDate;";

            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };

            DataTable resultTable = ExecuteSelectQuery(query, parameters);

            if (resultTable.Rows.Count > 0 && resultTable.Rows[0][0] != DBNull.Value)
            {
                decimal totalTurnover = Convert.ToInt32(resultTable.Rows[0][0]);
                return totalTurnover;
            }

            return 0;
        }

        public int GetNumberOfCustomers(DateTime startDate, DateTime endDate)
        {
            string query = "SELECT COUNT(DISTINCT studentId) FROM [ORDER] " +
                           "WHERE CAST([date] AS DATE) BETWEEN @startDate AND @endDate";

            SqlParameter[] parameters =
            {
                new SqlParameter("@startdate", startDate),
                new SqlParameter("@endDate", endDate)
            };

            DataTable resultTable = ExecuteSelectQuery(query, parameters);

            if (resultTable.Rows.Count > 0 && resultTable.Rows[0][0] != DBNull.Value)
            {
                int numberOfCustomers = Convert.ToInt32(resultTable.Rows[0][0]);
                return numberOfCustomers;
            }

            return 0;
        }
    }
}
