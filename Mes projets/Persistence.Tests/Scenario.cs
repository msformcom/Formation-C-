
using Metier.Concession;
using Persistence.Disque;

namespace Persistence.Tests
{
    [TestClass]
    public class Scenario
    {
        [TestMethod]
        public async Task UserScenario()
        {
            IPersistenceVoiture<int> store = new PersistenceVoitureSurDisque<int>() ;

            var v = new Voiture("C8",2000);
            var r=await store.AddVoiture(v);


        }
    }
}
