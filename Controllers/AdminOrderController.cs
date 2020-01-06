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
    [Authorize(Users = "Administrator")]
    public class AdminOrderController : Controller
    {
        private OrderContext db = new OrderContext();

        // GET: AdminOrder
        public ActionResult Index()
        {

            
            return View(db.OrderModels);
        }

        // GET: AdminOrder/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            var choice = db.Choices.Where(o => o.OrderNum == id).ToList();
            
            return View(choice);
        }

        // GET: AdminOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminOrder/Create
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

        // GET: AdminOrder/Edit/5
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

        // POST: AdminOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id,[Bind(Include = "Id,User,OrderTime")] OrderModels orderModels)
        {
            if (ModelState.IsValid)
            {
                string _user = System.Web.HttpContext.Current.User.Identity.Name;

                using (var ctx = new OrderContext())
                {
                    var sql = $"Update Choices SET Status='Fulfilled' WHERE OrderNum={id}";

                    ctx.Database.ExecuteSqlCommand(sql);

                }
                orderModels.Status = "Fulfilled";
                db.Entry(orderModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderModels);
        }

        public ActionResult Paid(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Paid(int? id, [Bind(Include = "Id,User,OrderTime")] OrderModels orderModels)
        {
            if (ModelState.IsValid)
            {
                string _user = System.Web.HttpContext.Current.User.Identity.Name;

                OrderModels orderModel2 = db.OrderModels.Find(id);
                orderModel2.Status = "Paid";

                using (var ctx = new OrderContext())
                {
                    var sql = $"Update Choices SET Status='Paid' WHERE OrderNum={id}";

                    ctx.Database.ExecuteSqlCommand(sql);

                }
                
                db.Entry(orderModel2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderModels);
        }

        public ActionResult Cancelled(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancelled(int? id, [Bind(Include = "Id,User,OrderTime")] OrderModels orderModels)
        {
            if (ModelState.IsValid)
            {
                string _user = System.Web.HttpContext.Current.User.Identity.Name;
                
                OrderModels orderModel2 = db.OrderModels.Find(id);
                
                    orderModel2.Status = "Cancelled";

                    using (var ctx = new OrderContext())
                    {
                        var sql = $"Update Choices SET Status='Cancelled' WHERE OrderNum={id}";

                        ctx.Database.ExecuteSqlCommand(sql);

                    }

                    db.Entry(orderModel2).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            
            return View(orderModels);
        }
        // GET: AdminOrder/Delete/5
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

        // POST: AdminOrder/Delete/5
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
