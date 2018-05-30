using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeterinaryClinicManagment.Models;

namespace VeterinaryClinicManagment.ViewModels
{
    public class AnimalViewModel
    {
        public Animal animal { get; set; }
        public Espece species { get; set; }
        public Utilisateur user { get; set; }
        public Remarques lastRem { get; set; }
        public SuiviPoids pds { get; set; }

        public AnimalViewModel(int id)
        {
            using(IDal dal=new Dal())
            {
                animal = dal.GetAnimalById(id);
                species = dal.GetSpecById(animal.idEspece);
                user = dal.GetClientById(animal.IdUtilisateur);
                lastRem = dal.GetLastRemForAnimal(animal.IdAnimal);
                pds = dal.GetLastPdsForAnimal(animal.IdAnimal);
            }
        }
    }
}