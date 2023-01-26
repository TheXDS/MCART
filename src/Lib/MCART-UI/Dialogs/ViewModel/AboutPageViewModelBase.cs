/*
AboutPageViewModelBase.cs

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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Component;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ViewModel;

namespace TheXDS.MCART.Dialogs.ViewModel;

/// <summary>
/// ViewModel que describe el comportamiento de una ventana que muestra
/// información sobre un <see cref="IExposeInfo"/>.
/// </summary>
/// <typeparam name="T">
/// Tipo de elemento para el cual mostrar detalles.
/// </typeparam>
public abstract class AboutPageViewModelBase<T> : ViewModelBase, IExposeExtendedInfo, INameable, IDescriptible where T : notnull, IExposeExtendedInfo, IExposeAssembly
{
    private T _element = default!;
    private bool _showAboutMcart = true;

    /// <summary>
    /// Obtiene o establece un valor que indica si el
    /// <see cref="IExposeInfo"/> presentado es MCART.
    /// </summary>
    public bool IsMcart => Element?.Assembly?.HasAttr<McartComponentAttribute>() ?? false;

    /// <summary>
    /// Obtiene un comando a ejecutar cuando el usuario desea obtener
    /// más información sobre MCART.
    /// </summary>
    public ICommand AboutMcartCommand { get; }

    /// <summary>
    /// Obtiene un comando a ejecutar cuando el usuario desea obtener
    /// información sobre la licencia del programa.
    /// </summary>
    public ICommand LicenseCommand { get; }

    /// <summary>
    /// Obtiene un comando a ejecutar cuando el usuario desea obtener
    /// más información sobre las licencias de terceros incluidas en la
    /// aplicación.
    /// </summary>
    public ICommand ThirdPartyLicensesCommand { get; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="AboutPageViewModelBase{T}"/>.
    /// </summary>
    protected AboutPageViewModelBase()
    {
        AboutMcartCommand = new ObservingCommand(this, OnAboutMcart)
            .RegisterObservedProperty(nameof(Element))
            .SetCanExecute(() => !IsMcart);

        LicenseCommand = new ObservingCommand(this, OnLicense)
            .RegisterObservedProperty(nameof(Element))
            .SetCanExecute(() => HasLicense);

        ThirdPartyLicensesCommand = new ObservingCommand(this, On3rdPartyLicenses)
            .RegisterObservedProperty(nameof(Element))
            .SetCanExecute(() => Has3rdPartyLicense);

        RegisterPropertyChangeBroadcast(nameof(Element), new[] {
                nameof(Authors),
                nameof(ClsCompliant),
                nameof(Copyright),
                nameof(Description),
                nameof(HasLicense),
                nameof(IsMcart),
                nameof(License),
                nameof(McartComponentKind),
                nameof(Name),
                nameof(ShowAboutMcart),
                nameof(Version),
                nameof(InformationalVersion),
                nameof(ThirdPartyLicenses),
                nameof(Beta),
                nameof(Unmanaged)
            });
    }

    /// <summary>
    /// Obtiene al autor del <see cref="IExposeInfo"/> para el cual se
    /// presentan los detalles.
    /// </summary>
    public IEnumerable<string>? Authors => Element?.Authors;

    /// <summary>
    /// Obtiene un valor que determina si el <see cref="IExposeInfo"/>
    /// cumple con las especificaciones del CLS.
    /// </summary>
    public bool ClsCompliant => Element?.ClsCompliant ?? false;

    /// <summary>
    /// Obtiene la información de Copyright del
    /// <see cref="IExposeInfo"/> para el cual se presentan los 
    /// detalles.
    /// </summary>
    public string? Copyright => Element?.Copyright;

    /// <summary>
    /// Obtiene la descripción del <see cref="IExposeInfo"/> para el
    /// cual se presentan los detalles.
    /// </summary>
    public string? Description => Element?.Description;

    /// <summary>
    /// Obtiene o establece el <see cref="IExposeInfo"/> para el cual
    /// se presentan los detalles.
    /// </summary>
    public T Element
    {
        get => _element;
        set => Change(ref _element, value);
    }

    /// <summary>
    /// Obtiene un valor que determina si el <see cref="IExposeInfo"/>
    /// contiene una licencia.
    /// </summary>
    public bool HasLicense => Element?.HasLicense ?? false;

    /// <summary>
    /// Obtiene la licencia del <see cref="IExposeInfo"/> para el cual
    /// se presentan los detalles.
    /// </summary>
    public License? License => Element?.License;

    /// <summary>
    /// Obtiene el nombre del <see cref="IExposeInfo"/> para el cual
    /// se presentan los detalles.
    /// </summary>
    public string Name => Element?.Name ?? string.Empty;

    /// <summary>
    /// Obtiene o establece un valor que indica si debe mostrarse un
    /// vínculo a una nueva ventana para ver detalles sobre MCART.
    /// </summary>
    public bool ShowAboutMcart
    {
        get => _showAboutMcart && !IsMcart;
        set => Change(ref _showAboutMcart, value);
    }

    /// <summary>
    /// Obtiene la versión del <see cref="IExposeInfo"/> para el cual
    /// se presentan los detalles.
    /// </summary>
    public Version? Version => Element?.Version;

    /// <summary>
    /// Obtiene una cadena que describe el tipo de componente que un ensamblado de MCART es.
    /// </summary>
    public string? McartComponentKind => Element?.Assembly?.GetAttr<McartComponentAttribute>()?.Kind.NameOf();

    /// <summary>
    /// Obtiene la versión informacional de este
    /// <see cref="IExposeInfo"/>.
    /// </summary>
    public string? InformationalVersion => Element?.InformationalVersion;

    /// <summary>
    /// Obtiene una colección de licencias de terceros.
    /// </summary>
    public IEnumerable<License>? ThirdPartyLicenses => Element?.ThirdPartyLicenses;

    /// <summary>
    /// Obtiene un valor que indica si este 
    /// <see cref="IExposeExtendedInfo"/> es considerado una versión
    /// beta.
    /// </summary>
    public bool Beta => Element?.Beta ?? false;

    /// <summary>
    /// Obtiene un valor que indica si este
    /// <see cref="IExposeExtendedInfo"/> podría contener código
    /// utilizado en contexto inseguro.
    /// </summary>
    public bool Unmanaged => Element?.Unmanaged ?? false;

    /// <summary>
    /// Obtiene un valor que indica si el elemento contiene licencias
    /// de terceros.
    /// </summary>
    public bool Has3rdPartyLicense => Element?.Has3rdPartyLicense ?? false;

    /// <summary>
    /// Ejecuta la acción de mostrar información adicional sobre la
    /// licencia del programa.
    /// </summary>
    protected virtual void OnLicense()
    {
        if (License is { LicenseUri: Uri uri }) Process.Start(uri.ToString());
    }

    /// <summary>
    /// Ejecuta la acción de mostrar más información sobre MCART.
    /// </summary>
    protected abstract void OnAboutMcart();

    /// <summary>
    /// Ejecuta la acción de mostrar más información sobre licencias de
    /// terceros incluidas en la aplicación.
    /// </summary>
    protected abstract void On3rdPartyLicenses();
}