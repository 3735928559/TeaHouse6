using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeaHouse.Models
{
    public class Food
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public string FoodType { get; set; }
        [StringLength(100)]
        public string Detail { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        
    }
}