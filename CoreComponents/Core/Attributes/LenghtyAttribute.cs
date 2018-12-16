using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Indica que un elemento podría tardar en ejecutarse.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Delegate)]
    [Serializable]
    public sealed class LenghtyAttribute : Attribute
    {
    }
}