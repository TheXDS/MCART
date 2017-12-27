//
//  Plugin.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
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

using MCART.Attributes;
using MCART.Types.TaskReporter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using St = MCART.Resources.Strings;

namespace MCART.PluginSupport
{
    /// <summary>
    /// Clase base para todos los plugins que puedan ser contruídos y
    /// administrador por MCART.
    /// </summary>
    public abstract partial class Plugin : IPlugin
    {
        ITaskReporter rptr = new DummyTaskReporter();
        /// <summary>
        /// Colección de <see cref="InteractionItem"/> del
        /// <see cref="Plugin"/>.
        /// </summary>
        protected readonly ObservableCollection<InteractionItem> uiMenu = new ObservableCollection<InteractionItem>();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Plugin"/>.
        /// </summary>
        public Plugin()
        {
            foreach (var j in GetType().GetMethods().Where(k => k.HasAttr<InteractionItemAttribute>()))
                uiMenu.Add(new InteractionItem(j, this));
            uiMenu.CollectionChanged += (sender, e) => RequestUIChange();
            RaisePluginLoaded(DateTime.Now);
        }
        
        #region Propiedades de identificación
        /// <summary>
        /// Obtiene el nombre de este <see cref="Plugin"/>.
        /// </summary>
        public virtual string Name => GetType().GetAttrAlt<NameAttribute>()?.Value ?? GetType().Name;
        /// <summary>
        /// Obtiene la versión de este <see cref="Plugin"/>.
        /// </summary>
        public virtual Version Version => GetType().GetAttrAlt<VersionAttribute>()?.Value ?? MyAssembly.GetName().Version;
        /// <summary>
        /// Obtiene la descripción de este <see cref="Plugin"/>.
        /// </summary>
        public virtual string Description => GetType().GetAttrAlt<DescriptionAttribute>()?.Value
            ?? (Attribute.GetCustomAttribute(MyAssembly, typeof(AssemblyDescriptionAttribute)) as AssemblyDescriptionAttribute)?.Description;
        /// <summary>
        /// Obtiene el autor de este <see cref="Plugin"/>.
        /// </summary>
        public virtual string Author => GetType().GetAttrAlt<AuthorAttribute>()?.Value
            ?? (Attribute.GetCustomAttribute(MyAssembly, typeof(AssemblyCompanyAttribute)) as AssemblyCompanyAttribute)?.Company;
        /// <summary>
        /// Obtiene la cadena de Copyright de este <see cref="Plugin"/>.
        /// </summary>
        public virtual string Copyright => GetType().GetAttrAlt<CopyrightAttribute>()?.Value
            ?? (Attribute.GetCustomAttribute(MyAssembly, typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute)?.Copyright;
        /// <summary>
        /// Obtiene el texto de la licencia de este <see cref="Plugin"/>.
        /// </summary>
        public virtual string License
        {
            get
            {
                try
                {
                    // Intentar buscar archivo...
                    if ((this.HasAttr<LicenseFileAttribute>(out var fileLic) || MyAssembly.HasAttr(out fileLic)) && File.Exists(fileLic?.Value))
                    {
                        StreamReader inp = new StreamReader(fileLic?.Value);
                        return inp.ReadToEnd();
                    }

                    // Intentar buscar archivo embebido...
                    if (this.HasAttr<EmbeededLicenseAttribute>(out var embLic) || MyAssembly.HasAttr(out embLic))
                    {
                        Stream s = null;
                        StreamReader r = null;
                        try
                        {
                            s = MyAssembly.GetManifestResourceStream(embLic?.Value);
                            r = new StreamReader(s);
                            return r.ReadToEnd();
                        }
                        finally
                        {
                            r?.Dispose();
                            s?.Dispose();
                        }
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
        public virtual Version TargetMCARTVersion => GetType().GetAttrAlt<TargetMCARTVersionAttribute>()?.Value;
        /// <summary>
        /// Determina si este <see cref="Plugin"/> es una versión Beta.
        /// </summary>
        public bool IsBeta => GetType().HasAttrAlt<BetaAttribute>();
        /// <summary>
        /// Determina si este <see cref="Plugin"/> es considerado como 
        /// inseguro.
        /// </summary>
        public bool IsUnsafe => GetType().HasAttrAlt<UnsecureAttribute>();
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
        public ITaskReporter Reporter { get => rptr; set => rptr = value ?? throw new ArgumentNullException(nameof(value)); }
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
        public event UIChangeRequestedEventHandler UIChangeRequested;
        /// <summary>
        /// Se produce cuando un <see cref="Plugin"/> va a ser finalizado.
        /// </summary>
        public event PluginFinalizingEventHandler PluginFinalizing;
        /// <summary>
        /// Se produce cuando un <see cref="Plugin"/> ha sido finalizado.
        /// </summary>
        public event PluginFinalizedEventHandler PluginFinalized;
        /// <summary>
        /// Se produce cuando un <see cref="Plugin"/> ha sido cargado.
        /// </summary>
        public event PluginLoadedEventHandler PluginLoaded;
        /// <summary>
        /// Se produce cuando un <see cref="Plugin"/> no pudo ser cargado.
        /// </summary>
        public event PluginLoadFailedEventHandler PluginLoadFailed;

        /// <summary>
        /// Genera el evento <see cref="UIChangeRequested"/>.
        /// </summary>
        public void RequestUIChange() => UIChangeRequested?.Invoke(this, new UIChangeEventArgs(PluginInteractions));
        /// <summary>
        /// Genera el evento <see cref="PluginLoadFailed"/>.
        /// </summary>
        /// <param name="ex">
        /// Parámetro opcional. <see cref="Exception"/> que ha causado que el
        /// <see cref="Plugin"/> no pueda inicializarse.
        /// </param>
        protected void RaiseFailed(Exception ex = null) => PluginLoadFailed?.Invoke(this, new PluginFinalizedEventArgs(ex));
        /// <summary>
        /// Genera el evento <see cref="PluginFinalizing"/>.
        /// </summary>
        /// <param name="reason">
        /// Parámetro opcional. Razón por la que el plugin va a finalizar.
        /// </param>
        protected void RaiseFinalizing(PluginFinalizingEventArgs.FinalizingReason reason = PluginFinalizingEventArgs.FinalizingReason.Shutdown) => PluginFinalizing?.Invoke(this, new PluginFinalizingEventArgs(reason));
        /// <summary>
        /// Genera el evento <see cref="PluginLoaded"/>.
        /// </summary>
        /// <param name="tme">
        /// Instante de carga del <see cref="Plugin"/>.
        /// </param>
        /// <remarks>
        /// Este es un método para uso interno de MCART. Esta función no debe
        /// ser llamada por ningún código externo.
        /// </remarks>
        private void RaisePluginLoaded(DateTime tme) => PluginLoaded?.Invoke(this, new PluginLoadedEventArgs((DateTime.Now - tme).Ticks));
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
            return !minVersion.IsNull();
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
            return !tgtVersion.IsNull();
        }
        #endregion

        /// <summary>
        /// Realiza algunas tareas previas a la destrucción de esta instancia 
        /// de <see cref="Plugin"/> por el colector de basura.
        /// </summary>
        ~Plugin()
        {
            PluginFinalized?.Invoke(null, new PluginFinalizedEventArgs());
        }
    }
}