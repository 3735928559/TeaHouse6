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
    public class ApiChoiceController : ApiController
    {
        private OrderContext db = new OrderContext();

        // GET: api/ApiChoice
        public IQueryable<Choice> GetChoices()
        {
            return db.Choices;
        }

        // GET: api/ApiChoice/5
        [ResponseType(typeof(Choice))]
        public IHttpActionResult GetChoice(int id)
        {
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return NotFound();
            }

            return Ok(choice);
        }

        // PUT: api/ApiChoice/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChoice(int id, Choice choice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != choice.Id)
            {
                return BadRequest();
            }

            db.Entry(choice).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChoiceExists(id))
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

        // POST: api/ApiChoice
        [ResponseType(typeof(Choice))]
        public IHttpActionResult PostChoice(Choice choice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Choices.Add(choice);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = choice.Id }, choice);
        }

        // DELETE: api/ApiChoice/5
        [ResponseType(typeof(Choice))]
        public IHttpActionResult DeleteChoice(int id)
        {
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return NotFound();
            }

            db.Choices.Remove(choice);
            db.SaveChanges();

            return Ok(choice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChoiceExists(int id)
        {
            return db.Choices.Count(e => e.Id == id) > 0;
        }
    }
}