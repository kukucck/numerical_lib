using System;
using numerical_lib.Basic;

namespace numerical_lib.Integration
{
    /// <summary>
    /// 龙贝格求积算法
    /// </summary>
    public class RombergMethod
    {
        private static readonly int ITAR_NUM = 4; 
        private float[] table_T;
        private float[] table_S;
        private float[] table_C;
        private float[] table_R;
        private bool[] calculated_T;
        private bool[] calculated_S;
        private bool[] calculated_C;
        private bool[] calculated_R;

        private Function _function;

        public RombergMethod(Function function)
        {
            _function = function;
        }

        public float Resolve(float a, float b)
        {
            // int n2 = (int) Math.Pow(2, ITAR_NUM - 1);
            ResetTable();
            return R(ITAR_NUM, a, b);
        }

        //梯形公式
        private float T(int n2, float a, float b)
        {
            int itarNum = (int) Math.Round(Math.Log(n2) / Math.Log(2));
            if (calculated_T[itarNum])
            {
                return table_T[itarNum];
            }

            if (n2 == 1)
            {
                float result = (b - a) / 2 * (_function(a) + _function(b));
                table_T[itarNum] = result;
                calculated_T[itarNum] = true;
                return result;
            }else{
                int n = n2 / 2;
                float h = (b - a) / n;
                float sum = 0;
                for (int i = 0; i <= n - 1; ++i)
                {
                    float x = a + (b - a) * (i + 0.5f) / n;
                    sum += _function(x);
                }
                float result = T(n, a, b) / 2 + h / 2 * sum;
                table_T[itarNum] = result;
                calculated_T[itarNum] = true;
                return result;
            }
        }
        
        //辛普生公式
        private float S(int n, float a, float b) {
            int itarNum = (int) Math.Round(Math.Log(n) / Math.Log(2));
            if (calculated_S[itarNum])
            {
                // cout<<"S, itarNum = "<<itarNum <<", value = "<<table_S[itarNum]<<endl;
                return table_S[itarNum];
            }
            float result = T(n*2, a, b) * 4 / 3 - T(n, a, b) / 3;
            table_S[itarNum] = result;
            calculated_S[itarNum] = true;
            return result;
        }
        
        //柯特斯公式
        private float C(int n, float a, float b) {
            int itarNum = (int) Math.Round(Math.Log(n) / Math.Log(2));
            if (calculated_C[itarNum])
            {
                // cout<<"C, itarNum = "<<itarNum <<", value = "<<table_C[itarNum]<<endl;
                return table_C[itarNum];
            }
            float result = S(n * 2, a, b) * 16 / 15 - S(n, a, b) / 15;
            table_C[itarNum] = result;
            calculated_C[itarNum] = true;
            return result;
        }

        //龙贝格公式
        private float R(int n, float a, float b) {
            int itarNum = (int) Math.Round(Math.Log(n) / Math.Log(2));
            if (calculated_R[itarNum])
            {
                // cout<<"R, itarNum = "<<itarNum <<", value = "<<table_R[itarNum]<<endl;
                return table_R[itarNum];
            }
            float result = C(n * 2, a, b) * 64 / 63 - C(n, a, b) / 63;
            table_R[itarNum] = result;
            calculated_R[itarNum] = true;
            return result;
        }

        private void ResetTable()
        {
            int tableSize = ITAR_NUM + 4;
            table_T = new float[tableSize];
            table_S = new float[tableSize];
            table_C = new float[tableSize];
            table_R = new float[tableSize];
            
            calculated_T = new bool[tableSize];
            calculated_S = new bool[tableSize];
            calculated_C = new bool[tableSize];
            calculated_R = new bool[tableSize];
        }
    }
}