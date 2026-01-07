// ApplicationInfoBase.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2026 César Andrés Morgan
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
/// Exposes the assembly information of a Windows application.
/// </summary>
/// <typeparam name="TApplication">
/// Windows application type for which to expose information.
/// </typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="ApplicationInfoBase{TApplication}"/> class.
/// </remarks>
/// <param name="assembly">
/// Assembly from which information will be displayed.
/// </param>
/// <param name="icon">Icon to display from the assembly.</param>
[method: RequiresAssemblyFiles]
[method: RequiresUnreferencedCode(AttributeErrorMessages.MethodLoadsAssemblyResources)]
public abstract class ApplicationInfoBase<TApplication>(Assembly assembly, Icon? icon) : IExposeExtendedGuiInfo<Icon?> where TApplication : notnull
{
    private readonly AssemblyInfo _infoExposer = new(assembly);

    /// <summary>
    /// Retrieves the icon provided by Windows for the specified assembly.
    /// </summary>
    /// <param name="asm">Assembly for which to retrieve the icon.</param>
    /// <returns>
    /// An instance of <see cref="System.Drawing.Icon"/> containing the icon
    /// provided by the operating system for the assembly, or
    /// <see langword="null"/> if the OS did not provide a valid icon.
    /// </returns>
    [RequiresAssemblyFiles]
    protected static Icon? GetIconFromOS(Assembly asm)
    {
        UriBuilder? uri = new(asm.Location ?? string.Empty);
        string? path = Uri.UnescapeDataString(uri.Path);
        return Icon.ExtractAssociatedIcon(path);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationInfoBase{TApplication}"/> class.
    /// </summary>
    /// <param name="application">
    /// Application whose information will be displayed.
    /// </param>
    [RequiresAssemblyFiles]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodLoadsAssemblyResources)]
    protected ApplicationInfoBase(TApplication application) : this(application, null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationInfoBase{TApplication}"/> class.
    /// </summary>
    /// <param name="application">
    /// Application whose information will be displayed.
    /// </param>
    /// <param name="icon">Icon to display for the application.</param>
    [RequiresAssemblyFiles]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodLoadsAssemblyResources)]
    protected ApplicationInfoBase(TApplication application, Icon? icon)
        : this(application.GetType().Assembly, icon) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationInfoBase{TApplication}"/> class.
    /// </summary>
    /// <param name="application">
    /// Application whose information will be displayed.
    /// </param>
    /// <param name="inferIcon">
    /// true to attempt to determine the application's icon, false to not display an icon.
    /// </param>
    [RequiresAssemblyFiles]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodLoadsAssemblyResources)]
    protected ApplicationInfoBase(TApplication application, bool inferIcon) : this(application, inferIcon ? GetIconFromOS(application.GetType().Assembly) : null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationInfoBase{TApplication}"/> class.
    /// </summary>
    /// <param name="assembly">
    /// Assembly from which information will be displayed.
    /// </param>
    /// <param name="inferIcon">
    /// true to attempt to determine the assembly's icon, false to not display an icon.
    /// </param>
    [RequiresAssemblyFiles]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodLoadsAssemblyResources)]
    protected ApplicationInfoBase(Assembly assembly, bool inferIcon) : this(assembly, inferIcon ? GetIconFromOS(assembly) : null) { }

    /// <summary>
    /// Gets the name of the element.
    /// </summary>
    public string Name => _infoExposer.Name;

    /// <summary>
    /// Gets the description of the element.
    /// </summary>
    public string Description => _infoExposer.Description ?? string.Empty;

    /// <summary>
    /// Gets an optional icon that describes the element.
    /// </summary>
    public virtual Icon? Icon { get; } = icon;

    /// <summary>
    /// Gets the author(s) of the <see cref="IExposeInfo" />.
    /// </summary>
    public IEnumerable<string>? Authors => _infoExposer.Authors;

    /// <summary>
    /// Gets the copyright of the <see cref="IExposeInfo" />.
    /// </summary>
    public string? Copyright => _infoExposer.Copyright;

    /// <summary>
    /// Gets the license of the <see cref="IExposeInfo" />.
    /// </summary>
    public License? License => _infoExposer.License;

    /// <summary>
    /// Gets the version of the <see cref="IExposeInfo" />.
    /// </summary>
    public Version? Version => _infoExposer.Version;

    /// <summary>
    /// Gets a value that indicates whether this
    /// <see cref="IExposeInfo" /> contains license information.
    /// </summary>
    public bool HasLicense => _infoExposer.HasLicense;

    /// <summary>
    /// Gets a value that indicates whether this
    /// <see cref="IExposeInfo" /> is CLS compliant.
    /// </summary>
    public bool ClsCompliant => _infoExposer.ClsCompliant;

    /// <summary>
    /// Gets a reference to the assembly from which information is
    /// exposed.
    /// </summary>
    public Assembly Assembly => _infoExposer.Assembly;

    /// <summary>
    /// Gets the informational version of this
    /// <see cref="IExposeInfo"/>.
    /// </summary>
    public string? InformationalVersion => _infoExposer.InformationalVersion ?? Version?.ToString();

    /// <summary>
    /// Gets a value that indicates whether this
    /// <see cref="IExposeExtendedInfo"/> is considered a beta
    /// version.
    /// </summary>
    public bool Beta => _infoExposer.Beta;

    /// <summary>
    /// Gets a value that indicates whether this
    /// <see cref="IExposeExtendedInfo"/> may contain unsafe code.
    /// </summary>
    public bool Unmanaged => _infoExposer.Unmanaged;

    /// <summary>
    /// Gets a collection of third‑party license contents for the object.
    /// </summary>
    public IEnumerable<License>? ThirdPartyLicenses => _infoExposer.ThirdPartyLicenses;

    /// <summary>
    /// Gets a value that indicates whether this
    /// <see cref="IExposeInfo"/> contains third‑party license information.
    /// </summary>
    public bool Has3rdPartyLicense => _infoExposer.Has3rdPartyLicense;
}
