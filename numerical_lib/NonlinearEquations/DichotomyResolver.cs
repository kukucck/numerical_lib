using System;
using numerical_lib.Basic;

namespace numerical_lib.NonlinearEquations
{
    /// <summary>
    /// 二分法
    /// </summary>
    public static class DichotomyResolver
    {
        public static float Solve(Function function, float a, float b)
        {
            if (function(a) * function(b) >= 0)
            {
                throw new Exception($"fun({a}) 和 fun({b})同号，不能用二分法");
            }
            float x;

            int itarNum = 0;
            while (true)
            {
                x = (a + b) / 2;
                float value = function(x);
                Console.WriteLine($"fun({x}) = {value}");
                if (Math.Abs(value) <= Const.ERROR)
                {
                    Console.WriteLine($"二分法迭代次数：{itarNum}");
                    return x;
                }
                if (function(x) * function(a) < 0)
                {
                    b = x;
                }
                else
                {
                    a = x;
                }
                itarNum++;
                if (itarNum > Const.MAX_ITAR_NUM)
                {
                    throw new Exception("无解");
                }
            }
            
            
        }
    }
}