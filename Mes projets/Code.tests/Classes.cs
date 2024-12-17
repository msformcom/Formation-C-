using System.Diagnostics;

[TestClass]
public class ClassesTests
{
    [TestMethod]
    public void PileTas()
    {
        int a = 1;

        // A l'instanciation de la voiture, les données non nullables et sans valeurs par défaut doivent être fournies
        Voiture v = new Voiture("208", 100000M);
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


public class Voiture
{
    // Cette "fonction" sera exécutée lorsque le mot-clé new sera utilisé pour 
    // instancier un élément
    public Voiture(string modele, decimal prix)
    {
        this.Modele = modele;

        this.Prix = prix; // this = référence de l'emplacement mémoire de cet objet
    }

    // static associe la donnée à ma classe et non à l'intance
    public static decimal PrixMin=100;
    public string Marque = "Peugeot";
    public string Modele; // Pas de valeur par défaut + pas de null

    public bool? radarRecul;  // autoriser les valeurs null

    private decimal _Prix; // Valeurs d'initialisation par défuat doivent être correctes

    // Propriété
    // Permet de spécifier deux accesseurs (optionnels)
    // get permet d'obtenir la varleur (var p=voiture.Prix)
    // set permet de changer le prix ( voiture.prix=1200)
    public decimal Prix
    {
        get
        {
            return _Prix;
        }
        set
        {
            if (value < PrixMin)
            {

                throw new ArgumentException(string.Format("Le prix doit être supérieur à {0}",  PrixMin));
                throw new Exception($"Le prix doit être supérieur à {PrixMin}");
                throw new Exception("Le prix doit être supérieur à " + PrixMin);
            }
            _Prix = value;
        }
    }



}