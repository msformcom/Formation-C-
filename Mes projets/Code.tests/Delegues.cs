[TestClass]
public class DeleguesTest{

    // Operation représente n'importe quelle fonction avec la signature 
    // (int,int)=>int
    delegate int Operation(int a, int b);

    int Soustraction(int a, int b){
        return a-b;
    }

    // OperationSurString est un nom donné à une signature
    public delegate int OperationSurString(string a);
    

    [TestMethod]
    public void Declarations(){


        // Parfois, nous devons manipuler des fonction comme des variables
        // f est un paramètre de type fonction (int) => int
        void Transform(int[] entiers, Func<int,int> f)
        {
            for(var i = 0; i < entiers.Length; i++)
            {
                var e = entiers[i];
                // e => e+1
                entiers[i] =f( entiers[i] );
            }
        };

        var tabV = new int[] { 1, 2, 3 };
        Transform(tabV, c => c + 2);






        // délégué de fonction
        Func<string,int> f;

     

        // Délégué d'action
        Action<int,int> g;

        Operation calcul=(a,b)=>a+b;
        calcul=Soustraction;

        Action a=Console.WriteLine;
      
        a();

    }
}