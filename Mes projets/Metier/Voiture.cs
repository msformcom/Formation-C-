using System;
using System.Diagnostics;
using System.Runtime.Serialization;

// Class
// Champs => stocker une information en mémoire
// Propriété => get set => donne ou permet d'accepter en validant des infos
// Methodes => Realiser des actions qui correspondent à la classe
// Constructeur => Initialiser la classe 
// Evenements => 

namespace Metier.Concession;
public partial class Voiture
{
    // Ce constructeur est destiné au sérializer
    // pour construire l'objet avant d'injecter les valeur des champs
    private Voiture()
    {

    }
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


    [DataMember(Name = "ma")]
    public string Marque = "Peugeot";
    [DataMember(Name = "mo")]
    public string Modele; // Pas de valeur par défaut + pas de null
    [DataMember(Name = "rr")]
    public bool? RadarRecul;  // autoriser les valeurs null



    [DataMember(Name = "p")]
    private decimal _Prix; // Valeurs d'initialisation par défuat doivent être correctes


    // cette classe sert à transporter les données relatives à l'évènement  AlertBaissePrix
    // Cette classe serait dans un fichier séparé
    // : EventArgs indique un héritage => java extends
    public class BeforeBaissePrixEventArgs : EventArgs
    {
        public Decimal PrixAvant { get; set; }
        public Decimal PrixApres { get; set; }
    }

    // EventHandler<int> => void (object? sender, int e)

    // Déclaration de l'évènement
    public static event EventHandler<BeforeBaissePrixEventArgs> BeforeBaissePrix;

    // protected => visible dans les classes héritées
    // virtual => dans les classes héritées, cette méthode pourra être réécrite
    protected virtual void OnBeforeBaisseOPrix(decimal prixAvant,decimal prixApres)
    {
        if (BeforeBaissePrix != null)
        {
            // Comme les fonctions gestionnaires auront besoin de prixAvant et prixApres
            // encapsules dans AlertBaissePrixEventArgs
            BeforeBaissePrix(this, new BeforeBaissePrixEventArgs() { PrixApres=prixApres,PrixAvant=prixAvant});
        }
    }

    // Propriété
    // Permet de spécifier deux accesseurs (optionnels)
    // get permet d'obtenir la varleur (var p=voiture.Prix)
    // set permet de changer le prix ( voiture.prix=1200)
    
    /// <summary>
    /// Change le prix
    /// </summary>
    /// <exception cref="InvalidOperationException">Si le prix n'est pas valide</exception>
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
         set
        {
            if (value < PrixMin)
            {

                throw new ArgumentException(string.Format("Le prix doit être supérieur à {0}", PrixMin));
                throw new Exception($"Le prix doit être supérieur à {PrixMin}");
                throw new Exception("Le prix doit être supérieur à " + PrixMin);
            }
            if (value < _Prix)
            {
               // Je déclenche l'évènement en fournissant les valeurs avant et après
               // Les gestionnaires associés peuvent déclencher une erreur
               // Libre à moi de gérer les erreurs ici
               // ou de les laisser interrompre l'opération
                OnBeforeBaisseOPrix(_Prix,value);
                _Prix = value;
            }
            else
            {
                _Prix = value;
            }
           
        }
    }



}

