using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BackeryShopDomain.Classes;
using BackeryShopDomain.DataModel;

namespace BackeryShop.Web.Controllers
{
    public class TurnoversController : Controller
    {
        private BackeryContext db = new BackeryContext();

        // GET: Turnovers
        public ActionResult Index()
        {
            var turnovers = db.Turnovers.Include(t => t.Backery);
            return View(turnovers.ToList());
        }

        // GET: Turnovers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turnover turnover = db.Turnovers.Find(id);
            if (turnover == null)
            {
                return HttpNotFound();
            }
            return View(turnover);
        }

        // GET: Turnovers/Create
        public ActionResult Create()
        {
            ViewBag.BackeryId = new SelectList(db.Backeries, "Id", "Name");
            return View();
        }

        // POST: Turnovers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,ShiftNo,BackeryId")] Turnover turnover)
        {
            if (ModelState.IsValid)
            {
                db.Turnovers.Add(turnover);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BackeryId = new SelectList(db.Backeries, "Id", "Name", turnover.BackeryId);
            return View(turnover);
        }

        // GET: Turnovers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turnover turnover = db.Turnovers.Find(id);
            if (turnover == null)
            {
                return HttpNotFound();
            }
            ViewBag.BackeryId = new SelectList(db.Backeries, "Id", "Name", turnover.BackeryId);
            return View(turnover);
        }

        // POST: Turnovers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,ShiftNo,BackeryId")] Turnover turnover)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turnover).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BackeryId = new SelectList(db.Backeries, "Id", "Name", turnover.BackeryId);
            return View(turnover);
        }

        // GET: Turnovers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turnover turnover = db.Turnovers.Find(id);
            if (turnover == null)
            {
                return HttpNotFound();
            }
            return View(turnover);
        }

        // POST: Turnovers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Turnover turnover = db.Turnovers.Find(id);
            db.Turnovers.Remove(turnover);
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
