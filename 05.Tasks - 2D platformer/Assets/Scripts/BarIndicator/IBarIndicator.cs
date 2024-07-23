using System;

public interface IBarIndicator
{
    public float InitIndicatorValue { get; }

    public event Action<float> IndicatorValueChanged;
}

