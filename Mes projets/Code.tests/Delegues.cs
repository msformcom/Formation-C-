[TestClass]
public class DeleguesTest{

    // 
    delegate int Operation(int a, int b);

    int Soustraction(int a, int b){
        return a-b;
    }

    

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