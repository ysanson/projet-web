using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeterinaryClinicManagment.Models;

namespace VeterinaryClinicManagment.ViewModels
{
    public class ConsultViewModel
    {
        public AnimalViewModel animal { get; set; }
        public Utilisateur veto { get; set; }
        public Consultation consult { get; set; }
        public List<PrescViewModel> prescriptions { get; set; }
        

        public ConsultViewModel(Consultation c)
        {
            consult = c;
            using(IDal dal = new Dal())
            {
                animal = new AnimalViewModel(c.IdAnimal);
                veto = dal.GetVetById(c.IdUtilisateur);
                List<PrescConsultation> prescs = dal.GetPrescriptionConsultByConsultId(c.idObservation);
                prescriptions = new List<PrescViewModel>();
                foreach(PrescConsultation p in prescs)
                {
                    prescriptions.Add(new PrescViewModel { presc = p, med = dal.GetMedicamentById(p.idMedicament) });
                }
               
            }
        }
    }
}