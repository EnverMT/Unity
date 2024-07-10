using System;

public interface IAttribute<T>
{
    public event Action<IAttribute<T>> ValueChanged;    

    public T Value { get; }
    public T MaxValue { get; }

    public IAttribute<T> ChangeValue(T value);
}