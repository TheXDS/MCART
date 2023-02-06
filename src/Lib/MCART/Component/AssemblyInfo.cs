/*
AssemblyInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Component;

/// <summary>
/// Expone la información de identificación de un ensamblado.
/// </summary>
public class AssemblyInfo : IExposeExtendedInfo, IExposeAssembly
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="AssemblyInfo"/>
    /// </summary>
    public AssemblyInfo()
    {
        Assembly = Assembly.GetCallingAssembly();
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="AssemblyInfo"/>
    /// </summary>
    /// <param name="assembly">
    /// Ensamblado del cual se mostrará la información.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="assembly"/> es <see langword="null"/>.
    /// </exception>
    public AssemblyInfo(Assembly assembly)
    {
        NullCheck(assembly, nameof(assembly));
        Assembly = assembly;
    }

    /// <summary>
    /// Obtiene una referencia al ensamblado del cual se expone la información.
    /// </summary>
    public Assembly Assembly { get; }

    /// <summary>
    /// Devuelve el autor del <see cref="IExposeInfo" />
    /// </summary>
    public IEnumerable<string>? Authors => Assembly.GetAttributes<AuthorAttribute>().Select(p => p.Value!).OrNull() ?? (Assembly.GetAttribute<AssemblyCompanyAttribute>()?.Company.OrNull() is { } company ? new[] { company } : null);

    /// <summary>
    /// Obtiene un valor que indica si este 
    /// <see cref="IExposeExtendedInfo"/> es considerado una versión
    /// beta.
    /// </summary>
    public bool Beta => Assembly.HasAttribute<BetaAttribute>();

    /// <summary>
    /// Obtiene un valor que indica si este <see cref="IExposeInfo" />
    /// cumple con el Common Language Standard (CLS)
    /// </summary>
    public bool ClsCompliant => Assembly.HasAttribute<CLSCompliantAttribute>();

    /// <summary>
    /// Devuelve el Copyright del <see cref="IExposeInfo" />
    /// </summary>
    public string? Copyright => Assembly.GetAttribute<CopyrightAttribute>()?.Value ?? Assembly.GetAttribute<AssemblyCopyrightAttribute>()?.Copyright;

    /// <summary>
    /// Devuelve una descripción del <see cref="IExposeInfo" />
    /// </summary>
    public string? Description => Assembly.GetAttribute<DescriptionAttribute>()?.Value ?? Assembly.GetAttribute<AssemblyDescriptionAttribute>()?.Description;

    /// <summary>
    /// Obtiene un valor que indica si este <see cref="IExposeInfo"/>
    /// contiene información de licencias de terceros.
    /// </summary>
    public bool Has3rdPartyLicense => ThirdPartyLicenses.Any();

    /// <summary>
    /// Obtiene un valor que determina si este <see cref="IExposeInfo" />
    /// contiene información de licencia.
    /// </summary>
    public bool HasLicense => Internals.HasLicense(Assembly);

    /// <summary>
    /// Obtiene la versión informacional del <see cref="IExposeInfo"/>.
    /// </summary>
    public string? InformationalVersion => Assembly.GetAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? Version?.ToString();

    /// <summary>
    /// Devuelve la licencia del <see cref="IExposeInfo" />
    /// </summary>
    public License? License => Assembly.GetAttributes<LicenseAttributeBase>().FirstOrDefault()?.GetLicense(Assembly);

    /// <summary>
    /// Devuelve el nombre del <see cref="IExposeInfo" />
    /// </summary>
    public string Name => Assembly.GetAttribute<NameAttribute>()?.Value ?? Assembly.GetAttribute<AssemblyTitleAttribute>()?.Title ?? Assembly.GetName().Name.OrNull() ?? Assembly.GetName().FullName;

    /// <summary>
    /// Devuelve el autor del <see cref="Assembly" />
    /// </summary>
    public string? Product => Assembly.GetAttribute<AssemblyProductAttribute>()?.Product;

    /// <summary>
    /// Obtiene una colección con todos los componentes marcados como de
    /// terceros en el ensamblado.
    /// </summary>
    public IEnumerable<Type> ThirdPartyComponents => Assembly.SafeGetTypes().Where(Types.Extensions.MemberInfoExtensions.HasAttribute<ThirdPartyAttribute>);

    /// <summary>
    /// Obtiene una colección con el contenido de licencias de terceros
    /// para el objeto.
    /// </summary>
    public IEnumerable<License> ThirdPartyLicenses
    {
        get
        {
            foreach (Type? j in ThirdPartyComponents)
            {
                if (j.HasAttribute(out LicenseAttributeBase? lic)) yield return lic!.GetLicense(j);
            }
        }
    }

    /// <summary>
    /// Devuelve la marca comercial del <see cref="Assembly" />
    /// </summary>
    public string? Trademark => Assembly.GetAttribute<AssemblyTrademarkAttribute>()?.Trademark;

    /// <summary>
    /// Obtiene un valor que indica si este
    /// <see cref="IExposeExtendedInfo"/> podría contener código
    /// utilizado en contexto inseguro.
    /// </summary>
    public bool Unmanaged => Assembly.HasAttribute<UnmanagedAttribute>();

    /// <summary>
    /// Devuelve la versión del <see cref="IExposeInfo" />
    /// </summary>
    public Version? Version => Assembly.GetName().Version;
}
