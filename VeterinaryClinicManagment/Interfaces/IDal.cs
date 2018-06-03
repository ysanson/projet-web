using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinicManagment.Models
{
    public interface IDal : IDisposable // Interface pour le Data Acess Layer, qui permet d'ajouter une couche d'abstraction supplémentaire pour accéder aux données.
    {
        List<Utilisateur> ObtainAllClients();
        List<Utilisateur> ObtainAllVeterinaries();
        List<Animal> ObtainClientsPets(Utilisateur client);
        List<Animal> GetAllAnimals();
        Utilisateur Authenticate(string email, string pwd);
        Utilisateur GetUserById(int id);
        Utilisateur GetUserByEmail(string email);
        Utilisateur GetClientById(int id);
        Utilisateur GetVetById(int id);
        bool IsEmailInUse(string email);
        int CreateClient(Utilisateur client);
        int CreateVeterinary(Utilisateur vet);
        void EditUser(int id, Utilisateur user);
        void EditPassword(Utilisateur user, string pwd);
        bool IsValidPassword(Utilisateur user, string pwd);
        Espece GetSpecById(int id);
        List<Espece> GetAllSpecies();
        void EditSpecies(int id, Espece spec);
        int CreateSpec(Espece spec);
        bool IsRaceExisting(string race);
        int CreateAnimal(Animal animal);
        Animal GetAnimalById(int id);
        void EditAnimalComments(int id, string comments);
        List<Medicament> GetAllMedicaments();
        Medicament GetMedicamentById(int id);
        int CreateMedicament(Medicament medicament);
        void EditMedicament(int id, Medicament medicament);
        List<Operation> GetAllOperations();
        int CreateOperation(Operation op);
        Operation GetOperationById(int id);
        void EditOperation(int id, Operation op);
        List<Consultation> GetAllConsultations();
        List<PrescConsultation> GetPrescriptionConsultByConsultId(int id);
        int CreateConsultation(Consultation cons);
        Consultation GetConsultationById(int id);
        List<PrescConsultation> GetPrescForConsultation(int id);
        int CreatePrescriptionForConsultation(PrescConsultation presc);
        void EditConsult(Consultation cons);
        void DeleteConsult(int id);
        int CreateRemark(Remarques rem);
        Remarques GetLastRemForAnimal(int id);
        List<Remarques> RemarksForAnimal(int id);
        SuiviPoids GetLastPdsForAnimal(int id);
        int CreateSuiviPds(SuiviPoids pds);
        List<SuiviPoids> GetPdsForAnimal(int id);
        List<Passe> GetAllOpPassed();
        void CreatePasseOperation(Passe passe);
        Passe GetLastOperationForAnimal(int id);
        Consultation GetLastConsultForAnimal(int id);
        List<Consultation> GetConsultForClient(int id);
        void DeleteAnimal(Animal animal);
    }
}
