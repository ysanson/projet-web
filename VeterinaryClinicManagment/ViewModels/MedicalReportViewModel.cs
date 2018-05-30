using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeterinaryClinicManagment.Models;

namespace VeterinaryClinicManagment.ViewModels
{
    public class MedicalReportViewModel
    {
        public Animal animal { get; set; }
        public Passe lastOp { get; set; }
        public Consultation lastConsult { get; set; }
        public Utilisateur vetForOp { get; set; }
        public Utilisateur vetForConsult { get; set; }
        public List<PrescViewModel> prescForConsult { get; set; }
        public Operation operationPassed { get; set; }

        public MedicalReportViewModel(Animal animal)
        {
            using(IDal dal = new Dal())
            {
                this.animal = animal;
                lastOp = dal.GetLastOperationForAnimal(animal.IdAnimal);
                lastConsult = dal.GetLastConsultForAnimal(animal.IdAnimal);
                if (lastConsult != null)
                {
                    vetForConsult = dal.GetVetById(lastConsult.IdUtilisateur);
                    List<PrescConsultation> prescs = dal.GetPrescriptionConsultByConsultId(lastConsult.idObservation);
                    prescForConsult = new List<PrescViewModel>();
                    foreach (PrescConsultation p in prescs)
                    {
                        prescForConsult.Add(new PrescViewModel { presc = p, med = dal.GetMedicamentById(p.idMedicament) });
                    }
                }               
                if (lastOp != null)
                {
                    vetForOp = dal.GetVetById(lastOp.IdUtilisateur);
                    operationPassed = dal.GetOperationById(lastOp.IdOperation);
                }
            }
        }
    }
}