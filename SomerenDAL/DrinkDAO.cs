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
        protected const decimal VATRateAlcohol = 0.21m;
        protected int SufficientStock = 10;

        public List<Drink> GetAllDrinks()
        {
            string query = "SELECT drinkId, name, VATRate, price, stock, alcoholic, numberOfDrinksSold  FROM [DRINK]" +
                           "ORDER BY name";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Drink> ReadTables(DataTable dataTable)
        {
            List<Drink> drinks = new List<Drink>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Drink drink = new Drink()
                {
                    Id = (int)dr["drinkId"],
                    Name = dr["name"].ToString(),
                    VATRate = (decimal)dr["VATRate"],
                    Price = (decimal)dr["price"],
                    Stock = (int)dr["stock"],
                    IsAlcoholic = (bool)dr["alcoholic"],
                    NumberOfDrinksSold = (int)dr["numberOfDrinksSold"]
                };

                if (drink.Stock > SufficientStock)
                    drink.StockStatus = "Stock sufficient";
                else
                    drink.StockStatus = "Stock nearly depleted";

                if (drink.VATRate == VATRateAlcohol)
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
                string query = "INSERT INTO [DRINK] (name, VATRate, price, stock, alcoholic, numberOfDrinksSold)" +
                               "VALUES (@Name, @VATRate, @Price, @Stock, @Alcoholic, @NumberOfDrinksSold);";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Name", drink.Name),
                    new SqlParameter("@VATRate", drink.VATRate),
                    new SqlParameter("@Price", drink.Price),
                    new SqlParameter("@Stock", drink.Stock),
                    new SqlParameter("@Alcoholic", drink.IsAlcoholic),
                    new SqlParameter("@NumberOfDrinksSold", drink.NumberOfDrinksSold)
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
    }
}