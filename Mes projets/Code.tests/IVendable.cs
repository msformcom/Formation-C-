using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.tests
{
    // Cette interface va obliger des classes (indépendamment de la hierachie heritage)
    // à implémenter les méthodes déclaréees
    public interface IVendable
    {
        public Decimal Prix { get; set; }
        public void Vendre();
    }
}
