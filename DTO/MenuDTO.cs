using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeaHouse.DTO
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Detail { get; set; }
        public decimal Price { get; set; }
    }
}