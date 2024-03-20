﻿using System.Data.SqlClient;
using System.Data;
using SomerenModel;
using System.Collections.Generic;

namespace SomerenDAL
{
    public class DrinkDao : BaseDao
    {
        public List<Drink> GetAllDrinks()
        {
            string query = "SELECT drinkId, name, VATRate, price, stock FROM [DRINK]" +
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
                };

                if (drink.Stock > 10)
                {
                    drink.StockStatus = "Stock sufficient";
                }
                else
                {
                    drink.StockStatus = "Stock nearly depleted";
                }

                drinks.Add(drink);
            }
            return drinks;
        }

        public void AddDrink(Drink drink)
        {
            string query = "INSERT INTO [DRINK]" +
                           "VALUES (@Id, @Name, @VATRate, @Price, @Stock";
            SqlParameter[] sqlParameters =
                {
                new SqlParameter("@Id", drink.Id),
                new SqlParameter("@Name", drink.Name),
                new SqlParameter("@VATRate", drink.VATRate),
                new SqlParameter("@Price", drink.Price),
                new SqlParameter("@Stock", drink.Stock),
            };

            ExecuteEditQuery(query, sqlParameters);
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
                           "SET id = @Id, name = @Name, VATRate = @VATRate, price = @Price, stock = @Stock";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@Id", drink.Id),
                new SqlParameter("@Name", drink.Name),
                new SqlParameter("@VATRate", drink.VATRate),
                new SqlParameter("@Price", drink.Price),
                new SqlParameter("@Stock", drink.Stock)
            };

            ExecuteEditQuery(query, sqlParameters);
        }
    }
}