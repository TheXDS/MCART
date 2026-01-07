/*
WindowsInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.PInvoke.Kernel32;

namespace TheXDS.MCART.Component;

/// <summary>
/// Exposes detailed information about Windows.
/// </summary>
public class WindowsInfo : INameable, IDescriptible, IExposeInfo
{
    private const string _regInfo = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";
    private readonly ManagementObject _managementObject = new ManagementClass(@"Win32_OperatingSystem").GetInstances().OfType<ManagementObject>().FirstOrDefault() ?? throw new PlatformNotSupportedException();

    /// <summary>
    /// Gets a string that represents the system's boot device.
    /// </summary>
    public string BootDevice => GetFromWmi<string>();

    /// <summary>
    /// Gets a string that represents the build branch from which this Windows
    /// version originates.
    /// </summary>
    public string BuildBranch => GetFromReg<string>();

    /// <summary>
    /// Gets a <see cref="Guid"/> that identifies this Windows version.
    /// </summary>
    public Guid BuildGUID => Guid.Parse(GetFromReg<string>());

    /// <summary>
    /// Gets a string that represents the build lab from which this Windows
    /// version originates.
    /// </summary>
    public string BuildLab => GetFromReg<string>();

    /// <summary>
    /// Gets an extended-formatted string that represents the build lab
    /// from which this Windows version originates.
    /// </summary>
    public string BuildLabEx => GetFromReg<string>();

    /// <summary>
    /// Gets a string that represents the Windows build number.
    /// </summary>
    public string BuildNumber => GetFromWmi<string>();

    /// <summary>
    /// Gets a string that represents the Windows build type.
    /// </summary>
    public string BuildType => GetFromWmi<string>();

    /// <summary>
    /// Gets a label for this Windows version.
    /// </summary>
    public string Caption => GetFromWmi<string>();

    /// <summary>
    /// Gets a string that represents the code page configured for Windows.
    /// </summary>
    public string CodeSet => GetFromWmi<string>();

    /// <summary>
    /// Gets the country code configured for this Windows instance.
    /// </summary>
    public string CountryCode => GetFromWmi<string>();

    /// <summary>
    /// Gets the currently configured time zone in Windows.
    /// </summary>
    public short CurrentTimeZone => GetFromWmi<short>();

    /// <summary>
    /// Gets a value indicating whether DEP is enabled for 32-bit applications.
    /// </summary>
    public bool DEP32bit => GetFromWmi<bool>("DataExecutionPrevention_32BitApplications");

    /// <summary>
    /// Gets a value indicating whether DEP is available.
    /// </summary>
    public bool DEPAvailable => GetFromWmi<bool>("DataExecutionPrevention_Available");

    /// <summary>
    /// Gets a value indicating the presence of DEP drivers in the system.
    /// </summary>
    public bool DEPDrivers => GetFromWmi<bool>("DataExecutionPrevention_Drivers");

    /// <summary>
    /// Gets a value representing the active DEP policy on the system.
    /// </summary>
    public byte DEPPolicy => GetFromWmi<byte>("DataExecutionPrevention_SupportPolicy");

    /// <summary>
    /// Gets a value indicating whether this Windows version was started with
    /// kernel debugging enabled.
    /// </summary>
    public bool Debug => GetFromWmi<bool>();

    /// <summary>
    /// Gets a string that describes this Windows version.
    /// </summary>
    public string Description => GetFromWmi<string>();

    /// <summary>
    /// Gets a value indicating whether Windows is running in a distributed
    /// environment.
    /// </summary>
    public bool Distributed => GetFromWmi<bool>();

    /// <summary>
    /// Gets a value that represents the edition of this Windows instance.
    /// </summary>
    public string EditionID => GetFromReg<string>();

    /// <summary>
    /// Gets a value that represents the system's encryption level.
    /// </summary>
    [CLSCompliant(false)]
    public uint EncryptionLevel => GetFromWmi<uint>();

    /// <summary>
    /// Gets the firmware type with which the computer was booted.
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
    /// Gets a value that indicates the amount of foreground application
    /// boost the operating system will provide.
    /// </summary>
    public byte ForegroundApplicationBoost => GetFromWmi<byte>();

    /// <summary>
    /// Gets a value that indicates the amount of available physical memory,
    /// in bytes.
    /// </summary>
    [CLSCompliant(false)]
    public ulong FreePhysicalMemory => GetFromWmi<ulong>();

    /// <summary>
    /// Gets the amount of paging file space, in bytes.
    /// </summary>
    [CLSCompliant(false)]
    public ulong FreeSpaceInPagingFiles => GetFromWmi<ulong>();

    /// <summary>
    /// Gets the amount of available virtual memory, in bytes.
    /// </summary>
    [CLSCompliant(false)]
    public ulong FreeVirtualMemory => GetFromWmi<ulong>();

    /// <summary>
    /// Gets a string that represents the installation type of this
    /// Windows instance.
    /// </summary>
    public string InstallationType => GetFromReg<string>();

    /// <summary>
    /// Gets the installation date and time of Windows on this computer.
    /// </summary>
    public DateTime InstallDate => DateFromWmi();

    /// <summary>
    /// Gets the last boot-up time of the computer.
    /// </summary>
    public DateTime LastBootUpTime => DateFromWmi();

    /// <summary>
    /// Gets the local date and time of the computer as reported by WMI.
    /// </summary>
    public DateTime LocalDateTime => DateFromWmi();

    /// <summary>
    /// Gets a string that represents the local language of the computer.
    /// </summary>
    public string Locale => GetFromWmi<string>();

    /// <summary>
    /// Gets the name of the computer's manufacturer.
    /// </summary>
    public string Manufacturer => GetFromWmi<string>();

    /// <summary>
    /// Gets the maximum number of processes that the operating system can
    /// manage.
    /// </summary>
    [CLSCompliant(false)]
    public uint MaxNumberOfProcesses => GetFromWmi<uint>();

    /// <summary>
    /// Gets the maximum memory size that can be assigned to a process on this computer.
    /// </summary>
    [CLSCompliant(false)]
    public uint MaxProcessMemorySize => GetFromWmi<uint>();

    /// <summary>
    /// Gets a collection of the MUI languages available in this Windows installation.
    /// </summary>
    public string[] MUILanguages => GetFromWmi<string[]>();

    /// <summary>
    /// Gets a descriptive name for this Windows installation.
    /// </summary>
    public string Name => GetFromWmi<string>();

    /// <summary>
    /// Gets the number of processes currently existing in the system.
    /// </summary>
    [CLSCompliant(false)]
    public uint NumberOfProcesses => GetFromWmi<uint>();

    /// <summary>
    /// Gets the number of users registered on the system.
    /// </summary>
    [CLSCompliant(false)]
    public uint NumberOfUsers => GetFromWmi<uint>();

    /// <summary>
    /// Gets a product SKU that identifies Windows.
    /// </summary>
    [CLSCompliant(false)]
    public uint OperatingSystemSKU => GetFromWmi<uint>();

    /// <summary>
    /// Gets the name of the organization registered on the computer.
    /// </summary>
    public string Organization => GetFromWmi<string>();

    /// <summary>
    /// Gets the operating system architecture name.
    /// </summary>
    public string OSArchitecture => GetFromWmi<string>();

    /// <summary>
    /// Gets a value that represents the system language.
    /// </summary>
    [CLSCompliant(false)]
    public uint OSLanguage => GetFromWmi<uint>();

    /// <summary>
    /// Gets a value that describes the product suite to which this OS belongs.
    /// </summary>
    [CLSCompliant(false)]
    public uint OSProductSuite => GetFromWmi<uint>();

    /// <summary>
    /// Gets a value that represents the operating system type.
    /// </summary>
    [CLSCompliant(false)]
    public ushort OSType => GetFromWmi<ushort>();

    /// <summary>
    /// Gets a value indicating whether this operating system is portable.
    /// </summary>
    public bool PortableOperatingSystem => GetFromWmi<bool>();

    /// <summary>
    /// Gets the Primary value from Windows instrumentation.
    /// </summary>
    /// <returns>
    /// The Primary value from Windows instrumentation.
    /// </returns>
    public bool Primary => GetFromWmi<bool>();

    /// <summary>
    /// Gets a value that represents the product type of this OS.
    /// </summary>
    [CLSCompliant(false)]
    public uint ProductType => GetFromWmi<uint>();

    /// <summary>
    /// Gets the registered user name in Windows.
    /// </summary>
    public string RegisteredUser => GetFromWmi<string>();

    /// <summary>
    /// Gets a string that identifies the distribution of this Windows version.
    /// </summary>
    public string ReleaseId => GetFromReg<string>();

    /// <summary>
    /// Gets the serial number of the computer.
    /// </summary>
    public string SerialNumber => GetFromWmi<string>();

    /// <summary>
    /// Gets the major Service Pack number of Windows.
    /// </summary>
    [CLSCompliant(false)]
    [Obsolete(AttributeErrorMessages.Win10NoServicePack)]
    public ushort ServicePackMajorVersion => GetFromWmi<ushort>();

    /// <summary>
    /// Gets the minor Service Pack number of Windows.
    /// </summary>
    [CLSCompliant(false)]
    [Obsolete(AttributeErrorMessages.Win10NoServicePack)]
    public ushort ServicePackMinorVersion => GetFromWmi<ushort>();

    /// <summary>
    /// Gets the size occupied by paging files on the computer’s local storage.
    /// </summary>
    [CLSCompliant(false)]
    public ulong SizeStoredInPagingFiles => GetFromWmi<ulong>();

    /// <summary>
    /// Gets a string that represents the current operating system state.
    /// </summary>
    public string Status => GetFromWmi<string>();

    /// <summary>
    /// Gets a value that can be used as a mask for the OS product suite,
    /// when applied to <see cref="OSProductSuite"/>.
    /// </summary>
    [CLSCompliant(false)]
    public uint SuiteMask => GetFromWmi<uint>();

    /// <summary>
    /// Gets a string that represents the storage device where Windows is installed.
    /// </summary>
    public string SystemDevice => GetFromWmi<string>();

    /// <summary>
    /// Gets the operating system directory path.
    /// </summary>
    public string SystemDirectory => GetFromWmi<string>();

    /// <summary>
    /// Gets the drive letter on which the operating system is installed.
    /// </summary>
    public string SystemDrive => GetFromWmi<string>();

    /// <summary>
    /// Gets the total size of virtual memory.
    /// </summary>
    [CLSCompliant(false)]
    public ulong TotalVirtualMemorySize => GetFromWmi<ulong>();

    /// <summary>
    /// Gets the total size of memory visible to the system.
    /// </summary>
    [CLSCompliant(false)]
    public ulong TotalVisibleMemorySize => GetFromWmi<ulong>();

    /// <summary>
    /// Gets the UBR value from Windows instrumentation.
    /// </summary>
    /// <returns>
    /// The UBR value from Windows instrumentation.
    /// </returns>
    public int UBR => GetFromReg<int>();

    /// <summary>
    /// Gets the operating system version.
    /// </summary>
    public Version Version => Version.Parse($"{GetFromWmi<string>()}.{UBR}");

    /// <summary>
    /// Gets the Windows directory path.
    /// </summary>
    public string WindowsDirectory => GetFromWmi<string>();

    /// <summary>
    /// Gets the operating system manufacturer.
    /// </summary>
    public IEnumerable<string> Authors => [Manufacturer];

    /// <summary>
    /// Gets the copyright notice associated with Windows.
    /// </summary>
    public string Copyright => @$"Copyright © {Manufacturer}";

    /// <summary>
    /// Gets the license text associated with Windows.
    /// </summary>
    public License License => new("Microsoft Windows EULA", new Uri(GetWinLicensePath()));

    /// <summary>
    /// Gets a value indicating whether Windows includes a CLUF.
    /// </summary>
    public bool HasLicense => File.Exists(GetWinLicensePath());

    /// <summary>
    /// Gets a value that indicates whether Windows is CLS compliant.
    /// </summary>
    /// <remarks>
    /// This function will always return <see langword="false"/>, because
    /// large portions of Microsoft Windows were written in C/C++, not C#
    /// or other CLR languages.
    /// </remarks>
    public bool ClsCompliant => false;

    /// <summary>
    /// Gets the informational descriptive version of Windows.
    /// </summary>
    public string? InformationalVersion => $"{Version}-{BuildLabEx}";

    /// <summary>
    /// Enumerates third‑party licenses included with the operating system.
    /// </summary>
    public IEnumerable<License>? ThirdPartyLicenses => null;

    /// <summary>
    /// Gets a value that indicates whether Microsoft Windows includes third‑party licenses.
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
            int.Parse(t[15..18]));
    }
}
