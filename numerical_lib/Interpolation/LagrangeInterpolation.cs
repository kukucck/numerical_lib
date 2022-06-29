using numerical_lib.Basic;

namespace numerical_lib.Interpolation
{
    /// <summary>
    /// 拉格朗日插值
    /// </summary>
    public class LagrangeInterpolation
    {
        public Point[] points;
        public float[] lDenominators;

        public LagrangeInterpolation(Point[] points)
        {
            this.points = points;
            CalculateDenominators();
        }
        
        public float Evaluate(float x)
        {
            float sum = 0;
            for (int i = 0; i < points.Length; i++)
            {
                sum += (GetL(i, x) * points[i].y);
            }
            return sum;
        }
    
        /// <summary>
        /// 获得基函数
        /// </summary>
        /// <param name="i"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private float GetL(int i, float x)
        {
            float denominator = lDenominators[i];
            float molecule = 1;
            for (int j = 0; j < points.Length; j++)
            {
                if (j == i)
                {
                    continue;
                }

                molecule *= (x - points[j].x);
            }
            return molecule / denominator;
        }

        /// <summary>
        /// 插值点确定之后，分母就可以确定了，先计算好
        /// </summary>
        private void CalculateDenominators()
        {
            lDenominators = new float[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                float value = 1;
                for (int j = 0; j < points.Length; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    value *= (points[i].x - points[j].x);
                }
                lDenominators[i] = value;
            }
        }
    }
}