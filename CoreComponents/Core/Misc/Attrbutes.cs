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

namespace TheXDS.MCART.Attributes
{
    #region Tipos base
    /* -= NOTA =-
     * Los atributos no soportan clases genéricas, por lo cual es necesario
     * crear una implementación base para cada posible tipo de atributo con
     * base de valor que pueda ser necesaria.
     */

    /// <summary>
    /// Clase base para los atributos basados en números enteros.
    /// </summary>
    public abstract class IntAttribute : Attribute
    {
        /// <summary>
        /// Obtiene el valor asociado a este atributo.
        /// </summary>
        public int Value { get; }
        /// <summary>
        /// Crea una nueva isntancia de la clase 
        /// <see cref="IntAttribute"/>.
        /// </summary>
        /// <param name="attributeValue">Valor de este atributo.</param>
        protected IntAttribute(int attributeValue)
        {
            Value = attributeValue;
        }
    }
    /// <summary>
    /// Clase base para los atributos basados en números flotantes.
    /// </summary>
    public abstract class FloatAttribute : Attribute
    {
        /// <summary>
        /// Obtiene el valor asociado a este atributo.
        /// </summary>
        public float Value { get; }
        /// <summary>
        /// Crea una nueva isntancia de la clase 
        /// <see cref="FloatAttribute"/>.
        /// </summary>
        /// <param name="attributeValue">Valor de este atributo.</param>
        protected FloatAttribute(float attributeValue)
        {
            Value = attributeValue;
        }
    }
    /// <summary>
    /// Clase base para los atributos basados en valores booleanos.
    /// </summary>
    public abstract class BoolAttribute : Attribute
    {
        /// <summary>
        /// Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo</value>
        public bool Value { get; }
        /// <summary>
        /// Crea una nueva isntancia de la clase 
        /// <see cref="BoolAttribute"/>.
        /// </summary>
        /// <param name="attributeValue">Valor de este atributo.</param>
        protected BoolAttribute(bool attributeValue)
        {
            Value = attributeValue;
        }
    }
    #endregion

    /// <summary>
    /// Agrega un elemento textual genérico a un elemento, además de ser la
    /// clase base para los atributos que describan un valor representable como
    /// <see cref="String"/> para un elemento.
    /// </summary>
    [AttributeUsage(All)]
    [Serializable]
    public class TextAttribute : Attribute
    {
        /// <summary>
        /// Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo.</value>
        public string Value { get; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="TextAttribute"/>.
        /// </summary>
        /// <param name="text">Valor de este atributo.</param>
        protected TextAttribute(string text)
        {
            Value = text;
        }
    }

    /// <summary>
    /// Especifica la versión de un elemento, además de ser la clase base para
    /// los atributos que describan un valor <see cref="Version"/> para un 
    /// elemento.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public class VersionAttribute : Attribute
    {
        /// <summary>
        /// Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo</value>
        public Version Value { get; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="VersionAttribute"/>.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        public VersionAttribute(int major, int minor, int build, int rev) { Value = new Version(major, minor, build, rev); }
    }

    /// <summary>
    /// Indica que un elemento es un proveedor de Thunking (facilita la llamada
    /// a otros elementos o miembros).
    /// </summary>
    [AttributeUsage(Property | Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class ThunkAttribute : Attribute { }

    /// <summary>
    /// Marca una clase para no ser cargada como 
    /// <see cref="PluginSupport.IPlugin"/>, a pesar de implementar
    /// <see cref="PluginSupport.IPlugin"/>.
    /// </summary>
    [AttributeUsage(Interface | Class)]
    [Serializable]
    public sealed class NotPluginAttribute : Attribute { }

    /// <summary>
    /// Especifica la versión mínima de MCART requerida por el elemento.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class MinMCARTVersionAttribute : VersionAttribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="MinMCARTVersionAttribute"/>.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        public MinMCARTVersionAttribute(int major, int minor) : base(major, minor, 0, 0) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="MinMCARTVersionAttribute"/>.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        public MinMCARTVersionAttribute(int major, int minor, int build, int rev) : base(major, minor, build, rev) { }

    }

    /// <summary>
    /// Especifica la versión de MCART recomendada para el elemento.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class TargetMCARTVersionAttribute : VersionAttribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="TargetMCARTVersionAttribute"/>.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        public TargetMCARTVersionAttribute(int major, int minor) : base(major, minor, int.MaxValue, int.MaxValue) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="TargetMCARTVersionAttribute"/>.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        public TargetMCARTVersionAttribute(int major, int minor, int build, int rev) : base(major, minor, build, rev) { }
    }

    /// <summary>
    /// Marca un elemento como versión Beta.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class BetaAttribute : Attribute { }

    /// <summary>
    /// Indica un elemento cuyo propósito es simplemente el de reservar
    /// espacio para una posible expansión, y por lo tanto, actualmente no
    /// tiene ninguna funcionalidad.
    /// </summary>
    [AttributeUsage(All)]
    [Serializable]
    public sealed class StubAttribute : Attribute { }

    /// <summary>
    /// Marca un elemento como no utilizable.
    /// </summary>
    [AttributeUsage(All)]
    [Serializable]
    public sealed class UnusableAttribute : Attribute { }

    /// <summary>
    /// Indica que el uso de un elemento es peligroso.
    /// </summary>
    [AttributeUsage(All)]
    [Serializable]
    public sealed class DangerousAttribute : Attribute { }

    /// <summary>
    /// Indica que un elemento contiene código no administrado.
    /// </summary>
    [AttributeUsage(Property | Method | Constructor | Class | Module | Assembly)]
    [Serializable]
    public sealed class UnmanagedAttribute : Attribute { }

    /// <summary>
    /// Indica que un elemento contiene código que podría ser inestable.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class UnstableAttribute : Attribute { }

    /// <summary>
    /// Establece un nombre personalizado para describir este elemento.
    /// </summary>
    [AttributeUsage(All)]
    [Serializable]
    public sealed class NameAttribute : TextAttribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NameAttribute"/>.
        /// </summary>
        /// <param name="name">Valor del atributo.</param>
        public NameAttribute(string name) : base(name) { }
    }

    /// <summary>
    /// Establece una descripción larga para este elemento.
    /// </summary>
    [AttributeUsage(All)]
    [Serializable]
    public sealed class DescriptionAttribute : TextAttribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DescriptionAttribute"/>.
        /// </summary>
        /// <param name="description">Valor del atributo.</param>
        public DescriptionAttribute(string description) : base(description) { }
    }

    /// <summary>
    /// Establece el autor del elemento.
    /// </summary>
    [AttributeUsage(Property | Method | Constructor | Class | Module | Assembly)]
    [Serializable]
    public sealed class AuthorAttribute : TextAttribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DescriptionAttribute"/>.
        /// </summary>
        /// <param name="attrValue">Valor del atributo.</param>
        public AuthorAttribute(string attrValue) : base(attrValue) { }
    }

    /// <summary>
    /// Establece la información de Copyright del elemento.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class CopyrightAttribute : TextAttribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DescriptionAttribute"/>.
        /// </summary>
        /// <param name="copyright">Valor del atributo.</param>
        public CopyrightAttribute(string copyright) : base(copyright) { }
    }

    /// <summary>
    /// Establece el texto de licencia a asociar con el elemento.
    /// </summary>
    [AttributeUsage(Class | Module | Assembly)]
    [Serializable]
    public sealed class LicenseTextAttribute : TextAttribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="LicenseTextAttribute"/>.
        /// </summary>
        /// <param name="licenseText">Texto de la licencia.</param>
        public LicenseTextAttribute(string licenseText) : base(licenseText) { }
    }

    /// <summary>
    /// Establece un archivo incrustado de licencia a asociar con el elemento.
    /// </summary>
    [AttributeUsage(Class | Module | Assembly)]
    [Serializable]
    public sealed class EmbeededLicenseAttribute : TextAttribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="EmbeededLicenseAttribute"/>.
        /// </summary>
        /// <param name="resourcePath">
        /// Archivo incrustado de la licencia.
        /// </param>
        public EmbeededLicenseAttribute(string resourcePath) : base(resourcePath) { }
    }

    /// <summary>
    /// Establece un archivo de licencia externo a asociar con el elemento.
    /// </summary>
    [AttributeUsage(Class | Module | Assembly)]
    [Serializable]
    public sealed class LicenseFileAttribute : TextAttribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="LicenseFileAttribute"/>.
        /// </summary>
        /// <param name="licenseFile">
        /// Ruta del archivo de licencia adjunto.
        /// </param>
        public LicenseFileAttribute(string licenseFile) : base(licenseFile) { }
    }

    /// <summary>
    /// Indica que un elemento podría tardar en ejecutarse.
    /// </summary>
    [AttributeUsage(Method | AttributeTargets.Delegate)]
    [Serializable]
    public sealed class LenghtyAttribute : Attribute { }

    /// <summary>
    /// Attributo que define la ruta de un servidor.
    /// </summary>
    /// <remarks>
    /// Es posible establecer este atributo más de una vez en un mismo elemento.
    /// </remarks>
    [AttributeUsage(All, AllowMultiple = true)]
    [Serializable]
    public sealed class ServerAttribute : Attribute
    {
        /// <summary>
        /// Obtiene el servidor.
        /// </summary>
        /// <value>
        /// La ruta del servidor a la cual este atributo apunta.
        /// </value>
        public string Server { get; }
        /// <summary>
        /// Obtiene o establece el puerto de conexión del servidor.
        /// </summary>
        /// <value>
        /// Un valor entre 1 y 65535 que establece el número de puerto a
        /// apuntar.
        /// </value>
        public int Port { get; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ServerAttribute"/> estableciendo el servidor y el puerto
        /// al cual este atributo hará referencia.
        /// </summary>
        /// <param name="server">Nombre del servidor / Dirección IP.</param>
        /// <param name="port">Número de puerto del servidor.</param>
        /// <remarks>
        /// Si se define un número de puerto en <paramref name="server"/>, el
        /// valor del parámetro <paramref name="port"/> tomará precedencia.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Se produce si el servidor es una ruta malformada.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Se produce si <paramref name="port"/> es inferior a 1, o superior
        /// a 65535.
        /// </exception>
        public ServerAttribute(string server, int port)
        {
            if (server.IsEmpty()) throw new ArgumentNullException(nameof(server));
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            Server = server;
            Port = port;
        }
        /// <summary>
        /// Devuelve una cadena que representa al objeto actual.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Server}:{Port}";
        }
    }
    /// <summary>
    /// Atributo que indica el compresor utilizado para este elemento.
    /// </summary>
    [AttributeUsage(Property | Field)]
    public sealed class CompressorAttribute : TextAttribute
    {
        /// <summary>
        /// Indica el compresor utilizado por este elemento.
        /// </summary>
        /// <param name="compressor">Nombre del compresor utilizado.</param>
        public CompressorAttribute(string compressor) : base(compressor) { }
    }
    /// <summary>
    /// Indica una cadena que puede utilizarse para identificar a este elemento.
    /// </summary>
    [AttributeUsage(All, AllowMultiple = true)]
    public sealed class IdentifierAttribute : TextAttribute
    {
        /// <summary>
        /// Indica una cadena que puede utilizarse para identificar a este elemento.
        /// </summary>
        /// <param name="identifier">Identificador a utilizar.</param>
        public IdentifierAttribute(string identifier) : base(identifier) { }
    }
}