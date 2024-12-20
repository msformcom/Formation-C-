using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.tests
{
    [TestClass]
    public  class StructRecordTest
    {
        [TestMethod]
        public void Record()
        {
            // Record
            var a = (1, "toto");
            a.Item1++;

            (int, string) b = a;
            (int n, string s) c = a;
            c = (1, "rt");
            var d = (a: 1, b: "trtr");
            c = d;
            c.s = d.b;
            // ... => spread => etaler les informations d'un objet dans un autre objet
           // var e=(...c,...d); // (e  n:1, s:"rt" a: 1, b: "trtr")
        }

        // Struct vs class
        struct PointStruct
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
        class PointClass
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
        [TestMethod]
        public void StrucVsClassTest()
        {
            var a = new PointClass() { X = 1, Y = 2 }; // a est une référence vers un emplacement mémoire
            var b = a; // b pointe vers le meme emplacement que a
            a.X = 3;
            var classX = b.X; //?  3

            var c=new PointStruct() { X = 1, Y = 2 }; // le struct représente des ddnnées dans la pile
            var d = c; // d contient une copie de c
            c.X = 3;
            var structX = d.X; //?  1 car c et d sont des conteneurs différents
        }

    }
}
