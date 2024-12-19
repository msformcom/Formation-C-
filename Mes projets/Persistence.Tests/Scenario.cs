
using Metier.Concession;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

                    #region configuration
                    // Ajout de la config à l'injecteur de dépendance
                    // J'utilise un builder pour configurer la confi
                    var builder = new ConfigurationBuilder();
                    // Ajout d'un fichier json => installer le package Microsoft.Configuration.Extensions.Json
                    builder.AddJsonFile("appsettings.json");
                    var config = builder.Build();
                    // Ajout de la dépendance IConfiguration
                    collection.AddSingleton<IConfiguration>(config);
                    #endregion



                    // Je definis les associations entre les interfaces et les classes concrètes utilisées
                    // Une demande de IPersistenceVoiture<int> entrainera la création d'une instance de 
                    // PersistenceVoitureSurDisque<int>
                    // AddSingleton => Une fois une instance créée, les autres demandes recevront cette instance
                    // AddTrasient => une nouvelle instance créée à chaque demande
                    collection.AddSingleton<IPersistenceVoiture<IdType>, PersistenceVoitureSurDisque<IdType>>();

                    collection.AddSingleton<GenerateurId<IdType>>(c => {
                            var candidat = Guid.NewGuid();
                            while (c.Keys.Contains(candidat))
                            {
                                candidat = Guid.NewGuid();
                            }
                            return candidat;
                        }
                    );
                    
                    _Di = collection.BuildServiceProvider();    
                }
                return _Di;
            }
        }


        [TestMethod]
        public async Task UserScenario()
        {
            // Je demande au provider une instance de classe qui correspond
            // a l'interface IPersistenceVoiture<int>
            IPersistenceVoiture<IdType> store = Scenario.Di.GetRequiredService<IPersistenceVoiture<IdType>>();
         
            //IPersistenceVoiture<int> store2 = Di.GetRequiredService<IPersistenceVoiture<int>>();
            var v = new Voiture("C8",2000);
            var r=await store.AddVoiture(v);
        }
    }
}
