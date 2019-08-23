
#nullable enable
using System;
using System.Management;
/*
WindowsInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System.Linq;
using Microsoft.Win32;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Component
{
    public class WindowsInfo : INameable, IDescriptible, IExposeInfo
    {
        private const string _regInfo = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";
        private readonly ManagementObject _managementObject = new ManagementClass(@"Win32_OperatingSystem").GetInstances().OfType<ManagementObject>().FirstOrDefault() ?? throw new PlatformNotSupportedException();

        public string BootDevice => GetFromWmi<string>();
        public string BuildBranch => GetFromReg<string>();
        public Guid BuildGUID => Guid.Parse(GetFromReg<string>());

        public string BuildLab => GetFromReg<string>();
        public string BuildLabEx => GetFromReg<string>();

        public string BuildNumber => GetFromWmi<string>();
        public string BuildType => GetFromWmi<string>();
        public string Caption => GetFromWmi<string>();
        public string CodeSet => GetFromWmi<string>();
        public string CountryCode => GetFromWmi<string>();

        public short CurrentTimeZone => GetFromWmi<short>();
        public bool DEP32bit => GetFromWmi<bool>("DataExecutionPrevention_32BitApplications");
        public bool DEPAvailable => GetFromWmi<bool>("DataExecutionPrevention_Available");
        public bool DEPDrivers => GetFromWmi<bool>("DataExecutionPrevention_Drivers");
        public byte DEPPolicy => GetFromWmi<byte>("DataExecutionPrevention_SupportPolicy");

        public bool Debug => GetFromWmi<bool>();
        public string Description => GetFromWmi<string>();
        public bool Distributed => GetFromWmi<bool>();

        public string EditionID => GetFromReg<string>();
        [CLSCompliant(false)]
        public uint EncryptionLevel => GetFromWmi<uint>();

        public byte ForegroundApplicationBoost => GetFromWmi<byte>();

        public ulong FreePhysicalMemory => GetFromWmi<ulong>();
        public ulong FreeSpaceInPagingFiles => GetFromWmi<ulong>();
        public ulong FreeVirtualMemory => GetFromWmi<ulong>();

        public string InstallationType => GetFromReg<string>();
        public DateTime InstallDate => DateFromWmi();
        public DateTime LastBootUpTime => DateFromWmi();
        public DateTime LocalDateTime => DateFromWmi();

        public string Locale => GetFromWmi<string>();
        public string Manufacturer => GetFromWmi<string>();

        public uint MaxNumberOfProcesses => GetFromWmi<uint>();
        public uint MaxProcessMemorySize => GetFromWmi<uint>();

        public string[] MUILanguages => GetFromWmi<string[]>();

        public string Name => GetFromWmi<string>();

        public uint NumberOfProcesses => GetFromWmi<uint>();
        public uint NumberOfUsers => GetFromWmi<uint>();
        public uint OperatingSystemSKU => GetFromWmi<uint>();

        public string Organization => GetFromWmi<string>();
        public string OSArchitecture => GetFromWmi<string>();

        public uint OSLanguage => GetFromWmi<uint>();
        public uint OSProductSuite => GetFromWmi<uint>();

        public ushort OSType => GetFromWmi<ushort>();

        public bool PortableOperatingSystem => GetFromWmi<bool>();
        public bool Primary => GetFromWmi<bool>();
        public uint ProductType => GetFromWmi<uint>();
        public string RegisteredUser => GetFromWmi<string>();

        public string ReleaseId => GetFromReg<string>();
        public string SerialNumber => GetFromWmi<string>();

        public ushort ServicePackMajorVersion => GetFromWmi<ushort>();
        public ushort ServicePackMinorVersion => GetFromWmi<ushort>();
        public ulong SizeStoredInPagingFiles => GetFromWmi<ulong>();

        public string Status => GetFromWmi<string>();
        public uint SuiteMask => GetFromWmi<uint>();

        public string SystemDevice => GetFromWmi<string>();
        public string SystemDirectory => GetFromWmi<string>();
        public string SystemDrive => GetFromWmi<string>();

        public ulong TotalVirtualMemorySize => GetFromWmi<ulong>();
        public ulong TotalVisibleMemorySize => GetFromWmi<ulong>();

        public int UBR => GetFromReg<int>();

        public Version Version => Version.Parse($"{GetFromWmi<string>()}.{UBR}");

        public string WindowsDirectory => GetFromWmi<string>();

        public string Author => Manufacturer;

        public string Copyright => @$"Copyright © {Manufacturer}";

        public string License => string.Empty;

        public bool HasLicense => false;

        public bool ClsCompliant => false;

        public string? InformationalVersion => $"{Version.ToString()}-{BuildLabEx}";

        private T GetFromWmi<T>([CallerMemberName]string property = "")
        {
            return _managementObject[property] is T v ? v : default;
        }

        private T GetFromReg<T>([CallerMemberName] string value = "")
        {
            return Registry.GetValue(_regInfo, value, default) is T v ? v : default;
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
