using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2;

namespace WebApplication2.Controllers
{
    public class RentPointsController : Controller
    {
        private VeloRentDBEntities db = new VeloRentDBEntities();

        // GET: RentPoints
        public ActionResult Index()
        {
            return View(db.RentPoints.ToList());
        }

        // GET: RentPoints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentPoints rentPoints = db.RentPoints.Find(id);
            if (rentPoints == null)
            {
                return HttpNotFound();
            }
            return View(rentPoints);
        }

        // GET: RentPoints/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RentPoints/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,address")] RentPoints rentPoints)
        {
            if (ModelState.IsValid)
            {
                db.RentPoints.Add(rentPoints);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rentPoints);
        }

        // GET: RentPoints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentPoints rentPoints = db.RentPoints.Find(id);
            if (rentPoints == null)
            {
                return HttpNotFound();
            }
            return View(rentPoints);
        }

        // POST: RentPoints/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,address")] RentPoints rentPoints)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentPoints).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rentPoints);
        }

        // GET: RentPoints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentPoints rentPoints = db.RentPoints.Find(id);
            if (rentPoints == null)
            {
                return HttpNotFound();
            }
            return View(rentPoints);
        }

        // POST: RentPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentPoints rentPoints = db.RentPoints.Find(id);
            db.RentPoints.Remove(rentPoints);
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
