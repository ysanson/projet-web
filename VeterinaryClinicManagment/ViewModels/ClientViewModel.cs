using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeterinaryClinicManagment.Models;

namespace VeterinaryClinicManagment.ViewModels
{
    public class ClientViewModel
    {
        public Utilisateur client { get; set; }
        public List<AnimalViewModel> clientsPets { get; set; }

        public ClientViewModel(Utilisateur client)
        {
            this.client = client;
            using (IDal dal = new Dal())
            {
                List<Animal> pets = dal.ObtainClientsPets(client);
                clientsPets = new List<AnimalViewModel>();
                foreach(Animal a in pets)
                {
                    clientsPets.Add(new AnimalViewModel(a.IdAnimal));
                }
            }
        }
    }
}