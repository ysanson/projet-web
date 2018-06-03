using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VeterinaryClinicManagment.Services;

namespace VeterinaryClinicManagment.Models
{
    public class Dal : IDal
    {
        private VeterinaryClinicEntities bd;

        public Dal()
        {
            bd = new VeterinaryClinicEntities();
        }

        public List<Utilisateur> ObtainAllClients()
        {
            List<Utilisateur> clients = bd.Utilisateur.ToList();
            clients.RemoveAll(c => c.estVeto == true); //We remove users that are not clients (ie are veterinaries)
            return clients;
        }

        public List<Utilisateur> ObtainAllVeterinaries()
        {
            List<Utilisateur> vet = bd.Utilisateur.ToList();
            vet.RemoveAll(v => v.estVeto == false);
            return vet;
        }

        public List<Animal> GetAllAnimals()
        {
            return bd.Animal.ToList();
        }

        public List<Animal> ObtainClientsPets(Utilisateur client)
        {
            List<Animal> pets = bd.Animal.ToList();
            pets.RemoveAll(p => p.IdUtilisateur != client.IdUtilisateur); //We remove all animals where the client's ID is different from the pet's client ID.
            return pets;
        }

        public Utilisateur GetUserById(int id)
        {
            return bd.Utilisateur.FirstOrDefault(u => u.IdUtilisateur == id);
        }

        public int CreateClient(Utilisateur client)
        {
            client.estVeto = false;
            client.nom = client.nom.ToUpper();
            if(!String.IsNullOrEmpty(client.ville))
                client.ville = Tools.FirstLetterToUpper(client.ville);
            client.prenom =  Tools.FirstLetterToUpper(client.prenom);
            //We need to salt and hash the user's password
            string salt = Tools.GenerateSalt(10);
            string saltedPwd = String.Concat(client.password,salt);
            client.password = Tools.ShaHash(saltedPwd);
            client.salt = salt;
            bd.Utilisateur.Add(client);
            bd.SaveChanges();
            return client.IdUtilisateur;
        }

        public int CreateVeterinary(Utilisateur vet)
        {
            vet.estVeto = true;
            vet.nom = vet.nom.ToUpper();
            if (!String.IsNullOrEmpty(vet.ville))
                vet.ville = Tools.FirstLetterToUpper(vet.ville);
            vet.prenom = Tools.FirstLetterToUpper(vet.prenom);
            //We need to salt and hash the user's password
            string salt = Tools.GenerateSalt(10);
            string saltedPwd = String.Concat(vet.password, salt);
            vet.password = Tools.ShaHash(saltedPwd);
            vet.salt = salt;
            bd.Utilisateur.Add(vet);
            bd.SaveChanges();
            return vet.IdUtilisateur;
        }

        public Utilisateur Authenticate(string mail, string pwd)
        {
            Utilisateur util = bd.Utilisateur.FirstOrDefault(u => u.mail == mail);
            if (util != null)
            {
                String saltedPwd = String.Concat(pwd, util.salt); //We must check if the entered password is the same as the stored password.
                saltedPwd = Tools.ShaHash(saltedPwd);//We must add the salt to the entered password.
                if (saltedPwd == util.password)
                    return util;
            }
            return null;
        }

        public Utilisateur GetUserByEmail(string email)
        {
            return  bd.Utilisateur.FirstOrDefault(u => u.mail == email);
        }

        public bool IsEmailInUse(string email)
        {
            Utilisateur user = bd.Utilisateur.FirstOrDefault(u => u.mail == email);
            return user != null ? true:false;
        }

        public Utilisateur GetClientById(int id)
        {
            return bd.Utilisateur.FirstOrDefault(c => c.IdUtilisateur == id && !c.estVeto);
        }

        public Utilisateur GetVetById(int id)
        {
            return bd.Utilisateur.FirstOrDefault(v => v.IdUtilisateur == id && v.estVeto);
        }

        public void EditUser(int id, Utilisateur user)
        {
            Utilisateur old = bd.Utilisateur.FirstOrDefault(u => u.IdUtilisateur == id);
            if (old != null)
            {
                old.nom = user.nom;
                old.prenom = user.prenom;                                         
                old.mail = user.mail;
                old.tel = user.tel;
                old.adresse = user.adresse;
                old.ville = user.ville;
                old.codePostal = user.codePostal;
                bd.SaveChanges();
            }  
        }

        public void EditPassword(Utilisateur user, string pwd)
        {
            string salt = Tools.GenerateSalt(10);
            string saltedPwd = String.Concat(pwd, salt);
            saltedPwd = Tools.ShaHash(saltedPwd);
            user.salt = salt;
            user.password = saltedPwd;
            bd.Entry(user).State = EntityState.Modified;
            bd.SaveChanges();
        }

        public bool IsValidPassword(Utilisateur user, string pwd)
        {
            return user.password == Tools.ShaHash(String.Concat(pwd, user.salt));
        }

        public Espece GetSpecById(int id)
        {
            return bd.Espece.FirstOrDefault(s => s.idEspece == id);
        }

        public int CreateSpec(Espece spec)
        {
            bd.Espece.Add(spec);
            bd.SaveChanges();
            return spec.idEspece;

        }

        public bool IsRaceExisting(string race)
        {
            Espece espece = bd.Espece.FirstOrDefault(e => e.race == race);
            return espece != null ? true : false;
        }



        public void EditSpecies(int id,Espece spec)
        {
            Espece old = bd.Espece.Find(id);
            if(old != null)
            {
                old.espece = spec.espece;
                old.race = spec.race;
                bd.SaveChanges();
            }
            
        }

        public List<Espece> GetAllSpecies()
        {
            return bd.Espece.ToList();
        }

        public int CreateAnimal(Animal animal)
        {
            bd.Animal.Add(animal);
            bd.SaveChanges();
            return animal.IdAnimal;
        }

        public Animal GetAnimalById(int id)
        {
            return bd.Animal.Find(id);
        }

        public void EditAnimalComments(int id, string comments)
        {
            Animal old = bd.Animal.Find(id);
            if (old != null)
            {
                old.commentaire = comments;
                bd.SaveChanges();
            }
            
        }

        public List<Medicament> GetAllMedicaments()
        {
            return bd.Medicament.ToList();
        }

        public Medicament GetMedicamentById(int id)
        {
            return bd.Medicament.Find(id);
        }

        public int CreateMedicament(Medicament medicament)
        {
            bd.Medicament.Add(medicament);
            bd.SaveChanges();
            return medicament.idMedicament;
        }

        public void EditMedicament(int id, Medicament medicament)
        {
            Medicament old = bd.Medicament.Find(id);
            if (medicament != null)
            {
                old.nomMedicament = medicament.nomMedicament;
                old.posologieMax = medicament.posologieMax;
                bd.SaveChanges();
            }         
        }

        public List<Operation> GetAllOperations()
        {
            return bd.Operation.ToList();
        }

        public int CreateOperation(Operation op)
        {
            bd.Operation.Add(op);
            bd.SaveChanges();
            return op.IdOperation;
        }

        public Operation GetOperationById(int id)
        {
            return bd.Operation.Find(id);
        }

        public void EditOperation(int id, Operation op)
        {
            Operation old = bd.Operation.Find(id);
            if (old != null)
            {
                old.label = op.label;
                bd.SaveChanges();
            }
        }

        public List<Consultation> GetAllConsultations()
        {
            return bd.Consultation.ToList();
        }

        public List<PrescConsultation> GetPrescriptionConsultByConsultId(int id)
        {
            List<PrescConsultation> prescs = bd.PrescConsultation.ToList();
            prescs.RemoveAll(p => p.idObservation != id);
            return prescs;
        }

        public int CreateConsultation(Consultation cons)
        {
            bd.Consultation.Add(cons);
            bd.SaveChanges();
            return cons.idObservation;
        }

        public Consultation GetConsultationById(int id)
        {
            return bd.Consultation.Find(id);
        }

        public List<PrescConsultation> GetPrescForConsultation(int id)
        {
            List<PrescConsultation> prescs = bd.PrescConsultation.ToList();
            prescs.RemoveAll(p => p.idObservation != id);
            return prescs;
        }

        public int CreatePrescriptionForConsultation(PrescConsultation presc)
        {
            bd.PrescConsultation.Add(presc);
            bd.SaveChanges();
            return presc.idMedicament;
        }

        public void EditConsult(Consultation cons)
        {
            Consultation old = bd.Consultation.Find(cons.idObservation);
            if (old != null)
            {
                old.examenClinique = cons.examenClinique;
                old.diagnostic = cons.diagnostic;
                old.description = cons.description;
                bd.SaveChanges();
            }
        }

        public void DeleteConsult(int id)
        {
            Consultation cons = bd.Consultation.Find(id);
            if (cons != null)
            {
                List<PrescConsultation> prescs = bd.PrescConsultation.ToList();
                prescs.RemoveAll(p => p.idObservation != id);
                bd.PrescConsultation.RemoveRange(prescs);

                bd.Consultation.Remove(cons);
                bd.SaveChanges();
            }
           
        }

        public int CreateRemark(Remarques rem)
        {
            bd.Remarques.Add(rem);
            bd.SaveChanges();
            return rem.idRemarques;
        }

        public Remarques GetLastRemForAnimal(int id)
        {
            List<Remarques> remarques = bd.Remarques.ToList();
            remarques.RemoveAll(r => r.IdAnimal != id);
            if (remarques.Count > 0)
            {
                Remarques mostRecent = remarques[remarques.Count - 1];
                return mostRecent;
            }
            return null;
        }

        public List<Remarques> RemarksForAnimal(int id)
        {
            List<Remarques> remarques = bd.Remarques.ToList();
            remarques.RemoveAll(r => r.IdAnimal != id);
            return remarques;
        }

        public SuiviPoids GetLastPdsForAnimal(int id)
        {
            List<SuiviPoids> pdsForAnimal = bd.SuiviPoids.ToList();
            pdsForAnimal.RemoveAll(s => s.IdAnimal != id);
            if (pdsForAnimal.Count > 0)
            {
                SuiviPoids mostRecent = pdsForAnimal[pdsForAnimal.Count - 1];
                return mostRecent;
            }
            return null;
        }

        public int CreateSuiviPds(SuiviPoids pds)
        {
            bd.SuiviPoids.Add(pds);
            bd.SaveChanges();
            return pds.idSuivi;
        }

        public List<SuiviPoids> GetPdsForAnimal(int id)
        {
            List<SuiviPoids> pds = bd.SuiviPoids.ToList();
            pds.RemoveAll(p => p.IdAnimal != id);
            return pds;
        }

        public List<Passe> GetAllOpPassed()
        {
            return bd.Passe.ToList();
        }

        public void CreatePasseOperation(Passe passe)
        {
            bd.Passe.Add(passe);
            bd.SaveChanges();
        }

        public Passe GetLastOperationForAnimal(int id)
        {
            List<Passe> passes = bd.Passe.ToList();
            passes.RemoveAll(p => p.IdAnimal != id);
            if (passes.Count > 0)
                return passes[passes.Count - 1];
            return null;
        }

        public Consultation GetLastConsultForAnimal(int id)
        {
            List<Consultation> consultations = bd.Consultation.ToList();
            consultations.RemoveAll(r => r.IdAnimal != id);
            if (consultations.Count > 0)
                return consultations[consultations.Count - 1];
            return null;
        }

        public List<Consultation> GetConsultForClient(int id)
        {
            List<Consultation> consultations=new List<Consultation>();
            List<Animal> animalsForClient = bd.Animal.ToList();
            animalsForClient.RemoveAll(a => a.IdUtilisateur != id);
            foreach(Animal a in animalsForClient)
            {
                consultations.AddRange(bd.Consultation.ToList().FindAll(c => c.IdAnimal == a.IdAnimal));
            }
            return consultations;
        }

        public void DeleteAnimal(Animal animal)
        {
            List<Consultation> consultsForAnimal = bd.Consultation.ToList();
            consultsForAnimal.RemoveAll(c => c.IdAnimal != animal.IdAnimal);
            foreach(Consultation c in consultsForAnimal)
            {
                DeleteConsult(c.idObservation);
            }
            List<Passe> opPassedForAnimal = bd.Passe.ToList();
            opPassedForAnimal.RemoveAll(p => p.IdAnimal != animal.IdAnimal);
            bd.Passe.RemoveRange(opPassedForAnimal);
            bd.Animal.Remove(animal);
            bd.SaveChanges();
        }

        public void Dispose()
        {
            bd.Dispose();
        }

    }
}