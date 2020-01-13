using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeaHouse.Models;

namespace TeaHouse.Controllers
{
    public class ChoiceController : Controller
    {
        private OrderContext db = new OrderContext();

        
        [Authorize]
                
        // GET: Choice
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_asc" : "";
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "price_asc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var food = from s in db.FoodMenu select s;
            food = food.Where(s => s.isActive.Equals(true));

            if (!String.IsNullOrEmpty(searchString))
            {
                food = food.Where(s => s.Name.Contains(searchString) || s.FoodType.Contains(searchString) || s.Detail.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_asc": food = food.OrderBy(s => s.Name); break;
                case "type_asc": food = food.OrderBy(s => s.FoodType); break;
                case "price_asc": food = food.OrderBy(s => s.Price); break;
                default: food = food.OrderBy(s => s.Name); break;
            }

            
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(food.ToPagedList(pageNumber, pageSize));

            
        }

        // GET: Choice/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.FoodMenu.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Choice/Create
        
        [Authorize]
        public ActionResult Create(int? id, [Bind(Include = "Id,User,OrderTime,Status,Food")] Choice choice)
        {
            Food food = db.FoodMenu.Find(id);
            choice.SelectedFood = food;
            choice.Status = "Ordered";
            
            if (ModelState.IsValid)
            {
                db.Choices.Add(choice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(choice);
        }

        // POST: Choice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*public ActionResult Create(int? id, [Bind(Include = "Id,User,OrderTime,Status,Food")] Choice choice)
        {
            Food food = db.FoodMenu.Find(id);
            choice.SelectedFood = food;
            choice.Status = "Ordered";
            if (ModelState.IsValid)
            {
                db.Choices.Add(choice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(choice);
        }*/

        // GET: Choice/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // POST: Choice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,User,OrderTime,Status")] Choice choice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(choice);
        }

        // GET: Choice/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // POST: Choice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Choice choice = db.Choices.Find(id);
            db.Choices.Remove(choice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
