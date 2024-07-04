using System;

public interface IAtribute
{
    public event Action<IAtribute> ValueChanged;

    public uint Value { get; }
    public uint MaxValue { get; }

}
