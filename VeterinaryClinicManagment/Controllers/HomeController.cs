using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeterinaryClinicManagment.Models;

namespace VeterinaryClinicManagment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using(IDal dal = new Dal())
            {
                List<Utilisateur> vets = dal.ObtainAllVeterinaries();
                return View(vets);
            }
            
        }
    }
}