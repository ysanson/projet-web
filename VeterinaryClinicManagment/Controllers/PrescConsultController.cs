using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VeterinaryClinicManagment.Models;
using VeterinaryClinicManagment.ViewModels;

namespace VeterinaryClinicManagment.Controllers
{
    [Authorize]
    public class PrescConsultController : Controller
    {


        [ChildActionOnly]
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                using (IDal dal = new Dal())
                {
                    ViewBag.idMedicament = new SelectList(dal.GetAllMedicaments(), "idMedicament", "nomMedicament");
                    PrescConsultation presc = new PrescConsultation { idObservation = (int)id };
                    return PartialView(presc);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrescConsultation presc)
        {
            using (IDal dal = new Dal())
            {
                if (ModelState.IsValid)
                {
                    dal.CreatePrescriptionForConsultation(presc);
                    Response.StatusCode = 201;
                    ViewBag.idMedicament = new SelectList(dal.GetAllMedicaments(), "idMedicament", "nomMedicament");
                    PrescConsultation newP = new PrescConsultation { idObservation = presc.idObservation };
                    return PartialView(newP);
                }
                ViewBag.idMedicament = new SelectList(dal.GetAllMedicaments(), "idMedicament", "nomMedicament");
                return PartialView(presc);
            }
        }

        public ActionResult DetailsForConsult(int? id)
        {
            if (id != null)
            {
                using(IDal dal = new Dal())
                {
                    List<PrescConsultation> prescs = dal.GetPrescForConsultation((int)id);
                    List<PrescViewModel> vm = new List<PrescViewModel>();
                    foreach(PrescConsultation p in prescs)
                    {
                        vm.Add(new PrescViewModel { presc = p, med = dal.GetMedicamentById(p.idMedicament) });
                    }
                    return PartialView(vm);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

    }
}