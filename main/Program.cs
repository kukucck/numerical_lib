using System;
using numerical_lib.Basic;
using numerical_lib.NonlinearEquations;

class Program
{
    static void Main(string[] args)
    {
        TestDichotomySolver();
    }

    //测试二分法
    static void TestDichotomySolver()
    {
        Function f = x => x * x * x - 4;
        DichotomySolver solver = new DichotomySolver(f, -1f, 300);
        try
        {
            float result = solver.Solve();
            Console.WriteLine($"result = {result}");
        }
        finally
        {
            
        }
        
    }

    static void TestMatrix()
    {
        MatrixN a = new MatrixN(new []{1f,2f,2f,1f,5f,0f,2f,3f,9f});
        MatrixN b = new MatrixN(new []{0f,2f,2f,1f,1f,4f,3f,6f,8f});
        
        VectorN v = new VectorN(new []{0f, 1f, 2f});

        MatrixN c = a * b;
        Console.WriteLine(a);
        Console.WriteLine(b);
        Console.WriteLine(c);
        
        Console.WriteLine("-----------------------");
        Console.WriteLine(a);
        Console.WriteLine(v);
        Console.WriteLine(a * v);
    }
}