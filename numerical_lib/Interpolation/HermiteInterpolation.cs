using System;
using numerical_lib.Basic;

namespace numerical_lib.Interpolation
{
    public class HermiteInterpolation
    {
        public Point[] points;
        /// <summary>
        /// 一阶导数
        /// </summary>
        public Point[] derivativePoints;
        /// <summary>
        /// 基函数的分母
        /// </summary>
        public float[] lDenominators;

        public float[] derivativeLs;

        public HermiteInterpolation(Point[] points, Point[] derivativePoints)
        {
            this.points = points;
            this.derivativePoints = derivativePoints;
            CalculateDenominators();
            CalculateDerivativeLs();
        }

        public float Evaluate(float x)
        {
            float sum = 0;
            for (int i = 0; i < points.Length; i++)
            {
                float L = GetL(i, x);
                sum += (points[i].y * GetA(i, x, L) + derivativePoints[i].y * GetB(i, x, L));
            }

            return sum;
        }
        
        /// <summary>
        /// hermite算法中的A
        /// </summary>
        /// <param name="i">index</param>
        /// <param name="x">当前模拟的x</param>
        /// <param name="l">拉格朗日基函数值</param>
        /// <returns></returns>
        private float GetA(int i, float x, float l)
        {
            float derivativeL = derivativeLs[i];
            float result = (1 - 2 * (x - points[i].x) * derivativeL) * l * l;
            return result;
        }

        /// <summary>
        /// Hermite算法中的B
        /// </summary>
        /// <param name="i">index</param>
        /// <param name="x">当前模拟的x</param>
        /// <param name="l">拉格朗日基函数值</param>
        /// <returns></returns>
        private float GetB(int i, float x, float l)
        {
            return (x - points[i].x) * l * l;
        }
        
        /// <summary>
        /// 获得拉格朗日基函数的导数
        /// </summary>
        /// <param name="i"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private float GetDerivativeL(int i)
        {
            float sum = 0;
            for (int j = 0; j < points.Length; j++)
            {
                if (i == j)
                {
                    continue;
                }

                sum += (1 / (points[i].x - points[j].x));
            }
            // Console.WriteLine($"derivativeL[{i}]({x}) = {sum}");
            return sum;
        }
        
        /// <summary>
        /// 获得拉格朗日基函数
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

            float result = molecule / denominator;
            // Console.WriteLine($"L[{i}]({x}) = {result}");
            return result;
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
        
        /// <summary>
        /// 预计算拉格朗日基函数的导数
        /// </summary>
        private void CalculateDerivativeLs()
        {
            derivativeLs = new float[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                float value = GetDerivativeL(i);
                derivativeLs[i] = value;
            }
        }
    }
}