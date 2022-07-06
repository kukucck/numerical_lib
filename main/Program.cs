using System;
using System.Drawing.Drawing2D;
using numerical_lib.Basic;
using numerical_lib.Integration;
using numerical_lib.Interpolation;
using numerical_lib.LinearEquations.DirectMethod;
using numerical_lib.NonlinearEquations;

class Program
{
    
    static void Main(string[] args)
    {
        TestRomberg();
        // TestTridiagonalResolver();
    }
    
    #region 插值测试用例

    static void TestSplineInterpolation()
    {
        Point[] points = new[]
        {
            new Point(0, 0),
            new Point(2, 16),
            new Point(4, 36),
            new Point(6, 54),
            new Point(10, 82)
        };
        SplineInterpolation splineInterpolation = new SplineInterpolation(points, new Point(0,8), new Point(10,7));
        float value = splineInterpolation.Evaluate(3f);
        Console.WriteLine($"result1 = {value}");
        value = splineInterpolation.Evaluate(8f);
        Console.WriteLine($"result2 = {value}");
    }
    
    static void TestHermiteInterpolation()
    {
        Point[] points = {new Point(1.3f, 0.6200860f), new Point(1.6f, 0.4554022f), new Point(1.9f, 0.2818186f)};
        Point[] derivativePoints = {new Point(1.3f, -0.5220232f), new Point(1.6f, -0.5698959f), new Point(1.9f, -0.5811571f)};
        HermiteInterpolation hermiteInterpolation = new HermiteInterpolation(points, derivativePoints);
        float value = hermiteInterpolation.Evaluate(1.5f);
        Console.WriteLine($"result = {value}");
    }

    static void TestLagrangeInterpolation()
    {
        // Point[] points = new[] {new Point(-2, 3), new Point(-1, 1)};
        Point[] points = new[] {new Point(-2, 3), new Point(-1, 1), new Point(0, 1), new Point(1, 6)};
        LagrangeInterpolation lagrangeInterpolation = new LagrangeInterpolation(points);
        float value = lagrangeInterpolation.Evaluate(0.5f);
        Console.WriteLine($"result = {value}");
    }
    
    static void TestNewtonInterpolation()
    {
        Point[] points = new[] {new Point(-2, 3), new Point(-1, 1)};
        // Point[] points = new[] {new Point(-2, 3), new Point(-1, 1), new Point(0, 1), new Point(1, 6)};
        NewtonInterpolation newtonInterpolation = new NewtonInterpolation(points);
        float value = newtonInterpolation.Evaluate(0.5f); //先用两个点插值
        Console.WriteLine($"result1 = {value}");
        newtonInterpolation.AddPoint(new Point(0, 1));//添加第3个点
        value = newtonInterpolation.Evaluate(0.5f);
        Console.WriteLine($"result2 = {value}");
        newtonInterpolation.AddPoint(new Point(1, 6));//添加第4个点
        value = newtonInterpolation.Evaluate(0.5f);
        Console.WriteLine($"result3 = {value}");
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

    #region 线性方程组直接法

    static void TestLuResolver()
    {
        // MatrixN A = new MatrixN(new []
        // {
        //     1f, -2f, -1,
        //     1f, 1f, 2f, 
        //     1f, -1f, 1f,
        // });
        // VectorN b = new VectorN(new []{1f, -1f, -1f});
        
        MatrixN A = new MatrixN(new []
        {
            1f, 2f, -1, 1,
            1f, 1f, 2f, -1f,
            3f, -1f, 1f, 1f,
            2f, 1f, 3f, -1f
        });
        VectorN b = new VectorN(new []{1f, -2f, 6f, -1f});

        VectorN result = LUResolver.Resolve(A, b);
        Console.WriteLine(result);
    }

    static void TestTridiagonalResolver()
    {
        MatrixN A = new MatrixN(new []
        {
            2f, -1f, 0,0,
            -1f, 2f, -1f, 0,
            0, -1f, 2f, -1f,
            0,0, -1f, 2f
        });
        VectorN b = new VectorN(new []{1f,0,0, 1f});
        TridiagonalResolver resolver = new TridiagonalResolver(A, b);
        VectorN result = resolver.Solve();
        Console.WriteLine(result);
    }

    #endregion

    #region 数值积分测试

    static void TestRomberg()
    {
        float result = 0;
        int itarNum = 128;
        float start = 0;
        float end = 1;
        int fixedNum = 16;
        Function f = (float x) =>
        {
            float result1 = (float) (Math.Log(1 + x) / (1 + x * x));
            return result1;
        };
        RombergMethod r = new RombergMethod(f);
        result = r.Resolve(start, end);
        Console.WriteLine("result = " + result);
    }

    #endregion

    #region 基础测试（矩阵等）

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
    
    static void TestMatrixMN()
    {
        MatrixMN a = new MatrixMN(3,2,new []{1f,0f,3f,0f,2f,1f});
        a.SwapCol(0,1);
        MatrixMN b = new MatrixMN(2,3,new []{1f,0f,3f,0f,2f,1f});
        
        VectorN v = new VectorN(new []{0f, 1f, 2f});

        MatrixMN c = a * b;
        Console.WriteLine(a);
        Console.WriteLine(b);
        Console.WriteLine(c);
        
        Console.WriteLine("-----------------------");
        Console.WriteLine(a);
        Console.WriteLine(v);
        Console.WriteLine(a * v);
    }

    #endregion
}