namespace Metier.Concession;

// partial = possibilité d'eécrire du code dans plusieurs fichiers
public partial class Voiture
{


	#region Propriété EstDemarree

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
		EstDemarree = true;
    }

	public void Arreter()
	{
		this.EstDemarree = false;
	}
}