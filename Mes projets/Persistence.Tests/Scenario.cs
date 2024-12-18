
using Metier.Concession;

namespace Persistence.Tests
{
    [TestClass]
    public sealed class Scenario
    {
        [TestMethod]
        public async Task UserScenario()
        {
            IPersistenceVoiture<int> store=null;

            var v = new Voiture("C8",2000);
            var r=await store.AddVoiture(v);


        }
    }
}
