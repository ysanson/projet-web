using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VeterinaryClinicManagment.Models;
using VeterinaryClinicManagment.ViewModels;

namespace VeterinaryClinicManagment.Controllers
{
    [Authorize]
    public class AnimalsController : Controller
    {

        public ActionResult Index() //Returns the list of all animals 
        {
            using(IDal dal = new Dal())
            {
                List<AnimalViewModel> lvm = new List<AnimalViewModel>();
                List<Animal> animals;
                if (HttpContext.User.IsInRole("Veterinary"))
                {
                    animals = dal.GetAllAnimals();
                    foreach (Animal a in animals)
                    {
                        lvm.Add(new AnimalViewModel (a.IdAnimal));
                    }

                }
                if (HttpContext.User.IsInRole("Client"))
                {
                    animals = dal.ObtainClientsPets(dal.GetClientById(int.Parse(HttpContext.User.Identity.Name)));
                    foreach (Animal a in animals)
                    {
                        lvm.Add(new AnimalViewModel(a.IdAnimal));
                    }
                }
                return View(lvm);
            }
        }

        [Authorize(Roles ="Veterinary")]
        public ActionResult Create()
        { //Only a veterinary can create a new animal (to prevent clients from adding animals who don't exist)
            using(IDal dal = new Dal())
            {
                List<SelectListItem> sexe = new List<SelectListItem>
                {

                    new SelectListItem { Text = "Mâle", Value = "male"},
                    new SelectListItem { Text = "Femelle", Value = "femelle" }
                };
                
                ViewBag.sexe = sexe;
                ViewBag.IdUtilisateur = new SelectList(dal.ObtainAllClients(), "IdUtilisateur", "nom");
                ViewBag.idEspece = new SelectList(dal.GetAllSpecies(), "idEspece", "esprace");
                return View();
            }
        }

        [Authorize(Roles ="Veterinary")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Animal animal)
        {
            if (animal.dateNaissance > DateTime.Now)
            {
                ViewData["DateNError"] = "La date de naissance ne peut pas être dans le futur !";
                return View(animal);
            }

            if (ModelState.IsValid)
            {
                using(IDal dal = new Dal())
                {
                    int id = dal.CreateAnimal(animal);
                    if (id != 0)
                    {
                        Response.StatusCode = 201;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Response.StatusCode = 503;
                        ViewData["DBError"] = "Erreur pendant le traitement dans la base de données. Veuillez réessayer plus tard.";
                        return View(animal);
                    }
                }
            }
            return View(animal);
        }

        public ActionResult Details(int? id)
        { // A client can only see the details of its animals
            if (id != null)
            {
                using (IDal dal = new Dal())
                {
                    Animal animal = dal.GetAnimalById((int)id);
                    if (animal != null)
                    {
                        AnimalViewModel vm = new AnimalViewModel(animal.IdAnimal);
                        if (HttpContext.User.IsInRole("Client"))
                            if (int.Parse(HttpContext.User.Identity.Name) == animal.IdUtilisateur)
                                return View(vm);
                        if (HttpContext.User.IsInRole("Veterinary"))
                            return View(vm);
                        return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    }
                    else
                        return HttpNotFound();
                }
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditComments(int? id)
        {
            if (id != null)
            {
                using (IDal dal = new Dal())
                {
                    Animal animal = dal.GetAnimalById((int)id);
                    ViewBag.IdUtilisateur = new SelectList(dal.ObtainAllClients(), "IdUtilisateur", "nom", animal.IdUtilisateur);
                    ViewBag.idEspece = new SelectList(dal.GetAllSpecies(), "idEspece", "race", animal.idEspece);
                    if (animal != null)
                    {
                        if (HttpContext.User.IsInRole("Client"))
                        {
                            if (int.Parse(HttpContext.User.Identity.Name) == animal.IdUtilisateur)
                            {
                                return View(animal);
                            }
                            
                        }
                        if (HttpContext.User.IsInRole("Veterinary"))
                        {
                            return View(animal);
                        }
                        return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    }
                    else
                        return HttpNotFound();
                }
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult EditComments(int? id, Animal a)
        {
            if (id != null)
            {
                NameValueCollection nvc = Request.Form;
                string comments = nvc["commentaire"];
                using(IDal dal = new Dal())
                {
                    Animal animal = dal.GetAnimalById((int)id);
                    dal.EditAnimalComments((int)id, comments);
                    return RedirectToAction("Index");
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult MedicalReport(int? id)
        {
            if (id != null)
            {
                using(IDal dal = new Dal())
                {
                    Animal animal = dal.GetAnimalById((int)id);
                    if (animal != null)
                    {
                        MedicalReportViewModel vm = new MedicalReportViewModel(animal);
                        if (HttpContext.User.IsInRole("Client"))
                            if (int.Parse(HttpContext.User.Identity.Name) == animal.IdUtilisateur)
                                return View(vm);
                        if (HttpContext.User.IsInRole("Veterinary"))
                            return View(vm);
                        return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    }                
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}