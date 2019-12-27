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
        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            string _user = System.Web.HttpContext.Current.User.Identity.Name;
            var food = from c in db.Choices
                        where (c.User.Equals(_user) && c.Status.Equals("Ordered"))
                               
                        select c;
            
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
        [Authorize]
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
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,User,OrderTime,Choices")] OrderModels orderModels)
        {
                  
            if (ModelState.IsValid)
            {
                db.OrderModels.Add(orderModels);
                db.SaveChanges();
                string _user = System.Web.HttpContext.Current.User.Identity.Name;
                
                using (var ctx = new OrderContext())
                { 
                    var sql = $"Update Choices SET OrderNum={orderModels.Id} WHERE [User]='{_user}' and Status='Ordered'";
                    ctx.Database.ExecuteSqlCommand(sql);
                
                    sql = $"Update Choices SET Status='Confirmed' WHERE [User]='{_user}' and Status='Ordered'";
                
                    ctx.Database.ExecuteSqlCommand(sql);
                    
                    
                }

                return RedirectToAction("Index");
            }
            
            
            

            return View(orderModels);
        }

        // GET: OrderModel/Edit/5
        [Authorize]
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
        [Authorize]
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

        [Authorize]
        public ActionResult OrderView()
        {
            string _user = System.Web.HttpContext.Current.User.Identity.Name;
            var order = from c in db.OrderModels 
                       where (c.User.Equals(_user) && !c.Status.Equals("Paid"))

                       select c;
            
            return View(order);
        }

        [Authorize]
        public ActionResult OrderDetails(int? id)
        {
            var choice = db.Choices.Where(o => o.OrderNum == id).ToList();

               
            return View(choice);

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
