using System;
using System.Diagnostics;

namespace Metier.Concession;
public partial class Voiture
{
    // Cette "fonction" sera exécutée lorsque le mot-clé new sera utilisé pour 
    // instancier un élément
    public Voiture(string modele, decimal prix)
    {
        // Le Timer est en marche si la voiture est démarée
        // Le niveau de carburant décroit toutes les secondes
        T.Elapsed += (o, e) =>
        {
            this.NiveauCarburant -= 1;
            Debug.WriteLine("Consommation");

        };
        this.Modele = modele;

        this.Prix = prix; // this = référence de l'emplacement mémoire de cet objet
    }

    // static associe la donnée à ma classe et non à l'intance
    public static decimal PrixMin = 100;
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
        // Modificateurs d'accès
        // public => accessible partout
        // private => dans le code de la classe
        // protected => dans la classe + classes héritées
        // internal => dans le projet
        private set
        {
            if (value < PrixMin)
            {

                throw new ArgumentException(string.Format("Le prix doit être supérieur à {0}", PrixMin));
                throw new Exception($"Le prix doit être supérieur à {PrixMin}");
                throw new Exception("Le prix doit être supérieur à " + PrixMin);
            }
            _Prix = value;
        }
    }



}

