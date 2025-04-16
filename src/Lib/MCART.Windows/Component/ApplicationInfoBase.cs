// ApplicationInfoBase.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2025 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Component;

/// <summary>
/// Expone la información de ensamblado de una aplicación de Windows.
/// </summary>
/// <typeparam name="TApplication">
/// Tipo de aplicación de Windows para el cual exponer información.
/// </typeparam>
/// <remarks>
/// Inicializa una nueva instancia de la clase
/// <see cref="ApplicationInfoBase{TApplication}"/>.
/// </remarks>
/// <param name="assembly">
/// Ensamblado del cual se mostrará la información.
/// </param>
/// <param name="icon">Ícono a mostrar del ensamblado.</param>
[method: RequiresAssemblyFiles]
[method: RequiresUnreferencedCode(AttributeErrorMessages.MethodLoadsAssemblyResources)]
public abstract class ApplicationInfoBase<TApplication>(Assembly assembly, Icon? icon) : IExposeExtendedGuiInfo<Icon?> where TApplication : notnull
{
    private readonly AssemblyInfo _infoExposer = new(assembly);

    /// <summary>
    /// Obtiene el ícono provisto por Windows para el ensamblado especificado.
    /// </summary>
    /// <param name="asm">Ensamblado para el cual obtener el ícono.</param>
    /// <returns>
    /// Una instancia de la clase <see cref="System.Drawing.Icon"/> que
    /// contiene al ícono provisto por el sistema operativo para el ensamblado,
    /// o <see langword="null"/> si el sistema operativo no ha provisto de un
    /// ícono válido.
    /// </returns>
    [RequiresAssemblyFiles]
    protected static Icon? GetIconFromOS(Assembly asm)
    {
        UriBuilder? uri = new(asm.Location ?? string.Empty);
        string? path = Uri.UnescapeDataString(uri.Path);
        return Icon.ExtractAssociatedIcon(path);
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ApplicationInfoBase{TApplication}"/>.
    /// </summary>
    /// <param name="application">
    /// Aplicación de la cual se mostrará la información.
    /// </param>
    [RequiresAssemblyFiles]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodLoadsAssemblyResources)]
    protected ApplicationInfoBase(TApplication application) : this(application, null) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ApplicationInfoBase{TApplication}"/>.
    /// </summary>
    /// <param name="application">
    /// Aplicación de la cual se mostrará la información.
    /// </param>
    /// <param name="icon">Ícono a mostrar de la aplicación.</param>
    [RequiresAssemblyFiles]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodLoadsAssemblyResources)]
    protected ApplicationInfoBase(TApplication application, Icon? icon)
        : this(application.GetType().Assembly, icon) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ApplicationInfoBase{TApplication}"/>.
    /// </summary>
    /// <param name="application">
    /// Aplicación de la cual se mostrará la información.
    /// </param>
    /// <param name="inferIcon">
    /// <see langword="true"/> para intentar determinar el ícono de la
    /// aplicación, <see langword="false"/> para no mostrar un ícono.
    /// </param>
    [RequiresAssemblyFiles]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodLoadsAssemblyResources)]
    protected ApplicationInfoBase(TApplication application, bool inferIcon) : this(application, inferIcon ? GetIconFromOS(application.GetType().Assembly) : null) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ApplicationInfoBase{TApplication}"/>.
    /// </summary>
    /// <param name="assembly">
    /// Ensamblado del cual se mostrará la información.
    /// </param>
    /// <param name="inferIcon">
    /// <see langword="true"/> para intentar determinar el ícono del
    /// ensamblado, <see langword="false"/> para no mostrar un ícono.
    /// </param>
    [RequiresAssemblyFiles]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodLoadsAssemblyResources)]
    protected ApplicationInfoBase(Assembly assembly, bool inferIcon) : this(assembly, inferIcon ? GetIconFromOS(assembly) : null) { }

    /// <summary>
    /// Obtiene el nombre del elemento.
    /// </summary>
    public string Name => _infoExposer.Name;

    /// <summary>
    /// Obtiene la descripción del elemento.
    /// </summary>
    public string Description => _infoExposer.Description ?? string.Empty;

    /// <summary>
    /// Obtiene un ícono opcional a mostrar que describe al elemento.
    /// </summary>
    public virtual Icon? Icon { get; } = icon;

    /// <summary>
    /// Devuelve el autor del <see cref="IExposeInfo" />
    /// </summary>
    public IEnumerable<string>? Authors => _infoExposer.Authors;

    /// <summary>
    /// Devuelve el Copyright del <see cref="IExposeInfo" />
    /// </summary>
    public string? Copyright => _infoExposer.Copyright;

    /// <summary>
    /// Devuelve la licencia del <see cref="IExposeInfo" />
    /// </summary>
    public License? License => _infoExposer.License;

    /// <summary>
    /// Devuelve la versión del <see cref="IExposeInfo" />
    /// </summary>
    public Version? Version => _infoExposer.Version;

    /// <summary>
    /// Obtiene un valor que determina si este <see cref="IExposeInfo" />
    /// contiene información de licencia.
    /// </summary>
    public bool HasLicense => _infoExposer.HasLicense;

    /// <summary>
    /// Obtiene un valor que indica si este <see cref="IExposeInfo" />
    /// cumple con el Common Language Standard (CLS)
    /// </summary>
    public bool ClsCompliant => _infoExposer.ClsCompliant;

    /// <summary>
    /// Referencia al ensamblado del cual se expone la información.
    /// </summary>
    public Assembly Assembly => _infoExposer.Assembly;

    /// <summary>
    /// Obtiene la versión informacional de este
    /// <see cref="IExposeInfo"/>.
    /// </summary>
    public string? InformationalVersion => _infoExposer.InformationalVersion ?? Version?.ToString();

    /// <summary>
    /// Obtiene un valor que indica si este 
    /// <see cref="IExposeExtendedInfo"/> es considerado una versión
    /// beta.
    /// </summary>
    public bool Beta => _infoExposer.Beta;

    /// <summary>
    /// Obtiene un valor que indica si este
    /// <see cref="IExposeExtendedInfo"/> podría contener código
    /// utilizado en contexto inseguro.
    /// </summary>
    public bool Unmanaged => _infoExposer.Unmanaged;

    /// <summary>
    /// Obtiene una colección con el contenido de licencias de terceros
    /// para el objeto.
    /// </summary>
    public IEnumerable<License>? ThirdPartyLicenses => _infoExposer.ThirdPartyLicenses;

    /// <summary>
    /// Obtiene un valor que indica si este <see cref="IExposeInfo"/>
    /// contiene información de licencias de terceros.
    /// </summary>
    public bool Has3rdPartyLicense => _infoExposer.Has3rdPartyLicense;
}
