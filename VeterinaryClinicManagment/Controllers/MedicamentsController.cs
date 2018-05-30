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
    public class MedicamentsController : Controller
    {
        
        public ActionResult Index()
        {
            using(IDal dal = new Dal())
            {
                return View(dal.GetAllMedicaments());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Medicament medicament)
        {
            if (ModelState.IsValid)
            {
                using(IDal dal=new Dal())
                {
                    int id =dal.CreateMedicament(medicament);
                    if (id != 0)
                    {
                        Response.StatusCode = 201;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Response.StatusCode = 503;
                        ViewData["DBError"] = "Erreur pendant le traitement dans la base de données. Veuillez réessayer plus tard.";
                        return View(medicament);
                    }
                }
            }
            return View(medicament);
        }

        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                using(IDal dal = new Dal())
                {
                    Medicament med = dal.GetMedicamentById((int)id);
                    if (med != null)
                        return View(med);
                    return HttpNotFound();
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Medicament med)
        {
            if (ModelState.IsValid && id!=null)
            {
                using (IDal dal = new Dal())
                {
                    dal.EditMedicament((int)id, med);
                    Response.StatusCode = 205;
                    return RedirectToAction("Index");
                }
            }
            return View(med);
        }
    }
}