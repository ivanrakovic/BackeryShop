﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BackeryShopDomain.Classes;
using BackeryShopDomain.DataModel;

namespace BackeryShop.Web.Controllers
{
    public class PriceListDetailsController : Controller
    {
        private BackeryContext db = new BackeryContext();

        // GET: PriceListDetails
        public ActionResult Index()
        {
            var priceListDetails = db.PriceListDetails.Include(p => p.PriceList).Include(p => p.Product);
            return View(priceListDetails.ToList());
        }

        // GET: PriceListDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var priceListDetail = db.PriceListDetails.Find(id);
            if (priceListDetail == null)
            {
                return HttpNotFound();
            }
            return View(priceListDetail);
        }

        // GET: PriceListDetails/Create
        //public ActionResult Create()
        //{
        //    ViewBag.PriceListId = new SelectList(db.PriceLists, "Id", "Name");
        //    ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
        //    return View();
        //}

        public ActionResult Create(int? id)
        {
            
            ViewBag.PriceListId = new SelectList(db.PriceLists.Where(p => p.Id == id), "Id", "Name", id);
            var productsForList = db.Products.Where(p => !db.PriceListDetails.Where(x => x.PriceListId == id).Any(p2 => p2.ProductId == p.Id));
            ViewBag.ProductId = new SelectList(productsForList, "Id", "Name");
            return View();
        }

        // POST: PriceListDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Price,OrderNo,PriceListId,ProductId")] PriceListDetail priceListDetail)
        {
            if (ModelState.IsValid)
            {
                db.PriceListDetails.Add(priceListDetail);
                db.SaveChanges();
                return Json(new { success = true });
            }

            ViewBag.PriceListId = new SelectList(db.PriceLists, "Id", "Name", priceListDetail.PriceListId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", priceListDetail.ProductId);
            return View(priceListDetail);
        }

        // GET: PriceListDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var priceListDetail = db.PriceListDetails.Find(id);
            if (priceListDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.PriceListId = new SelectList(db.PriceLists, "Id", "Name", priceListDetail.PriceListId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", priceListDetail.ProductId);
            return PartialView(priceListDetail);
        }

        // POST: PriceListDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Price,OrderNo,PriceListId,ProductId")] PriceListDetail priceListDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(priceListDetail).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            ViewBag.PriceListId = new SelectList(db.PriceLists, "Id", "Name", priceListDetail.PriceListId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", priceListDetail.ProductId);
            return View(priceListDetail);
        }

        // GET: PriceListDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var priceListDetail = db.PriceListDetails.Find(id);
            if (priceListDetail == null)
            {
                return HttpNotFound();
            }
            return View(priceListDetail);
        }

        // POST: PriceListDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var priceListDetail = db.PriceListDetails.Find(id);
            db.PriceListDetails.Remove(priceListDetail);
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
