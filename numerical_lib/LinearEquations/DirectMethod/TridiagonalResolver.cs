using System;
using numerical_lib.Basic;

namespace numerical_lib.LinearEquations.DirectMethod
{
    /// <summary>
    /// 三对角算法（追赶法）（数值计算方法第二版，上册，林成森，Page 96）
    /// 1、三对角算法将系数矩阵A，划分成待定系数的下三角L和上三角U，其中，Y=LX
    /// 2、再通过LU分解求解y(LY=b)的解（追的过程）
    /// 3、再通过LX=Y，求解出X（赶的过程）
    /// </summary>
    public class TridiagonalResolver
    {
        public MatrixN A;
        public VectorN B;
        public TridiagonalResolver(MatrixN A, VectorN B)
        {
            this.A = A;
            this.B = B;
            // Console.WriteLine($"a = {GetA(1)}, c = {GetC(1)}");
        }

        public VectorN Solve()
        {
            if (Math.Abs(GetD(0)) < Const.FLOAT_EQUAL)
            {
                throw new Exception("Method Failed,分母不能为0");
            }

            int n = A.dimension;
            float[] pList = new float[n];
            float[] qList = new float[n];
            float[] yList = new float[n];
            pList[0] = GetD(0);
            qList[0] = GetC(0) / GetD(0);

            for (int k = 1; k < n - 1; k++)
            {
                pList[k] = GetD(k) - GetA(k) * qList[k - 1];
                if (Math.Abs(pList[k]) < Const.FLOAT_EQUAL)
                {
                    throw new Exception("Method Failed,分母不能为0");
                }
                qList[k] = GetC(k) / pList[k];
            }

            pList[n - 1] = GetD(n - 1) - GetA(n - 1) * qList[n - 2];
            if (Math.Abs(pList[n - 1]) < Const.FLOAT_EQUAL)
            {
                throw new Exception("Method Failed,分母不能为0");
            }
            
            //计算y的过程称为“追”的过程
            yList[0] = B.Get(0) / pList[0];
            for (int k = 1; k < n; k++)
            {
                yList[k] = (B.Get(k) - GetA(k) * yList[k - 1]) / pList[k];
            }
            
            //计算x的过程称为“赶”的过程
            VectorN x = new VectorN(n);
            
            x.Set(n-1, yList[n-1]);

            for (int i = n-2; i >= 0; i--)
            {
                x.Set(i, yList[i] - qList[i]*x.Get(i+1));
            }
            
            return x;
        }

        private float GetA(int index)
        {
            if (index == 0)
            {
                throw new Exception($"a的最小下标是1");
            }
            return A.Get(index, index - 1);
        }

        private float GetD(int index)
        {
            return A.Get(index, index);
        }

        private float GetC(int index)
        {
            return A.Get(index, index + 1);
        }
        
        
    }
}