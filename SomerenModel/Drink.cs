using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Drink
    {
        public int Id {  get; set; }         // database id
        public string Name { get; set; }    // drink's name, e.g. wine, beer
        public decimal Price { get; set; }  // drink's price, e.g. $2.50
        public decimal VATRate { get; set; }  // alcoholic drink: 0.21, non-alcoholic drink: 0.09
        public int Stock {  get; set; }    // number of drinks, e.g. wine: 120 
        public string StockStatus {  get; set; }  // > 40: "Stock sufficient", < 40: "Nearly depleted"

        public bool IsAlcoholic { get; set; }  // alcoholic = true, non-alcoholic = false
   
        public Drink(string name, decimal price, decimal vatRate, int stock, bool isAlcoholic)
        {
            Name = name;
            Price = price;
            VATRate = vatRate; 
            Stock = stock;
            IsAlcoholic = isAlcoholic;
        }
    }
}
