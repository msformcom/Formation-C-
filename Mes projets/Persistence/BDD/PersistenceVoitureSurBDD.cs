using Metier.Concession;
using Persistence.BDD.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.BDD
{
    public class PersistenceVoitureSurBDD : IPersistenceVoiture<Guid>
    {
        private readonly ConcessionContext context;


        // cette classe a besoin du ConcessionContext
        // pour géréer les données
        // C'est l'injection de dépendance qui fournira les dépendance
        public PersistenceVoitureSurBDD(ConcessionContext context)
        {
            this.context = context;
        }
        public async Task<Guid> AddVoiture(Voiture voiture)
        {
            // J'injecte dans une objet VoitureDAO Les données à sauvegarder
            // l'association des propriétés peut être fait automatiquement par un package : AutoMapper
            var dao = new VoitureDAO();
            dao.Marque = voiture.Marque;
            dao.Radar = voiture.radarRecul;
            dao.EstDemarree = voiture.EstDemarree;
            dao.Modele = voiture.Modele;
            dao.NiveauCarburant = voiture.NiveauCarburant;
            dao.Prix = voiture.Prix;

            // Ajout de l'objet au DbSet => Insert
            context.Voitures.Add(dao);

            // Je demande au context de sauvegarder les changements
            // Connection BDD => INSERT INTO Voitures...
            await context.SaveChangesAsync();
            
            return dao.Id;


        }

        public Task<IEnumerable<ISearchResult<Guid>>> Find(ISearchVoitureModel filter)
        {
            //context.Voitures
            //    .Where(c => filter.PrixMin == null || c.Prix >= filter.PrixMin)
            //    .Where(c => filter.Texte == null || c.Modele.Contains(filter.Texte));
            IQueryable<VoitureDAO> vue = context.Voitures.AsQueryable();
            // IQueryable représente une requete dans la BDD
            vue=context.Voitures; // SELECT * FROM Voitures
            //vue = context.Voitures.Where(c => c.Prix > 1000); // SELECT * FROM Voitures WHERE Prix >1000
            //vue = vue.OrderByDescending(c => c.Prix);// SELECT * FROM Voitures WHERE Prix >1000 ORDER BY Prix DESC

            if (filter.PrixMin != null)
            {
                vue = vue.Where(v => v.Prix >= filter.PrixMin);
            }
            // Si texte renseigné, j'ajoute un filtre à vue 
            if (filter.Texte != null)
            {
                vue = vue.Where(v => v.Marque.Contains(filter.Texte));
            }
            // 



        }

        public Task<Voiture> GetVoiture(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveVoiture(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SetVoiture(Guid id, Voiture voiture)
        {
            throw new NotImplementedException();
        }
    }
}
