using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VeterinaryClinicManagment.Models;

namespace VeterinaryClinicManagment.Controllers
{
    [Authorize(Roles ="Veterinary")]
    public class VeterinariesController : Controller
    {

        public ActionResult Index()
        {
            using(IDal dal = new Dal())
            {
                List<Utilisateur> vets = dal.ObtainAllVeterinaries();
                return View(vets);
            }
            
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Utilisateur vet)
        {
            if (!ModelState.IsValid)
                return View(vet);
            using(IDal dal = new Dal())
            {
                if (!dal.IsEmailInUse(vet.mail))
                {
                    int id = dal.CreateVeterinary(vet);
                    if (id != 0)
                    {
                        Response.StatusCode = 201;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Response.StatusCode = 503;
                        ViewData["DBError"] = "Erreur pendant le traitement dans la base de données. Veuillez réessayer plus tard.";
                        return View(vet);
                    }
                        
                }
                else
                {
                    Response.StatusCode = 409;
                    ViewData["InUse"] = "Ce nom d'utilisateur est déjà pris. Veuillez en choisir un nouveau.";
                    return View(vet);
                }
                
            }
        }

        public ActionResult Details(int? id)
        {
            using(IDal dal = new Dal())
            {
                Utilisateur vet;
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                else
                {
                    vet = dal.GetVetById((int)id);
                    if (vet == null)
                        return HttpNotFound();
                    else
                        return View(vet);
                }
            }
        }

        public ActionResult Edit(int? id)
        {//A veterinary can only modify everyone, but not for their passwords and usernames.
            using(IDal dal = new Dal())
            {
                Utilisateur vet;
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                else
                {
                    vet = dal.GetVetById((int)id);
                    if (vet == null)
                        return HttpNotFound();
                    else
                        return View(vet);
                }
            }
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Utilisateur vet)
        {

            if (ModelState.IsValid && id!=null)
            {
                using (IDal dal = new Dal())
                {
                    dal.EditUser((int) id, vet);
                    return RedirectToAction("Index");
                }
            }
            return View(vet);
        }

        public ActionResult EditPassword(int? id)
        {//Only the current veterinary can edit its login informations
            using (IDal dal = new Dal())
            {
                Utilisateur vet = null;
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Veterinary"))
                {
                    vet = dal.GetVetById(int.Parse(HttpContext.User.Identity.Name));
                    if (vet == null)
                        return HttpNotFound();
                    return View(vet);
                }
                else if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPassword()
        {
            NameValueCollection nvc = Request.Form;
            string oldpwd = null, newpwd = null, confirm = null;
            if (!String.IsNullOrEmpty(nvc["oldpwd"]))
                oldpwd = nvc["oldpwd"];
            if (!String.IsNullOrEmpty(nvc["newpwd"]))
                newpwd = nvc["newpwd"];
            if (!String.IsNullOrEmpty(nvc["confirm"]))
                confirm = nvc["confirm"];
            if (newpwd == confirm)
            {
                using (IDal dal = new Dal())
                {
                    Utilisateur vet = dal.GetVetById(int.Parse(HttpContext.User.Identity.Name));
                    if (dal.IsValidPassword(vet, oldpwd))
                    {
                        dal.EditPassword(vet, newpwd);
                        Response.StatusCode = 205;
                        return RedirectToAction("Details", new { id = vet.IdUtilisateur });
                    }
                    else
                    {
                        ViewData["BadPwd"] = "L'ancien mot de passe est erroné.";
                        return View();
                    }

                }
            }
            else
            {
                ViewData["NotCorresponding"] = "Les mots de passe ne correspondent pas.";
                return View();
            }
        }
    }
}