[TestClass]
public class Tp
{

    [TestMethod]
    public void TP1Filter()
    {




        var entiers = new List<int>() { 1, 8, 3, 6, 3, 5, 9 };
        var liste1=entiers.Where(c=>c<6).OrderBy(c=>Math.Cos(c)).Reverse().Skip(15).Take(5).Sum();

        // Fonction générique
        // Cette fonction travaille sur une liste de type T
        // T est le type des éléments de la liste
        // La fonction filtre est un paramètre
        // filtre prends un élément de la liste de type T et le trasnforme en bool
        // pour déterminer d'il fait partie des résultats 
        List<T> Filter<T>(List<T> liste, Func<T, bool> filtre)
        {
            var resultat = new List<T>();
            for (int i = 0; i < liste.Count; i++)
            {
                if (filtre(liste[i]))
                {
                    resultat.Add(liste[i]);
                }
            }
            return resultat;
        }


        // Func<int,bool>
        bool FiltreA(int c){
            return c>10;
        }

        var petitsEntiers = Filter(entiers, c => c < 6); // new List() => .Add => seulement si c<6
        var grosEntier=Filter(entiers,FiltreA);

        var chaines=new List<string>(){"toto","tata","tututu"};
        var chainesPetites=Filter(chaines,c=>c.Length<=4);
    }

}