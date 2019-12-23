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
    public class OrderModelController : Controller
    {
        private OrderContext db = new OrderContext();

        // GET: OrderModel
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            string _user = System.Web.HttpContext.Current.User.Identity.Name;
            //var food = db.Choices.Include(c => c.SelectedFood);

            var food = from c in db.Choices
                        where (c.User.Equals(_user)) && ((c.Status.Equals("Ordered")) &&(c.OrderNum.Equals(null)))
                               
                        select c;
            //return View(food.ToList());

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

            switch (sortOrder)
            {
                case "name_asc": food = food.OrderBy(c => c.SelectedFood.Name); break;
                case "type_asc": food = food.OrderBy(c => c.SelectedFood.FoodType); break;
                case "price_asc": food = food.OrderBy(c => c.SelectedFood.Price); break;
                default: food = food.OrderBy(c => c.SelectedFood.Name); break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(food.ToPagedList(pageNumber, pageSize));


        }

        // GET: OrderModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderModels orderModels = db.OrderModels.Find(id);
            if (orderModels == null)
            {
                return HttpNotFound();
            }
            return View(orderModels);
        }

        // GET: OrderModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,User,OrderTime,Choices")] OrderModels orderModels)
        {
                  
            if (ModelState.IsValid)
            {

                
                db.OrderModels.Add(orderModels);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            orderModels.SelectedChoice.Status = "Confirmed";
            orderModels.SelectedChoice.OrderNum = orderModels.Id;
            
            

            return View(orderModels);
        }
        
        // GET: OrderModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderModels orderModels = db.OrderModels.Find(id);
            if (orderModels == null)
            {
                return HttpNotFound();
            }
            return View(orderModels);
        }

        // POST: OrderModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,User,OrderTime")] OrderModels orderModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderModels);
        }

        // GET: OrderModel/Delete/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            Choice choice = db.Choices.Find(id);
            db.Choices.Remove(choice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OrderView()
        {
            string _user = System.Web.HttpContext.Current.User.Identity.Name;
            var order = from c in db.OrderModels
                       where (c.User.Equals(_user))

                       select c;

            return View(db.OrderModels);
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
