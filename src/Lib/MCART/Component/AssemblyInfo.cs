/*
AssemblyInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Component;

/// <summary>
/// Exposes information for an assembly.
/// </summary>
[RequiresUnreferencedCode(AttributeErrorMessages.ClassHeavilyUsesReflection)]
public class AssemblyInfo : IExposeExtendedInfo, IExposeAssembly
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssemblyInfo"/> class.
    /// </summary>
    [RequiresAssemblyFiles]
    public AssemblyInfo()
    {
        Assembly = Assembly.GetCallingAssembly();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssemblyInfo"/> class.
    /// </summary>
    /// <param name="assembly">
    /// Assembly from which to extract the information.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="assembly"/> is <see langword="null"/>.
    /// </exception>
    public AssemblyInfo(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        Assembly = assembly;
    }

    /// <summary>
    /// Gets a reference to the assembly for which information is being exposed.
    /// </summary>
    public Assembly Assembly { get; }

    /// <summary>
    /// Returns the author of the <see cref="Assembly" />.
    /// </summary>
    public IEnumerable<string>? Authors => Assembly.GetAttributes<AuthorAttribute>().Select(p => p.Value).OrNull() ?? (Assembly.GetAttribute<AssemblyCompanyAttribute>()?.Company.OrNull() is { } company ? new[] { company } : null);

    /// <summary>
    /// Gets a value that indicates if the <see cref="Assembly"/> is considered
    /// a Beta version.
    /// </summary>
    public bool Beta => Assembly.HasAttribute<BetaAttribute>();

    /// <summary>
    /// Gets a value that indicates if this <see cref="Assembly"/> is Common
    /// Language Specification (CLS) compliant.
    /// </summary>
    public bool ClsCompliant => Assembly.HasAttribute<CLSCompliantAttribute>();

    /// <summary>
    /// Gets the Copyright information from the <see cref="Assembly" />.
    /// </summary>
    public string? Copyright => Assembly.GetAttribute<CopyrightAttribute>()?.Value ?? Assembly.GetAttribute<AssemblyCopyrightAttribute>()?.Copyright;

    /// <summary>
    /// Gets the description for the <see cref="Assembly" />.
    /// </summary>
    public string? Description => Assembly.GetAttribute<DescriptionAttribute>()?.Value ?? Assembly.GetAttribute<AssemblyDescriptionAttribute>()?.Description;

    /// <summary>
    /// Gets a value that indicates whether this <see cref="Assembly"/>
    /// contains third party licences.
    /// </summary>
    public bool Has3rdPartyLicense => ThirdPartyLicenses.Any();

    /// <summary>
    /// Gets a value that indicates whether this <see cref="Assembly"/>
    /// contains license information.
    /// </summary>
    public bool HasLicense => Assembly.HasAttribute<LicenseAttributeBase>();

    /// <summary>
    /// Gets the informational version of the <see cref="Assembly"/>.
    /// </summary>
    public string? InformationalVersion => Assembly.GetAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? Version?.ToString();

    /// <summary>
    /// Gets the license for the <see cref="Assembly" />.
    /// </summary>
    public License? License => Assembly.GetAttributes<LicenseAttributeBase>().FirstOrDefault()?.GetLicense(Assembly);

    /// <summary>
    /// Gets the common name for the <see cref="Assembly" />.
    /// </summary>
    public string Name => Assembly.GetAttribute<NameAttribute>()?.Value ?? Assembly.GetAttribute<AssemblyTitleAttribute>()?.Title ?? Assembly.GetName().Name.OrNull() ?? Assembly.GetName().FullName;

    /// <summary>
    /// Gets the product name for the <see cref="Assembly" />.
    /// </summary>
    public string? Product => Assembly.GetAttribute<AssemblyProductAttribute>()?.Product;

    /// <summary>
    /// Gets a collection that enumerates all the included components that are
    /// marked as third-party on the <see cref="Assembly"/>.
    /// </summary>
    public IEnumerable<Type> ThirdPartyComponents => Assembly.GetTypes().Where(Types.Extensions.MemberInfoExtensions.HasAttribute<ThirdPartyAttribute>);

    /// <summary>
    /// Gets a collection of all licenses from third-party components in the
    /// <see cref="Assembly"/>.
    /// </summary>
    public IEnumerable<License> ThirdPartyLicenses
    {
        get
        {
            foreach (Type? j in ThirdPartyComponents)
            {
                if (j.HasAttribute(out LicenseAttributeBase? lic)) yield return lic.GetLicense(j);
            }
        }
    }

    /// <summary>
    /// Gets the trademark of the <see cref="Assembly" />
    /// </summary>
    public string? Trademark => Assembly.GetAttribute<AssemblyTrademarkAttribute>()?.Trademark;

    /// <summary>
    /// Gets a value that indicates whether this <see cref="Assembly"/>
    /// includes unmanaged code.
    /// </summary>
    public bool Unmanaged => Assembly.HasAttribute<UnmanagedAttribute>();

    /// <summary>
    /// Gets the <see cref="Assembly" /> version information.
    /// </summary>
    public Version? Version => Assembly.GetName().Version;
}
