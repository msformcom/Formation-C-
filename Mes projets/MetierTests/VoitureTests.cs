using Microsoft.VisualStudio.TestTools.UnitTesting;
using Metier.Concession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Metier.Concession.Tests
{



    [TestClass()]
    public class VoitureTests
    {
        [TestMethod]

        [DoNotParallelize]
        public void BaisseDePrixTest()
        {
            // Arrange
            bool alertOk = false;
            Exception reducErreur = null;
            Voiture.BeforeBaissePrix += (o,e)=> {
                alertOk = true;
                if (e.PrixApres < e.PrixAvant * 0.5M)
                {
                    throw new InvalidOperationException();
                }
              
            };


            // Act
            var v = new Voiture("C8", 1000);
            try
            {
                v.Prix -= 600;
            }
            catch (Exception ex)
            {

                reducErreur=ex;
            }
           

            // Assert 
            Assert.IsTrue(alertOk);

            Assert.IsInstanceOfType(reducErreur, typeof(InvalidOperationException));
            
        }



        [TestMethod]
        public async Task Jauge()
        {
            var v = new Voiture("C8", 1000) { CapaciteReservoir = 8 };
            v.FaireLePlein();
            v.Demarrer();
            v.AlerteJaugeCarburant += (o, e) =>
            {
                Debug.WriteLine("Attention, faire le plein rapidement");
            };
            // Attente de 10 seconde
            await Task.Delay(100000);
        }

  

        [TestMethod]
        public async Task TestConsommation()
        {
            // Arrange
            var v = new Voiture("C8", 10000) { CapaciteReservoir=5};
            v.FaireLePlein();
            v.Demarrer();


            // Act
   

            // attente est une tache => C'est un objet qui représente une tache qui peut être attendue
            var attente = Task.Delay(6000);
            // La tache est en cours mais cette ligne s'exécute sans attente



            await attente; // le code écrit à la ligne 32 sera exécuté
            // uniquement quand la tache ser terminée



            // Assert
            Assert.IsFalse(v.EstDemarree);
            Assert.AreEqual(v.NiveauCarburant, 0);
        }



        [TestMethod()]
        public void DemarrerTest()
        {
            // Arrange
            var v = new Voiture("C8", 19999);
            Exception erreurAuDeuxiemeDemarrage = null;

            // Act
            bool estDemarreeBefore = v.EstDemarree;
            v.Demarrer();
            bool estDemarreeAfter = v.EstDemarree;
            try
            {
                v.Demarrer();
            }
            catch (Exception ex)
            {
                erreurAuDeuxiemeDemarrage = ex;
            }

            // Assert
            Assert.IsFalse(estDemarreeBefore, "Une nouvelle instance de voiture est déjà demarrée");
            Assert.IsTrue(estDemarreeAfter, "Après exécution de Demarree, EstDemarree vaut faux");
            Assert.IsInstanceOfType(erreurAuDeuxiemeDemarrage, typeof(InvalidOperationException), "La voiture peut être demarrée deux fois sans erreur");
        }

        [TestMethod()]
        public void ArreterTest()
        {
            Assert.Fail();
        }
    }
}