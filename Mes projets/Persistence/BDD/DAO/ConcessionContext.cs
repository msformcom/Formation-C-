using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.BDD.DAO
{
    // L'héritage avec DbContext permet à cette classe 
    // d'avoir des méthodes qui font l'accès aux données
    public partial class ConcessionContext : DbContext
    {
        // Le constructeur demande les options à utiliser
        // elles seront fournies par l'Injection de dépendance
        // et passées au constructeur de DbContext
        public ConcessionContext(DbContextOptions options) : base(options) 
        {
            
        }



        // Cette propriété est associée avec une table 
        // par défaut : Table Voitures => colonnes = propriétés de VoitureDAO

        internal DbSet<VoitureDAO> Voitures { get; set; }
    }
}
