using System;
using numerical_lib.Basic;

namespace numerical_lib.NonlinearEquations
{
    /// <summary>
    /// 牛顿法
    /// </summary>
    public static class NewtonItarativeResolver
    {
        public static float Solve(Function fun, Function derivative, float beginX)
        {
            float x = beginX;
            
            int itarNum = 0;
            while (true)
            {
                float derivativeValue = derivative(x);
                if (Math.Abs(derivativeValue) < 0.0000001f)
                {
                    throw new Exception("导数为0");
                }
                float p = x -  fun(x) / derivativeValue;
                Console.WriteLine($"Func(x+1) = {p}");
                if (Math.Abs((p - x) / p) <= Const.ERROR)
                {
                    Console.WriteLine($"牛顿迭代次数：{itarNum}");
                    return x;
                }
                x = p;
                itarNum++;
                if (itarNum > Const.MAX_ITAR_NUM)
                {
                    throw new Exception("无解");
                }
            }
        }
    }
}