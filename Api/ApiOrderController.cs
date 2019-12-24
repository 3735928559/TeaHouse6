using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TeaHouse.Models;

namespace TeaHouse.Api
{
    public class ApiOrderController : ApiController
    {
        private OrderContext db = new OrderContext();

        // GET: api/ApiOrder
        public IQueryable<OrderModels> GetOrderModels()
        {
            return db.OrderModels;
        }

        // GET: api/ApiOrder/5
        [ResponseType(typeof(OrderModels))]
        public IHttpActionResult GetOrderModels(int id)
        {
            OrderModels orderModels = db.OrderModels.Find(id);
            if (orderModels == null)
            {
                return NotFound();
            }

            return Ok(orderModels);
        }

        // PUT: api/ApiOrder/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderModels(int id, OrderModels orderModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderModels.Id)
            {
                return BadRequest();
            }

            db.Entry(orderModels).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderModelsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ApiOrder
        [ResponseType(typeof(OrderModels))]
        public IHttpActionResult PostOrderModels(OrderModels orderModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderModels.Add(orderModels);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orderModels.Id }, orderModels);
        }

        // DELETE: api/ApiOrder/5
        [ResponseType(typeof(OrderModels))]
        public IHttpActionResult DeleteOrderModels(int id)
        {
            OrderModels orderModels = db.OrderModels.Find(id);
            if (orderModels == null)
            {
                return NotFound();
            }

            db.OrderModels.Remove(orderModels);
            db.SaveChanges();

            return Ok(orderModels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderModelsExists(int id)
        {
            return db.OrderModels.Count(e => e.Id == id) > 0;
        }
    }
}