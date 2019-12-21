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

            var order = from s in db.Choices select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                order = order.Where(s => s.User.Equals(System.Web.HttpContext.Current.User.Identity.Name) || s.SelectedFood.Name.Contains(searchString) || s.SelectedFood.Detail.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_asc": order = order.OrderBy(s => s.SelectedFood.Name); break;
                case "type_asc": order = order.OrderBy(s => s.SelectedFood.FoodType); break;
                case "price_asc": order = order.OrderBy(s => s.SelectedFood.Price); break;
                default: order = order.OrderBy(s => s.SelectedFood.Name); break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(order.ToPagedList(pageNumber, pageSize));

            //return View(Order.ToList());
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
        public ActionResult Create([Bind(Include = "Id,User,OrderTime")] OrderModels orderModels)
        {
            if (ModelState.IsValid)
            {
                db.OrderModels.Add(orderModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            OrderModels orderModels = db.OrderModels.Find(id);
            if (orderModels == null)
            {
                return HttpNotFound();
            }
            return View(orderModels);
        }

        // POST: OrderModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderModels orderModels = db.OrderModels.Find(id);
            db.OrderModels.Remove(orderModels);
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
