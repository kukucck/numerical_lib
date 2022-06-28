using System;
using numerical_lib.Basic;

namespace numerical_lib.NonlinearEquations
{
    /// <summary>
    /// 不动点迭代法
    /// </summary>
    public static class NormalIterativeResolver
    {
        public static float Solve(Function gFunction, float beginX)
        {
            float x = beginX;
            
            int itarNum = 0;
            while (true)
            {
                float p = gFunction(x);
                Console.WriteLine($"g(x) = {p}");
                if (Math.Abs((p - x) / p) <= Const.ERROR)
                {
                    Console.WriteLine($"不动点迭代次数：{itarNum}");
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