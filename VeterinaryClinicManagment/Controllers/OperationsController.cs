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
    public class OperationsController : Controller
    {

        public ActionResult Index()
        {
            using (IDal dal = new Dal())
            {
                return View(dal.GetAllOperations());
            }  
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Operation op)
        {
            if (ModelState.IsValid)
            {
                using(IDal dal = new Dal())
                {
                    int id = dal.CreateOperation(op);
                    if (id != 0)
                    {
                        Response.StatusCode = 201;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Response.StatusCode = 503;
                        ViewData["DBError"] = "Erreur pendant le traitement dans la base de données. Veuillez réessayer plus tard.";
                        return View(op);
                    }
                }
            }
            return View(op);
        }

        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                using (IDal dal = new Dal())
                {
                    Operation op = dal.GetOperationById((int)id);
                    if (op != null)
                        return View(op);
                    return HttpNotFound();
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Operation op)
        {
            if(ModelState.IsValid && id != null)
            {
                using(IDal dal = new Dal())
                {
                    dal.EditOperation((int)id, op);
                    Response.StatusCode = 205;
                    return RedirectToAction("Index");
                }
            }
            return View(op);
        }


    }
}