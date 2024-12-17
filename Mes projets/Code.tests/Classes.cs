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
        v.Prix = -10000;


    }
}


class Voiture
{
    // Cette "fonction" sera exécutée lorsque le mot-clé new sera utilisé pour 
    // instancier un élément
    public Voiture(string modele, decimal prix)
    {
        this.Modele = modele;

        this.Prix = prix; // this = référence de l'emplacement mémoire de cet objet
    }
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
            if (value < 100)
            {
                value = 2000;
            }
            _Prix = value;
        }
    }



}