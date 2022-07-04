namespace numerical_lib.Basic
{
    public struct VectorN
    {
        public int size;
        public float[] items;

        public VectorN(int size) : this()
        {
            this.size = size;
            items = new float[size];
        }

        public VectorN(float[] items) : this()
        {
            this.items = items;
            size = items.Length;
        }

        public float Get(int i)
        {
            return items[i];
        }

        public void Set(int i, float value)
        {
            items[i] = value;
        }

        public void SwapValue(int i, int j)
        {
            float tmp = items[i];
            items[i] = items[j];
            items[j] = tmp;
        }

        public static VectorN operator *(VectorN a, float b)
        {
            VectorN result = new VectorN(a.items);
            int len = result.items.Length;
            for (int i = 0; i < len; i++)
            {
                result.items[i] = result.items[i] * b;
            }
            return result;
        }
        
        public static VectorN operator *(float b, VectorN a)
        {
            VectorN result = new VectorN(a.items);
            int len = result.items.Length;
            for (int i = 0; i < len; i++)
            {
                result.items[i] = result.items[i] * b;
            }
            return result;
        }

        public override string ToString()
        {
            string s = "size:" + size + "\n";
            for (int i = 0; i < size; i++)
            {
                s += items[i] + "\n";
            }
            return s;
        }
    }
}