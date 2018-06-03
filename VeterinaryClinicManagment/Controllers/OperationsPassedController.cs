using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeterinaryClinicManagment.Models;
using VeterinaryClinicManagment.ViewModels;

namespace VeterinaryClinicManagment.Controllers
{
    [Authorize(Roles ="Veterinary")]
    public class OperationsPassedController : Controller
    {
        // GET: PasseOperation
        public ActionResult Index()
        {
            using(IDal dal = new Dal())
            {
                List<Passe> allOp = dal.GetAllOpPassed();
                allOp.OrderByDescending(p => p.DateOp);
                List<OperationViewModel> vm = new List<OperationViewModel>();
                foreach(Passe p in allOp)
                {
                    vm.Add(new OperationViewModel(p));
                }
                return View(vm);
            }           
        }


        public ActionResult Create(int? id)
        {//Only the current veterinary can create operations on its name
         //Id is animal Id
            using (IDal dal = new Dal())
            {
                ViewBag.nomVeto = dal.GetVetById(int.Parse(HttpContext.User.Identity.Name)).nom + " " + dal.GetVetById(int.Parse(HttpContext.User.Identity.Name)).prenom;
                if (id != null)
                {
                    Animal animal = dal.GetAnimalById((int)id);
                    if (animal != null)
                    {
                        Passe op = new Passe
                        {
                            IdAnimal = animal.IdAnimal,
                            DateOp = DateTime.Now,
                            IdUtilisateur = int.Parse(HttpContext.User.Identity.Name)
                        };
                        ViewBag.IdOperation = new SelectList(dal.GetAllOperations(), "IdOperation", "label");
                        ViewBag.animalClient = dal.GetAnimalById(animal.IdAnimal).nom + " du client " + dal.GetClientById(animal.IdUtilisateur).nom + " " + dal.GetClientById(animal.IdUtilisateur).prenom;
                        return View(op);
                    }
                    return HttpNotFound();
                }
                Passe op2 = new Passe
                {
                    DateOp = DateTime.Now,
                    IdUtilisateur = int.Parse(HttpContext.User.Identity.Name)
                };
                ViewBag.IdOperation = new SelectList(dal.GetAllOperations(), "IdOperation", "label");
                ViewBag.IdAnimal = new SelectList(dal.GetAllAnimals(), "IdAnimal", "nom");
                return View(op2);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Passe passe)
        {
            if (ModelState.IsValid)
            {
                using(IDal dal = new Dal())
                {
                    dal.CreatePasseOperation(passe);
                    return RedirectToAction("Index");
                }
            }
            return View(passe);
        }


    }
}