/*
WindowsInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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

#pragma warning disable CA1822

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Windows;

namespace TheXDS.MCART.Component
{
    /// <summary>
    /// Enumera los posibles tipos de firmware que podría utilizar un equipo.
    /// </summary>
    public enum FirmwareType
    {
        /// <summary>
        /// Tipo de firmware desconocido.
        /// </summary>
        FirmwareTypeUnknown,
        /// <summary>
        /// Firmware clásico BIOS
        /// </summary>
        FirmwareTypeBios,
        /// <summary>
        /// Firmware UEFI
        /// </summary>
        FirmwareTypeUefi,
        /// <summary>
        /// Valor máximo de la enumeración.
        /// </summary>
        FirmwareTypeMax
    }

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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public uint EncryptionLevel => GetFromWmi<uint>();

        /// <summary>
        /// Obtiene el tipo de firmware con el cual el equipo ha sido arrancado.
        /// </summary>
        public FirmwareType FirmwareType
        {
            get
            {
                uint firmwaretype = 0;
                if (PInvoke.GetFirmwareType(ref firmwaretype))
                    return (FirmwareType)firmwaretype;
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public ulong FreePhysicalMemory => GetFromWmi<ulong>();

        /// <summary>
        /// Obtiene la cantidad de espacio en bytes que existe en archivos 
        /// de paginación.
        /// </summary>
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public ulong FreeSpaceInPagingFiles => GetFromWmi<ulong>();

        /// <summary>
        /// Obtiene la cantidad de memoria virtual disponible para el
        /// sistema, en bytes.
        /// </summary>
#if CLSCompliance
        [CLSCompliant(false)]
#endif
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public uint MaxNumberOfProcesses => GetFromWmi<uint>();

        /// <summary>
        /// Obtiene el tamaño máximo de memoria que se le puede asignar a
        /// un proceso en este equipo.
        /// </summary>
#if CLSCompliance
        [CLSCompliant(false)]
#endif
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public uint NumberOfProcesses => GetFromWmi<uint>();

        /// <summary>
        /// Obtiene la cantidad de usuarios registrados en el sistema.
        /// </summary>
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public uint NumberOfUsers => GetFromWmi<uint>();

        /// <summary>
        /// Obtiene un número de producto (SKU) que identifica a Windows.
        /// </summary>
#if CLSCompliance
        [CLSCompliant(false)]
#endif
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public uint OSLanguage => GetFromWmi<uint>();

        /// <summary>
        /// Obtiene un valor que describe la suite de productos a la cual
        /// pertenece este sistema operativo.
        /// </summary>
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public uint OSProductSuite => GetFromWmi<uint>();

        /// <summary>
        /// Obtiene un valor que representa el tipo de sistema operativo.
        /// </summary>
#if CLSCompliance
        [CLSCompliant(false)]
#endif
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        [Obsolete("Windows 10 ya no proporciona números de versión de Service Pack.")]
        public ushort ServicePackMajorVersion => GetFromWmi<ushort>();

        /// <summary>
        /// Obtiene el número menor de Service Pack de Windows.
        /// </summary>
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        [Obsolete("Windows 10 ya no proporciona números de versión de Service Pack.")]
        public ushort ServicePackMinorVersion => GetFromWmi<ushort>();

        /// <summary>
        /// Obtiene el tamaño ocupado por los archivos de paginación en el
        /// almacenamiento local del equpio.
        /// </summary>
#if CLSCompliance
        [CLSCompliant(false)]
#endif
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public ulong TotalVirtualMemorySize => GetFromWmi<ulong>();

        /// <summary>
        /// Obtiene el tamaño total de memoria que es visible al sistema.
        /// </summary>
#if CLSCompliance
        [CLSCompliant(false)]
#endif
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
        public License License => new License("Microsoft Windows EULA", new Uri(GetWinLicencePath()));

        /// <summary>
        /// Obtiene un valor que determina si Windows incluye un CLUF
        /// </summary>
        public bool HasLicense => System.IO.File.Exists(GetWinLicencePath());

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
        /// Enumera las liciencias de terceros incluidas con el sistema
        /// operativo.
        /// </summary>
        public IEnumerable<License>? ThirdPartyLicenses => null;

        /// <summary>
        /// Obtiene un valor que indica si Microsoft Windows incluye licencias 
        /// de terceros.
        /// </summary>
        public bool Has3rdPartyLicense => false;

        private string GetWinLicencePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "license.rtf");
        }

        private T GetFromWmi<T>([CallerMemberName]string property = "")
        {
            return _managementObject[property] is T v ? v : default!;
        }

        private T GetFromReg<T>([CallerMemberName] string value = "")
        {
            return Registry.GetValue(_regInfo, value, default) is T v ? v : default!;
        }

        private DateTime DateFromWmi([CallerMemberName] string value = "")
        {
            var t = GetFromWmi<string>(value);
            var y = int.Parse(t.Substring(0, 4));
            var M = int.Parse(t.Substring(4, 2));
            var d = int.Parse(t.Substring(6, 2));
            var h = int.Parse(t.Substring(8, 2));
            var m = int.Parse(t.Substring(10, 2));
            var s = int.Parse(t.Substring(12, 2));
            var S = int.Parse(t.Substring(15, 3));
            return new DateTime(y, M, d, h, m, s, S);
        }
    }
}
