using AutoMapper;
using Metier.Concession;
using Microsoft.EntityFrameworkCore;
using Persistence.BDD.DAO;
using Persistence.Models;
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
        // static => 1 seul mapper pour toutes les instances de cette class
        private static readonly IMapper mapper;

        // Constructeur static => destiné à initialiser les membres static
        // 1 seule exécution
        static PersistenceVoitureSurBDD()
        {
            var config = new MapperConfiguration(mappings =>
            {
                // Créé un mapping automatique => Propriété <=> Propriete
                mappings.CreateMap<VoitureDAO, Voiture>()
                    .ForMember(c => c.RadarRecul, o =>
                    {
                        o.MapFrom(c => c.Radar);
                    })
                    .ReverseMap();
    
            });
            mapper = config.CreateMapper();


        }



        // cette classe a besoin du ConcessionContext
        // pour géréer les données
        // C'est l'injection de dépendance qui fournira les dépendance
        public PersistenceVoitureSurBDD(ConcessionContext context)
        {
            // Ce context va être utilisé par toutes les méthodes pour cette instance
            this.context = context;
        }
        public async Task<Guid> AddVoiture(Voiture voiture)
        {



            // J'injecte dans une objet VoitureDAO Les données à sauvegarder
            // l'association des propriétés peut être fait automatiquement par un package : AutoMapper
            //var dao = new VoitureDAO();
            //dao.Marque = voiture.Marque;
            //dao.Radar = voiture.RadarRecul;
            //dao.EstDemarree = voiture.EstDemarree;
            //dao.Modele = voiture.Modele;
            //dao.NiveauCarburant = voiture.NiveauCarburant;
            //dao.Prix = voiture.Prix;
            var dao = mapper.Map<VoitureDAO>(voiture);


            var elementsTraques = context.ChangeTracker.Entries().ToList();
            // Ajout de l'objet au DbSet => Insert
            context.Voitures.Add(dao);
            elementsTraques = context.ChangeTracker.Entries().ToList();

            // Je demande au context de sauvegarder les changements
            // Connection BDD => INSERT INTO Voitures...
            await context.SaveChangesAsync();
            elementsTraques = context.ChangeTracker.Entries().ToList();

            // Le async/await => Transforme Guid => Task<Guid>
            // le return est encapsulé dans une tache
            return dao.Id;


        }

        public async Task<IEnumerable<ISearchResult<Guid>>> Find(ISearchVoitureModel filter)
        {
            //context.Voitures
            //    .Where(c => filter.PrixMin == null || c.Prix >= filter.PrixMin)
            //    .Where(c => filter.Texte == null || c.Modele.Contains(filter.Texte));
            IQueryable<VoitureDAO> vue = context.Voitures;
            // IQueryable représente une requete dans la BDD
            //vue=context.Voitures; // SELECT * FROM Voitures
            //vue = context.Voitures.Where(c => c.Prix > 1000); // SELECT * FROM Voitures WHERE Prix >1000
            //vue = vue.OrderByDescending(c => c.Prix);// SELECT * FROM Voitures WHERE Prix >1000 ORDER BY Prix DESC

            if (filter.PrixMin != null)
            {
                vue = vue.Where(v => v.Prix >= filter.PrixMin);
                // SELECT * FROM Voitures => SELECT * FROM Voitures WHERE Prix >= 1000
            }
            // Si texte renseigné, j'ajoute un filtre à vue 
            if (filter.Texte != null)
            {
                vue = vue.Where(v => v.Marque.Contains(filter.Texte));
                // SELECT * FROM Voitures => SELECT * FROM Voitures WHERE Prix >= 1000 AND Marque LIKE '%Peug%'
            }
            // vue => Ensemble de VoitureDAO

            var resultats = await vue.Select(c => new SearchResult<Guid>()
            {
                Id = c.Id,
                Libelle = c.Modele,
                Indication = c.Prix.ToString("{0:C}")
            } as ISearchResult<Guid>).ToListAsync();
            // ToListAsync => Créer la liste et la remplir avec les résultats
            // => La requète est envoyée
            return resultats;

        }

        public async Task<Voiture> GetVoiture(Guid id)
        {
            var elementsTraques = context.ChangeTracker.Entries().ToList();
            var dao = await context.Voitures.FindAsync(id);
            if (dao == null)
            {
                throw new KeyNotFoundException();
            }
            elementsTraques = context.ChangeTracker.Entries().ToList();
            // Demande de mappage du dao en voiture
            return mapper.Map<Voiture>(dao);

        }

        public async Task RemoveVoiture(Guid id)
        {
            // Je souhaite avoir un context dédié à cette méthode
            // Comment demander à l'injection de dépendance un context ici;

            var elementsTraques = context.ChangeTracker.Entries().ToList();
            // Find recherche la voiture par son id // Si le dao contient une colonne id => id
            var dao = context.Voitures.Find(id); // SELECT * FROM Voitures WHERE Id=..
            // dao fait partie des objets trackes => Surveilles
            if (dao == null)
            {
                throw new InvalidDataException();
            }
            //elementsTraques = context.ChangeTracker.Entries().ToList();

            //dao =new VoitureDAO() { Id = id }; // Créer un objet qui contient la clé
            //context.Voitures.Entry(dao).State = EntityState.Deleted;
            context.Voitures.Remove(dao);
            elementsTraques = context.ChangeTracker.Entries().ToList();
            await context.SaveChangesAsync();
            elementsTraques = context.ChangeTracker.Entries().ToList();
            // Le async/await => Transforme void => Task
        }

        public async Task SetVoiture(Guid id, Voiture voiture)
        {
            var dao = context.Voitures.Find(id); // SELECT * FROM Voitures WHERE Id=..
            // dao fait partie des objets trackes => Surveilles
            if (dao == null)
            {
                throw new InvalidDataException();
            }
            // Injecter les données de la voiture dans le dao
            mapper.Map(voiture, dao);
            await context.SaveChangesAsync();
        }
    }
}
