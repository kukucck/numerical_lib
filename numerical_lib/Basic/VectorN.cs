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