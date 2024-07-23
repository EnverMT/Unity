using System;

namespace Platformer.Attribute
{
    public interface IAttribute<T>
    {
        public event Action<IAttribute<T>> ValueChanged;

        public T Value { get; }
        public T MaxValue { get; }

        public IAttribute<T> Increase(T value);
        public IAttribute<T> Decrease(T value);
    }
}
