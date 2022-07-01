using System;
using numerical_lib.Basic;
using numerical_lib.LinearEquations.DirectMethod;

namespace numerical_lib.Interpolation
{
    /// <summary>
    /// 三次样条插值
    /// </summary>
    public class SplineInterpolation
    {
        public Point[] points;
        public Point firstDerivative;
        public Point lastDerivative;

        private int n;//节点数量-1
        private float[,] A;//每段x和x+1区间分段3次函数的待定系数
        private float[] h;//h就是x(i+1)-x(i)
        private float[] b;//x(i)和x(i+1)的1阶差商（1阶均差）
        private float[] d;//三对角方程组的右边系数

        public SplineInterpolation(Point[] points, Point firstDerivative, Point lastDerivative)
        {
            this.points = points;
            this.firstDerivative = firstDerivative;
            this.lastDerivative = lastDerivative;
            if (points.Length == 1)
            {
                throw new Exception("只有一个点，不能插值");
            }

            n = points.Length - 1;
            A = new float[n,4];
            h = new float[n];
            b = new float[n];
            d = new float[n+1];
            this.Construct();
        }

        public float Evaluate(float x)
        {
            for (int i = n-1; i >= 0; i--)
            {
                if (points[i].x < x)
                {
                    return GetS(i, x);
                }
            }
            throw new Exception("不在模拟范围内");
        }

        private float GetS(int i, float x)
        {
            double sum = 0;
            for (int k = 0; k < 4; k++)
            {
                sum += A[i, k] * Math.Pow((x - points[i].x), k);
            }        
            return (float) sum;
        }

        private void Construct()
        {
            for (int i = 0; i < n; i++)
            {
                h[i] = points[i + 1].x - points[i].x;
                b[i] = (points[i + 1].y - points[i].y) / h[i];
            }
            
            //根据公式，写出三对角方程组的右端项d
            d[0] = b[0] - firstDerivative.y;
            d[n] = lastDerivative.y - b[n - 1];

            for (int i = 1; i < n; i++)
            {
                d[i] = b[i] - b[i - 1];
            }
            
            //构造A矩阵
            MatrixN _A = new MatrixN(n+1);
            _A.Set(0,0,2*h[0]);
            for (int i = 1; i < n+1; i++)
            {
                _A.Set(i - 1,i, h[i-1]);
                _A.Set(i,i - 1, h[i-1]);
                if (i == n)
                {
                    _A.Set(n, n, 2 * h[n-1]);
                }
                else
                {
                    _A.Set(i,i, 2 * (h[i - 1] + h[i]));
                }
            }
            
            VectorN _b = new VectorN(d) * 6;
            TridiagonalResolver resolver = new TridiagonalResolver(_A, _b);
            VectorN result = resolver.Solve();
            float[] m = result.items;//根据公式，m为三对角方程组的根，它也是S在每个xi上的二阶导
            for (int i = 0; i < n; i++)
            {
                //以下4行套公式。公式的推导见（数值计算方法第二版，上册，林成森，Page 176-177）
                A[i, 0] = points[i].y;
                A[i, 1] = b[i] - h[i] / 6 * (m[i + 1] + 2 * m[i]);
                A[i, 2] = m[i] / 2;
                A[i, 3] = (m[i + 1] - m[i]) / (6 * h[i]);
            }
        }
    }
}