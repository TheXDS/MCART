// WindowsInfoTests.cs
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

#pragma warning disable CS0618

using TheXDS.MCART.Component;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Windows.Tests.Component;

internal class WindowsInfoTests
{
    [Test]
    public void Class_exposes_all_info()
    {
        var info = new WindowsInfo();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(info, Is.Not.Null);
            Assert.That(info.BootDevice, Is.TypeOf<string>().Or.Null);
            Assert.That(info.BuildBranch, Is.TypeOf<string>().Or.Null);
            Assert.That(info.BuildGUID, Is.TypeOf<Guid>());
            Assert.That(info.BuildLab, Is.TypeOf<string>().Or.Null);
            Assert.That(info.BuildLabEx, Is.TypeOf<string>().Or.Null);
            Assert.That(info.BuildNumber, Is.TypeOf<string>().Or.Null);
            Assert.That(info.BuildType, Is.TypeOf<string>().Or.Null);
            Assert.That(info.Caption, Is.TypeOf<string>().Or.Null);
            Assert.That(info.CodeSet, Is.TypeOf<string>().Or.Null);
            Assert.That(info.CountryCode, Is.TypeOf<string>().Or.Null);
            Assert.That(info.CurrentTimeZone, Is.TypeOf<short>());
            Assert.That(info.DEP32bit, Is.TypeOf<bool>());
            Assert.That(info.DEPAvailable, Is.TypeOf<bool>());
            Assert.That(info.DEPDrivers, Is.TypeOf<bool>());
            Assert.That(info.DEPPolicy, Is.TypeOf<byte>());
            Assert.That(info.Debug, Is.TypeOf<bool>());
            Assert.That(info.Description, Is.TypeOf<string>().Or.Null);
            Assert.That(info.Distributed, Is.TypeOf<bool>());
            Assert.That(info.EditionID, Is.TypeOf<string>().Or.Null);
            Assert.That(info.EncryptionLevel, Is.TypeOf<uint>());
            Assert.That(info.FirmwareType, Is.TypeOf<FirmwareType>());
            Assert.That(info.ForegroundApplicationBoost, Is.TypeOf<byte>());
            Assert.That(info.FreePhysicalMemory, Is.TypeOf<ulong>());
            Assert.That(info.FreeSpaceInPagingFiles, Is.TypeOf<ulong>());
            Assert.That(info.FreeVirtualMemory, Is.TypeOf<ulong>());
            Assert.That(info.InstallationType, Is.TypeOf<string>().Or.Null);
            Assert.That(info.InstallDate, Is.TypeOf<DateTime>());
            Assert.That(info.LastBootUpTime, Is.TypeOf<DateTime>());
            Assert.That(info.LocalDateTime, Is.TypeOf<DateTime>());
            Assert.That(info.Locale, Is.TypeOf<string>().Or.Null);
            Assert.That(info.Manufacturer, Is.TypeOf<string>().Or.Null);
            Assert.That(info.MaxNumberOfProcesses, Is.TypeOf<uint>());
            Assert.That(info.MaxProcessMemorySize, Is.TypeOf<uint>());
            Assert.That(info.MUILanguages, Is.TypeOf<string[]>());
            Assert.That(info.Name, Is.TypeOf<string>().Or.Null);
            Assert.That(info.NumberOfProcesses, Is.TypeOf<uint>());
            Assert.That(info.NumberOfUsers, Is.TypeOf<uint>());
            Assert.That(info.OperatingSystemSKU, Is.TypeOf<uint>());
            Assert.That(info.Organization, Is.TypeOf<string>().Or.Null);
            Assert.That(info.OSArchitecture, Is.TypeOf<string>().Or.Null);
            Assert.That(info.OSLanguage, Is.TypeOf<uint>());
            Assert.That(info.OSProductSuite, Is.TypeOf<uint>());
            Assert.That(info.OSType, Is.TypeOf<ushort>());
            Assert.That(info.PortableOperatingSystem, Is.TypeOf<bool>());
            Assert.That(info.Primary, Is.TypeOf<bool>());
            Assert.That(info.ProductType, Is.TypeOf<uint>());
            Assert.That(info.RegisteredUser, Is.TypeOf<string>().Or.Null);
            Assert.That(info.ReleaseId, Is.TypeOf<string>().Or.Null);
            Assert.That(info.SerialNumber, Is.TypeOf<string>().Or.Null);
            Assert.That(info.ServicePackMajorVersion, Is.TypeOf<ushort>());
            Assert.That(info.ServicePackMinorVersion, Is.TypeOf<ushort>());
            Assert.That(info.SizeStoredInPagingFiles, Is.TypeOf<ulong>());
            Assert.That(info.Status, Is.TypeOf<string>().Or.Null);
            Assert.That(info.SuiteMask, Is.TypeOf<uint>());
            Assert.That(info.SystemDevice, Is.TypeOf<string>().Or.Null);
            Assert.That(info.SystemDirectory, Is.TypeOf<string>().Or.Null);
            Assert.That(info.SystemDrive, Is.TypeOf<string>().Or.Null);
            Assert.That(info.TotalVirtualMemorySize, Is.TypeOf<ulong>());
            Assert.That(info.TotalVisibleMemorySize, Is.TypeOf<ulong>());
            Assert.That(info.UBR, Is.TypeOf<int>());
            Assert.That(info.Version, Is.TypeOf<Version>());
            Assert.That(info.WindowsDirectory, Is.TypeOf<string>().Or.Null);
            Assert.That(info.Authors.ToArray(), Is.TypeOf<string[]>());
            Assert.That(info.Copyright, Is.TypeOf<string>().Or.Null);
            Assert.That(info.License, Is.TypeOf<License>());
            Assert.That(info.HasLicense, Is.TypeOf<bool>());
            Assert.That(info.ClsCompliant, Is.False);
            Assert.That(info.InformationalVersion, Is.TypeOf<string>().Or.Null);
            Assert.That(info.ThirdPartyLicenses, Is.Null);
            Assert.That(info.Has3rdPartyLicense, Is.False);
        }
    }
}
