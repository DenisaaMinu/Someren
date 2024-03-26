using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenService
{
    public class OrderService
    {
        private OrderDao orderdb;

        public OrderService()
        {
            orderdb = new OrderDao();
        }

        public void PlaceOrder(Order order)
        {
            orderdb.PlaceOrder(order.DrinkId, order.StudentId, order.Amount, (double)order.Price, order.Date);
        }

        public void UpdateDrinkSupplies(Order order)
        {
            orderdb.UpdateDrinkSupplies(order.DrinkId, order.Amount);
        }

        public int GetTotalDrinksSold(DateTime startDate, DateTime endDate)
        {
            return orderdb.GetTotalDrinksSold(startDate, endDate);
        }

        public decimal GetTurnover(DateTime startDate, DateTime endDate)
        {
            return orderdb.GetTurnover(startDate, endDate);
        }

        public int GetNumberOfCustomers(DateTime startDate, DateTime endDate)
        {
            return orderdb.GetNumberOfCustomers(startDate, endDate);
        }
    }
}
