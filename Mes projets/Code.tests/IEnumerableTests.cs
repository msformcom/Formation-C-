using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Code.tests
{
    [TestClass]
    public class IEnumerableTests
    {

        // Dans cette méthode, le IEnumerable est renvoyé élément par élément
        // C'est le générateur
        // Si l'itérateur arrète son enumération, il n'y a plus de génération
        public IEnumerable<int> Range(int a, int b)
        {
   
            for(var i =a; i <=b; i++)
            {
                yield return i; 
            }
       

        }

        [TestMethod]
        public void IEnumerabletest()
        {
            // Générateur => function qui est prète a générer des entiers
            var listeEntiers = Range(0, int.MaxValue);

            // Itérator => code qui demande les entiers du générateur
            foreach(var e in listeEntiers)
            {
                Console.WriteLine(e);
                //break;
                // Si s'itération s'arrète, la génération aussi
            }
        }



            [TestMethod]
        public void IEnumerabletest2()
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
