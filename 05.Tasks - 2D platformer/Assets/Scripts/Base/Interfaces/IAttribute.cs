using System;

public interface IAttribute
{
    public event Action<IAttribute> ValueChanged;

    public uint Value { get; }
    public uint MaxValue { get; }

}
