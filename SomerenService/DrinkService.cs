using SomerenDAL;
using SomerenModel;
using System.Collections.Generic;

namespace SomerenService
{
    public class DrinkService
    {
        private DrinkDao drinkdb;

        public DrinkService()
        {
            drinkdb = new DrinkDao();
        }

        public List<Drink> GetDrinks()
        {
            List<Drink> drinks = drinkdb.GetAllDrinks();
            return drinks;
        }

        public void AddDrink(Drink drink)
        {
            drinkdb.AddDrink(drink);
        }

        public void DeleteDrink(Drink drink)
        {
            drinkdb.DeleteDrink(drink);
        }

        public void ModifyDrink(Drink drink)
        {
            drinkdb.ModifyDrink(drink);
        }
    }
}
