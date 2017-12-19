//
// PluginExample.cs
//
// Este archivo es un ejemplo sobre creación de plugins para MCART.
// Este plugin requiere MCART 0.6 (cualquier plataforma), pero ha sido compilado
// para Win32. En teoría, debería poder funcionar perfectamente en Windows,
// tanto Win32 como WPF, y en Linux, tanto en Gtk como en el port de Win32 en
// Mono, e incluso Wine si se cuenta con algún framework compatible.
//
//  This file is part of MCART
//
// Author:
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Copyright (c) 2011 - 2018 César Andrés Morgan
//
// MCART Is free software: you can redistribute it And/Or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, Or
// (at your option) any later version.
//
// MCART Is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If Not, see <http://www.gnu.org/licenses/>.

using MCART;
using MCART.Attributes;
using MCART.PluginSupport;
using MCART.Security.Checksum;
using MCART.Types.TaskReporter;
using System;
using System.IO;
using static MCART.Common;
using static Microsoft.VisualBasic.Interaction;

namespace PluginExample
{
    [Description("CRC32 Calculator")]
    [Beta]
    [MinMCARTVersion(0, 6)]
    [TargetMCARTVersion(0, 6, 1)]
    public class CRC32 : ChecksumPlugin, IDisposable
    {
        bool disposedValue;
        uint[] CRC32_Tab = new uint[256];
        const uint Seed = 3988292384U;
        TimeSpan re = new TimeSpan(0, 0, 0, 0, 250);
        public override byte[] Compute(byte[] X)
        {
            try
            {
                Reporter.Begin();
                uint CRC = uint.MaxValue;
                DateTime tm = DateTime.Now;
                float pr = 0;
                int progrss = 1;
                foreach (byte BT in X)
                {
                    pr = (progrss / X.Length) * 100;
                    if (Reporter.CancelPending)
                    {
                        Reporter.Stop(new ProgressEventArgs(pr));
                        return new byte[] { };
                    }
                    CRC = (CRC >> 8) ^ CRC32_Tab[(CRC & 0xff) ^ BT];
                    progrss += 1;
                    if (DateTime.Now - tm > re)
                    {
                        Reporter.Report(new ProgressEventArgs(pr));
                        tm = DateTime.Now;
                    }
                }
                Reporter.End();
                return BitConverter.GetBytes(~CRC);
            }
            catch (Exception ex)
            {
                if (Reporter.OnDuty)
                    Reporter.EndWithError(ex);
                return new byte[] { };
            }
        }
        public override byte[] Compute(Stream X)
        {
            try
            {
                Reporter.Begin();
                uint CRC = uint.MaxValue;
                int bt = X.ReadByte();
                DateTime tm = DateTime.Now;
                float pr = 0;
                while (!(bt == -1))
                {
                    pr = (X.Position / X.Length) * 100;
                    if (Reporter.CancelPending)
                    {
                        Reporter.Stop(new ProgressEventArgs(pr));
                        return new byte[] { };
                    }
                    CRC = (CRC >> 8) ^ CRC32_Tab[(CRC & 0xff) ^ bt];
                    bt = X.ReadByte();
                    if (DateTime.Now - tm > re)
                    {
                        Reporter.Report(new ProgressEventArgs(pr));
                        tm = DateTime.Now;
                    }
                }
                Reporter.End();
                return BitConverter.GetBytes(~CRC);
            }
            catch (Exception ex)
            {
                if (Reporter.OnDuty)
                    Reporter.EndWithError(ex);
                return new byte[] { };
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                CRC32_Tab = null;
            }
            disposedValue = true;
        }
        public void Dispose()
        {
            Dispose(true);
        }
        public CRC32()
        {
            uint seed = Seed;
            uint CRC = 0;
            for (short i = 0; i <= 255; i++)
            {
                CRC = (uint)i;
                for (byte j = 0; j <= 7; j++)
                {
                    if ((CRC & 0x1) == 0x1)
                    {
                        CRC = (CRC >> 1) ^ seed;
                    }
                    else
                    {
                        CRC = CRC >> 1;
                    }
                }
                CRC32_Tab[i] = CRC;
            }
#if DumpTable
        StreamWriter x = new StreamWriter("CRCTab.txt");
        foreach (uint j in CRC32_Tab) {
            x.WriteLine("0x" + Conversion.Hex(j));
        }
        x.Flush();
        x.Close();
        x.Dispose();
        x = null;
#endif
            MyMenu.Add(new InteractionItem(SampleCompute, "Calcular CRC...", "Permite calcular la CRC32 de una cadena de texto"));
            MyMenu.Add(new InteractionItem((a, b) => About(this), "Acerca de " + Name));
        }
        void SampleCompute(object a, EventArgs b)
        {
            MsgBox(Compute(InputBox("Introduzca una cadena")).ToHex());
        }
    }

    public class TestPlugin : Plugin
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TestPlugin"/>.
        /// </summary>
        public TestPlugin()
        {
            MyMenu.Add(new InteractionItem(
                (sender, e) =>
            {
                Console.Write("Prueba de plugin.");
            }
                , "Test", "Plugin de prueba"));
        }
    }
}