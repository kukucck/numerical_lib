using System;
using numerical_lib.Basic;
using numerical_lib.Interpolation;
using numerical_lib.NonlinearEquations;

class Program
{
    
    static void Main(string[] args)
    {
        TestLagrangeInterpolation();
    }
    
    #region 插值测试用例

    static void TestLagrangeInterpolation()
    {
        Point[] points = new[] {new Point(-2, 3), new Point(-1, 1), new Point(0, 1), new Point(1, 6)};
        LagrangeInterpolation lagrangeInterpolation = new LagrangeInterpolation(points);
        float value = lagrangeInterpolation.Evaluate(0.5f);
        Console.WriteLine($"result = {value}");
    }

    #endregion

    #region 非线性方程组测试用例

    static void TestSecantItar()
    {
        Function fun = (float x) => { return x * x * x + 2 * x * x - 4; };
        float value = SecantItarativeResolver.Solve(fun, 100, 120);
        Console.WriteLine($"result = {value}");
    }

    static void TestNewtonItar()
    {
        Function fun = (float x) => { return x * x * x + 2 * x * x - 4; };
        Function derivativeFun = (float x) => { return 3 * x * x + 4 * x;};

        float value = NewtonItarativeResolver.Solve(fun, derivativeFun, 100);
        Console.WriteLine($"result = {value}");
    }

    //测试不动点迭代法
    static void TestNormalItar()
    {
        Function gFunc = (float x) =>
        {
            double result = 2 * Math.Pow(1 / (2 + x), 0.5f);
            return (float) result;
            
        };
        float value = NormalIterativeResolver.Solve(gFunc, 100f);
        Console.WriteLine($"result = {value}");
    }

    //测试二分法
    static void TestDichotomySolver()
    {
        try
        {
            Function f = x => x * x * x - 4;
            float result = DichotomyResolver.Solve(f, -1f, 300);
            Console.WriteLine($"result = {result}");
        }
        finally
        {
            
        }
        
    }

    #endregion

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