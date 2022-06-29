using System;
using numerical_lib.Basic;

namespace numerical_lib.Interpolation
{
    /// <summary>
    /// 牛顿插值
    /// </summary>
    public class NewtonInterpolation
    {
        /// <summary>
        /// 差商表
        /// </summary>
        private float[,] _differenceQuotientTable;
        private bool[,] _differenceQuotientFlagTable;
        public Point[] points;

        public NewtonInterpolation(Point[] points)
        {
            this.points = points;
            _differenceQuotientTable = new float[points.Length , points.Length];
            _differenceQuotientFlagTable = new bool[points.Length, points.Length];
        }

        public float Evaluate(float x)
        {
            int n = points.Length - 1;
            float sum = points[0].y;
            for (int i = 1; i <= n; i++)
            {
                sum += CalcDifferenceQuotient(0, i) * GetOmega(i, x);
            }
            return sum;
        }

        public void AddPoint(Point p)
        {
            int len = points.Length + 1;
            Array.Resize(ref points, len);
            points[points.Length - 1] = p;
            float[,] oldDifferenceQuotientTable = _differenceQuotientTable;
            bool[,] oldDifferenceQuotientFlagTable = _differenceQuotientFlagTable;
            _differenceQuotientTable = new float[len , len];
            _differenceQuotientFlagTable = new bool[len, len];
            for (int i = 0; i < len - 1; i++)
            {
                for (int j = 0; j < len - 1; j++)
                {
                    _differenceQuotientTable[i, j] = oldDifferenceQuotientTable[i, j];
                    _differenceQuotientFlagTable[i, j] = oldDifferenceQuotientFlagTable[i, j];
                }
            }
        }

        /// <summary>
        /// 计算从第index个元素开始的stage阶差商
        /// </summary>
        /// <param name="index"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        private float CalcDifferenceQuotient(int index, int stage)
        {
            if (_differenceQuotientFlagTable[index, stage])
            {
                return _differenceQuotientTable[index, stage];
            }
            if (stage == 0)
            {
                _differenceQuotientTable[index, stage] = points[index].y;
                _differenceQuotientFlagTable[index, stage] = true;
                return _differenceQuotientTable[index, stage];
            }

            float sum = 0;
            for (int i = 0; i <= stage; i++)
            {
                float productValue = 1;

                for (int j = 0; j <= stage; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    productValue *= (points[index + i].x - points[index + j].x);
                }

                sum += points[index + i].y / productValue;
            }
            _differenceQuotientTable[index, stage] = sum;
            _differenceQuotientFlagTable[index, stage] = true;
            return _differenceQuotientTable[index, stage];
        }

        /// <summary>
        /// 这里的omega实际上就是(x-x0)(x-x1)...(x-x(k-1))，是公式的一部分，单独提出来一个方法
        /// </summary>
        /// <param name="n"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private float GetOmega(int n, float x)
        {
            if (n == 0)
            {
                return 1;
            }

            float productValue = 1;
            for (int i = 0; i < n; i++)
            {
                productValue *= (x - points[i].x);
            }

            return productValue;
        }
    }
}