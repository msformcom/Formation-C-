
using Metier.Concession;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence.BDD;
using Persistence.BDD.DAO;
using Persistence.Disque;
using IdType = System.Guid;
namespace Persistence.Tests
{


    [TestClass]
    public class Scenario
    {
        static IServiceProvider _Di = null;
        static IServiceProvider Di
        {
            get
            {
                
                if (_Di == null)
                {
                    // Créer un provider
                    var collection = new ServiceCollection();
                    #region Logging
                    // AddLogging => Fonction extension dans le package Extensions.logging
                    collection.AddLogging(builder =>
                    {
                        // le builder nous permet de choisir comment logger
                        // en ajoutant des packages spécifiques pour chaque cible
                        // des messages de log
                        builder.AddDebug();
                    });
                    #endregion

                    #region Configuration
                    // Ajout de la config à l'injecteur de dépendance
                    // J'utilise un builder pour configurer la confi
                    var builder = new ConfigurationBuilder();
                    // Ajout d'un fichier json => installer le package Microsoft.Configuration.Extensions.Json
                    builder.AddJsonFile("appsettings.json");
                    var config = builder.Build();
                    // Ajout de la dépendance IConfiguration
                    collection.AddSingleton<IConfiguration>(config);
                    #endregion

                    #region Persistence

                    // Je definis les associations entre les interfaces et les classes concrètes utilisées
                    // Une demande de IPersistenceVoiture<int> entrainera la création d'une instance de 
                    // PersistenceVoitureSurDisque<int>
                    // AddSingleton => Une fois une instance créée, les autres demandes recevront cette instance
                    // AddTrasient => une nouvelle instance créée à chaque demande
                    collection.AddSingleton<IPersistenceVoiture<IdType>, PersistenceVoitureSurBDD>();

                    // Cette méthode d'extension ajoute le ConcessionContext dans la liste des services
                   
                    // collection.AddDbContextFactory si je veux maitriser plus finement dans mes méthodes
                    // la création des contexte (pour éviter des confilts entre différents appels)
                    collection.AddDbContext<ConcessionContext>(builder =>
                    {
                        // cette fonction me permet d'utiliser un builder pour specifier les options
                        // UseSqlServer => Fonction extension qui
                        // paramètre les options avec un provider pour SQLServer
                        // Avec une chaine de connection présente dans la config
                        // avec le nom ConcessionConnectionString
                        builder.UseSqlServer("name=ConcessionConnectionString");
                    });



                    collection.AddSingleton<GenerateurId<IdType>>(c => {
                            var candidat = Guid.NewGuid();
                            while (c.Keys.Contains(candidat))
                            {
                                candidat = Guid.NewGuid();
                            }
                            return candidat;
                        }
                    );
                    #endregion

                    _Di = collection.BuildServiceProvider();    
                }
                return _Di;
            }
        }

        class SearchVoitureModel : ISearchVoitureModel
        {
            public Decimal? PrixMin { get; set; }
            public string? Texte { get; set; }
        }

        [TestMethod]
        public async Task UserScenario()
        {

            //
            Di.GetRequiredService<ConcessionContext>().Database.EnsureDeleted();
            Di.GetRequiredService<ConcessionContext>().Database.EnsureCreated();

            // Je demande au provider une instance de classe qui correspond
            // a l'interface IPersistenceVoiture<int>
            IPersistenceVoiture<IdType> store = Scenario.Di.GetRequiredService<IPersistenceVoiture<IdType>>();
         
            //IPersistenceVoiture<int> store2 = Di.GetRequiredService<IPersistenceVoiture<int>>();
      
            // SCRUD  : CREATE
            await store.AddVoiture(new Voiture("C8",2000));
            await store.AddVoiture(new Voiture("C3", 300));
            await store.AddVoiture(new Voiture("208", 6000));


            var idVoitureASupprimer=await store.AddVoiture(new Voiture("Panda", 100));

            var v = await  store.GetVoiture(idVoitureASupprimer);

            v.Prix++;

            await store.SetVoiture(idVoitureASupprimer, v);

            // Acceder au logger via l'injection de dépendance
            var logger = Di.GetRequiredService<ILogger<Scenario>>();
            logger.LogWarning("BDD Remplie");



            // SCRUD : DELETE

            await store.RemoveVoiture(idVoitureASupprimer);


            // SCRUD : SEARCH
            var recherche = await store.Find(new SearchVoitureModel() { PrixMin = 1000 });
            // recherche est obtenu de la part du service
            // mais le filtre n'est pas encore appliqué

            var liste = recherche.Take(2) // Limitation du filtrage à l'obtention de 2 élément
                .ToList();

            Assert.IsTrue(liste.Count == 2);
        }
    }
}
