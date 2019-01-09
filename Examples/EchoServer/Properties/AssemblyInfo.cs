/*
AssemblyInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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

using System.Reflection;
using TheXDS.MCART.Attributes;

[assembly: AssemblyTitle("EchoServer")]
#pragma warning disable CS7035

[assembly: AssemblyFileVersion("1.1.*")]
#pragma warning restore CS7035

[assembly: MinMCARTVersion(0, 8, 5, 0)]
[assembly: TargetMCARTVersion(0, 8, 8, 2)]