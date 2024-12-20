using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.tests
{
    [TestClass]
    public  class FunctionTests
    {
        // autres permet de recevoir des entiers supplémentaires
        int Addition(int a, int b,params int[] autres)
        {
            return a + b + autres.Sum();
        }


        [TestMethod]
        public void Params()
        {
            var r = Addition(1, 2);
            r = Addition(1, 2, 7, 6, 5, 4); // autres=[7, 6, 5, 4]
            var tab = new int[] { 5, 7, 8 };
            r = Addition(1, 2, tab);
        }
    }
}
