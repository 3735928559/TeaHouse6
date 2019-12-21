using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeaHouse.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public IEnumerable<Detail> Details { get; set; }
        public class Detail
        {
            public int MenuID { get; set; }
            public string MenuItem { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
        
    }
}