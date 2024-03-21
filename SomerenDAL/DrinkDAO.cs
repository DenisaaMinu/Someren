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
            string query = "SELECT name, VATRate, price, stock, alcoholic  FROM [DRINK]" +
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
                    isAlcoholic: (bool)dr["alcoholic"]
                );

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
                               "VALUES (@Name, @VATRate, @Price, @Stock, @Alcoholic);" ;

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
                           "SET name = @Name, VATRate = @VATRate, price = @Price, stock = @Stock, alcoholic = @Alcoholic " +
                           "WHERE drinkId = @Id;";

            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@Id", drink.Id),
                new SqlParameter("@Name", drink.Name),
                new SqlParameter("@VATRate", drink.VATRate),
                new SqlParameter("@Price", drink.Price),
                new SqlParameter("@Stock", drink.Stock),
                new SqlParameter("@Alcoholic", drink.IsAlcoholic)
            };

            ExecuteEditQuery(query, sqlParameters);
        }
    
        public void PlaceOrder(int amount, double price, DateTime date, int drinkId, int studentId)
        {
            string query = "INSERT INTO [ORDER] " +
                           "VALUES (@amount, @price, @date, @drinkId, @studentId)";

            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@amount", amount),
                new SqlParameter("@price", price),
                new SqlParameter("@date", date),
                new SqlParameter("@drinkId", drinkId),
                new SqlParameter("@studentId", studentId)
            };

            ExecuteEditQuery(query, sqlParameters);
        }
    }
}