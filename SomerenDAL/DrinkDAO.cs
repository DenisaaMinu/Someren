using System.Data.SqlClient;
using System.Data;
using SomerenModel;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SomerenDAL
{
    public class DrinkDao : BaseDao
    {
        public List<Drink> GetAllDrinks()
        {
            string query = "SELECT drinkId, name, VATRate, price, stock, alcoholic  FROM [DRINK]" +
                           "ORDER BY name";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Drink> ReadTables(DataTable dataTable)
        {
            List<Drink> drinks = new List<Drink>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Drink drink = new Drink(
                    name: dr["name"].ToString(),
                    vatRate: (decimal)dr["VATRate"],
                    price: (decimal)dr["price"],
                    stock: (int)dr["stock"],
                    isAlcoholic: (bool)dr["alcoholic"]);

                drink.Id = (int)dr["drinkId"];

                if (drink.Stock > 10)
                    drink.StockStatus = "Stock sufficient";
                else
                    drink.StockStatus = "Stock nearly depleted";

                if (drink.VATRate == 0.21m)
                    drink.IsAlcoholic = true;
                else
                    drink.IsAlcoholic = false;

                drinks.Add(drink);
            }

            return drinks;
        }

        public Drink AddDrink(Drink drink)
        {
            try
            {
                string query = "INSERT INTO [DRINK] (name, VATRate, price, stock, alcoholic)" +
                               "VALUES (@Name, @VATRate, @Price, @Stock, @Alcoholic);";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Name", drink.Name),
                    new SqlParameter("@VATRate", drink.VATRate),
                    new SqlParameter("@Price", drink.Price),
                    new SqlParameter("@Stock", drink.Stock),
                    new SqlParameter("@Alcoholic", drink.IsAlcoholic)
                };

                ExecuteEditQuery(query, parameters);
                return drink;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteDrink(Drink drink)
        {
            string query = "DELETE FROM [DRINK] " +
                           "WHERE drinkId = @Id";

            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@Id", drink.Id)
            };

            ExecuteEditQuery(query, sqlParameters);
        }

        public void ModifyDrink(Drink drink)
        {
            string query = "UPDATE [DRINK] " +
                           "SET [name] = @Name, VATRate = @VATRate, price = @Price, stock = @Stock, alcoholic = @Alcoholic " +
                           "WHERE drinkId = @Id;";

            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@Name", drink.Name),
                new SqlParameter("@VATRate", drink.VATRate),
                new SqlParameter("@Price", drink.Price),
                new SqlParameter("@Stock", drink.Stock),
                new SqlParameter("@Alcoholic", drink.IsAlcoholic),
                new SqlParameter("@Id", drink.Id)
            };

            ExecuteEditQuery(query, sqlParameters);
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

        public int GetTotalDrinksSold(DateTime startDate, DateTime endDate)
        {
            string query = "SELECT SUM(amount) " +
                           "FROM [ORDER] " +
                           "WHERE [date] BETWEEN @StartDate AND @EndDate;";

            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };

            DataTable resultTable = ExecuteSelectQuery(query, parameters);

            if (resultTable.Rows.Count > 0 && resultTable.Rows[0][0] != DBNull.Value)
            {
                int numberOfDrinksSold = Convert.ToInt32(resultTable.Rows[0][0] );
                return numberOfDrinksSold;
            }

            return 0;
        }

        public decimal GetTurnover(DateTime startDate, DateTime endDate)
        {
            string query = "SELECT SUM(amount * price) " +
                           "FROM [ORDER] " +
                           "WHERE [date] BETWEEN @StartDate AND @EndDate;";

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
            string query = "SELECT COUNT(DISTINCT studentId) " +
                           "FROM [ORDER] " +
                           "WHERE [date] BETWEEN @startDate AND @endDate";

            SqlParameter[] parameters =
            {
                new SqlParameter("@startdate", startDate),
                new SqlParameter("@endDate", endDate)
            };

            DataTable resultTable = ExecuteSelectQuery(query, parameters);

            if(resultTable.Rows.Count > 0 && resultTable.Rows[0][0] != DBNull.Value)
            {
                int numberOfCustomers = Convert.ToInt32(resultTable.Rows[0][0]);
                return numberOfCustomers;
            }

            return 0;
        }
    }
}