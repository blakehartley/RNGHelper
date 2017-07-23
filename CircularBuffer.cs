namespace FF12RNGHelper
{
    public class CircularBuffer<T> : IDeepCloneable<CircularBuffer<T>>
    {
        private T[] buffer;
        private int nextFree;
        private int length;

        public CircularBuffer(int length)
        {
            this.length = length;
            buffer = new T[length];
            nextFree = 0;
        }

        public void Add(T o)
        {
            buffer[nextFree] = o;
            nextFree = (nextFree + 1) % buffer.Length;
        }

        public CircularBuffer<T> DeepClone()
        {
            CircularBuffer<T> copy = new CircularBuffer<T>(length);
            copy.buffer = buffer.Clone() as T[];
            copy.nextFree = nextFree;
            copy.length = length;
            return copy;
        }

        object IDeepCloneable.DeepClone()
        {
            return DeepClone();
        }



        public T this[long index]
        {
            get
            {
                int tempIndex = (int)(index % buffer.Length);
                //Make negative indexes behave properly.
                if (tempIndex < 0)
                {
                    tempIndex = buffer.Length + tempIndex;
                }
                return buffer[tempIndex];
            }
            set
            {
                buffer[index % buffer.Length] = value;
            }
        }
        public T this[ulong index]
        {
            get
            {

                return buffer[index % (ulong)buffer.LongLength];
            }
            set
            {
                buffer[index % (ulong)buffer.Length] = value;
            }
        }
    }
}
