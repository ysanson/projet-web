using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeterinaryClinicManagment.Models;

namespace VeterinaryClinicManagment.ViewModels
{
    public class UserViewModel
    {
        public Utilisateur user { get; set; }
        public bool auth { get; set; }
        public string[] roles { get; set; }
    }
}