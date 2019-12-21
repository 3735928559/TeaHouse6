using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeaHouse.Models;

namespace TeaHouse.Controllers
{
    [Authorize(Users = "Administrator")]
    public class FoodController : Controller
    {
        private OrderContext db = new OrderContext();

        // GET: Food
        
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

                //return View(food.ToList());
            }

        

        // GET: Food/Details/5
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

        // GET: Food/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Food/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,FoodType,Detail,Price")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.FoodMenu.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(food);
        }

        // GET: Food/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Food/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var foodToUpdate = db.FoodMenu.Find(id);

            if (TryUpdateModel(foodToUpdate, "", new string[] { "Name", "FoodType", "Detail", "Price" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException  /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log. 
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }
            return View(foodToUpdate);
        }


        // GET: Food/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again.";
            }

            Food food = db.FoodMenu.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Food/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Food food = db.FoodMenu.Find(id); db.FoodMenu.Remove(food);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException /* dex */)
            { //Log the error (uncomment dex variable name and add a line here to write a log. 
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
