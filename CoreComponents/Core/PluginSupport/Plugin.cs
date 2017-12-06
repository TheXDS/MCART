//
//  Plugin.cs
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
using System.Collections.ObjectModel;
using System.Reflection;
using MCART.Types.Extensions;
using MCART.Types.TaskReporter;
using MCART.Attributes;
using MCART.Exceptions;
using St = MCART.Resources.Strings;
using System.IO;

namespace MCART.PluginSupport
{
    /// <summary>
    /// Clase base para todos los plugins que puedan ser contruídos y
    /// administrador por MCART.
    /// </summary>
    public abstract partial class Plugin : IPlugin
    {
        /// <summary>
        /// Provee a este <see cref="Plugin"/> de un menú de interacciones.
        /// </summary>
        protected readonly List<InteractionItem> MyMenu = new List<InteractionItem>();
        /// <summary>
        /// Valor privado para la propiedad <see cref="AutoUpdateMyMenu"/>.
        /// </summary>
        bool auMyMenu;
        /// <summary>
        /// Determina si <see cref="MyMenu"/> solicitará automáticamente la
        /// actualización de la interfaz gráfica a la aplicación.
        /// </summary>
        protected bool AutoUpdateMyMenu
        {
            get => auMyMenu;
            set
            {
                if (value)
                {
                    MyMenu.AddedItem += AutoUpdateEvtHandler;
                    MyMenu.ModifiedItem += AutoUpdateEvtHandler;
                    MyMenu.RemovedItem += AutoUpdateEvtHandler;
                    MyMenu.ListCleared += AutoUpdateEvtHandler;
                    MyMenu.ListUpdated += AutoUpdateEvtHandler;
                }
                else
                {
                    MyMenu.AddedItem -= AutoUpdateEvtHandler;
                    MyMenu.ModifiedItem -= AutoUpdateEvtHandler;
                    MyMenu.RemovedItem -= AutoUpdateEvtHandler;
                    MyMenu.ListCleared -= AutoUpdateEvtHandler;
                    MyMenu.ListUpdated -= AutoUpdateEvtHandler;
                }
                auMyMenu = value;
            }
        }
        /// <summary>
        /// Maneja las llamadas de actualizacióón automática de la
        /// interfaz gráfica de este <see cref="Plugin"/>.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        void AutoUpdateEvtHandler(object sender, EventArgs e) { RequestUIChange(); }
        /// <summary>
        /// Obtiene el nombre de este <see cref="Plugin"/>.
        /// </summary>
        public virtual string Name => this.GetAttr<NameAttribute>()?.Value ?? GetType().Name;
        /// <summary>
        /// Obtiene la versión de este <see cref="Plugin"/>.
        /// </summary>
        public virtual Version Version => this.GetAttr<VersionAttribute>()?.Value ?? MyAssembly.GetName().Version;
        /// <summary>
        /// Obtiene la descripción de este <see cref="Plugin"/>.
        /// </summary>
        public virtual string Description => this.GetAttr<DescriptionAttribute>()?.Value ?? MyAssembly.GetAttr<AssemblyDescriptionAttribute>().Description;
        /// <summary>
        /// Obtiene el autor de este <see cref="Plugin"/>.
        /// </summary>
        public virtual string Author => this.GetAttr<AuthorAttribute>()?.Value ?? MyAssembly.GetAttr<AssemblyCompanyAttribute>().Company;
        /// <summary>
        /// Obtiene la cadena de Copyright de este <see cref="Plugin"/>.
        /// </summary>
        public virtual string Copyright => this.GetAttr<CopyrightAttribute>()?.Value ?? MyAssembly.GetAttr<AssemblyCopyrightAttribute>().Copyright;
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
                    string outp = this.GetAttr<LicenseFileAttribute>()?.Value;
                    if (!outp.IsEmpty() && File.Exists(outp))
                        outp = MyAssembly.GetAttr<LicenseFileAttribute>()?.Value;
                    if (!outp.IsEmpty() && File.Exists(outp))
                    {
                        StreamReader inp = new StreamReader(outp);
                        return inp.ReadToEnd();
                    }

                    // Intentar buscar archivo embebido...
                    outp = this.GetAttr<EmbeededLicenseAttribute>()?.Value;
                    if (!outp.IsEmpty())
                    {
                        Stream s = null;
                        StreamReader r = null;
                        try
                        {
                            s = MyAssembly.GetManifestResourceStream(outp);
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
                    outp = this.GetAttr<LicenseTextAttribute>()?.Value;
                    if (!outp.IsEmpty())
                        outp = MyAssembly.GetAttr<LicenseTextAttribute>()?.Value;
                    if (!outp.IsEmpty()) return outp;

                    // Todo ha fallado.
                    return St.Warn(St.UnspecLicense);
                }
                catch (Exception ex)
                {
                    return ex.Message + "\n-------------------------\n" + ex.StackTrace;
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
        public virtual Version MinMCARTVersion => this.GetAttr<MinMCARTVersionAttribute>()?.Value ?? MyAssembly.GetAttr<MinMCARTVersionAttribute>()?.Value ?? TargetMCARTVersion;
        /// <summary>
        /// Determina la versión mínima de MCART necesaria para este 
        /// <see cref="Plugin"/>.
        /// </summary>
        /// <param name="minVersion">Versión mínima de MCART.</param>
        /// <returns>
        /// <c>True</c> si fue posible obtener información sobre la versión 
        /// mínima de MCART; de lo contrario, <c>false</c>.
        /// </returns>
        public bool MinRTVersion(out Version minVersion)
        {
            minVersion = MinMCARTVersion;
            return !minVersion.IsNull();
        }
        /// <summary>
        /// Determina la versión objetivo de MCART para este 
        /// <see cref="Plugin"/>.
        /// </summary>
        public virtual Version TargetMCARTVersion => this.GetAttr<TargetMCARTVersionAttribute>()?.Value ?? MyAssembly.GetAttr<TargetMCARTVersionAttribute>()?.Value ?? default(Version);
        /// <summary>
        /// Determina la versión objetivo de MCART necesaria para este 
        /// <see cref="Plugin"/>.
        /// </summary>
        /// <param name="tgtVersion">Versión objetivo de MCART.</param>
        /// <returns>
        /// <c>True</c> si fue posible obtener información sobre la versión 
        /// objetivo de MCART; de lo contrario, <c>false</c>.
        /// </returns>
        public bool TargetRTVersion(out Version tgtVersion)
        {
            tgtVersion = TargetMCARTVersion;
            return !tgtVersion.IsNull();
        }
        /// <summary>
        /// Determina si este <see cref="Plugin"/> es una versión Beta.
        /// </summary>
        public bool IsBeta => !this.GetAttr<BetaAttribute>().IsNull();
        /// <summary>
        /// Determina si este <see cref="Plugin"/> es considerado como 
        /// inseguro.
        /// </summary>
        public bool IsUnsafe => !this.GetAttr<UnsecureAttribute>().IsNull();
        /// <summary>
        /// Determina si este <see cref="Plugin"/> es considerado como 
        /// inestable.
        /// </summary>
        public bool IsUnstable => !this.GetAttr<UnstableAttribute>().IsNull();
        /// <summary>
        /// Obtiene una lista con los nombres de las interfaces implementadas
        /// por este <see cref="Plugin"/>.
        /// </summary>
        public System.Collections.Generic.IEnumerable<string> InterfaceNames
        {
            get { foreach (Type j in Interfaces) yield return j.FullName; }
        }
        /// <summary>
        /// Obtiene una colección de las interfaces implementadas por este
        /// <see cref="Plugin"/>.
        /// </summary>
        public System.Collections.Generic.IEnumerable<Type> Interfaces
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
        public Assembly MyAssembly => GetType().Assembly;
        /// <summary>
        /// Contiene una lista de interacciones que este <see cref="Plugin"/>.
        /// provee para incluir en una interfaz gráfica.
        /// </summary>
        public ReadOnlyCollection<InteractionItem> PluginInteractions
        {
            get
            {
                if (!HasInteractions) throw new FeatureNotAvailableException();
                return MyMenu.AsReadOnly();
            }
        }
        /// <summary>
        /// Indica si este <see cref="Plugin"/> contiene o no interacciones.
        /// </summary>        
        public bool HasInteractions => MyMenu.Count > 0;
        /// <summary>
        /// Objeto privado de la propiedad <see cref="Reporter"/>.
        /// </summary>
        ITaskReporter rptr = new DummyTaskReporter();
        /// <summary>
        /// Referencia al objeto <see cref="ITaskReporter"/> a utilizar por las
        /// funciones de este <see cref="Plugin"/>.
        /// </summary>
        public ITaskReporter Reporter { get => rptr; set => rptr = value ?? throw new ArgumentNullException(nameof(value)); }
        /// <summary>
        /// Objeto privado de la propiedad <see cref="Tag"/>.
        /// </summary>
        object tg;
        /// <summary>
        /// Contiene un objeto de libre uso para almacenamiento de cualquier
        /// inatancia que el usuario desee asociar a este <see cref="Plugin"/>.
        /// </summary>
        public object Tag { get => tg; set => tg = value; }
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
        /// Convierte el <see cref="Plugin"/> en un objeto del tipo
        /// especificado, realizando pruebas sobre la validez del mismo.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// Un objeto de tipo <typeparamref name="T"/> que cumple con todas las
        /// restricciones de tipo aplicables a este <see cref="Plugin"/>.
        /// </returns>
        [Thunk]
        public T CType<T>() where T : class
        {
            Type x = typeof(T);
            if (!x.IsInterface) throw new InterfaceExpectedException(x);
            if (!x.IsAssignableFrom(GetType())) throw new InterfaceNotImplementedException(x);
            return this as T;
        }
        /// <summary>
        /// Genera el evento <see cref="UIChangeRequested"/>.
        /// </summary>
        public void RequestUIChange() => UIChangeRequested?.Invoke(this, new UIChangeEventArgs(MyMenu.AsReadOnly()));
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
        internal void RaisePlgLoad(DateTime tme) => PluginLoaded?.Invoke(this, new PluginLoadedEventArgs((DateTime.Now - tme).Ticks));
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations 
        /// before the <see cref="Plugin"/> is reclaimed by garbage collection.
        /// </summary>
        ~Plugin()
        {
            AutoUpdateMyMenu = false;
            MyMenu.TriggerEvents = false;
            MyMenu.Clear();
            PluginFinalized?.Invoke(null, new PluginFinalizedEventArgs());
        }
    }
}