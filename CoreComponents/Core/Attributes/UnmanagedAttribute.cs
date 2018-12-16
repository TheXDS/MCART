using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Indica que un elemento contiene código no administrado.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Class | AttributeTargets.Module | AttributeTargets.Assembly)]
    [Serializable]
    public sealed class UnmanagedAttribute : Attribute
    {
    }
}