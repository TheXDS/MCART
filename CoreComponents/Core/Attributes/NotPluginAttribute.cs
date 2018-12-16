using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Marca una clase para no ser cargada como
    ///     <see cref="PluginSupport.IPlugin" />, a pesar de implementar
    ///     <see cref="PluginSupport.IPlugin" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    [Serializable]
    public sealed class NotPluginAttribute : Attribute
    {
    }
}