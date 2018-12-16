using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Marca un elemento como no utilizable.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    [Serializable]
    public sealed class UnusableAttribute : Attribute
    {
    }
}