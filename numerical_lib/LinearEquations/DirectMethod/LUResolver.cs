using System;
using numerical_lib.Basic;

namespace numerical_lib.LinearEquations.DirectMethod
{
    /// <summary>
    /// LU分解法（Crout）
    /// </summary>
    public static class LUResolver
    {

        public static VectorN Resolve(MatrixN A, VectorN b)
        {
            int m = A.MaxOfCol(0);
            float maxValue = A.Get(m, 0);
            if (Math.Abs(maxValue) < Const.FLOAT_EQUAL)
            {
                throw new Exception("A is singular,A是奇异矩阵");
            }

            if (m != 0)
            {
                A.SwapRow(m, 0);
                b.SwapValue(m, 0);
            }

            for (int j = 1; j < A.dimension; j++)
            {
                float newValue = A.Get(0, j) / A.Get(0, 0);
                A.Set(0, j, newValue);
            }

            for (int k = 1; k < A.dimension - 1; k++)
            {
                for (int i = k; i < A.dimension; i++)
                {
                    float sum = 0;
                    for (int r = 0; r <= k-1; r++)
                    {
                        float newValue = A.Get(i, r) * A.Get(r, k);
                        sum += newValue;
                    }
                    A.Set(i, k, A.Get(i, k) - sum);
                }

                m = A.MaxOfCol(k);
                maxValue = A.Get(m, k);
                if (Math.Abs(maxValue) < Const.FLOAT_EQUAL)
                {
                    throw new Exception("A is singular,A是奇异矩阵");
                }

                if (m != k)
                {
                    A.SwapRow(m,k);
                    b.SwapValue(m, k);
                }

                for (int j = k+1; j < A.dimension; j++)
                {
                    float sum = 0;
                    for (int r = 0; r <= k-1; r++)
                    {
                        sum += A.Get(k, r) * A.Get(r, j);
                    }
                    A.Set(k, j, (A.Get(k, j) - sum) / A.Get(k, k));
                }
            }
            
            float sum1 = 0;
            int maxIndex = A.dimension - 1;
            for (int r = 0; r < maxIndex; r++)
            {
                sum1 += A.Get(maxIndex, r) * A.Get(r, maxIndex);
            }
            A.Set(maxIndex, maxIndex, A.Get(maxIndex,maxIndex) - sum1);

            if (Math.Abs(Math.Abs(A.Get(maxIndex, maxIndex))) < Const.FLOAT_EQUAL)
            {
                throw new Exception("A is singular,A是奇异矩阵");
            }
            
            //LU分解完成，开始解方程
            
            // Console.WriteLine("LU分解完成，LU矩阵如下，开始解方程");
            // Console.WriteLine(A);
                
            b.Set(0, b.Get(0) / A.Get(0,0));
            for (int k = 1; k < A.dimension; k++)
            {
                float sum = 0;
                for (int r = 0; r <= k - 1; r++)
                {
                    sum += A.Get(k, r) * b.Get(r);
                }
                float newValue = (b.Get(k) - sum) / A.Get(k, k);
                b.Set(k, newValue);
            }
            
            VectorN x = new VectorN(A.dimension);
            x.Set(A.dimension - 1, b.Get(A.dimension - 1));
            for (int k = A.dimension - 2; k >= 0; k--)
            {
                float sum = 0;
                for (int r = k+1; r < A.dimension; r++)
                {
                    sum += A.Get(k, r) * x.Get(r);
                }

                float newValue = b.Get(k) - sum;
                x.Set(k, newValue);
            }

            return x;
        }
        
        
    }
}