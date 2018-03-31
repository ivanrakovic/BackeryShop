using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BackeryShopDomain.Classes;
using BackeryShopDomain.DataModel;

namespace BackeryShop.Web.Controllers
{
    public class BackeriesController : Controller
    {

        // GET: Backeries
        public ActionResult Index()
        {
	        using (var db = new BackeryContext())
	        {
				var backeries = db.Backeries.Include(b => b.PriceList);
		        return View(backeries.ToList());
			}
			
        }

        // GET: Backeries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

	        using (var db = new BackeryContext())
	        {
		        var backery = db.Backeries.Find(id);
		        if (backery == null)
		        {
			        return HttpNotFound();
		        }

		        return View(backery);
	        }
        }

        // GET: Backeries/Create
        public ActionResult Create()
        {
	        using (var db = new BackeryContext())
	        {
		        ViewBag.PriceListId = new SelectList(db.PriceLists, "Id", "Name");
		        return View();
	        }
        }

        // POST: Backeries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,NumberOfShifts,PriceListId")] Backery backery)
        {
	        using (var db = new BackeryContext())
	        {
		        if (ModelState.IsValid)
		        {
			        db.Backeries.Add(backery);
			        db.SaveChanges();
			        return RedirectToAction("Index");
		        }

		        ViewBag.PriceListId = new SelectList(db.PriceLists, "Id", "Name", backery.PriceListId);
		        return View(backery);
	        }
        }

        // GET: Backeries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

	        using (var db = new BackeryContext())
	        {
		        var backery = db.Backeries.Find(id);
		        if (backery == null)
		        {
			        return HttpNotFound();
		        }

		        ViewBag.PriceListId = new SelectList(db.PriceLists, "Id", "Name", backery.PriceListId);
		        return View(backery);
	        }
        }

        // POST: Backeries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,NumberOfShifts,PriceListId")] Backery backery)
        {
	        using (var db = new BackeryContext())
	        {
		        if (ModelState.IsValid)
		        {
			        db.Entry(backery).State = EntityState.Modified;
			        db.SaveChanges();
			        return RedirectToAction("Index");
		        }

		        ViewBag.PriceListId = new SelectList(db.PriceLists, "Id", "Name", backery.PriceListId);
		        return View(backery);
	        }
        }

        // GET: Backeries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

	        using (var db = new BackeryContext())
	        {
		        var backery = db.Backeries.Find(id);
		        if (backery == null)
		        {
			        return HttpNotFound();
		        }

		        return View(backery);
	        }
        }

        // POST: Backeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
	        using (var db = new BackeryContext())
	        {
		        var backery = db.Backeries.Find(id);
		        db.Backeries.Remove(backery);
		        db.SaveChanges();
		        return RedirectToAction("Index");
	        }
        }
    }
}
