using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeaHouse.Models;

namespace TeaHouse.DAL
{
    public class OrderContextInit : System.Data.Entity.DropCreateDatabaseIfModelChanges<OrderContext>
    {
        protected override void Seed(OrderContext context)
        {
            var newMenu = new List<Food>
            {
                new Food() { Name = "Cheese Cake", FoodType = "Cake", Detail = "Cheese Cake", Price = 35M },
                new Food() { Name = "Strewberry Cake", FoodType = "Cake", Detail = "Strewberry Cake", Price = 30M },
                new Food() { Name = "Strewberry Short Cake", FoodType = "Cake", Detail = "Strewberry Short Cake", Price = 30M },
                new Food() { Name = "Earl Grey Tea", FoodType = "Tea", Detail = "Earl Grey Tea", Price = 20M },
                new Food() { Name = "English Breakfast Tea", FoodType = "Tea", Detail = "English Breakfast Tea", Price = 20M }
            };
            newMenu.ForEach(f => context.FoodMenu.Add(f));
            context.SaveChanges();
            /*
            var order = new Order() { TableName = "Table1" };
            var orderdetail = new List<OrderDetail>()
            {
                new OrderDetail() { Food = newMenu[0], Quantity = 2, Order = order},
                new OrderDetail() { Food = newMenu[1], Quantity = 4, Order = order}
            };
            context.Order.Add(order);
            orderdetail.ForEach(o => context.OrderDetail.Add(o));
            context.SaveChanges();*/
        }
    }
}