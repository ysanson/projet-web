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
    public class ClientsController : Controller
    {
        // GET: Clients
        public ActionResult Index()
        {
            using (IDal dal = new Dal())
            {
                if (HttpContext.User.IsInRole("Veterinary")) //Clients aren't allowed the access to the list of clients
                {
                    List<Utilisateur> clients = dal.ObtainAllClients();
                    return View(clients);
                    
                }   
                Response.StatusCode = 403;
                return RedirectToAction("Details", new {id= int.Parse(HttpContext.User.Identity.Name)});
            }
        }
        
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Utilisateur client)
        {
            if (!ModelState.IsValid)
            {
                return View(client);
            }
            using(IDal dal = new Dal())
            {
                if (!dal.IsEmailInUse(client.mail))
                {
                    int id = dal.CreateClient(client);
                    if (id != 0)
                    {
                        Response.StatusCode = 201;
                        return RedirectToAction("Details", new { id });
                    }
                    else
                    {
                        Response.StatusCode = 503;
                        ViewData["DBError"] = "Erreur pendant le traitement dans la base de données. Veuillez réessayer plus tard.";
                        return View(client);
                    }    
                }
                else
                {
                    Response.StatusCode = 409;
                    ViewData["InUse"] = "Ce nom d'utilisateur est déjà pris. Veuillez en choisir un nouveau.";
                    return View(client);
                }
            }
        }

        public ActionResult Details(int? id)
        {//A client can only sees its details
            using(IDal dal = new Dal())
            {
                Utilisateur client;
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Client"))
                {
                    if (int.Parse(HttpContext.User.Identity.Name) != id)
                        return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    client = dal.GetClientById(int.Parse(HttpContext.User.Identity.Name));
                    if (client == null)
                        return HttpNotFound();
                }
                else if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                else
                {
                    client = dal.GetClientById((int)id);
                    if (client == null)
                        return HttpNotFound();
                }
                ClientViewModel clientView = new ClientViewModel(client);
                return View(clientView);
            }           
        }

        public ActionResult Edit(int? id)
        {//A client can modify itself, but a veterinary can modify every client, except for their passwords and usernames.
            using (IDal dal = new Dal())
            {
                Utilisateur client;
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Client"))
                {
                    client = dal.GetClientById(int.Parse(HttpContext.User.Identity.Name));
                    if (client == null)
                        return HttpNotFound();
                    else if (id != client.IdUtilisateur)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    }                      
                }
                else if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                else
                {
                    client = dal.GetClientById((int)id);
                    if (client == null)
                        return HttpNotFound();
                }
                if (client == null)
                {
                    return HttpNotFound();
                }
                return View(client);
            }   
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Utilisateur client)
        {
            if (ModelState.IsValid && id!= null)
            {
                using(IDal dal = new Dal())
                {
                    dal.EditUser((int)id, client);
                    Response.StatusCode = 205;
                    return RedirectToAction("Details", new { id });
                }
            }
            return View(client);
        }

        public ActionResult EditPassword(int? id)
        {//Only the current client can edit its password
            using (IDal dal = new Dal())
            {
                Utilisateur client=null;
                if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Client"))
                {
                    client = dal.GetClientById(int.Parse(HttpContext.User.Identity.Name));
                    if (client == null)
                        return HttpNotFound();          
                    else if(id==client.IdUtilisateur)
                        return View(client);
                }
                else if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }

        [HttpPut]
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
                    Utilisateur client = dal.GetClientById(int.Parse(HttpContext.User.Identity.Name));
                    if (dal.IsValidPassword(client, oldpwd))
                    {
                        dal.EditPassword(client, newpwd);
                        Response.StatusCode = 205;
                        return RedirectToAction("Details", new { id = client.IdUtilisateur });
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