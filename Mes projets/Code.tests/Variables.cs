using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Code.tests;

[TestClass]
public class Variables
{

   [TestMethod]
   public void Listes()
    {
        // Classe générique : Liste qui contient des décimaux
        List<decimal> entiers=new List<decimal>(){1001,9000,7999,3000};

        // Augmenter prix prend une fonction en paramètre
        // la fonction est utilisée pour calculer le nouveau prix par rapport à l'ancien
        void AumenterPrix(Func<decimal,decimal> op){
            for(int i=0;i<entiers.Count; i++){
            
                entiers[i]=op(entiers[i]);
            }
        }

        AumenterPrix(c=>c+1);
        AumenterPrix(c=>c*2);

    }
    
    // Concat est une variable à laquelle on peut associer une fonction
    // le type de la fonction est définie par le délégue Func<string,string,string>
    Func<string,string,string> Concat=(s1,s2)=>s1+","+s2;
    [TestMethod]
    public void Declarations()
    {

        int a = 1; // déclaration eet assignation
        int b;  //Int32,Int16,Int64, UInt32; => Initialisés à 0
        b = default(int);

        Console.WriteLine(b);

        int? c;

        int d = int.MaxValue;

        unchecked
        {
            d++; // Combien vaut d int.MinValue !
        }
        if (false)
        {
            checked
            {
                d--; // Erreur car dépassement de capacité en mode check => Exception

            }
        }


    
        var f = 1; // var => mùot clé engendrant une inférence de type
        int g = f / 2;  // type inféré => pour des types simples source d'erreur

#region Nombres exacts ou pas
        double dd = 0D;
        for (int i = 0; i < 10; i++)
        {
            dd += 1D / 5D;
        }
        if (dd == 2D)
        {
            Assert.IsTrue(true);
            // On passe ici ou pas
        }

        decimal de = 0M;
        for (int i = 0; i < 10; i++)
        {
            de += 1M / 5M;
        }
        if (de == 2M)
        {
            Assert.IsTrue(true);
            // On passe ici ou pas
        }
#endregion

        int int1 = 1;

        double double1 = int1;

        int1 = (int)double1; // Erreur de compilation => non mais peut-etre erreur d'exécution si la valeur dans double1 depace la capacité du int

        double infini = 1D / -0D;  // => valeur -Infinity (pas en décimal)



        string s = "Toto";
        s = s + " et tata"; // String imutable => en mémoire toto subsiste toujours et s pointe vers un nouvel emplacement mémoire 
        // avec Toto et tata

#if DEBUG
    Debug.WriteLine(s); // Sera compilé dans l'assembly seulement en mode DEBUG
    #else
    Console.WriteLine(s); // Sera compilé autrement
#endif
    

        s = "";
        for (int n = 0; n < 1000; n++)
        {
            s += "*";   //Prend inutilement beaucoup de memoire
        }

        var sb = new StringBuilder();
        for (int n = 0; n < 1000; n++)
        {
            sb.Append("*");   // Construction de chaine bien gérée en mémoire
        }


    }
    int Addition(int a, int b = 0, int c = 1, int d = 2) // définition déclarative
    {
        return a + b;
    }

    [TestMethod]
    public void Functions()
    {
        if(true){
            Concat=(s1,s2)=>s1+";"+s2;
        }

        var r1 = Addition(1, 2);
        var r = Addition(1, c: 4, b: 7);

        // Délégué => représente un type de fonction => 2 parametres de type double => double
        Func<double, double, double> operation = (a, b) => a - b; // Operation est la fonction correspondant à une soustraction
        Func<int, int, int, int, int> f = Addition;
        operation=(a,b)=>{
            return a*b;
        };
        operation=(double a,double b)=>{
            return a+b;
        };

        var r3=operation(1,2);



        var resultat=Concat("Toto","Tata");

    }

}