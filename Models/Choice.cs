using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TeaHouse.Models
{
    
   public class Choice
    {
        public int Id { get; set; }

        private string _user = System.Web.HttpContext.Current.User.Identity.Name;

        public string User { get{ return _user; } set { _user = value; } }
        private DateTime _date = DateTime.Now;
        public DateTime OrderTime
        {
            get { return _date; }
            set { _date = value; }
        }
        public virtual Food SelectedFood { get; set; }
        
        public string Status { get;set; }
        public int OrderNum { get; set; }
    }
}