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
    public class TurnoverDetailsController : Controller
    {
        private BackeryContext db = new BackeryContext();

        // GET: TurnoverDetails
        public ActionResult Index()
        {
            var turnoverDetails = db.TurnoverDetails.Include(t => t.Product).Include(t => t.Turnover);
            return View(turnoverDetails.ToList());
        }

        // GET: TurnoverDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TurnoverDetail turnoverDetail = db.TurnoverDetails.Find(id);
            if (turnoverDetail == null)
            {
                return HttpNotFound();
            }
            return View(turnoverDetail);
        }

        // GET: TurnoverDetails/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.TurnoverId = new SelectList(db.Turnovers, "Id", "Id");
            return View();
        }

        // POST: TurnoverDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TurnoverId,ProductId,Price,PreviousBalance,BakedNew,Sold,Scrap,NewBalance")] TurnoverDetail turnoverDetail)
        {
            if (ModelState.IsValid)
            {
                db.TurnoverDetails.Add(turnoverDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", turnoverDetail.ProductId);
            ViewBag.TurnoverId = new SelectList(db.Turnovers, "Id", "Id", turnoverDetail.TurnoverId);
            return View(turnoverDetail);
        }

        // GET: TurnoverDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TurnoverDetail turnoverDetail = db.TurnoverDetails.Find(id);
            if (turnoverDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", turnoverDetail.ProductId);
            ViewBag.TurnoverId = new SelectList(db.Turnovers, "Id", "Id", turnoverDetail.TurnoverId);
            return View(turnoverDetail);
        }

        // POST: TurnoverDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TurnoverId,ProductId,Price,PreviousBalance,BakedNew,Sold,Scrap,NewBalance")] TurnoverDetail turnoverDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turnoverDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", turnoverDetail.ProductId);
            ViewBag.TurnoverId = new SelectList(db.Turnovers, "Id", "Id", turnoverDetail.TurnoverId);
            return View(turnoverDetail);
        }

        // GET: TurnoverDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TurnoverDetail turnoverDetail = db.TurnoverDetails.Find(id);
            if (turnoverDetail == null)
            {
                return HttpNotFound();
            }
            return View(turnoverDetail);
        }

        // POST: TurnoverDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TurnoverDetail turnoverDetail = db.TurnoverDetails.Find(id);
            db.TurnoverDetails.Remove(turnoverDetail);
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
