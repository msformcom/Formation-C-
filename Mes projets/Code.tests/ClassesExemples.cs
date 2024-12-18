using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// sealed => interdire la réécriture dans les classes enfants
// sealed => interdire l'héritage de la classe
// virtual => autorise la réécriture d'une fonction dans une classe enfant
// abstract => classe abstraite => permet de créer une classe sans tout implémenter
//          => ne peut être instancier
// override => indique que je réécris un membre de la classe parent
// base. => permet d'accéder aux membres de la classe parent
// var v=new Dobermann()
// v.Crier() => Crier du dobermann
// ((Chien)v).Crier(); => Crier du Chien

namespace Code.tests
{
    // cette classe est juste une classe de base
    // Attention : je ne peux pas exécuter new Animal
     public abstract class Animal
    {
        public Animal(double poids)
        {
            
            Poids = poids;
        }
        public virtual string Espece { get; set; }
        public virtual double Poids { get; set; }
        public abstract string Crier();

    }

    // Chien peut hériter d'une seule classe mais
    // implémenter autant d'interface qu'on veut
    public  class Chien : Animal, IVendable
    {
        // Le constructeur de Chien doit appeler le constructeur de sa classe de base
        // equivalent jave de super(poids) dans le code du constructeur
        public Chien(double poids):base(poids)
        {
            this.Espece = "Chien";
        }


        public override string Crier()
        {
            return "ouaf";
        }

        // virtual autorise une classe enfant à faire un override
        public virtual void Mordre()
        {

        }
        #region IVendable
        public decimal Prix { get; set; }
        public void Vendre()
        {
            throw new NotImplementedException();
        }
        #endregion
    }


    public class Dobermann : Chien
    {
        public Dobermann(double poids) : base(poids)
        {

        }

        public override double Poids {
            get
            {
                return base.Poids;
            }
     
            set  {
                // Pour le dobermann, la validation du poids est différente
                if (value < 1)
                {
                    throw new ArgumentException();
                }
                base.Poids = value; 
            }
        }

        public override string Espece { 
            // Je réécris le getter de l'espèce 
            get => base.Espece+ " (méchant)"; 
            set => base.Espece = value;
        }

        public override void Mordre()
        {
            base.Mordre();
        }

        // Empécher les classes enfant de réécrire la fonction Crier
        public override sealed string Crier()
        {
            // return ((Chien)this).Crier();
            return base.Crier().ToUpper();
        }
    }

    // sealed = Aucune classe ne peut hériter de Chiwawa    //
    public sealed class Chiwawa : Chien
    {
        public Chiwawa(double poids) : base(poids)
        {

        }
        public override string Crier()
        {
            return base.Crier();
        }
    }


    public class Tele : IVendable
    {
        #region IVendable
        public decimal Prix { get; set; }
        public void Vendre()
        {
            throw new NotImplementedException();
        }
        #endregion

    }

}
