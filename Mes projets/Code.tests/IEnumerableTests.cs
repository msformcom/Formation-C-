using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.tests
{
    [TestClass]
    public class IEnumerableTests
    {
        [TestMethod]
        public void IEnumerabletest()
        {
            IEnumerable<char> liste = new List<char>() { 'a', 'b', 'c' };
      

            // J'obtient à partir de mon IEnumerable un Enumerator
            // qui va me servir à progresser dans l'ensemble
            var enumerator = liste.GetEnumerator();
            // moveNext => Essaye d'avancer et renvoit vrai si elle a progresse
            while (enumerator.MoveNext()) { 
                char c= enumerator.Current;
            }

            // Syntaxic sugar
            foreach (char c in liste)
            {
                Debug.WriteLine(c);
            }

            IEnumerable<char> s = "Toto";
            foreach (char c in s) { 
            }

            IEnumerable<char> h;
            h = "Toto";
            h = new List<Char>();
            h = new char[10]; // Tableau char
            h= new Dictionary<int, char>().Values;    


        }
    }
}
