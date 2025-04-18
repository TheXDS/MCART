﻿/*
WindowsInfo.cs

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

#pragma warning disable CA1822

using Microsoft.Win32;
using System.Management;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.PInvoke.Kernel32;

namespace TheXDS.MCART.Component;

/// <summary>
/// Expone información detallada sobre Windows.
/// </summary>
public class WindowsInfo : INameable, IDescriptible, IExposeInfo
{
    private const string _regInfo = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";
    private readonly ManagementObject _managementObject = new ManagementClass(@"Win32_OperatingSystem").GetInstances().OfType<ManagementObject>().FirstOrDefault() ?? throw new PlatformNotSupportedException();

    /// <summary>
    /// Obtiene una cadena que representa el dispositivo de arranque
    /// del equipo.
    /// </summary>
    public string BootDevice => GetFromWmi<string>();

    /// <summary>
    /// Obtiene una cadena que representa la rama de compilación desde
    /// la cual se origina esta versión de Windows.
    /// </summary>
    public string BuildBranch => GetFromReg<string>();

    /// <summary>
    /// Obtiene un <see cref="Guid"/> que identifica a esta versión de
    /// Windows.
    /// </summary>
    public Guid BuildGUID => Guid.Parse(GetFromReg<string>());

    /// <summary>
    /// Obtiene una cadena que representa el laboratorio de
    /// compilación desde el cual se origina esta versión de Windows.
    /// </summary>
    public string BuildLab => GetFromReg<string>();

    /// <summary>
    /// Obtiene una cadena con formato extendido que representa el
    /// laboratorio de compilación desde el cual se origina esta 
    /// versión de Windows.
    /// </summary>
    public string BuildLabEx => GetFromReg<string>();

    /// <summary>
    /// Obtiene una cadena que representa el número de compilación de
    /// Windows.
    /// </summary>
    public string BuildNumber => GetFromWmi<string>();

    /// <summary>
    /// Obtiene una cadena que representa el tipo de compilación de
    /// Windows.
    /// </summary>
    public string BuildType => GetFromWmi<string>();

    /// <summary>
    /// Obtiene una etiqueta para esta versión de Windows.
    /// </summary>
    public string Caption => GetFromWmi<string>();

    /// <summary>
    /// Obtiene una cadena que representa la página de códigos
    /// configurada para Windows.
    /// </summary>
    public string CodeSet => GetFromWmi<string>();

    /// <summary>
    /// Obtiene el código de país configurado para esta instancia de
    /// Windows.
    /// </summary>
    public string CountryCode => GetFromWmi<string>();

    /// <summary>
    /// Obtiene la zona horaria actualmente configurada en Windows.
    /// </summary>
    public short CurrentTimeZone => GetFromWmi<short>();

    /// <summary>
    /// Obtiene un valor que indica si DEP se encuentra habilitado para
    /// aplicaciones de 32 bits.
    /// </summary>
    public bool DEP32bit => GetFromWmi<bool>("DataExecutionPrevention_32BitApplications");

    /// <summary>
    /// Obtiene un valor que indica si DEP se encuentra habilitado.
    /// </summary>
    public bool DEPAvailable => GetFromWmi<bool>("DataExecutionPrevention_Available");

    /// <summary>
    /// Obtiene un valor que indica la presencia de controladores DEP
    /// en el sistema.
    /// </summary>
    public bool DEPDrivers => GetFromWmi<bool>("DataExecutionPrevention_Drivers");

    /// <summary>
    /// Obtiene un valor que representa la política DEP activa en el
    /// equipo.
    /// </summary>
    public byte DEPPolicy => GetFromWmi<byte>("DataExecutionPrevention_SupportPolicy");

    /// <summary>
    /// Obtiene un valor que indica si esta versión de Windows se ha
    /// iniciado con depuración de Kernel activada.
    /// </summary>
    public bool Debug => GetFromWmi<bool>();

    /// <summary>
    /// Obtiene una cadena que describe a esta versión de Windows.
    /// </summary>
    public string Description => GetFromWmi<string>();

    /// <summary>
    /// Obtiene un valor que indica si Windows se está ejecutando en un
    /// entorno distribuido.
    /// </summary>
    public bool Distributed => GetFromWmi<bool>();

    /// <summary>
    /// Obtiene un valor que representa la edición de esta instancia de
    /// Windows.
    /// </summary>
    public string EditionID => GetFromReg<string>();

    /// <summary>
    /// Obtiene un valor que representa el nivel de encriptado del
    /// sistema.
    /// </summary>
    [CLSCompliant(false)]
    public uint EncryptionLevel => GetFromWmi<uint>();

    /// <summary>
    /// Obtiene el tipo de firmware con el cual el equipo ha sido arrancado.
    /// </summary>
    public FirmwareType FirmwareType
    {
        get
        {
            uint firmwareType = 0;
            if (GetFirmwareType(ref firmwareType))
                return (FirmwareType)firmwareType;
            else
                return FirmwareType.FirmwareTypeUnknown;
        }
    }

    /// <summary>
    /// Obtiene un valor que indica la cantidad de "empuje" adicional
    /// que una aplicación en primer plano recibirá por parte del
    /// sistema operativo.
    /// </summary>
    public byte ForegroundApplicationBoost => GetFromWmi<byte>();

    /// <summary>
    /// Obtiene un valor que indica la cantidad de memoria física
    /// disponible, en bytes.
    /// </summary>
    [CLSCompliant(false)]
    public ulong FreePhysicalMemory => GetFromWmi<ulong>();

    /// <summary>
    /// Obtiene la cantidad de espacio en bytes que existe en archivos 
    /// de paginación.
    /// </summary>
    [CLSCompliant(false)]
    public ulong FreeSpaceInPagingFiles => GetFromWmi<ulong>();

    /// <summary>
    /// Obtiene la cantidad de memoria virtual disponible para el
    /// sistema, en bytes.
    /// </summary>
    [CLSCompliant(false)]
    public ulong FreeVirtualMemory => GetFromWmi<ulong>();

    /// <summary>
    /// Obtiene una cadena que representa el tipo de instalación de
    /// esta instancia de Windows.
    /// </summary>
    public string InstallationType => GetFromReg<string>();

    /// <summary>
    /// Obtiene la fecha y hora de instalación de Windows en este
    /// equipo.
    /// </summary>
    public DateTime InstallDate => DateFromWmi();

    /// <summary>
    /// Obtiene la fecha y hora del último arranque del equipo.
    /// </summary>
    public DateTime LastBootUpTime => DateFromWmi();

    /// <summary>
    /// Obtiene la fecha y hora locales del equipo a partir de la
    /// información reportada por WMI.
    /// </summary>
    public DateTime LocalDateTime => DateFromWmi();

    /// <summary>
    /// Obtiene una cadena que representa el lenguaje local del equipo.
    /// </summary>
    public string Locale => GetFromWmi<string>();

    /// <summary>
    /// Obtiene el nombre del fabricante del equipo.
    /// </summary>
    public string Manufacturer => GetFromWmi<string>();

    /// <summary>
    /// Obtiene la cantidad máxima de procesos que el sistema operativo
    /// es capaz de gestionar.
    /// </summary>
    [CLSCompliant(false)]
    public uint MaxNumberOfProcesses => GetFromWmi<uint>();

    /// <summary>
    /// Obtiene el tamaño máximo de memoria que se le puede asignar a
    /// un proceso en este equipo.
    /// </summary>
    [CLSCompliant(false)]
    public uint MaxProcessMemorySize => GetFromWmi<uint>();

    /// <summary>
    /// Obtiene una colección de los lenguajes MUI disponibles en esta 
    /// instalación de Windows.
    /// </summary>
    public string[] MUILanguages => GetFromWmi<string[]>();

    /// <summary>
    /// Obtiene un nombre descriptivo para esta instalación de Windows.
    /// </summary>
    public string Name => GetFromWmi<string>();

    /// <summary>
    /// Obtiene la cantidad de procesos existentes actualmente en el
    /// sistema.
    /// </summary>
    [CLSCompliant(false)]
    public uint NumberOfProcesses => GetFromWmi<uint>();

    /// <summary>
    /// Obtiene la cantidad de usuarios registrados en el sistema.
    /// </summary>
    [CLSCompliant(false)]
    public uint NumberOfUsers => GetFromWmi<uint>();

    /// <summary>
    /// Obtiene un número de producto (SKU) que identifica a Windows.
    /// </summary>
    [CLSCompliant(false)]
    public uint OperatingSystemSKU => GetFromWmi<uint>();

    /// <summary>
    /// Obtiene el nombre de la organización registrada en el equipo.
    /// </summary>
    public string Organization => GetFromWmi<string>();

    /// <summary>
    /// Obtiene el nombre de la arquitectura del sistema operativo.
    /// </summary>
    public string OSArchitecture => GetFromWmi<string>();

    /// <summary>
    /// Obtiene un valor que representa el idioma del sistema 
    /// operativo.
    /// </summary>
    [CLSCompliant(false)]
    public uint OSLanguage => GetFromWmi<uint>();

    /// <summary>
    /// Obtiene un valor que describe la suite de productos a la cual
    /// pertenece este sistema operativo.
    /// </summary>
    [CLSCompliant(false)]
    public uint OSProductSuite => GetFromWmi<uint>();

    /// <summary>
    /// Obtiene un valor que representa el tipo de sistema operativo.
    /// </summary>
    [CLSCompliant(false)]
    public ushort OSType => GetFromWmi<ushort>();

    /// <summary>
    /// Obtiene un valor que indica si este sistema operativo es
    /// portátil.
    /// </summary>
    public bool PortableOperatingSystem => GetFromWmi<bool>();

    /// <summary>
    /// Obtiene el valor "Primary" desde la instrumentación de Windows.
    /// </summary>
    /// <returns>
    /// El valor "Primary" desde la instrumentación de Windows.
    /// </returns>
    public bool Primary => GetFromWmi<bool>();

    /// <summary>
    /// Obtiene un valor que representa el tipo de producto que es este
    /// sistema operativo.
    /// </summary>
    [CLSCompliant(false)]
    public uint ProductType => GetFromWmi<uint>();

    /// <summary>
    /// Obtiene el nombre del usuario registrado en Windows.
    /// </summary>
    public string RegisteredUser => GetFromWmi<string>();

    /// <summary>
    /// Obtiene una cadena que identifica a la distribución de esta
    /// versión de Windows.
    /// </summary>
    public string ReleaseId => GetFromReg<string>();

    /// <summary>
    /// Obtiene el número de serie del equipo.
    /// </summary>
    public string SerialNumber => GetFromWmi<string>();

    /// <summary>
    /// Obtiene el número mayor de Service Pack de Windows.
    /// </summary>
    [CLSCompliant(false)]
    [Obsolete("Desde Windows 10 ya no se proporcionan números de versión de Service Pack.")]
    public ushort ServicePackMajorVersion => GetFromWmi<ushort>();

    /// <summary>
    /// Obtiene el número menor de Service Pack de Windows.
    /// </summary>
    [CLSCompliant(false)]
    [Obsolete("Desde Windows 10 ya no se proporcionan números de versión de Service Pack.")]
    public ushort ServicePackMinorVersion => GetFromWmi<ushort>();

    /// <summary>
    /// Obtiene el tamaño ocupado por los archivos de paginación en el
    /// almacenamiento local del equipo.
    /// </summary>
    [CLSCompliant(false)]
    public ulong SizeStoredInPagingFiles => GetFromWmi<ulong>();

    /// <summary>
    /// Obtiene una cadena que representa el estado actual del sistema
    /// operativo.
    /// </summary>
    public string Status => GetFromWmi<string>();

    /// <summary>
    /// Obtiene un valor que se puede utilizar para determinar el valor
    /// de Suite del sistema operativo al ser aplicado como una
    /// máscara sobre <see cref="OSProductSuite"/>.
    /// </summary>
    [CLSCompliant(false)]
    public uint SuiteMask => GetFromWmi<uint>();

    /// <summary>
    /// Obtiene una cadena que representa al dispositivo de
    /// almacenamiento donde se encuentra instalado Windows.
    /// </summary>
    public string SystemDevice => GetFromWmi<string>();

    /// <summary>
    /// Obtiene la ruta de directorio del sistema operativo.
    /// </summary>
    public string SystemDirectory => GetFromWmi<string>();

    /// <summary>
    /// Obtiene la letra de unidad donde se encuentra instalado el
    /// sistema operativo.
    /// </summary>
    public string SystemDrive => GetFromWmi<string>();

    /// <summary>
    /// Obtiene el tamaño total de la memoria virtual.
    /// </summary>
    [CLSCompliant(false)]
    public ulong TotalVirtualMemorySize => GetFromWmi<ulong>();

    /// <summary>
    /// Obtiene el tamaño total de memoria que es visible al sistema.
    /// </summary>
    [CLSCompliant(false)]
    public ulong TotalVisibleMemorySize => GetFromWmi<ulong>();

    /// <summary>
    /// Obtiene el valor "UBR" desde la instrumentación de Windows.
    /// </summary>
    /// <returns>
    /// El valor "UBR" desde la instrumentación de Windows.
    /// </returns>
    public int UBR => GetFromReg<int>();

    /// <summary>
    /// Obtiene la versión del sistema operativo.
    /// </summary>
    public Version Version => Version.Parse($"{GetFromWmi<string>()}.{UBR}");

    /// <summary>
    /// Obtiene la ruta del directorio de Windows.
    /// </summary>
    public string WindowsDirectory => GetFromWmi<string>();

    /// <summary>
    /// Obtiene al fabricante del sistema operativo.
    /// </summary>
    public IEnumerable<string> Authors => new[] { Manufacturer };

    /// <summary>
    /// Obtiene la nota de copyright asociada a Windows.
    /// </summary>
    public string Copyright => @$"Copyright © {Manufacturer}";

    /// <summary>
    /// Obtiene el texto de licencia asociado a Windows.
    /// </summary>
    public License License => new("Microsoft Windows EULA", new Uri(GetWinLicensePath()));

    /// <summary>
    /// Obtiene un valor que determina si Windows incluye un CLUF
    /// </summary>
    public bool HasLicense => File.Exists(GetWinLicensePath());

    /// <summary>
    /// Obtiene un valor que indica si Windows cumple con el CLS.
    /// </summary>
    /// <remarks>
    /// Esta función siempre devolverá <see langword="false"/>, debido
    /// a que grandes porciones de Microsoft Windows fueron escritas
    /// utilizando C/C++, y no C# u otros lenguajes CLR.
    /// </remarks>
    public bool ClsCompliant => false;

    /// <summary>
    /// Obtiene la versión descriptiva informacional de Windows.
    /// </summary>
    public string? InformationalVersion => $"{Version}-{BuildLabEx}";

    /// <summary>
    /// Enumera las licencias de terceros incluidas con el sistema
    /// operativo.
    /// </summary>
    public IEnumerable<License>? ThirdPartyLicenses => null;

    /// <summary>
    /// Obtiene un valor que indica si Microsoft Windows incluye licencias 
    /// de terceros.
    /// </summary>
    public bool Has3rdPartyLicense => false;

    private string GetWinLicensePath()
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "license.rtf");
    }

    private T GetFromWmi<T>([CallerMemberName] string property = "")
    {
        return _managementObject[property] is T v ? v : default!;
    }

    private T GetFromReg<T>([CallerMemberName] string value = "")
    {
        return Registry.GetValue(_regInfo, value, default) is T v ? v : default!;
    }

    private DateTime DateFromWmi([CallerMemberName] string value = "")
    {
        string? t = GetFromWmi<string>(value);
        return new DateTime(
            int.Parse(t[..4]),
            int.Parse(t[4..6]),
            int.Parse(t[6..8]),
            int.Parse(t[8..10]),
            int.Parse(t[10..12]),
            int.Parse(t[12..14]),
            int.Parse(t[14..17]));
    }
}
