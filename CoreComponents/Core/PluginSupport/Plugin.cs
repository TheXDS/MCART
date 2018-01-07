//
//  Plugin.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types.TaskReporter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Exceptions;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.PluginSupport
{
    /// <summary>
    /// Clase base para todos los plugins que puedan ser contruídos y
    /// administrador por MCART.
    /// </summary>
    public abstract partial class Plugin : IPlugin
    {
        ITaskReporter _reporter = new DummyTaskReporter();
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
        protected readonly ObservableCollection<InteractionItem> uiMenu = new ObservableCollection<InteractionItem>();
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
                    uiMenu.Add(new InteractionItem(j, this));
                else
#if PreferExceptions
                    throw new PluginException(this, new InvalidMethodSignatureException(j));
#else
                    System.Diagnostics.Debug.Print(St.Warn(St.InvalidSignature(St.XYQuotes(St.TheMethod, $"{GetType().FullName}.{j.Name}"))));
#endif
            }
            uiMenu.CollectionChanged += (sender, e) => OnUIChanged();
        }
        #region Propiedades de identificación
        /// <summary>
        /// Obtiene el nombre de este <see cref="Plugin"/>.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="NameAttribute"/> definido para
        /// este <see cref="Plugin"/>, o en caso de no establecer el atributo,
        /// se devolverá el nombre del tipo de este <see cref="Plugin"/>.
        /// </value>
        public virtual string Name => GetType().GetAttrAlt<NameAttribute>()?.Value ?? GetType().Name;
        /// <summary>
        /// Obtiene la versión de este <see cref="Plugin"/>.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="VersionAttribute"/> definido para 
        /// la implementación de <see cref="Plugin"/>, o en caso de no 
        /// establecer el atributo, se devolverá la versión del ensamblado que
        /// contiene a este <see cref="Plugin"/>.
        /// </value>
        public virtual Version Version => GetType().GetAttrAlt<VersionAttribute>()?.Value ?? MyAssembly.GetName().Version;
        /// <summary>
        /// Obtiene la descripción de este <see cref="Plugin"/>.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="DescriptionAttribute"/> definido
        /// para este <see cref="Plugin"/>, o en caso de no establecer el
        /// atributo, se devolverá la descripción del ensamblado que contiene a
        /// este <see cref="Plugin"/>, o <c>null</c> en caso de no existir.
        /// </value>
        public virtual string Description => GetType().GetAttrAlt<DescriptionAttribute>()?.Value
            ?? (Attribute.GetCustomAttribute(MyAssembly, typeof(AssemblyDescriptionAttribute)) as AssemblyDescriptionAttribute)?.Description;
        /// <summary>
        /// Obtiene el autor de este <see cref="Plugin"/>.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="AuthorAttribute"/> definido para
        /// este <see cref="Plugin"/>, o en caso de no establecer el atributo,
        /// se devolverá el nombre de la compañía del ensamblado que contiene a
        /// este <see cref="Plugin"/>, o <c>null</c> en caso de no existir.
        /// </value>
        public virtual string Author => GetType().GetAttrAlt<AuthorAttribute>()?.Value
            ?? (Attribute.GetCustomAttribute(MyAssembly, typeof(AssemblyCompanyAttribute)) as AssemblyCompanyAttribute)?.Company;
        /// <summary>
        /// Obtiene la cadena de Copyright de este <see cref="Plugin"/>.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="AuthorAttribute"/> definido para
        /// este <see cref="Plugin"/>, o en caso de no establecer el atributo,
        /// se devolverá el nombre de la compañía del ensamblado que contiene a
        /// este <see cref="Plugin"/>, o <c>null</c> en caso de no existir.
        /// </value>
        public virtual string Copyright => GetType().GetAttrAlt<CopyrightAttribute>()?.Value
            ?? (Attribute.GetCustomAttribute(MyAssembly, typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute)?.Copyright;
        /// <summary>
        /// Obtiene el texto de la licencia de este <see cref="Plugin"/>.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="LicenseFileAttribute"/>,
        /// <see cref="EmbeededLicenseAttribute"/> o
        /// <see cref="LicenseTextAttribute"/>, cualesquiera esté definido
        /// primero para este <see cref="Plugin"/>, o en caso de no establecer
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
        public virtual string License
        {
            get
            {
                try
                {
                    // Intentar buscar archivo...
                    if ((this.HasAttr<LicenseFileAttribute>(out var fileLic) || MyAssembly.HasAttr(out fileLic)) && File.Exists(fileLic?.Value))
                    {
                        using (StreamReader inp = new StreamReader(fileLic?.Value))
                            return inp.ReadToEnd();
                    }

                    // Intentar buscar archivo embebido...
                    if (this.HasAttr<EmbeededLicenseAttribute>(out var embLic) || MyAssembly.HasAttr(out embLic))
                    {
                        using (var s = MyAssembly.GetManifestResourceStream(embLic?.Value))
                        using (var r = new StreamReader(s))
                            return r.ReadToEnd();
                    }

                    // Buscar texto de licencia...
                    if (this.HasAttr<LicenseTextAttribute>(out var txtLic) || MyAssembly.HasAttr(out txtLic) && !txtLic.Value.IsEmpty())
                        return txtLic?.Value;
                    else
                        return St.Warn(St.UnspecLicense);
                }
                catch (Exception ex)
                {
                    return $"{ex.Message}\n-------------------------\n{ex.StackTrace}";
                }
            }
        }
        /// <summary>
        /// Determina la versión mínima de MCART necesaria para este 
        /// <see cref="Plugin"/>.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="MinMCARTVersionAttribute"/>
        /// definido como la versión mínima de MCART requerida para un correcto
        /// funcionamiento de este <see cref="Plugin"/>.
        /// </value>
        /// <remarks>
        /// Si no se encuentra el atributo 
        /// <see cref="MinMCARTVersionAttribute"/> en la clase o en el 
        /// ensamblado, se devolverá <see cref="TargetMCARTVersion"/>.
        /// </remarks>
        public virtual Version MinMCARTVersion => GetType().GetAttrAlt<MinMCARTVersionAttribute>()?.Value ?? TargetMCARTVersion;
        /// <summary>
        /// Determina la versión objetivo de MCART para este 
        /// <see cref="Plugin"/>.
        /// </summary>
        /// <value>
        /// El valor del atributo <see cref="TargetMCARTVersionAttribute"/>
        /// definido como la versión de MCART para la cual este 
        /// <see cref="Plugin"/> ha sido diseñado.
        /// </value>
        /// <remarks>
        /// Si este <see cref="Plugin"/> se intenta cargar en una versión no
        /// soportada de MCART, la carga es abortada.
        /// <note type="caution">(NO RECOMENDADO)
        /// En caso de que desee cargar un plugin sin verificación de
        /// compatibilidad, cree un nuevo <see cref="PluginLoader"/> utilizando
        /// una nueva instancia de <see cref="RelaxedPluginChecker"/> como
        /// verificador de <see cref="Plugin"/>. Tome en cuenta que, es posible
        /// que el programa falle si se intenta utilizar un plugin no
        /// compatible, debido a los posibles cambios de API entre versiones de
        /// MCART.
        /// </note>
        /// </remarks>
        public virtual Version TargetMCARTVersion => GetType().GetAttrAlt<TargetMCARTVersionAttribute>()?.Value;
        /// <summary>
        /// Determina si este <see cref="Plugin"/> es una versión Beta.
        /// </summary>
        public bool IsBeta => GetType().HasAttrAlt<BetaAttribute>();
        /// <summary>
        /// Determina si este <see cref="Plugin"/> contiene código no
        /// administrado.
        /// </summary>
        public bool IsUnmanaged => GetType().HasAttrAlt<UnmanagedAttribute>();
        /// <summary>
        /// Determina si este <see cref="Plugin"/> es considerado como 
        /// inestable.
        /// </summary>
        public bool IsUnstable => GetType().HasAttrAlt<UnstableAttribute>();
        #endregion
        #region Propiedades
        /// <summary>
        /// Obtiene una colección de las interfaces implementadas por este
        /// <see cref="Plugin"/>.
        /// </summary>
        public IEnumerable<Type> Interfaces
        {
            get
            {
                foreach (Type j in GetType().GetInterfaces())
                    if (j.IsNeither(typeof(IPlugin), typeof(IDisposable)))
                        yield return j;
            }
        }
        /// <summary>
        /// Obtiene la referencia al emsamblado que contiene a este 
        /// <see cref="Plugin"/>.
        /// </summary>
        /// <value>
        /// Ensamblado en el cual se declara este <see cref="Plugin"/>.
        /// </value>
        [Thunk] public Assembly MyAssembly => GetType().Assembly;
        /// <summary>
        /// Contiene una lista de interacciones que este <see cref="Plugin"/>.
        /// provee para incluir en una interfaz gráfica.
        /// </summary>
        public ReadOnlyCollection<InteractionItem> PluginInteractions => new ReadOnlyCollection<InteractionItem>(uiMenu);
        /// <summary>
        /// Indica si este <see cref="Plugin"/> contiene o no interacciones.
        /// </summary>        
        public bool HasInteractions => uiMenu.Any();
        /// <summary>
        /// Referencia al objeto <see cref="ITaskReporter"/> a utilizar por las
        /// funciones de este <see cref="Plugin"/>.
        /// </summary>
        public ITaskReporter Reporter { get => _reporter; set => _reporter = value ?? throw new ArgumentNullException(nameof(value)); }
        /// <summary>
        /// Contiene un objeto de libre uso para almacenamiento de cualquier
        /// inatancia que el usuario desee asociar a este <see cref="Plugin"/>.
        /// </summary>
        public object Tag { get; set; }
        #endregion
        #region Eventos y señales
        /// <summary>
        /// Se produce cuando un <see cref="Plugin"/> solicita que se actualice
        /// su interfaz gráfica, en caso de contenerla.
        /// </summary>
        public event EventHandler<UIChangedEventArgs> UIChanged;
        /// <summary>
        /// Se produce cuando un <see cref="Plugin"/> va a ser finalizado.
        /// </summary>
        public event EventHandler<PluginFinalizingEventArgs> PluginFinalizing;
        /// <summary>
        /// Genera el evento <see cref="UIChanged"/>.
        /// </summary>
        /// <param name="e">Argumentos del evento.</param>
        protected virtual void OnUIChanged(UIChangedEventArgs e) => UIChanged?.Invoke(this, e);
        /// <summary>
        /// Genera el evento <see cref="UIChanged"/>.
        /// </summary>
        protected void OnUIChanged() => OnUIChanged(new UIChangedEventArgs(PluginInteractions));
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
        /// <c>true</c> si fue posible obtener información sobre la versión 
        /// mínima de MCART, <c>false</c> en caso contrario.
        /// </returns>
        [Thunk]
        public bool MinRTVersion(out Version minVersion)
        {
            minVersion = MinMCARTVersion;
            return !(minVersion is null);
        }
        /// <summary>
        /// Determina la versión objetivo de MCART necesaria para este 
        /// <see cref="Plugin"/>.
        /// </summary>
        /// <param name="tgtVersion">Versión objetivo de MCART.</param>
        /// <returns>
        /// <c>true</c> si fue posible obtener información sobre la versión 
        /// objetivo de MCART, <c>false</c> en caso contrario.
        /// </returns>
        [Thunk]
        public bool TargetRTVersion(out Version tgtVersion)
        {
            tgtVersion = TargetMCARTVersion;
            return !(tgtVersion is null);
        }
        #endregion
    }
}