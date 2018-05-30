using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VeterinaryClinicManagment.Models;

namespace VeterinaryClinicManagment.Tests
{
    [TestClass]
    public class DalTests
    {
        /*[TestMethod]
        public void CreateClient_WithNewClient_ObtainAllClientsReturnsThisClient()
        {

            using(IDal dal = new Dal())
            {
                Utilisateur client = new Utilisateur{ nom="Test", password="1234", identifiant="test", estVeto=false};
                dal.CreateClient(client);
                List<Utilisateur> allClients = dal.ObtainAllClients();

                Assert.IsNotNull(allClients);
                Assert.AreEqual(allClients.Count, 1);
                Assert.AreEqual(allClients[0].nom, "Test");
                Assert.AreEqual(allClients[0].estVeto, false);
            }
        }*/
    }
}
