using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeaHouse.Models
{
    public class OrderModels
    {
        [Key]
        public int Id { get; set; }

        private string _user = System.Web.HttpContext.Current.User.Identity.Name;

        public string User { get { return _user; } set { _user = value; } }

        private DateTime _date = DateTime.Now;
        public DateTime OrderTime
        {
            get { return _date; }
            set { _date = value; }
        }
        public virtual Choice SelectedChoice { get; set; }
        
        

       

    }
}