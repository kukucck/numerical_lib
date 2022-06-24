using System;
using numerical_lib.Basic;

class Program
{
    static void Main(string[] args)
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