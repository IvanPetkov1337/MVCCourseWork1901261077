using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourseWork1.Models;
using PagedList;

namespace CourseWork1.Controllers
{
    public class OrdersController : Controller
    {
        private SystemDBController db = new SystemDBController();

        // GET: Orders
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            ViewBag.TimeOfOrderSortParm = String.IsNullOrEmpty(sortOrder) ? "timeoforder_desc" : "";
            ViewBag.RatingSortParm = String.IsNullOrEmpty(sortOrder) ? "rating_desc" : "";
            ViewBag.TipSortParm = String.IsNullOrEmpty(sortOrder) ? "tip_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var orders = from n in db.Orders select n;

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.FirstNameOfClient.Contains(searchString)
                                         ||s.LastNameOfClient.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "firstname_desc":
                    orders = orders.OrderByDescending(n => n.FirstNameOfClient);
                    break;
                case "lastname_desc":
                    orders = orders.OrderByDescending(n => n.LastNameOfClient);
                    break;
                case "timeoforder_desc":
                    orders = orders.OrderByDescending(n => n.TimeOfOrder);
                    break;
                case "rating_desc":
                    orders = orders.OrderByDescending(n => n.Rating);
                    break;
                case "tip_desc":
                    orders = orders.OrderByDescending(n => n.Tip);
                    break;

                default:
                    orders = orders.OrderBy(n => n.FirstNameOfClient);
                    break;

            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(orders.ToPagedList(pageNumber, pageSize));
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.MenuId = new SelectList(db.Menus, "Id", "ItemName");
            ViewBag.WaiterId = new SelectList(db.Waiters, "Id", "FirstName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstNameOfClient,LastNameOfClient,Table,TimeOfOrder,Notes,Tip,Rating,MenuId,WaiterId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                db.SaveChanges();
                    
                return RedirectToAction("Index");
            }
            

            ViewBag.MenuId = new SelectList(db.Menus, "Id", "ItemName", orders.MenuId);
            ViewBag.WaiterId = new SelectList(db.Waiters, "Id", "FirstName", orders.WaiterId);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuId = new SelectList(db.Menus, "Id", "ItemName", orders.MenuId);
            ViewBag.WaiterId = new SelectList(db.Waiters, "Id", "FirstName", orders.WaiterId);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstNameOfClient,LastNameOfClient,Table,TimeOfOrder,Notes,Tip,Rating,MenuId,WaiterId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuId = new SelectList(db.Menus, "Id", "ItemName", orders.MenuId);
            ViewBag.WaiterId = new SelectList(db.Waiters, "Id", "FirstName", orders.WaiterId);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
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
