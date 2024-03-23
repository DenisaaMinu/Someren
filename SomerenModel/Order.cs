using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Order
    {
        public int Id { get; set; }         // database id
        public int DrinkId { get; set; }    // drink's id
        public int StudentId { get; set; }    // student's id
        public int Amount { get; set; }     // number of drinks orderd
        public decimal Price { get; set; }  // order's price, e.g. $12.50
       
       
        public Order(int amount, decimal price)
        {
            Amount = amount;
            Price = price;
        }
    }
}
