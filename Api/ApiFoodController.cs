﻿using System;
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
    public class ApiFoodController : ApiController
    {
        private OrderContext db = new OrderContext();

        // GET: api/ApiFood
        public IQueryable<Food> GetFoodMenu()
        {
            return db.FoodMenu;
        }

        // GET: api/ApiFood/5
        [ResponseType(typeof(Food))]
        public IHttpActionResult GetFood(int id)
        {
            Food food = db.FoodMenu.Find(id);
            if (food == null)
            {
                return NotFound();
            }

            return Ok(food);
        }

        // PUT: api/ApiFood/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFood(int id, Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != food.Id)
            {
                return BadRequest();
            }

            db.Entry(food).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
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

        // POST: api/ApiFood
        [ResponseType(typeof(Food))]
        public IHttpActionResult PostFood(Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FoodMenu.Add(food);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = food.Id }, food);
        }

        // DELETE: api/ApiFood/5
        [ResponseType(typeof(Food))]
        public IHttpActionResult DeleteFood(int id)
        {
            Food food = db.FoodMenu.Find(id);
            if (food == null)
            {
                return NotFound();
            }

            db.FoodMenu.Remove(food);
            db.SaveChanges();

            return Ok(food);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodExists(int id)
        {
            return db.FoodMenu.Count(e => e.Id == id) > 0;
        }
    }
}