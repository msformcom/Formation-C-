using Microsoft.VisualStudio.TestTools.UnitTesting;
using Metier.Concession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier.Concession.Tests
{
    [TestClass()]
    public class VoitureTests
    {
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
                erreurAuDeuxiemeDemarrage=ex;
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