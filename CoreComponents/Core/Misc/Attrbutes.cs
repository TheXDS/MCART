//
//  Attrbutes.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace MCART.Attributes
{
    #region Tipos base
    /// <summary>
    /// Clase base para los atributos basados en texto.
    /// </summary>
    public abstract class TextAttributeBase : Attribute
    {
        /// <summary>
        /// Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo</value>
        public readonly string Value;
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="TextAttributeBase"/>.
        /// </summary>
        /// <param name="attrValue">Valor de este atributo.</param>
        protected TextAttributeBase(string attrValue)
        {
            Value = attrValue;
        }
    }

    /// <summary>
    /// Clase base para los atributos basados en números.
    /// </summary>
    public abstract class IntAttributeBase : Attribute
    {
        /// <summary>
        /// Obtiene el valor asociado a este atributo.
        /// </summary>
        public readonly int Value;
        /// <summary>
        /// Crea una nueva isntancia de la clase 
        /// <see cref="IntAttributeBase"/>.
        /// </summary>
        /// <param name="attrValue">Valor de este atributo.</param>
        protected IntAttributeBase(int attrValue)
        {
            Value = attrValue;
        }
    }

    /// <summary>
    /// Clase base para los atributos basados en números.
    /// </summary>
    public abstract class FloatAttributeBase : Attribute
    {
        /// <summary>
        /// Obtiene el valor asociado a este atributo.
        /// </summary>
        public readonly float Value;
        /// <summary>
        /// Crea una nueva isntancia de la clase 
        /// <see cref="FloatAttributeBase"/>.
        /// </summary>
        /// <param name="attrValue">Valor de este atributo.</param>
        protected FloatAttributeBase(float attrValue)
        {
            Value = attrValue;
        }
    }
    /// <summary>
    /// Clase base para los atributos basados en valores booleanos.
    /// </summary>
    public abstract class BoolAttributeBase : Attribute
    {
        /// <summary>
        /// Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo</value>
        public readonly bool Value;
        /// <summary>
        /// Crea una nueva isntancia de la clase 
        /// <see cref="BoolAttributeBase"/>.
        /// </summary>
        /// <param name="attrValue">Valor de este atributo.</param>
        protected BoolAttributeBase(bool attrValue)
        {
            Value = attrValue;
        }
    }

    /// <summary>
    /// Clase base para los atributos basados en valores booleanos.
    /// </summary>
    public abstract class VersionAttributeBase : Attribute
    {
        /// <summary>
        /// Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo</value>
        public readonly Version Value;
        /// <summary>
        /// Crea una nueva isntancia de la clase 
        /// <see cref="VersionAttributeBase"/>.
        /// </summary>
        /// <param name="attrValue">Valor de este atributo.</param>
        protected VersionAttributeBase(Version attrValue)
        {
            Value = attrValue;
        }
    }
    #endregion

    /// <summary>
    /// Marca un elemento como método de Thunking (facilita la llamada de otros
    /// métodos).
    /// </summary>
    [AttributeUsage((AttributeTargets)71)]
    public sealed class ThunkAttribute : Attribute { }

    /// <summary>
    /// Marca una clase para no ser cargada como 
    /// <see cref="PluginSupport.IPlugin"/>, a pesar de implementar
    /// <see cref="PluginSupport.IPlugin"/>.
    /// </summary>
    [AttributeUsage((AttributeTargets)1028)]
    public sealed class NotPluginAttribute : Attribute { }

    /// <summary>
    /// Especifica la versión de un elemento
    /// </summary>
    [AttributeUsage((AttributeTargets)71)]
    public sealed class VersionAttribute : VersionAttributeBase
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="VersionAttribute"/>.
        /// </summary>
        /// <param name="attrValue">Valor del atributo.</param>
        public VersionAttribute(Version attrValue) : base(attrValue) { }
        /// <summary>
        /// Inicializa unanueva instancia de la clase 
        /// <see cref="VersionAttribute"/>.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        public VersionAttribute(int major, int minor, int build = 0, int rev = 0)
            : base(new Version(major, minor, build, rev)) { }
        /// <summary>
        /// Inicializa unanueva instancia de la clase 
        /// <see cref="VersionAttribute"/>.
        /// </summary>
        /// <param name="majorMinor">Número de versión mayor/menor.</param>
        public VersionAttribute(float majorMinor = 1.0f)
            : base(new Version(majorMinor.ToString() + ".0.0")) { }
    }

    /// <summary>
    /// Especifica la versión mínima de MCART requerida por el elemento.
    /// </summary>
    [AttributeUsage((AttributeTargets)71)]
    public sealed class MinMCARTVersionAttribute : VersionAttributeBase
    {
        /// <summary>
        /// Inicializa unanueva instancia de la clase 
        /// <see cref="VersionAttribute"/>.
        /// </summary>
        /// <param name="attrValue">Valor del atributo.</param>
        public MinMCARTVersionAttribute(Version attrValue) : base(attrValue) { }
        /// <summary>
        /// Inicializa unanueva instancia de la clase 
        /// <see cref="VersionAttribute"/>.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        public MinMCARTVersionAttribute(int major, int minor, int build = 0, int rev = 0)
            : base(new Version(major, minor, build, rev)) { }
        /// <summary>
        /// Inicializa unanueva instancia de la clase 
        /// <see cref="VersionAttribute"/>.
        /// </summary>
        /// <param name="majorMinor">Número de versión mayor/menor.</param>
        public MinMCARTVersionAttribute(float majorMinor = 1.0f)
            : base(new Version(majorMinor.ToString() + ".0.0")) { }
    }

    /// <summary>
    /// Especifica la versión de MCART recomendada para el elemento.
    /// </summary>
    [AttributeUsage((AttributeTargets)71)]
    public sealed class TargetMCARTVersionAttribute : VersionAttributeBase
    {
        /// <summary>
        /// Inicializa unanueva instancia de la clase 
        /// <see cref="VersionAttribute"/>.
        /// </summary>
        /// <param name="attrValue">Valor del atributo.</param>
        public TargetMCARTVersionAttribute(Version attrValue) : base(attrValue) { }
        /// <summary>
        /// Inicializa unanueva instancia de la clase 
        /// <see cref="VersionAttribute"/>.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        public TargetMCARTVersionAttribute(int major, int minor, int build = int.MaxValue, int rev = int.MaxValue)
            : base(new Version(major, minor, build, rev)) { }
        /// <summary>
        /// Inicializa unanueva instancia de la clase 
        /// <see cref="VersionAttribute"/>.
        /// </summary>
        /// <param name="majorMinor">Número de versión mayor/menor.</param>
        public TargetMCARTVersionAttribute(float majorMinor = 1.0f)
            : base(new Version(majorMinor.ToString() + ".0.0")) { }
    }

    /// <summary>
    /// Marca un elemento como versión Beta.
    /// </summary>
    [AttributeUsage((AttributeTargets)71)]
    public sealed class BetaAttribute : Attribute { }

    /// <summary>
    /// Marca un elemento como inseguro.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class UnsecureAttribute : Attribute { }

    /// <summary>
    /// Marca un elemento como inestable.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Method |
        AttributeTargets.Class |
        AttributeTargets.Module |
        AttributeTargets.Assembly)]
    public sealed class UnstableAttribute : Attribute { }

    /// <summary>
    /// Establece un nombre personalizado para mostrar del elemento.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class NameAttribute : TextAttributeBase
    {
        /// <summary>
        /// Inicializa unanueva instancia de la clase 
        /// <see cref="NameAttribute"/>.
        /// </summary>
        /// <param name="attrValue">Valor del atributo.</param>
        public NameAttribute(string attrValue) : base(attrValue) { }
    }

    /// <summary>
    /// Establece una descripción para el elemento.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class DescriptionAttribute : TextAttributeBase
    {
        /// <summary>
        /// Inicializa unanueva instancia de la clase 
        /// <see cref="DescriptionAttribute"/>.
        /// </summary>
        /// <param name="attrValue">Valor del atributo.</param>
        public DescriptionAttribute(string attrValue) : base(attrValue) { }
    }

    /// <summary>
    /// Establece el autor del elemento.
    /// </summary>
    [AttributeUsage((AttributeTargets)71)]
    public sealed class AuthorAttribute : TextAttributeBase
    {
        /// <summary>
        /// Inicializa unanueva instancia de la clase 
        /// <see cref="DescriptionAttribute"/>.
        /// </summary>
        /// <param name="attrValue">Valor del atributo.</param>
        public AuthorAttribute(string attrValue) : base(attrValue) { }
    }

    /// <summary>
    /// Establece el autor del elemento.
    /// </summary>
    [AttributeUsage((AttributeTargets)71)]
    public sealed class CopyrightAttribute : TextAttributeBase
    {
        /// <summary>
        /// Inicializa unanueva instancia de la clase 
        /// <see cref="DescriptionAttribute"/>.
        /// </summary>
        /// <param name="attrValue">Valor del atributo.</param>
        public CopyrightAttribute(string attrValue) : base(attrValue) { }
    }

    /// <summary>
    /// Establece el texto de licencia a asociar con el elemento.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Module |
        AttributeTargets.Assembly)]
    public sealed class LicenseTextAttribute : TextAttributeBase
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="LicenseTextAttribute"/>.
        /// </summary>
        /// <param name="attrValue">Texto de la licencia.</param>
        public LicenseTextAttribute(string attrValue) : base(attrValue) { }
    }

    /// <summary>
    /// Establece un archivo de licencia a asociar con el elemento.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Module |
        AttributeTargets.Assembly)]
    public sealed class LicenseFileAttribute : TextAttributeBase
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="LicenseFileAttribute"/>.
        /// </summary>
        /// <param name="attrValue">
        /// Ruta del archivo de licencia adjunto.
        /// </param>
        public LicenseFileAttribute(string attrValue) : base(attrValue) { }
    }

    /// <summary>
    /// Marca un elemento como no utilizable.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class UnusableAttribute : Attribute { }

    /// <summary>
    /// Indica que un elemento podría tardar en ejecutarse, y por lo tanto,
    /// es capaz de reportar su progreso.
    /// </summary>
    [AttributeUsage((AttributeTargets)71)]
    public sealed class LenghtyAttribute : Attribute { }

    /// <summary>
    /// Indica que un elemento no está completo, y que no posee la
    /// funcionalidad necesaria.
    /// </summary>
    [AttributeUsage((AttributeTargets)71)]
    public sealed class StubAttribute : Attribute { }

    /// <summary>
    /// Attributo que define a un servidor.
    /// </summary>
    /// <remarks>
    /// Es posible establecer este atributo más de una vez en un mismo elemento.
    /// Los servidores adicionales definidos se utilizarán como redundancias, en
    /// caso que alguno de los servidores falle o no se encuentre accesible.
    /// </remarks>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class ServerAttribute : Attribute
    {
        string srv;
        ushort prt;
        /// <summary>
        /// Obtiene o establece el servidor.
        /// </summary>
        public string Server
        {
            get => srv;
            set
            {
                if (value.Contains(":"))
                {
                    // Probablemente, se incluye el puerto.
                    if (ushort.TryParse(value.Split(':')[1], out ushort prt))
                    {
                        Port = prt;
                    }
                    else throw new ArgumentException(nameof(Port));
                    srv = value.Split(':')[0];
                }
                else srv = value;
            }
        }
        /// <summary>
        /// Obtiene o establece el puerto de conexión del servidor.
        /// </summary>
        public ushort Port
        {
            get => prt;
            set
            {
                if (value == 0) throw new ArgumentOutOfRangeException(nameof(value));
                prt = value;
            }
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ServerAttribute"/> estableciendo el servidor y el puerto
        /// al cual este atributo hará referencia.
        /// </summary>
        /// <param name="server">Nombre del servidor / Dirección IP.</param>
        /// <param name="port">Número de puerto del servidor.</param>
        public ServerAttribute(string server, ushort port)
        {
            Server = server;
            if (Port != 0 && Port != port) throw new ArgumentException(nameof(port));
            Port = port;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ServerAttribute"/> estableciendo el servidor y el puerto
        /// al cual este atributo hará referencia.
        /// </summary>
        /// <param name="server">
        /// Nombre del servidor / Dirección IP. También incluye el número de 
        /// puerto en el formato <c>"servidor:puerto"</c>.
        /// </param>
        public ServerAttribute(string server)
        {
            Server = server;

            //Esta comprobación parece ser redundante...
            //if (Port == 0) throw new ArgumentException($"{nameof(server)} debe incluir el número de puerto.", nameof(server));
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ServerAttribute"/> a partir de un objeto
        /// <see cref="System.Net.IPEndPoint"/>.
        /// </summary>
        /// <param name="endPoint">
        /// <see cref="System.Net.IPEndPoint"/> que apunta al servidor.
        /// </param>
        public ServerAttribute(System.Net.IPEndPoint endPoint)
        {
            Server = endPoint.Address.ToString();
            Port = (ushort)endPoint.Port;
        }
    }
}