using Metier.Concession;
using System.Diagnostics;
using System.Diagnostics.Metrics;

[TestClass]
public class ClassesTests
{
    [TestMethod]
    public void PileTas()
    {
        int a = 1;

        // A l'instanciation de la voiture, les données non nullables et sans valeurs par défaut doivent être fournies
        Voiture v = new Metier.Concession.Voiture("208", 100000M);
        // On change la valeur du prix
        // Attention la valeur fournie peut générer une erreur

        try {
             v.Prix = -10000;

            Voiture.PrixMin=200;
        }
        catch(ArgumentException ex){
            Debug.WriteLine(ex.Message);
            // un message pkus user-friendly
            Console.WriteLine("Il y une erreur sur la valeur fournie pour le prix");
        }
        catch(Exception ex){
            // Agir si la valeur n'est pas acceptée
            // le message contenu dans ex n'est pas forcément destiné à être affciché
            Debug.WriteLine(ex.Message);
            // un message pkus user-friendly
            Console.WriteLine("Il y une erreur non spécifique");
        }



    }
}

