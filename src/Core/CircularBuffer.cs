namespace FF12RNGHelper.Core
{
    internal class CircularBuffer<T> : IDeepCloneable<CircularBuffer<T>>
    {
        private T[] _buffer;
        private int _nextFree;
        private int _length;

        public CircularBuffer(int length)
        {
            _length = length;
            _buffer = new T[length];
            _nextFree = 0;
        }

        public void Add(T o)
        {
            _buffer[_nextFree] = o;
            _nextFree = ++_nextFree % _buffer.Length;
        }

        public T this[int index]
        {
            get => _buffer[AdjustIndex(index)];
            set => _buffer[AdjustIndex(index)] = value;
        }

        public CircularBuffer<T> DeepClone()
        {
            CircularBuffer<T> copy = new CircularBuffer<T>(_length)
            {
                _buffer = _buffer.Clone() as T[],
                _nextFree = _nextFree,
                _length = _length
            };
            return copy;
        }

        object IDeepCloneable.DeepClone()
        {
            return DeepClone();
        }

        private int AdjustIndex(int index)
        {
            int tempIndex = index % _buffer.Length;
            //Make negative indexes behave properly.
            if (tempIndex < 0)
            {
                tempIndex = _buffer.Length + tempIndex;
            }
            return tempIndex;
        }
    }
}
