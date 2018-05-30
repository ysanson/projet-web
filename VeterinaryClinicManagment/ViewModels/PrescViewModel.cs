using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeterinaryClinicManagment.Models;

namespace VeterinaryClinicManagment.ViewModels
{
    public class PrescViewModel
    {
        public Medicament med { get; set; }
        public PrescConsultation presc { get; set; }
    }
}