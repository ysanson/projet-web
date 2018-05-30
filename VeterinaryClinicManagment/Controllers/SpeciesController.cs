using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VeterinaryClinicManagment.Models;

namespace VeterinaryClinicManagment.Controllers
{
    [Authorize(Roles ="Veterinary")]
    public class SpeciesController : Controller
    {
        public ActionResult Index()
        {
            using(IDal dal = new Dal())
            {
                return View(dal.GetAllSpecies());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Espece esp)
        {
            if (ModelState.IsValid)
            {
                using(IDal dal = new Dal())
                {
                    if (!dal.IsRaceExisting(esp.race))
                    {
                        int id = dal.CreateSpec(esp);
                        if (id != 0)
                        {
                            Response.StatusCode = 201;
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Response.StatusCode = 503;
                            ViewData["DBError"] = "Erreur pendant le traitement dans la base de données. Veuillez réessayer plus tard.";
                            return View(esp);
                        }
                    }
                    else
                    {
                        Response.StatusCode = 409;
                        ViewData["InUse"] = "Cette race existe déjà !";
                        return View(esp);
                    }
                }

            }
            return View(esp);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using(IDal dal = new Dal())
                {
                    Espece spec = dal.GetSpecById((int)id);
                    if (spec == null)
                        return HttpNotFound();
                    return View(spec);
                }
            }
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Espece esp)
        {
            if (ModelState.IsValid && id!=null)
            {
                using (IDal dal = new Dal())
                {
                    dal.EditSpecies((int)id, esp);
                    Response.StatusCode = 205;
                    return RedirectToAction("Index");
                }
            }
            return View(esp);
        }
    }
}