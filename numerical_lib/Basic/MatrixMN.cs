using System;

namespace numerical_lib.Basic
{
    public class MatrixMN
    {
        public int dimensionM;
        public int dimensionN;
        
        public float[] items;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimensionM">列维度</param>
        /// <param name="dimensionN">行维度</param>
        public MatrixMN(int dimensionM, int dimensionN)
        {
            this.dimensionM = dimensionM;
            this.dimensionN = dimensionN;
            this.items = new float[dimensionM*dimensionN];
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimensionM">列维度</param>
        /// <param name="dimensionN">行维度</param>
        /// <param name="items"></param>
        public MatrixMN(int dimensionM, int dimensionN, float[] items)
        {
            this.dimensionM = dimensionM;
            this.dimensionN = dimensionN;
            this.items = items;
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i">第几行</param>
        /// <param name="j">第几列</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public float Get(int i, int j)
        {
            int index = i * dimensionM + j;
            if (index >= items.Length)
            {
                throw new Exception("数组越界!!!!!!!!");
            }
            return items[index];
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i">第几行</param>
        /// <param name="j">第几列</param>
        /// <param name="value"></param>
        /// <exception cref="Exception"></exception>
        public void Set(int i, int j, float value)
        {
            int index = i * dimensionM + j;
            if (index >= items.Length)
            {
                throw new Exception("数组越界!!!!!!!!");
            }
            items[index] = value;
        }
        
        /// <summary>
        /// 交换两行
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        public void SwapRow(int i1, int i2)
        {
            for (int i = 0; i < dimensionM; i++)
            {
                float value = Get(i1, i);
                Set(i1,i, Get(i2, i));
                Set(i2, i, value);
            }
        }

        /// <summary>
        /// 交换两列
        /// </summary>
        /// <param name="j1"></param>
        /// <param name="j2"></param>
        public void SwapCol(int j1, int j2)
        {
            for (int i = 0; i < dimensionN; i++)
            {
                float value = Get(i, j1);
                Set(i,j1, Get(i, j2));
                Set(i, j2, value);
            }
        }
        
        /// <summary>
        /// 某一行最大值
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public float MaxOfRow(int rowIndex)
        {
            float max = float.MinValue;
            int maxColIndex = 0;
            for (int i = 0; i < dimensionM; i++)
            {
                float value = Get(rowIndex, i);
                if (value > max)
                {
                    maxColIndex = i;
                    max = value;
                }
            }
            return maxColIndex;
        }
        
        /// <summary>
        /// 某一列最大值
        /// </summary>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public float MaxOfCol(int colIndex)
        {
            float max = float.MinValue;
            int maxRowIndex = 0;
            for (int i = 0; i < dimensionN; i++)
            {
                float value = Get(i, colIndex);
                if (value > max)
                {
                    maxRowIndex = i;
                    max = value;
                }
            }
            return maxRowIndex;
        }
        
        #region 重载运算符

        public static MatrixMN operator +(MatrixMN a, MatrixMN b)
        {
            if ((a.dimensionM != b.dimensionM) || (a.dimensionN != b.dimensionN))
            {
                throw new Exception("运算的两个矩阵维度不一致");
            }

            MatrixMN c = new MatrixMN(a.dimensionM, a.dimensionN);
            for (int i = 0; i < a.items.Length; i++)
            {
                c.items[i] = a.items[i] + b.items[i];
            }

            return c;
        }
        
        /// <summary>
        /// 方阵的乘法
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static MatrixMN operator *(MatrixMN a, MatrixMN b)
        {
            if (a.dimensionM != b.dimensionN)
            {
                throw new Exception("第1个矩阵的列数不等于第二个矩阵的行数，不能相乘");
            }
            MatrixMN c = new MatrixMN(b.dimensionM, a.dimensionN);

            int dimensionM = b.dimensionM;
            int dimensionN = a.dimensionN;
            int len = dimensionM * dimensionN;
            int multiplyLen = a.dimensionM;

            for (int itemIndex = 0; itemIndex < len; itemIndex++)
            {
                int i = itemIndex / dimensionN;
                int j = itemIndex % dimensionN;
                float sum = 0;
                for (int dimensionIndex = 0; dimensionIndex < multiplyLen; dimensionIndex++)
                {
                    float value = a.Get(i, dimensionIndex) * b.Get(dimensionIndex, j);
                    sum += value;
                }
                c.items[itemIndex] = sum;
            }
            return c;
        }
        
        /// <summary>
        /// 矩阵阵左乘向量（注意，此方法，结果向量和参数向量的维度不一致，除非M=N）
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static VectorN operator *(MatrixMN a, VectorN b)
        {
            if (a.dimensionM != b.size)
            {
                throw new Exception("运算的矩阵列数和向量的长度不一致");
            }
            VectorN c = new VectorN(a.dimensionN);
            int len = b.size;
            int cLen = c.size;
            for (int itemIndex = 0; itemIndex < cLen; itemIndex++)
            {
                float sum = 0;
                for (int dimensionIndex = 0; dimensionIndex < len; dimensionIndex++)
                {
                    sum += a.Get(itemIndex, dimensionIndex) * b.Get(dimensionIndex);
                }
                c.items[itemIndex] = sum;
            }
            return c;
        }

        #endregion
        
        public override string ToString()
        {
            string s = "dimension:" + dimensionM + ", "+ dimensionN + "\n";
            for (int i = 0; i < dimensionN; i++)
            {
                for (int j = 0; j < dimensionM; j++)
                {
                    s += Get(i, j) + ",";
                }
                s += "\n";
            }
            return s;
        }
    }
}