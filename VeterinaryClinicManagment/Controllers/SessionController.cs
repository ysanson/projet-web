using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VeterinaryClinicManagment.Models;
using VeterinaryClinicManagment.ViewModels;

namespace VeterinaryClinicManagment.Controllers
{

    public class SessionController : Controller
    {
        private IDal dal;

        public SessionController() : this(new Dal())
        {

        }

        private SessionController(IDal dalIoc)
        {
            dal = dalIoc;
        }
       
        public ActionResult SignIn(String returnUrl)
        {
            UserViewModel vm = new UserViewModel { auth = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                vm.user = dal.GetUserById(int.Parse(HttpContext.User.Identity.Name));
            }
            if (!string.IsNullOrEmpty(returnUrl))
                Response.StatusCode = 401;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(UserViewModel vm, String returnUrl)
        {
            if (vm.user.mail!= null && vm.user.password!=null)
            {
                Utilisateur user = dal.Authenticate(vm.user.mail, vm.user.password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.IdUtilisateur.ToString(), false);
                    Response.StatusCode = 304;
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return Redirect("/");
                }
                ModelState.AddModelError("User.mail", "Email ou mot de passe incorrect(s)");
            }
            return View(vm);
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return View();
        }

    }
}