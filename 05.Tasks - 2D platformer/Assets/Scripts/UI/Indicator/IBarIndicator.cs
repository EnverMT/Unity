using System;

namespace Platformer.UI.Indicator
{
    public interface IBarIndicator
    {
        public float InitIndicatorValue { get; }

        public event Action<float> IndicatorValueChanged;
    }
}