using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeterinaryClinicManagment.Models;

namespace VeterinaryClinicManagment.ViewModels
{
    public class OperationViewModel
    {
        public Utilisateur vet { get; set; }
        public AnimalViewModel animal { get; set; }
        public Operation operation { get; set; }
        public Passe passing { get; set; }

        public OperationViewModel(Passe p)
        {
            passing = p;
            using (IDal dal= new Dal())
            {
                animal = new AnimalViewModel(p.IdAnimal);
                operation = dal.GetOperationById(p.IdOperation);
                vet = dal.GetVetById(p.IdUtilisateur);
            }
        }

    }
}