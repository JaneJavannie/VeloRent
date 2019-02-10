using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private VeloRentDBEntities db = new VeloRentDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Velos()
        {
            VeloRentDBEntities db = new VeloRentDBEntities();
            var velos = new List<object>();

            foreach (var v in db.Velos)
                velos.Add(new { id = v.id, model = v.model });

            return Json(velos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VeloReqs(int id)
        {
            var veloreqs = new List<object>();
            DataTable t1, t2;

            using (SqlConnection connection = new SqlConnection("data source=(local)\\SQLEXPRESS;initial catalog=VeloRentDB;integrated security=True;"))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    // Строка с текстом SQL запроса.
                    string query = "SELECT Clients.name, a.name, b.name, Req.dateT, Req.dateNT, Req.dateRR FROM Clients, Req LEFT JOIN RentPoints a ON Req.pointID = a.id LEFT JOIN RentPoints b ON Req.returnPointID = b.id WHERE Req.VeloID = @SearchId AND Req.ClientID = Clients.id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter(
                          "@SearchId", id));

                        // Получаем данные
                        DataSet dataSet = new DataSet();
                        adapter.SelectCommand = command;
                        adapter.Fill(dataSet);
                        t1 = dataSet.Tables[0];
                    }

                    //query = "SELECT RentPoints.name FROM RentPoints, Req WHERE Req.VeloID = @SearchID AND Req.returnPointID = RentPoints.id";

                    //using (SqlCommand command = new SqlCommand(query, connection))
                    //{
                    //    command.Parameters.Add(new SqlParameter(
                    //      "@SearchId", id));

                    //    // Получаем данные
                    //    DataSet dataSet = new DataSet();
                    //    adapter.SelectCommand = command;
                    //    adapter.Fill(dataSet);
                    //    t2 = dataSet.Tables[0];
                    //}
                }
            }

            
            for (int i = 0; i < t1.Rows.Count; i++)
            {
                DataRow r = t1.Rows[i];
                veloreqs.Add(new
                {
                    client = r.ItemArray[0],
                    rentPoint = r.ItemArray[1],
                    returnPoint = r.ItemArray[2],// t2.Rows.Count > i ? t2.Rows[i].ItemArray[0] : "-",
                    dateT = r.ItemArray[3],
                    dateNT = r.ItemArray[4],
                    dateRR = r.ItemArray[5]
                });
            }
                

            return Json(veloreqs, JsonRequestBehavior.AllowGet);
        }
    }
}

