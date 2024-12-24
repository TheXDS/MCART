/*
ServerAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Attribute that defines the path to a server.
/// </summary>
/// <param name="server">Server name / IP address.</param>
/// <param name="port">Server port number.</param>
/// <remarks>
/// This attribute can be set more than once on the same element.<br/>
/// If a port number is defined in <paramref name="server" />, the
/// value of the <paramref name="port" /> parameter will take precedence.
/// </remarks>
/// <exception cref="ArgumentException">
/// Thrown if the server is a malformed path.
/// </exception>
/// <exception cref="ArgumentOutOfRangeException">
/// Thrown if <paramref name="port" /> is less than 1 or greater
/// than 65535.
/// </exception>
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
[Serializable]
public sealed class ServerAttribute(string server, int port) : Attribute, IValueAttribute<string>
{
    /// <summary>
    /// Gets the server.
    /// </summary>
    /// <value>
    /// The path of the server to which this attribute points.
    /// </value>
    public string Server { get; } = EmptyChecked(server, nameof(server));

    /// <summary>
    /// Gets or sets the connection port of the server.
    /// </summary>
    /// <value>
    /// A value between 1 and 65535 that sets the port number to
    /// point to.
    /// </value>
    public int Port { get; } = RangeChecked(port, 1, 65535);

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"{Server}:{Port}";
    }

    /// <summary>
    /// Gets the value of this attribute.
    /// </summary>
    public string Value => ToString();
}
