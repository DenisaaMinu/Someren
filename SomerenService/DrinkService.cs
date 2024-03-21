using SomerenDAL;
using SomerenModel;
using System.Collections.Generic;

namespace SomerenService
{
    public class DrinkService
    {
        private DrinkDAO drinkdb;

        public DrinkService()
        {
            drinkdb = new DrinkDAO();
        }

        public List<Drink> GetDrinks()
        {
            List<Drink> drinks = drinkdb.GetAllDrinks();
            return drinks;
        }
    }
}