using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.BDD.DAO
{
    // Cet objet est un DAO (Data Access Object)
    // Il représente dans notre code les données présentes dans une table
    internal class VoitureDAO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Modele { get; set; }

        public string Marque { get; set; }

        public Decimal Prix { get; set; }
        public bool? Radar { get; set; }

        public bool EstDemarree { get; set; } = false;

        public double NiveauCarburant { get; set; }

        public DateTime DateCreation { get; set; } =DateTime.Now;

        public bool Supprimee { get; set; } = false;
    }
}
