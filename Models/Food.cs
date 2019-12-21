using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeaHouse.Models
{
    public class Food
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string FoodType { get; set; }
        public string Detail { get; set; }
        public decimal Price { get; set; }
    }
}