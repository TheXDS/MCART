/*
AutoDictionary.cs

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

namespace TheXDS.MCART.Types;

/// <summary>
/// Dictionary with support for automatic instantiation 
/// of non-existent keys.
/// </summary>
/// <typeparam name="TKey">Key type to use.</typeparam>
/// <typeparam name="TValue">
/// Type of value contained in 
/// this dictionary.
/// </typeparam>
public class AutoDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    where TKey : notnull
    where TValue : new()
{
    /// <summary>
    /// Gets or sets the value associated with the specified key, instantiating
    /// a new value if it does not exist.
    /// </summary>
    /// <param name="key">
    /// Key of the value to get or set. A new value will be created if the key
    /// does not exist.
    /// </param>
    /// <returns>
    /// Value associated with the specified key. If the key is not found, a new
    /// element with that key will be 
    /// created.
    /// </returns>
    public new TValue this[TKey key]
    {
        get
        {
            if (!ContainsKey(key)) Add(key, new TValue());
            return base[key];
        }
        set => base[key] = value;
    }
}
