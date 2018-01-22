//
//  Properties.cs
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2017 César Morgan
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.PluginSupport;

[assembly: AssemblyTitle("Servidor de LightChat para MCART")]
[assembly: AssemblyDescription("Servidor de LightChat para MCART")]
[assembly: AssemblyCompany("TheXDS! non-Corp.")]
[assembly: AssemblyProduct("MCA Runtime")]
[assembly: AssemblyCopyright("Copyright © 2017 César Andrés Morgan")]
[assembly: AssemblyTrademark("MCA Corporation")]
[assembly: AssemblyVersion("1.0.*")]

namespace LightChat
{
    /// <summary>
    /// Protocolo simple de chat.
    /// </summary>
    [Description("Protocolo de chat ligero LightChat, ensamblado de servidor.")]
    [Beta]
    [MinMCARTVersion(0, 6)]
    [TargetMCARTVersion(0, 6)]
    [Unsecure]
    public partial class LightChat : Plugin { }
}