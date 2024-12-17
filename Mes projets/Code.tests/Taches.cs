using System.Diagnostics;

namespace Code.tests;

[TestClass]
public class Taches
{

    public int Addition(int a,int b)
    {
        return a + b;
    }

    // On considère cette fonction comme Asynchrone
    // le retour de cette fonction est une Task
    // l'objet Task va permettre de gérer le temps d'exécution de cette fonction
    public async  Task<int> AdditionAsync(Int64 a, Int64 b)
    {
        int r = 0;
        for (Int64 i = 0; i < a; i++) r++;
        for (Int64 i = 0;i < b; i++) r++;
        return r;
    }

    void GereFinTache(Task<int> tache)
    {
        if (!tache.IsFaulted)
        {
            Debug.WriteLine("Le résultat eqt " + tache.Result);
        }
        else
        {
            Debug.WriteLine("Erreur pendant l'addition");
        }
    }

    [TestMethod]
    public async Task Attente()
    {
        // t est une task
        // t représente l'exécution de la fonction passée en paramètre
        // t s'exécute de façon parrallele à mon code
        var t = Task.Run(() =>
        {
            // code long 
            int r = 0;
            for (Int64 i = 0; i < 10000000000; i++)
            {
                r++;
            }
            Debug.WriteLine("Fin du code parrallele");
            return r;
        });

        Debug.WriteLine("Ca continue");
        t.ContinueWith(tache => {
            if (!tache.IsFaulted)
            {
                Debug.WriteLine("Le résultat eqt "+tache.Result);
            }
            else
            {
                Debug.WriteLine("Erreur pendant l'addition");
            }
        });
        Debug.WriteLine("Ca continue encore");

        // Le thread entrant va être inactif pendant toute la duree de l'attention
        //t.Wait();
        var r = await t;


    }
}
