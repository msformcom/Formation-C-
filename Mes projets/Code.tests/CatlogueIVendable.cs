using Metier.Concession;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.tests
{
    [TestClass]
    public class CaatlogueIVendable
    {
        [TestMethod]
        public void CatalogueTest()
        {
            var catalogueProduit = new List<IVendable>();
            catalogueProduit.Add(new Chien(12));

            catalogueProduit.Add(new Tele());
            // catalogueProduit.Add(new Voiture("C8", 1778));

            foreach(var item in catalogueProduit)
            {
                item.Vendre();

                // tester si item est du type Chien
                // exécuter la méthode Crier()
                if(item is Chien c)
                {
                    ((Chien)item).Crier();
                    c.Crier();
                }
            }


        }
    }
}
