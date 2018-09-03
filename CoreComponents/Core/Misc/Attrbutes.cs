/*
Attrbutes.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using static System.AttributeTargets;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace TheXDS.MCART.Attributes
{
    #region Tipos base

    /* -= NOTA =-
     * Los atributos no soportan clases genéricas, por lo cual es necesario
     * crear una implementación base para cada posible tipo de atributo con
     * base de valor que pueda ser necesaria.
     */

    /// <summary>
    ///     Define una interfaz para los atributos que expongan valores por
    ///     medio de la propiedad <see cref="Value"/>.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo del valor espuesto por este atributo.
    /// </typeparam>
    public interface IValueAttribute<out T>
    {
        /// <summary>
        /// Obtiene el valor de este atributo.
        /// </summary>
        T Value { get; }
    }

    /// <summary>
    ///     Clase base para los atributos de cualquier tipo.
    /// </summary>
    public abstract class ObjectAttribute : Attribute, IValueAttribute<object>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Crea una nueva isntancia de la clase
        ///     <see cref="T:TheXDS.MCART.Attributes.ObjectAttribute" />.
        /// </summary>
        /// <param name="attributeValue">Valor de este atributo.</param>
        protected ObjectAttribute(object attributeValue)
        {
            Value = attributeValue;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el valor asociado a este atributo.
        /// </summary>
        public object Value { get; }
    }

#if !CLSCompliance
    /// <inheritdoc />
    /// <summary>
    ///     Clase base para los atributos de cualquier tipo.
    /// </summary>
    public abstract class ValueAttribute : Attribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Crea una nueva isntancia de la clase
        ///     <see cref="T:TheXDS.MCART.Attributes.ObjectAttribute" />.
        /// </summary>
        /// <param name="attributeValue">Valor de este atributo.</param>
        protected ValueAttribute(dynamic attributeValue)
        {
            Value = attributeValue;
        }

        /// <summary>
        ///     Obtiene el valor asociado a este atributo.
        /// </summary>
        public dynamic Value { get; }
    }
#endif

    /// <inheritdoc />
    /// <summary>
    ///     Clase base para los atributos basados en números enteros.
    /// </summary>
    public abstract class IntAttribute : Attribute, IValueAttribute<int>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Crea una nueva isntancia de la clase
        ///     <see cref="T:TheXDS.MCART.Attributes.IntAttribute" />.
        /// </summary>
        /// <param name="attributeValue">Valor de este atributo.</param>
        protected IntAttribute(int attributeValue)
        {
            Value = attributeValue;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el valor asociado a este atributo.
        /// </summary>
        public int Value { get; }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Clase base para los atributos basados en números flotantes.
    /// </summary>
    public abstract class FloatAttribute : Attribute, IValueAttribute<float>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Crea una nueva isntancia de la clase
        ///     <see cref="FloatAttribute" />.
        /// </summary>
        /// <param name="attributeValue">Valor de este atributo.</param>
        protected FloatAttribute(float attributeValue)
        {
            Value = attributeValue;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el valor asociado a este atributo.
        /// </summary>
        public float Value { get; }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Clase base para los atributos basados en valores booleanos.
    /// </summary>
    public abstract class BoolAttribute : Attribute, IValueAttribute<bool>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Crea una nueva isntancia de la clase
        ///     <see cref="BoolAttribute" />.
        /// </summary>
        /// <param name="attributeValue">Valor de este atributo.</param>
        protected BoolAttribute(bool attributeValue)
        {
            Value = attributeValue;
        }

        /// <summary>
        ///     Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo.</value>
        public bool Value { get; }
    }

    #endregion

    /// <summary>
    ///     Agrega un elemento textual genérico a un elemento, además de ser la
    ///     clase base para los atributos que describan un valor representable como
    ///     <see cref="String" /> para un elemento.
    /// </summary>
    [AttributeUsage(All)]
    [Serializable]
    public class TextAttribute : Attribute, IValueAttribute<string>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="TextAttribute" />.
        /// </summary>
        /// <param name="text">Valor de este atributo.</param>
        protected TextAttribute(string text)
        {
            Value = text;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo.</value>
        public string Value { get; }
    }

    /// <summary>
    ///     Agrega un elemento de tipo a un elemento, además de ser la
    ///     clase base para los atributos que describan un valor representable como
    ///     <see cref="Type" /> para un elemento.
    /// </summary>
    [AttributeUsage(All)]
    [Serializable]
    public class TypeAttribute : Attribute, IValueAttribute<Type>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="TypeAttribute" />.
        /// </summary>
        /// <param name="type">Valor de este atributo.</param>
        protected TypeAttribute(Type type)
        {
            Value = type;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo.</value>
        public Type Value { get; }
    }

    /// <summary>
    ///     Especifica la versión de un elemento, además de ser la clase base para
    ///     los atributos que describan un valor <see cref="Version" /> para un
    ///     elemento.
    /// </summary>
    [Serializable]
    public abstract class VersionAttributeBase : Attribute, IValueAttribute<Version>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="VersionAttributeBase" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        protected VersionAttributeBase(int major, int minor, int build, int rev)
        {
            Value = new Version(major, minor, build, rev);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo.</value>
        public Version Value { get; }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indica que un elemento es un proveedor de Thunking (facilita la llamada
    ///     a otros elementos o miembros).
    /// </summary>
    [AttributeUsage(Property | Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class ThunkAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Marca una clase para no ser cargada como
    ///     <see cref="PluginSupport.IPlugin" />, a pesar de implementar
    ///     <see cref="PluginSupport.IPlugin" />.
    /// </summary>
    [AttributeUsage(Interface | Class)]
    [Serializable]
    public sealed class NotPluginAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Especifica la versión mínima de MCART requerida por el elemento.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class MinMCARTVersionAttribute : VersionAttributeBase
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="MinMCARTVersionAttribute" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        public MinMCARTVersionAttribute(int major, int minor) : base(major, minor, 0, 0)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="MinMCARTVersionAttribute" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        public MinMCARTVersionAttribute(int major, int minor, int build, int rev) : base(major, minor, build, rev)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Especifica la versión de MCART recomendada para el elemento.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class TargetMCARTVersionAttribute : VersionAttributeBase
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="TargetMCARTVersionAttribute" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        public TargetMCARTVersionAttribute(int major, int minor) : base(major, minor, int.MaxValue, int.MaxValue)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="TargetMCARTVersionAttribute" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        public TargetMCARTVersionAttribute(int major, int minor, int build, int rev) : base(major, minor, build, rev)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Especifica la versión del elemento.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class VersionAttribute : VersionAttributeBase
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="VersionAttribute" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        public VersionAttribute(int major, int minor) : base(major, minor, int.MaxValue, int.MaxValue)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="VersionAttribute" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        public VersionAttribute(int major, int minor, int build, int rev) : base(major, minor, build, rev)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Establece un valor mínimo al cual se deben limitar los campos y propiedades.
    /// </summary>
    [AttributeUsage(Property | Field)]
    public class MinimumAttribute : ObjectAttribute
    {
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia del atributo
        /// <see cref="T:TheXDS.MCART.Attributes.MinimumAttribute" /> estableciendo el valor mínimo a
        /// representar.
        /// </summary>
        /// <param name="attributeValue">Valor del atributo.</param>
        public MinimumAttribute(object attributeValue) : base(attributeValue)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Establece un valor máximo al cual se deben limitar los campos y propiedades.
    /// </summary>
    [AttributeUsage(Property | Field)]
    public class MaximumAttribute : ObjectAttribute
    {
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia del atributo
        /// <see cref="T:TheXDS.MCART.Attributes.MinimumAttribute" /> estableciendo el valor máximo a
        /// representar.
        /// </summary>
        /// <param name="attributeValue">Valor del atributo.</param>
        public MaximumAttribute(object attributeValue) : base(attributeValue)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Marca un elemento como versión Beta.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class BetaAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indica un elemento cuyo propósito es simplemente el de reservar
    ///     espacio para una posible expansión, y por lo tanto, actualmente no
    ///     tiene ninguna funcionalidad.
    /// </summary>
    [AttributeUsage(All)]
    [Serializable]
    public sealed class StubAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Marca un elemento como no utilizable.
    /// </summary>
    [AttributeUsage(All)]
    [Serializable]
    public sealed class UnusableAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indica que el uso de un elemento es peligroso.
    /// </summary>
    [AttributeUsage(All)]
    [Serializable]
    public sealed class DangerousAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indica que un elemento contiene código no administrado.
    /// </summary>
    [AttributeUsage(Property | Method | Constructor | Class | Module | Assembly)]
    [Serializable]
    public sealed class UnmanagedAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indica que un elemento contiene código que podría ser inestable.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class UnstableAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Establece un nombre personalizado para describir este elemento.
    /// </summary>
    [AttributeUsage(All)]
    [Serializable]
    public sealed class NameAttribute : TextAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="NameAttribute" />.
        /// </summary>
        /// <param name="name">Valor del atributo.</param>
        public NameAttribute(string name) : base(name)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Establece una descripción larga para este elemento.
    /// </summary>
    [AttributeUsage(All)]
    [Serializable]
    public sealed class DescriptionAttribute : TextAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="DescriptionAttribute" />.
        /// </summary>
        /// <param name="description">Valor del atributo.</param>
        public DescriptionAttribute(string description) : base(description)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Establece el autor del elemento.
    /// </summary>
    [AttributeUsage(Property | Method | Constructor | Class | Module | Assembly)]
    [Serializable]
    public sealed class AuthorAttribute : TextAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="DescriptionAttribute" />.
        /// </summary>
        /// <param name="attrValue">Valor del atributo.</param>
        public AuthorAttribute(string attrValue) : base(attrValue)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Establece la información de Copyright del elemento.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class CopyrightAttribute : TextAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="DescriptionAttribute" />.
        /// </summary>
        /// <param name="copyright">Valor del atributo.</param>
        public CopyrightAttribute(string copyright) : base(copyright)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Establece el texto de licencia a asociar con el elemento.
    /// </summary>
    [AttributeUsage(Class | Module | Assembly)]
    [Serializable]
    public sealed class LicenseTextAttribute : TextAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="LicenseTextAttribute" />.
        /// </summary>
        /// <param name="licenseText">Texto de la licencia.</param>
        public LicenseTextAttribute(string licenseText) : base(licenseText)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Establece un archivo incrustado de licencia a asociar con el elemento.
    /// </summary>
    [AttributeUsage(Class | Module | Assembly)]
    [Serializable]
    public sealed class EmbeededLicenseAttribute : TextAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="EmbeededLicenseAttribute" />.
        /// </summary>
        /// <param name="resourcePath">
        ///     Archivo incrustado de la licencia.
        /// </param>
        public EmbeededLicenseAttribute(string resourcePath) : base(resourcePath)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Establece un archivo de licencia externo a asociar con el elemento.
    /// </summary>
    [AttributeUsage(Class | Module | Assembly)]
    [Serializable]
    public sealed class LicenseFileAttribute : TextAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="LicenseFileAttribute" />.
        /// </summary>
        /// <param name="licenseFile">
        ///     Ruta del archivo de licencia adjunto.
        /// </param>
        public LicenseFileAttribute(string licenseFile) : base(licenseFile)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indica que un elemento podría tardar en ejecutarse.
    /// </summary>
    [AttributeUsage(Method | AttributeTargets.Delegate)]
    [Serializable]
    public sealed class LenghtyAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Attributo que define la ruta de un servidor.
    /// </summary>
    /// <remarks>
    ///     Es posible establecer este atributo más de una vez en un mismo elemento.
    /// </remarks>
    [AttributeUsage(All, AllowMultiple = true)]
    [Serializable]
    public sealed class ServerAttribute : Attribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ServerAttribute" /> estableciendo el servidor y el puerto
        ///     al cual este atributo hará referencia.
        /// </summary>
        /// <param name="server">Nombre del servidor / Dirección IP.</param>
        /// <param name="port">Número de puerto del servidor.</param>
        /// <remarks>
        ///     Si se define un número de puerto en <paramref name="server" />, el
        ///     valor del parámetro <paramref name="port" /> tomará precedencia.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///     Se produce si el servidor es una ruta malformada.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Se produce si <paramref name="port" /> es inferior a 1, o superior
        ///     a 65535.
        /// </exception>
        public ServerAttribute(string server, int port)
        {
            if (server.IsEmpty()) throw new ArgumentNullException(nameof(server));
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            Server = server;
            Port = port;
        }

        /// <summary>
        ///     Obtiene el servidor.
        /// </summary>
        /// <value>
        ///     La ruta del servidor a la cual este atributo apunta.
        /// </value>
        public string Server { get; }

        /// <summary>
        ///     Obtiene o establece el puerto de conexión del servidor.
        /// </summary>
        /// <value>
        ///     Un valor entre 1 y 65535 que establece el número de puerto a
        ///     apuntar.
        /// </value>
        public int Port { get; }

        /// <summary>
        ///     Devuelve una cadena que representa al objeto actual.
        /// </summary>
        /// <returns>Una cadena que representa al objeto actual.</returns>
        public override string ToString()
        {
            return $"{Server}:{Port}";
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Atributo que indica el compresor utilizado para este elemento.
    /// </summary>
    [AttributeUsage(Property | Field)]
    public sealed class CompressorAttribute : TextAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Indica el compresor utilizado por este elemento.
        /// </summary>
        /// <param name="compressor">Nombre del compresor utilizado.</param>
        public CompressorAttribute(string compressor) : base(compressor)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indica una cadena que puede utilizarse para identificar a este elemento.
    /// </summary>
    [AttributeUsage(All, AllowMultiple = true)]
    public sealed class IdentifierAttribute : TextAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Indica una cadena que puede utilizarse para identificar a este elemento.
        /// </summary>
        /// <param name="identifier">Identificador a utilizar.</param>
        public IdentifierAttribute(string identifier) : base(identifier)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Establece un formato de protocolo para abrir un vínculo por medio del 
    /// sistema operativo.
    /// </summary>
    [AttributeUsage(Property | Field)]
    public sealed class ProtocolFormatAttribute : Attribute
    {
        /// <summary>
        /// Formato de llamada de protocolo.
        /// </summary>
        public string Format { get; }
        /// <inheritdoc />
        /// <summary>
        /// Establece un formato de protocolo para abrir un vínculo por medio del sistema operativo.
        /// </summary>
        /// <param name="format">Máscara a aplicar.</param>
        public ProtocolFormatAttribute(string format)
        {
            Format = format;
        }
        /// <summary>
        ///     Abre un url con este protocolo formateado.
        /// </summary>
        /// <param name="url">
        ///     URL del recurso a abrir por medio del protocolo definido por
        ///     este atributo.
        /// </param>
        public void Open(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return;
            try { System.Diagnostics.Process.Start(string.Format(Format, url)); }
            catch { /* Ignorar excepción */ }
        }
    }
}