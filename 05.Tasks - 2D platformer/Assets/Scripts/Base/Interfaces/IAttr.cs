using System;

public interface IAttr<T>
{
    public event Action<IAttr<T>> ValueChanged;

    public T Value { get; }
    public T MaxValue { get; }

}
