using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VeterinaryClinicManagment.Models;

namespace VeterinaryClinicManagment.Controllers
{
    [Authorize]
    public class RemarksController : Controller
    {

        public ActionResult RemarksForAnimal(int? id)
        {
            if (id != null)
            {
                using(IDal dal = new Dal())
                {
                    Animal animal = dal.GetAnimalById((int)id);
                    if (animal != null)
                    {
                        ViewBag.DescAnimal = animal.nom + " (" + dal.GetSpecById(animal.idEspece).esprace + ")";
                        ViewBag.ID = animal.IdAnimal.ToString();
                        List<Remarques> rem = dal.RemarksForAnimal((int)id);
                        rem.OrderByDescending(r => r.dateRemarque);
                        return View(rem);
                    }
                    return HttpNotFound();
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult Create(int? id)
        {
            using (IDal dal=new Dal())
            {
                if (id != null)
                {
                    Remarques rem = new Remarques { IdAnimal = (int)id, dateRemarque = DateTime.Now };
                    ViewBag.PourAnimal = "pour l'animal " + dal.GetAnimalById((int)id).nom + " (" + dal.GetSpecById(dal.GetAnimalById((int)id).idEspece).esprace+")";
                    return View(rem);
                }
                ViewBag.IdAnimal = new SelectList(dal.GetAllAnimals(), "IdAnimal", "nom");
                Remarques remarques = new Remarques { dateRemarque = DateTime.Now };
                return View(remarques);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Remarques rem)
        {
            using (IDal dal = new Dal())
            {
                if (ModelState.IsValid)
                {
                    int id = dal.CreateRemark(rem);
                    if (id != 0)
                    {
                        Response.StatusCode = 201;
                        Remarques nouveau = new Remarques { IdAnimal = rem.IdAnimal, dateRemarque = DateTime.Now };
                        return RedirectToAction("Details", "Animals", new { id = rem.IdAnimal });
                    }
                }
                return View(rem);
            }
        }
    }
}