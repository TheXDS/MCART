/*
HttpStreamUriParser.cs

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

using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.IO;

/// <summary>
/// Retrieves a <see cref="Stream"/> from a <see cref="Uri"/> that 
/// points to a web resource.
/// </summary>
public class HttpStreamUriParser : SimpleStreamUriParser
{
    /// <summary>
    /// Enumerates the protocols supported by this 
    /// <see cref="HttpStreamUriParser"/>.
    /// </summary>
    protected override IEnumerable<string> SchemeList
    {
        get
        {
            yield return "http";
            yield return "https";
        }
    }

    /// <summary>
    /// Opens a <see cref="Stream"/> from the specified 
    /// <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">The web address to resolve.</param>
    /// <returns>
    /// A <see cref="Stream"/> that allows obtaining the resource 
    /// pointed to by the specified <see cref="Uri"/>.
    /// </returns>
    public sealed override Stream Open(Uri uri)
    {
        using var c = new HttpClient();
        return c.GetStreamAsync(uri).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets a value indicating whether this object prefers full 
    /// transfers when exposing a <see cref="Stream"/>.
    /// </summary>
    public override bool PreferFullTransfer => true;

    /// <summary>
    /// Opens a <see cref="Stream"/> from the specified 
    /// <see cref="Uri"/>, making a full transfer to the computer's 
    /// memory.
    /// </summary>
    /// <param name="uri">The web address to resolve.</param>
    /// <returns>
    /// A <see cref="Stream"/> that allows obtaining the resource 
    /// pointed to by the specified <see cref="Uri"/>.
    /// </returns>
    public override async Task<Stream?> OpenFullTransferAsync(Uri uri)
    {
        try
        {
            MemoryStream? ms = new();
            using var c = new HttpClient();
            (await c.GetStreamAsync(uri)).CopyTo(ms);
            ms.Position = 0;
            return ms;
        }
        catch
        {
            return null;
        }
    }
}
