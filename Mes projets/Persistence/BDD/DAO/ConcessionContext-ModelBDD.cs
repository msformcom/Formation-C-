using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer;

namespace Persistence.BDD.DAO
{
     partial class ConcessionContext
    {
        // Fonction du DbContext 
        // Destination : Configurer la BDD
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Config de la table associée à VoitureDAO
            modelBuilder.Entity<VoitureDAO>(options =>
            {
                // Clé de la table
                options.HasKey(c => c.Id);
                // Nom de la colonne pour la propriété Id
                options.Property(c => c.Id).HasColumnName("PK_Voiture");

                // En installant Microsft.EntityFrameworkCore.SqlServer
                options.ToTable("TBL_Voitures");

                // Longueur de la colonne
                options.Property(c => c.Marque).HasMaxLength(20);
                options.Property(c => c.Modele).HasMaxLength(20).IsRequired();
                options.Property(c => c.Prix).IsRequired();

                // Ajout d'un index permettant de optimiser les recherche par prix
                options.HasIndex(c => c.Prix);
            });

        }
    }
}
