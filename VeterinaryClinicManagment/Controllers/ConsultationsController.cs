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
    public class ConsultationsController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            using(IDal dal=new Dal())
            {
                if (HttpContext.User.IsInRole("Client"))
                {
                    List<ConsultViewModel> cvm = new List<ConsultViewModel>();
                    List<Consultation> clientConsults = dal.GetConsultForClient(int.Parse(HttpContext.User.Identity.Name));
                    clientConsults.OrderByDescending(c => c.dateObs);
                    foreach(Consultation c in clientConsults)
                    {
                        cvm.Add(new ConsultViewModel(c));
                    }
                    return View(cvm);
                }
                if (HttpContext.User.IsInRole("Veterinary"))
                {
                    List<ConsultViewModel> consults = new List<ConsultViewModel>();
                    List<Consultation> consultations = dal.GetAllConsultations();
                    consultations.OrderByDescending(c => c.dateObs);
                    foreach (Consultation c in consultations)
                    {
                        consults.Add(new ConsultViewModel(c));
                    }
                    return View(consults);
                }
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }          
        }

        [Authorize(Roles = "Veterinary")]
        public ActionResult Create(int? id)
        {//A veterinary can only create consultations on its own name
         //The id is an animal ID, so we don't have to search for it for ages. We can do it without.
            using (IDal dal = new Dal())
            {
                ViewBag.nomVeto = dal.GetVetById(int.Parse(HttpContext.User.Identity.Name)).nom + " " + dal.GetVetById(int.Parse(HttpContext.User.Identity.Name)).prenom;
                if (id != null)
                {                 
                    Animal animal = dal.GetAnimalById((int)id);
                    if (animal != null)
                    {
                        Consultation consultation = new Consultation
                        {
                            dateObs = DateTime.Now,
                            IdAnimal = animal.IdAnimal,
                            IdUtilisateur = int.Parse(HttpContext.User.Identity.Name)
                        };
                        ViewBag.animalClient = dal.GetAnimalById(animal.IdAnimal).nom + " du client " + dal.GetClientById(animal.IdUtilisateur).nom + " " + dal.GetClientById(animal.IdUtilisateur).prenom;
                        return View(consultation);
                    }
                    return HttpNotFound();
                }
                Consultation consult = new Consultation
                {
                    dateObs = DateTime.Now,
                    IdUtilisateur = int.Parse(HttpContext.User.Identity.Name)
                };
                ViewBag.IdAnimal = new SelectList(dal.GetAllAnimals(), "IdAnimal", "nom");
                return View(consult);
            }
        }

        [Authorize(Roles = "Veterinary")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Consultation cons)
        {
            if (ModelState.IsValid)
            {
                using(IDal dal = new Dal())
                {
                    int id=dal.CreateConsultation(cons);
                    if (id != 0)
                    {
                        Response.StatusCode = 201;
                        return RedirectToAction("Details", new { id });
                    }
                    Response.StatusCode = 503;
                    ViewData["DBError"] = "Erreur pendant le traitement dans la base de données. Veuillez réessayer plus tard.";
                    return View(cons);
                }
            }
            return View(cons);
        }

        [Authorize(Roles = "Veterinary")]
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                using(IDal dal = new Dal())
                {
                    Consultation cons = dal.GetConsultationById((int)id);
                    if (cons != null)
                    {
                        ConsultViewModel cvm = new ConsultViewModel(cons);
                        return View(cvm);
                    }
                        
                    return HttpNotFound();
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "Veterinary")]
        public ActionResult Edit(int? id)
        {//Only the vet can edit its consultations
            if (id != null)
            {
                using(IDal dal = new Dal())
                {
                    Consultation cons = dal.GetConsultationById((int)id);
                    if (cons != null)
                    {
                        if(cons.IdUtilisateur == int.Parse(HttpContext.User.Identity.Name))
                        {
                            return View(cons);
                        }
                        else
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                        }
                    }
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "Veterinary")]
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Consultation cons)
        {
            if(ModelState.IsValid && cons.idObservation != 0)
            {
                using (IDal dal = new Dal())
                {
                    dal.EditConsult(cons);
                    Response.StatusCode = 205;
                    return RedirectToAction("Details", new { id = cons.idObservation });
                }
            }
            return View(cons);
        }

        [Authorize(Roles = "Veterinary")]
        [HttpDelete]
        public ActionResult Delete(int? id)
        {//Only the vet can delete its own consultations
            if (id != null)
            {
                using (IDal dal = new Dal())
                {
                    Consultation cons = dal.GetConsultationById((int)id);
                    if (cons != null)
                    {
                        if (cons.IdUtilisateur == int.Parse(HttpContext.User.Identity.Name))
                        {
                            dal.DeleteConsult((int)id);
                            Response.StatusCode = 201;
                            return Json(new { id = (int)id });
                        }
                        else
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                        }
                    }
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

    }
}