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
    public class ReqsController : Controller
    {
        private VeloRentDBEntities db = new VeloRentDBEntities();

        // GET: Reqs
        public ActionResult Index()
        {
            var req = db.Req.Include(r => r.Clients).Include(r => r.RentPoints).Include(r => r.Velos);
            return View(req.ToList());
        }

        // GET: Reqs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Req req = db.Req.Find(id);
            if (req == null)
            {
                return HttpNotFound();
            }
            return View(req);
        }

        // GET: Reqs/Create
        public ActionResult Create()
        {
            ViewBag.clientID = new SelectList(db.Clients, "id", "name");
            ViewBag.pointID = new SelectList(db.RentPoints, "id", "name");
            ViewBag.veloID = new SelectList(db.Velos, "id", "model");
            ViewBag.returnPointID = new SelectList(db.RentPoints, "id", "name"); //added
            return View();
        }

        // POST: Reqs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,clientID,veloID,dateT,dateNT,dateRR,pointID,returnPointID")] Req req)
        {
            if (ModelState.IsValid)
            {
                db.Req.Add(req);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clientID = new SelectList(db.Clients, "id", "name", req.clientID);
            ViewBag.pointID = new SelectList(db.RentPoints, "id", "name", req.pointID);
            ViewBag.veloID = new SelectList(db.Velos, "id", "model", req.veloID);
            ViewBag.returnPointID = new SelectList(db.RentPoints, "id", "name", req.RentPoints1); //added
           
            return View(req);
        }

        // GET: Reqs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Req req = db.Req.Find(id);
            if (req == null)
            {
                return HttpNotFound();
            }
            ViewBag.clientID = new SelectList(db.Clients, "id", "name", req.clientID);
            ViewBag.pointID = new SelectList(db.RentPoints, "id", "name", req.pointID);
            ViewBag.veloID = new SelectList(db.Velos, "id", "model", req.veloID);
            ViewBag.returnPointID = new SelectList(db.RentPoints, "id", "name", req.RentPoints1); //added
            return View(req);
        }

        // POST: Reqs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,clientID,veloID,dateT,dateNT,dateRR,pointID,returnPointID")] Req req)
        {
            if (ModelState.IsValid)
            {
                db.Entry(req).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clientID = new SelectList(db.Clients, "id", "name", req.clientID);
            ViewBag.pointID = new SelectList(db.RentPoints, "id", "name", req.pointID);
            ViewBag.veloID = new SelectList(db.Velos, "id", "model", req.veloID);
            ViewBag.returnPointID = new SelectList(db.RentPoints, "id", "name", req.RentPoints1); //added
            return View(req);
        }

        // GET: Reqs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Req req = db.Req.Find(id);
            if (req == null)
            {
                return HttpNotFound();
            }
            return View(req);
        }

        // POST: Reqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Req req = db.Req.Find(id);
            db.Req.Remove(req);
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
