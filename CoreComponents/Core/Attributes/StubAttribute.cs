using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Indica un elemento cuyo propósito es simplemente el de reservar
    ///     espacio para una posible expansión, y por lo tanto, actualmente no
    ///     tiene ninguna funcionalidad.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    [Serializable]
    public sealed class StubAttribute : Attribute
    {
    }
}