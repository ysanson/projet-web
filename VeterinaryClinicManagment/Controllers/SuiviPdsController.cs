using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using VeterinaryClinicManagment.Models;

namespace VeterinaryClinicManagment.Controllers
{
    public class SuiviPdsController : Controller
    {
        // GET: SuiviPds
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                SuiviPoids pds = new SuiviPoids { dateSuivi = DateTime.Now, IdAnimal = (int)id };
                return PartialView(pds);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SuiviPoids pds)
        {
            using (IDal dal = new Dal())
            {
                if (ModelState.IsValid)
                {
                    dal.CreateSuiviPds(pds);
                    Response.StatusCode = 201;
                    SuiviPoids nouv = new SuiviPoids { dateSuivi = DateTime.Now, IdAnimal = pds.IdAnimal };
                    ViewBag.Success = "Correctement enregistré.";
                    return PartialView(nouv);
                }
                return PartialView(pds);        
            }
        }

        public ActionResult GraphForAnimal(int? id)
        {
            if (id != null)
            {
                using(IDal dal = new Dal())
                {
                    List<SuiviPoids> suiviPoids = dal.GetPdsForAnimal((int)id);
                    Chart graph = new Chart(width: 400, height: 400)
                        .AddTitle("Suivi du poids en fonction du temps")
                        .AddSeries(
                            chartType: "Line",
                            xValue: suiviPoids.Select(s => s.dateSuivi).ToArray(),
                            yValues: suiviPoids.Select(s => s.poids).ToArray());
                    return File(graph.GetBytes("png"), "image/png");
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}