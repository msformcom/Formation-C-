using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Timers;

namespace Metier.Concession;
// partial = possibilité d'eécrire du code dans plusieurs fichiers

// Attribut DataContract => Permet decustomiser la sérialisation
[DataContract]
public partial class Voiture
{
	// Demande découpe planche client : 200mm => Decimal
	// Mesure de la découpe par sensor : 200.14 => Double
	// Le cast (double)d ou (decimal)d lors des comparaisons
	// va permettre de prendre conscience de l'imprécision des valeurs mesurées
	// et d'inclure une marge 

	public event EventHandler AlerteJaugeCarburant;



	#region Propriété CapaciteReservoir

	// DataMember inclut le champs dans la sérialisation
	// json {"cr"=5}
	[DataMember(Name ="cr")]
    private Decimal _CapaciteReservoir;

	public Decimal CapaciteReservoir
	{
		get { return _CapaciteReservoir; }
		set
		{
			// TODO : Check value
			if (value <= 0)
			{
				throw new ArgumentException("Impossible");
			}
			_CapaciteReservoir = value;
		}
	}
    #endregion


    #region Propriété NiveauCarburant

    [DataMember(Name = "nc")]
    private double _NiveauCarburant;

	public double NiveauCarburant
	{
		get { return _NiveauCarburant; }
		set
		{
			if((decimal)value > CapaciteReservoir)
			{
                throw new ArgumentException("Impossible");
            }
			if (value <= 0)
			{
				this.Arreter();
				value = 0;
			}
			if(value<(double)this.CapaciteReservoir*0.2 
					&& _NiveauCarburant> (double)this.CapaciteReservoir * 0.2)
			{
				// On passe d'un niveau > 20% à un niveau <20%
				// Déclencher l'exécution des fonctions associées à AlerteJaugeCarburant
				// tester si des fonctions sont associées à cet évenment
				if (AlerteJaugeCarburant != null)
				{
					// Décleche l'exécution des fonctions
					// en passant des infos
					// o=> l'objet qui déclenche l'évenement
					// e=> EventArgs => Infos sur l'évènement
					AlerteJaugeCarburant(this, EventArgs.Empty);
				}
            }

            _NiveauCarburant = value;
		}
	}
	#endregion


	public double FaireLePlein()
	{
		var nbLitres = (double)CapaciteReservoir - NiveauCarburant;
		this.NiveauCarburant = this.NiveauCarburant + nbLitres;
		return nbLitres;
	}

    #region Propriété EstDemarree

    [DataMember(Name = "ed")]
    private bool _EstDemarree;

	public bool EstDemarree
	{
		get { return _EstDemarree; }
		protected set
		{
			// TODO : Check value
			_EstDemarree = value;
		}
	}
	#endregion


	private Timer T = new Timer() { 
		Interval=1000 
		
	};

	/// <summary>
	/// Demarre la voiture
	/// </summary>
	/// <param name="a">Juste pour la démo</param>
	/// <exception cref="InvalidOperationException"></exception>
	public void Demarrer(int a=0)
    {
		if (EstDemarree) {
			throw new InvalidOperationException("GRRRRR");
		}
		if (this.NiveauCarburant < 0.01D) {
            throw new InvalidOperationException("VVVVVV");
        }
		EstDemarree = true;

		T.Start();
		// Elapsed est un évènement
		// On peut lui associer des fonctions à exécuter
		// T dit quand
		// Le gestionaire dit ce qui doit être fait
		//T.Elapsed += (o, e) => { Debug.WriteLine("Consommation"); };

    }

	public void Arreter()
	{
		this.EstDemarree = false;
		T.Stop();

	}
}