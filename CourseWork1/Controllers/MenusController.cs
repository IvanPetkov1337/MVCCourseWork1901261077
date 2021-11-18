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
    public class MenusController : Controller
    {
        private SystemDBController db = new SystemDBController();

        // GET: Menus
        public ActionResult Index(string sortOrder, string searchString , string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewBag.CaloriesSortParm = String.IsNullOrEmpty(sortOrder) ? "calories_desc" : "";
            ViewBag.WeightSortParm = String.IsNullOrEmpty(sortOrder) ? "weight_desc" : "";
            ViewBag.VeganSortParm = String.IsNullOrEmpty(sortOrder) ? "vegan_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var menus = from n in db.Menus select n;

            if (!String.IsNullOrEmpty(searchString))
            {
                menus = menus.Where(s => s.ItemName.Contains(searchString));                                      
            }
            switch (sortOrder){
                case "name_desc":
                    menus = menus.OrderByDescending(n => n.ItemName);
                    break;
                case "price_desc":
                    menus = menus.OrderByDescending(n => n.Price);
                    break;
                case "calories_desc":
                    menus = menus.OrderByDescending(n => n.Calories);
                    break;
                case "vegan_desc":
                    menus = menus.OrderByDescending(n => n.IsVegan);
                    break;
                case "weight_desc":
                    menus = menus.OrderByDescending(n => n.Weight);
                    break;

                default:                    
                    menus = menus.OrderBy(n => n.ItemName);
                    break;

            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(menus.ToPagedList(pageNumber, pageSize));           

        }

        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Menus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ItemName,IsVegan,Price,Calories,Weight")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemName,IsVegan,Price,Calories,Weight")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
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
