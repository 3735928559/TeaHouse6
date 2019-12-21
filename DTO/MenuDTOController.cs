using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeaHouse.Models;

namespace TeaHouse.DTO
{
    public class MenuDTOController : ApiController
    {
        private OrderContext db = new OrderContext();

        
        private IQueryable<MenuDTO> MapProducts()
        {
            return from f in db.FoodMenu
                   select new MenuDTO()
                   { Id = f.Id, Name = f.Name, Type=f.FoodType, Detail=f.Detail, Price = f.Price };
        }

        public IEnumerable<MenuDTO> GetProducts()
        {
            return MapProducts().AsEnumerable();
        }

        public MenuDTO GetProduct(int id)
        {
            var food = (from f in MapProducts()
                           where f.Id == 1
                           select f).FirstOrDefault();
            if (food == null)
            {
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return food;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

