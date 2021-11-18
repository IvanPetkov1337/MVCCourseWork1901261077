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
    public class WaitersController : Controller
    {
        private SystemDBController db = new SystemDBController();

        // GET: Waiters
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            ViewBag.ExperiencesSortParm = String.IsNullOrEmpty(sortOrder) ? "experience_desc" : "";
            ViewBag.SalarySortParm = String.IsNullOrEmpty(sortOrder) ? "salary_desc" : "";
          
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var waiters = from n in db.Waiters select n;

            if (!String.IsNullOrEmpty(searchString))
            {
                waiters = waiters.Where(s => s.FirstName.Contains(searchString)
                                           ||s.LastName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "firstname_desc":
                    waiters = waiters.OrderByDescending(n => n.FirstName);
                    break;
                case "lastname_desc":
                    waiters = waiters.OrderByDescending(n => n.LastName);
                    break;
                case "experience_desc":
                    waiters = waiters.OrderByDescending(n => n.Experience);
                    break;
                case "salary_desc":
                    waiters = waiters.OrderByDescending(n => n.Salary);
                    break;

                default:
                    waiters = waiters.OrderBy(n => n.FirstName);
                    break;

            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(waiters.ToPagedList(pageNumber, pageSize));
            
        }

        // GET: Waiters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waiter waiter = db.Waiters.Find(id);
            if (waiter == null)
            {
                return HttpNotFound();
            }
            return View(waiter);
        }

        // GET: Waiters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Waiters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,IsVaccinated,Experience,Salary")] Waiter waiter)
        {
            if (ModelState.IsValid)
            {
                db.Waiters.Add(waiter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(waiter);
        }

        // GET: Waiters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waiter waiter = db.Waiters.Find(id);
            if (waiter == null)
            {
                return HttpNotFound();
            }
            return View(waiter);
        }

        // POST: Waiters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,IsVaccinated,Experience,Salary")] Waiter waiter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(waiter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(waiter);
        }

        // GET: Waiters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waiter waiter = db.Waiters.Find(id);
            if (waiter == null)
            {
                return HttpNotFound();
            }
            return View(waiter);
        }

        // POST: Waiters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Waiter waiter = db.Waiters.Find(id);
            db.Waiters.Remove(waiter);
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
