using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.tests
{
    // Ce code rend disponible une fonction Ellipsis sur une Chaine
    // 1) Fonction définie dans une classe static
    // 2) Fonction static
    // 3) Le premier paramètre est marqué avec le mot-clé this
    // => la fonction apparait sur le type du premier paramètre
    static class MesExtensions
    {
        public static string Ellispsis(this string s, int maxLength)
        {
            if (s.Length <= maxLength)
            {
                return s;
            }
            return s.Substring(0, maxLength - 3) + "...";
        }
    }




    [TestClass]
    public class FonctionExtensions
    {
        [TestMethod]
        public void EllipsisTest()
        {
            string s = "Dominique";
            // J'aimerais bien ajouter une fonction à une classe existante 
            // (sans pouvoir modifier la classe)
            // => Fonction d'extension
       
            var s2 = MesExtensions.Ellispsis(s, 7);
            s.Skip(2).Take(3);

            s2 = s.Ellispsis(7); //=> Compil MesExtensions.Ellispsis(s, 7);
        }
    }
}
