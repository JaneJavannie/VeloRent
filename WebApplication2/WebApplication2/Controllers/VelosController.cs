using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2;

namespace WebApplication2.Controllers
{
    public class VelosController : Controller
    {
        private VeloRentDBEntities db = new VeloRentDBEntities();

        // GET: Velos
        public ActionResult Index()
        {
            VeloRentDBEntities db = new VeloRentDBEntities();
            return View(db.Velos.ToList());
        }

        // GET: Velos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Velos velos = db.Velos.Find(id);
            if (velos == null)
            {
                return HttpNotFound();
            }

            using (SqlConnection connection = new SqlConnection("data source=(local)\\SQLEXPRESS;initial catalog=VeloRentDB;integrated security=True;"))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    // Строка с текстом SQL запроса.
                    string query = "SELECT name, AVG(datediff(hh,dateT,dateRR)) FROM Req,RentPoints WHERE VeloID = @SearchId AND RentPoints.id = Req.pointID GROUP BY (name)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter(
                          "@SearchId", id));

                        // Получаем данные
                        DataSet dataSet = new DataSet();
                        adapter.SelectCommand = command;
                        adapter.Fill(dataSet, "Velos");


                        // Передаем данные в представлние
                        ViewData["Velos"] = dataSet.Tables[0];
                    }
                }

                return View(velos);
            }
        }

        // GET: Velos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Velos/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,model,taken,timesT")] Velos velos)
        {
            if (ModelState.IsValid)
            {
                db.Velos.Add(velos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(velos);
        }

        // GET: Velos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Velos velos = db.Velos.Find(id);
            if (velos == null)
            {
                return HttpNotFound();
            }
            return View(velos);
        }

        // POST: Velos/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,model,taken,timesT")] Velos velos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(velos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(velos);
        }

        // GET: Velos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Velos velos = db.Velos.Find(id);
            if (velos == null)
            {
                return HttpNotFound();
            }
            return View(velos);
        }

        // POST: Velos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Velos velos = db.Velos.Find(id);
            db.Velos.Remove(velos);
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
