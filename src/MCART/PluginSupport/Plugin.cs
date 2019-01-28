/*
Plugin.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Component;
using static TheXDS.MCART.Misc.Internal;
using St = TheXDS.MCART.Resources.Strings;
using St2 = TheXDS.MCART.Resources.InternalStrings;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace TheXDS.MCART.PluginSupport
{
    /// <inheritdoc />
    /// <summary>
    /// Clase base para todos los plugins que puedan ser construidos y
    /// administrador por MCART.
    /// </summary>
    public abstract class Plugin : IPlugin
    {
        /// <summary>
        ///     Muestra información básica sobre el <see cref="IPlugin"/> en la
        ///     consola del sistema.
        /// </summary>
        /// <param name="p"></param>
        public static void About(IPlugin p)
        {
            if (p is null) return;
            try
            {
                Console.WriteLine(p.Name);
                Console.WriteLine(p.Version);
                Console.WriteLine(p.Description);
                Console.WriteLine(p.Copyright);
                if (p.HasLicense) Console.WriteLine(p.License);
            }
            catch
            {
#if DEBUG
                System.Diagnostics.Debug.Print(St.Warn(St2.ErrorShowingPluginInfo));
#else    
                /* Ignorar, probablemente no hay consola. */
#endif
            }
        }

        /// <summary>
        /// Colección de <see cref="InteractionItem"/> del
        /// <see cref="Plugin"/>.
        /// </summary>
        /// <remarks>
        /// Esta colección contendrá todas las interacciones asociadas al
        /// <see cref="Plugin"/>, tanto las que MCART cablea automáticamente,
        /// como la que sean agregadas durante la ejecución.
        /// </remarks>
        /// <example>
        /// En este ejemplo se muestran distintos métodos para agregar
        /// elementos de interacción a un <see cref="Plugin"/>:
        /// <code language="cs" source="..\..\Documentation\Examples\PluginSupport\Plugin.cs" region="uiMenu1"/>
        /// <code language="vb" source="..\..\Documentation\Examples\PluginSupport\Plugin.vb" region="uiMenu1"/>
        /// </example>
        /// <seealso cref="InteractionItem"/>
        protected ObservableCollection<InteractionItem> InteractionItems { get; } = new ObservableCollection<InteractionItem>();
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Plugin"/>.
        /// </summary>
        /// <remarks>
        /// Este constructor cableará automáticamente cualquier método público
        /// con una firma compatible con <see cref="EventArgs"/> que tenga el
        /// atributo <see cref="InteractionItemAttribute"/>.
        /// </remarks>
        [System.Diagnostics.DebuggerStepThrough]
        protected Plugin()
        {
            foreach (var j in GetType().GetMethods().Where(k => k.HasAttr<InteractionItemAttribute>()))
            {
                if (!(Delegate.CreateDelegate(typeof(EventHandler), this, j, false) is null))
                    InteractionItems.Add(new InteractionItem(j, this));
                else
#if PreferExceptions
                    throw new PluginException(this, new InvalidMethodSignatureException(j));
#else
                    System.Diagnostics.Debug.Print(St.Warn(St.InvalidSignature(St.XYQuotes(St.TheMethod, $"{GetType().FullName}.{j.Name}"))));
#endif
            }
            InteractionItems.CollectionChanged += (sender, e) => OnUiChanged();
        }
        #region Propiedades de identificación
        /// <inheritdoc />
        /// <summary>
        /// Obtiene el nombre de este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="T:TheXDS.MCART.Attributes.NameAttribute" /> definido para
        /// este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />, o en caso de no establecer el atributo,
        /// se devolverá el nombre del tipo de este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </value>
        public virtual string Name => GetType().GetAttrAlt<NameAttribute>()?.Value ?? GetType().Name;
        /// <inheritdoc />
        /// <summary>
        /// Obtiene la versión de este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="T:TheXDS.MCART.Attributes.VersionAttribute" /> definido para 
        /// la implementación de <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />, o en caso de no 
        /// establecer el atributo, se devolverá la versión del ensamblado que
        /// contiene a este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </value>
        public virtual Version Version => GetType().GetAttrAlt<VersionAttribute>()?.Value ?? Assembly.GetName().Version;
        /// <inheritdoc />
        /// <summary>
        /// Obtiene la descripción de este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="T:TheXDS.MCART.Attributes.DescriptionAttribute" /> definido
        /// para este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />, o en caso de no establecer el
        /// atributo, se devolverá la descripción del ensamblado que contiene a
        /// este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />, o <see langword="null" /> en caso de no existir.
        /// </value>
        public virtual string Description => GetType().GetAttrAlt<DescriptionAttribute>()?.Value
            ?? (Attribute.GetCustomAttribute(Assembly, typeof(AssemblyDescriptionAttribute)) as AssemblyDescriptionAttribute)?.Description;
        /// <inheritdoc />
        /// <summary>
        /// Obtiene el autor de este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="T:TheXDS.MCART.Attributes.AuthorAttribute" /> definido para
        /// este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />, o en caso de no establecer el atributo,
        /// se devolverá el nombre de la compañía del ensamblado que contiene a
        /// este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />, o <see langword="null" /> en caso de no existir.
        /// </value>
        public virtual string Author => GetType().GetAttrAlt<AuthorAttribute>()?.Value
            ?? (Attribute.GetCustomAttribute(Assembly, typeof(AssemblyCompanyAttribute)) as AssemblyCompanyAttribute)?.Company;
        /// <inheritdoc />
        /// <summary>
        /// Obtiene la cadena de Copyright de este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="T:TheXDS.MCART.Attributes.AuthorAttribute" /> definido para
        /// este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />, o en caso de no establecer el atributo,
        /// se devolverá el nombre de la compañía del ensamblado que contiene a
        /// este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />, o <see langword="null" /> en caso de no existir.
        /// </value>
        public virtual string Copyright => GetType().GetAttrAlt<CopyrightAttribute>()?.Value
            ?? (Attribute.GetCustomAttribute(Assembly, typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute)?.Copyright;
        /// <inheritdoc />
        /// <summary>
        /// Obtiene el texto de la licencia de este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="T:TheXDS.MCART.Attributes.LicenseFileAttribute" />,
        /// <see cref="T:TheXDS.MCART.Attributes.EmbeddedLicenseAttribute" /> o
        /// <see cref="T:TheXDS.MCART.Attributes.LicenseTextAttribute" />, cualesquiera esté definido
        /// primero para este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />, o en caso de no establecer
        /// ninguno de los atributos, se devolverá un texto de licencia no
        /// establecida, o un mensaje de error junto con el StackTrace en caso
        /// de no poder obtener la información de licencia debido a una
        /// excepción.
        /// <note type="note">
        /// La licencia será buscada en el mismo orden establecido en el
        /// apartado de valor, y se devolverá únicamente un atributo al ser
        /// encontrado.
        /// </note>
        /// </value>
        public virtual string License =>
            ReadLicense(this) ??
            ReadLicense(Assembly,false);

        /// <inheritdoc />
        /// <summary>
        /// Obtiene un valor que determina si este <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// contiene información de licencia.
        /// </summary>
        public bool HasLicense => HasLicense(this) || HasLicense(Assembly);


        /// <inheritdoc />
        /// <summary>
        /// Determina la versión mínima de MCART necesaria para este 
        /// <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="T:TheXDS.MCART.Attributes.MinMCARTVersionAttribute" />
        /// definido como la versión mínima de MCART requerida para un correcto
        /// funcionamiento de este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </value>
        /// <remarks>
        /// Si no se encuentra el atributo 
        /// <see cref="T:TheXDS.MCART.Attributes.MinMCARTVersionAttribute" /> en la clase o en el 
        /// ensamblado, se devolverá <see cref="P:TheXDS.MCART.PluginSupport.Plugin.TargetMCARTVersion" />.
        /// </remarks>
        public virtual Version MinMcartVersion => GetType().GetAttrAlt<MinMcartVersionAttribute>()?.Value ?? TargetMcartVersion;
        /// <inheritdoc />
        /// <summary>
        /// Determina la versión objetivo de MCART para este 
        /// <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="T:TheXDS.MCART.Attributes.TargetMCARTVersionAttribute" />
        /// definido como la versión de MCART para la cual este 
        /// <see cref="T:TheXDS.MCART.PluginSupport.Plugin" /> ha sido diseñado.
        /// </value>
        /// <remarks>
        /// Si este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" /> se intenta cargar en una versión no
        /// soportada de MCART, la carga es abortada.
        /// <note type="caution">(NO RECOMENDADO)
        /// En caso de que desee cargar un plugin sin verificación de
        /// compatibilidad, cree un nuevo <see cref="T:TheXDS.MCART.PluginSupport.PluginLoader" /> utilizando
        /// una nueva instancia de <see cref="T:TheXDS.MCART.PluginSupport.RelaxedPluginChecker" /> como
        /// verificador de <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />. Tome en cuenta que, es posible
        /// que el programa falle si se intenta utilizar un plugin no
        /// compatible, debido a los posibles cambios de API entre versiones de
        /// MCART.
        /// </note>
        /// </remarks>
        public virtual Version TargetMcartVersion => GetType().GetAttrAlt<TargetMCARTVersionAttribute>()?.Value;

        /// <summary>
        ///     Obtiene un valor que determina si el <see cref="Plugin"/> es compatible con esta versión de MCART.
        /// </summary>
        public bool? IsSupported => Resources.RtInfo.RtSupport(GetType());

        /// <inheritdoc />
        /// <summary>
        /// Determina si este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" /> es una versión Beta.
        /// </summary>
        public bool IsBeta => GetType().HasAttrAlt<BetaAttribute>();
        /// <inheritdoc />
        /// <summary>
        /// Determina si este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" /> contiene código no
        /// administrado.
        /// </summary>
        public bool IsUnmanaged => GetType().HasAttrAlt<UnmanagedAttribute>();
        /// <inheritdoc />
        /// <summary>
        /// Determina si este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" /> es considerado como 
        /// inestable.
        /// </summary>
        public bool IsUnstable => GetType().HasAttrAlt<UnstableAttribute>();

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un valor que indica si este <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        ///     cumple con el Common Language Standard (CLS)
        /// </summary>
        public bool ClsCompliant => GetType().HasAttrAlt<CLSCompliantAttribute>();

        #endregion
        #region Propiedades

        /// <inheritdoc />
        /// <summary>
        /// Obtiene una colección de las interfaces implementadas por este
        /// <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </summary>
        public IEnumerable<Type> Interfaces
        {
            get
            {
                foreach (var j in GetType().GetInterfaces())
                    if (j.IsNeither(typeof(IPlugin), typeof(IDisposable)))
                        yield return j;
            }
        }
        /// <inheritdoc />
        /// <summary>
        /// Obtiene la referencia al ensamblado que contiene a este 
        /// <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </summary>
        /// <value>
        /// Ensamblado en el cual se declara este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </value>
        [Thunk] public Assembly Assembly => GetType().Assembly;
        /// <inheritdoc />
        /// <summary>
        /// Contiene una lista de interacciones que este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// provee para incluir en una interfaz gráfica.
        /// </summary>
        public ReadOnlyCollection<InteractionItem> PluginInteractions => new ReadOnlyCollection<InteractionItem>(InteractionItems);
        /// <inheritdoc />
        /// <summary>
        /// Indica si este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" /> contiene o no interacciones.
        /// </summary>        
        public bool HasInteractions => InteractionItems.Any();
        /// <inheritdoc />
        /// <summary>
        /// Contiene un objeto de libre uso para almacenamiento de cualquier
        /// instancia que el usuario desee asociar a este <see cref="T:TheXDS.MCART.PluginSupport.Plugin" />.
        /// </summary>
        public object Tag { get; set; }

        #endregion
        #region Eventos y señales
        /// <inheritdoc />
        /// <summary>
        /// Se produce cuando un <see cref="T:TheXDS.MCART.PluginSupport.Plugin" /> solicita que se actualice
        /// su interfaz gráfica, en caso de contenerla.
        /// </summary>
        public event EventHandler<UiChangedEventArgs> UiChanged;
        /// <inheritdoc />
        /// <summary>
        /// Se produce cuando un <see cref="T:TheXDS.MCART.PluginSupport.Plugin" /> va a ser finalizado.
        /// </summary>
        public event EventHandler<PluginFinalizingEventArgs> PluginFinalizing;
        /// <summary>
        /// Genera el evento <see cref="UiChanged"/>.
        /// </summary>
        /// <param name="e">Argumentos del evento.</param>
        protected virtual void OnUiChanged(UiChangedEventArgs e) => UiChanged?.Invoke(this, e);
        /// <summary>
        /// Genera el evento <see cref="UiChanged"/>.
        /// </summary>
        protected void OnUiChanged() => OnUiChanged(new UiChangedEventArgs(PluginInteractions));
        /// <summary>
        /// Genera el evento <see cref="PluginFinalizing"/>.
        /// </summary>
        /// <param name="e">Argumentos del evento.</param>
        protected virtual void OnPluginFinalizing(PluginFinalizingEventArgs e) => PluginFinalizing?.Invoke(this, e);
        /// <summary>
        /// Genera el evento <see cref="PluginFinalizing"/>.
        /// </summary>
        /// <param name="reason">
        /// Parámetro opcional. Razón por la que el plugin va a finalizar.
        /// </param>
        protected void OnPluginFinalizing(PluginFinalizingEventArgs.FinalizingReason reason = PluginFinalizingEventArgs.FinalizingReason.Shutdown) => OnPluginFinalizing(new PluginFinalizingEventArgs(reason));
        #endregion
        #region Métodos públicos
        /// <summary>
        /// Determina la versión mínima de MCART necesaria para este 
        /// <see cref="Plugin"/>.
        /// </summary>
        /// <param name="minVersion">Versión mínima de MCART.</param>
        /// <returns>
        /// <see langword="true"/> si fue posible obtener información sobre la versión 
        /// mínima de MCART, <see langword="false"/> en caso contrario.
        /// </returns>
        [Thunk]
        public bool MinRtVersion(out Version minVersion)
        {
            minVersion = MinMcartVersion;
            return !(minVersion is null);
        }
        /// <summary>
        /// Determina la versión objetivo de MCART necesaria para este 
        /// <see cref="Plugin"/>.
        /// </summary>
        /// <param name="tgtVersion">Versión objetivo de MCART.</param>
        /// <returns>
        /// <see langword="true"/> si fue posible obtener información sobre la versión 
        /// objetivo de MCART, <see langword="false"/> en caso contrario.
        /// </returns>
        [Thunk]
        public bool TargetRtVersion(out Version tgtVersion)
        {
            tgtVersion = TargetMcartVersion;
            return !(tgtVersion is null);
        }
        #endregion
    }
}