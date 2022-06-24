using System;
using numerical_lib.Basic;

namespace numerical_lib.NonlinearEquations
{
    /// <summary>
    /// 二分法
    /// </summary>
    public class DichotomySolver
    {
        private Function _function;
        private float _a;
        private float _b;

        public DichotomySolver(Function function, float a, float b)
        {
            _function = function;
            _a = a;
            _b = b;
        }

        public float Solve()
        {
            if (_function(_a) * _function(_b) >= 0)
            {
                throw new Exception($"fun({_a}) 和 fun({_b})同号，不能用二分法");
            }
            float a = _a;
            float b = _b;
            float x;

            int itarNum = 0;
            while (true)
            {
                x = (a + b) / 2;
                float value = _function(x);
                Console.WriteLine($"fun({x}) = {value}");
                if (Math.Abs(value) <= Const.ERROR)
                {
                    Console.WriteLine($"二分法迭代次数：{itarNum}");
                    return x;
                }
                if (_function(x) * _function(a) < 0)
                {
                    b = x;
                }
                else
                {
                    a = x;
                }
                itarNum++;
                if (itarNum > 200)
                {
                    throw new Exception("无解");
                }
            }
            
            
        }
    }
}