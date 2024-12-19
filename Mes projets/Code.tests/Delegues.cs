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