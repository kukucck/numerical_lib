using System;
using numerical_lib.Basic;

namespace numerical_lib.NonlinearEquations
{
    /// <summary>
    /// 弦截法
    /// </summary>
    public static class SecantItarativeResolver
    {
        public static float Solve(Function fun, float beginX1, float beginX2)
        {
            float p0 = beginX1;
            float p1 = beginX2;
            float q0 = fun(p0);
            float q1 = fun(p1);
            
            int itarNum = 0;
            while (true)
            {
                float p = p1 - q1 * (p1 - p0) / (q1 - q0);
                Console.WriteLine($"Func(x+1) = {p}");
                if (Math.Abs(p - p1) <= Const.ERROR)
                {
                    Console.WriteLine($"弦截迭代次数：{itarNum}");
                    return p;
                }

                p0 = p1;
                q0 = q1;
                p1 = p;
                q1 = fun(p);
                itarNum++;
                if (itarNum > Const.MAX_ITAR_NUM)
                {
                    throw new Exception("无解");
                }
            }
        }
    }
}