using System;

namespace numerical_lib.Basic
{
    public struct MatrixN
    {
        public int dimension;
        public float[] items;

        public MatrixN(int dimension)
        {
            this.dimension = dimension;
            this.items = new float[dimension * dimension];
        }

        public MatrixN(float[] items)
        {
            this.dimension = (int) Math.Sqrt(items.Length);
            this.items = items;
        }

        public float Get(int i, int j)
        {
            int index = i * dimension + j;
            if (index >= items.Length)
            {
                throw new Exception("数组越界!!!!!!!!");
            }
            return items[index];
        }

        public void Set(int i, int j, float value)
        {
            int index = i * dimension + j;
            if (index >= items.Length)
            {
                throw new Exception("数组越界!!!!!!!!");
            }
            items[index] = value;
        }

        public static MatrixN operator +(MatrixN a, MatrixN b)
        {
            MatrixN c = new MatrixN(a.dimension);
            if (a.dimension != b.dimension)
            {
                throw new Exception("运算的两个矩阵长度不一致");
            }
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
        public static MatrixN operator *(MatrixN a, MatrixN b)
        {
            MatrixN c = new MatrixN(a.dimension);
            if (a.dimension != b.dimension)
            {
                throw new Exception("运算的两个矩阵长度不一致");
            }

            int len = a.items.Length;
            int dimension = a.dimension;
            
            for (int itemIndex = 0; itemIndex < len; itemIndex++)
            {
                int i = itemIndex / dimension;
                int j = itemIndex % dimension;
                float sum = 0;
                for (int dimensionIndex = 0; dimensionIndex < dimension; dimensionIndex++)
                {
                    float value = a.Get(i, dimensionIndex) * b.Get(dimensionIndex, j);
                    sum += value;
                }
                c.items[itemIndex] = sum;
            }
            return c;
        }
        
        /// <summary>
        /// 方阵左乘向量
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static VectorN operator *(MatrixN a, VectorN b)
        {
            if (a.dimension != b.size)
            {
                throw new Exception("运算的两个矩阵长度不一致");
            }
            VectorN c = new VectorN(b.size);
            int len = b.size;
            for (int itemIndex = 0; itemIndex < len; itemIndex++)
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

        public override string ToString()
        {
            string s = "dimension:" + dimension + "\n";
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    s += Get(i, j) + ",";
                }
                s += "\n";
            }
            return s;
        }
    }
}