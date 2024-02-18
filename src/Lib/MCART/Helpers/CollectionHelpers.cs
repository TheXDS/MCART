/*
CollectionHelpers.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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

using System.Collections;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Funciones auxiliares para trabajar con colecciones y enumeraciones.
/// </summary>
public static partial class CollectionHelpers
{
    /// <summary>
    /// Determina si un conjunto de cadenas están vacías.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si las cadenas están vacías o son
    /// <see langword="null" />; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="stringCollection">Cadenas a comprobar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="stringCollection"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static async Task<bool> AllEmpty(this IAsyncEnumerable<string?> stringCollection)
    {
        AllEmpty_Contract(stringCollection);
        await foreach (string? j in stringCollection.ConfigureAwait(false))
        {
            if (!j.IsEmpty()) return false;
        }
        return true;
    }

    /// <summary>
    /// Determina si un conjunto de cadenas están vacías.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si las cadenas están vacías o son
    /// <see langword="null" />; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="stringCollection">Cadenas a comprobar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="stringCollection"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static bool AllEmpty(this IEnumerable<string?> stringCollection)
    {
        AllEmpty_Contract(stringCollection);
        return stringCollection.All(j => j.IsEmpty());
    }

    /// <summary>
    /// Determina si alguna cadena está vacía.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si alguna cadena está vacía o es
    /// <see langword="null" />; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="stringCollection">Cadenas a comprobar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="stringCollection"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static async Task<bool> AnyEmpty(this IAsyncEnumerable<string?> stringCollection)
    {
        AnyEmpty_Contract(stringCollection);
        await foreach (string? j in stringCollection.ConfigureAwait(false))
        {
            if (j.IsEmpty()) return true;
        }
        return false;
    }

    /// <summary>
    /// Determina si alguna cadena está vacía.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si alguna cadena está vacía o es 
    /// <see langword="null" />; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="stringCollection">Cadenas a comprobar.</param>
    /// <param name="index">
    /// Argumento de salida. Índices de las cadenas vacías encontradas.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="stringCollection"/> es 
    /// <see langword="null"/>.
    /// </exception>
    public static bool AnyEmpty(this IEnumerable<string?> stringCollection, out IEnumerable<int> index)
    {
        AnyEmpty_Contract(stringCollection);
        List<int>? idx = new();
        int c = 0;
        foreach (string? j in stringCollection)
        {
            if (j.IsEmpty()) idx.Add(c);
            c++;
        }
        index = idx.AsEnumerable();
        return idx.Any();
    }

    /// <summary>
    /// Determina si alguna cadena está vacía.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si alguna cadena está vacía o es
    /// <see langword="null" />; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="stringCollection">Cadenas a comprobar.</param>
    /// <param name="firstIndex">
    /// Argumento de salida. Índice de la primera cadena vacía encontrada.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="stringCollection"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static bool AnyEmpty(this IEnumerable<string?> stringCollection, out int firstIndex)
    {
        AnyEmpty_Contract(stringCollection);
        bool r = AnyEmpty(stringCollection, out IEnumerable<int> indexes);
        int[]? a = indexes.ToArray();
        firstIndex = a.Any() ? a.First() : -1;
        return r;
    }

    /// <summary>
    /// Determina si alguna cadena está vacía.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si alguna cadena está vacía o es
    /// <see langword="null" />; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="stringCollection">Cadenas a comprobar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="stringCollection"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static bool AnyEmpty(this IEnumerable<string?> stringCollection)
    {
        AnyEmpty_Contract(stringCollection);
        return stringCollection.Any(j => j.IsEmpty());
    }

    /// <summary>
    /// Determina si todos los objetos son <see langword="null" />.
    /// </summary>
    /// <returns>
    /// <see langword="true" />, si todos los objetos son <see langword="null" />; de lo contrario,
    /// <see langword="false" />.
    /// </returns>
    /// <param name="collection">Objetos a comprobar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static bool AreAllNull(this IEnumerable<object?> collection)
    {
        NullCheck(collection, nameof(collection));
        return collection.All(p => p is null);
    }

    /// <summary>
    /// Determina si cualquiera de los objetos es <see langword="null" />.
    /// </summary>
    /// <returns>
    /// <see langword="true" />, si cualquiera de los objetos es <see langword="null" />; de lo
    /// contrario, <see langword="false" />.
    /// </returns>
    /// <param name="x">Objetos a comprobar.</param>
    public static bool IsAnyNull(this IEnumerable<object?>? x)
    {
        return x?.Any(p => p is null) ?? true;
    }

    /// <summary>
    /// Enumera todas las cadenas no nulas ni vacías de una colección.
    /// </summary>
    /// <param name="stringCollection">
    /// Colección desde la cual obtener las cadenas.
    /// </param>
    /// <returns>
    /// Una enumeración de las cadenas no nulas ni vacías de la colección.
    /// </returns>
    public static IEnumerable<string> NotEmpty(this IEnumerable<string?> stringCollection)
    {
        NotEmpty_Contract(stringCollection);
        foreach (string? j in stringCollection)
        {
            if (!j.IsEmpty()) yield return j;
        }
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="double" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="double" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="min">Valor que representará 0%.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
    /// iguales.
    /// </exception>
    public static async IAsyncEnumerable<double> ToPercent(this IAsyncEnumerable<double> collection, double min, double max)
    {
        ToPercent_Contract(collection, min, max);
        await foreach (double j in collection)
            if (j.IsValid())
                yield return (j - min) / (max - min).Clamp(1, double.NaN);
            else
                yield return double.NaN;
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="double" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="double" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo de la colección y
    /// <paramref name="max"/> son iguales.
    /// </exception>
    public static IAsyncEnumerable<double> ToPercent(this IAsyncEnumerable<double> collection, in double max)
    {
        return ToPercent(collection, 0, max);
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="float" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="float" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="min">Valor que representará 0%.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
    /// iguales.
    /// </exception>
    public static async IAsyncEnumerable<float> ToPercent(this IAsyncEnumerable<float> collection, float min, float max)
    {
        ToPercent_Contract(collection, min, max);
        await foreach (float j in collection)
            if (j.IsValid())
                yield return (j - min) / (max - min).Clamp(1, float.NaN);
            else
                yield return float.NaN;
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="float" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="float" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo de la colección y
    /// <paramref name="max"/> son iguales.
    /// </exception>
    public static IAsyncEnumerable<float> ToPercent(this IAsyncEnumerable<float> collection, in float max)
    {
        return ToPercent(collection, 0, max);
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="double" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="double" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="min">Valor que representará 0%.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
    /// iguales.
    /// </exception>
    public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, double min, double max)
    {
        ToPercent_Contract(collection, min, max);
        foreach (double j in collection)
            if (j.IsValid())
                yield return (j - min) / (max - min).Clamp(1, double.NaN);
            else
                yield return double.NaN;
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="double" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="double" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="baseZero">
    /// Si es <see langword="true" />, la base de
    /// porcentaje es cero; de lo contrario, se utilizará el valor mínimo
    /// dentro de la colección.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo y máximo de la colección son iguales.
    /// </exception>
    public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, in bool baseZero)
    {
        ToPercent_Contract(collection);
        List<double>? enumerable = collection.ToList();
        return ToPercent(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="double" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="double" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo de la colección y
    /// <paramref name="max"/> son iguales.
    /// </exception>
    public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, in double max)
    {
        return ToPercent(collection, 0, max);
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="double" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="double" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo y máximo de la colección son iguales.
    /// </exception>
    public static IEnumerable<double> ToPercent(this IEnumerable<double> collection)
    {
        ToPercent_Contract(collection);
        List<double>? enumerable = collection.ToList();
        return ToPercent(enumerable, enumerable.Min(), enumerable.Max());
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="float" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="float" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="min">Valor que representará 0%.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
    /// iguales.
    /// </exception>
    public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, float min, float max)
    {
        ToPercent_Contract(collection, min, max);
        foreach (float j in collection)
            if (j.IsValid())
                yield return (j - min) / (max - min).Clamp(1, float.NaN);
            else
                yield return float.NaN;
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="float" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="float" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="baseZero">
    /// Si es <see langword="true" />, la base de
    /// porcentaje es cero; de lo contrario, se utilizará el valor mínimo
    /// dentro de la colección.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo y máximo de la colección son iguales.
    /// </exception>
    public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, in bool baseZero)
    {
        ToPercent_Contract(collection);
        List<float>? enumerable = collection.ToList();
        return ToPercent(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="float" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="float" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo de la colección y
    /// <paramref name="max"/> son iguales.
    /// </exception>
    public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, in float max)
    {
        return ToPercent(collection, 0, max);
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="float" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="float" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo y máximo de la colección son iguales.
    /// </exception>
    public static IEnumerable<float> ToPercent(this IEnumerable<float> collection)
    {
        ToPercent_Contract(collection);
        List<float>? enumerable = collection.ToList();
        return ToPercent(enumerable, enumerable.Min(), enumerable.Max());
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="int" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="double" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo de la colección y
    /// <paramref name="max"/> son iguales.
    /// </exception>
    public static IAsyncEnumerable<double> ToPercentDouble(this IAsyncEnumerable<int> collection, in int max)
    {
        return ToPercentDouble(collection, 0, max);
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="int" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="double" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="min">Valor que representará 0%.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
    /// iguales.
    /// </exception>
    public static async IAsyncEnumerable<double> ToPercentDouble(this IAsyncEnumerable<int> collection, int min, int max)
    {
        ToPercent_Contract(collection, min, max);
        await foreach (int j in collection) yield return (j - min) / (double)(max - min);
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="int" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="double" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="baseZero">
    /// Opcional. si es <see langword="true" />, la base de
    /// porcentaje es cero; de lo contrario, se utilizará el valor mínimo
    /// dentro de la colección.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo y máximo de la colección son iguales.
    /// </exception>
    public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, in bool baseZero)
    {
        ToPercentDouble_Contract(collection);
        List<int>? enumerable = collection.ToList();
        return ToPercentDouble(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="int" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="double" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo de la colección y
    /// <paramref name="max"/> son iguales.
    /// </exception>
    public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, in int max)
    {
        return ToPercentDouble(collection, 0, max);
    }

    /// <summary>
    /// Enumera los elementos de la colección, incluyendo el índice de cada
    /// elemento devuelto.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
    /// <param name="collection">
    /// Colección para la cual enumerar los elementos junto con su índice.
    /// </param>
    /// <returns>Una enumeración de cada elemento junto a su índice.</returns>
    public static IEnumerable<(int index, T element)> WithIndex<T>(this IEnumerable<T> collection)
    {
        int i = 0;
        foreach (var j in collection)
        {
            yield return (i++, j);
        }
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="int" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="double" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="min">Valor que representará 0%.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
    /// iguales.
    /// </exception>
    public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, int min, int max)
    {
        ToPercent_Contract(collection, min, max);
        foreach (int j in collection) yield return (j - min) / (double)(max - min);
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="int" /> a porcentajes.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="double" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo y máximo de la colección son iguales.
    /// </exception>
    public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection)
    {
        ToPercentDouble_Contract(collection);
        List<int>? enumerable = collection.ToList();
        return ToPercentDouble(enumerable, 0, enumerable.Max());
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="int" /> a porcentajes de precisión simple.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="float" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo de la colección y
    /// <paramref name="max"/> son iguales.
    /// </exception>
    public static IAsyncEnumerable<float> ToPercentSingle(this IAsyncEnumerable<int> collection, in int max)
    {
        return ToPercentSingle(collection, 0, max);
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="int" /> a porcentajes de precisión simple.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="float" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="min">Valor que representará 0%.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
    /// iguales.
    /// </exception>
    public static async IAsyncEnumerable<float> ToPercentSingle(this IAsyncEnumerable<int> collection, int min, int max)
    {
        ToPercent_Contract(collection, min, max);
        await foreach (int j in collection) yield return (j - min) / (float)(max - min);
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="int" /> a porcentajes de precisión simple.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="float" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="baseZero">
    /// Opcional. si es <see langword="true" />, la base de
    /// porcentaje es cero; de lo contrario, se utilizará el valor mínimo
    /// dentro de la colección.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo y máximo de la colección son iguales.
    /// </exception>
    public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, in bool baseZero)
    {
        ToPercentSingle_Contract(collection);
        List<int>? enumerable = collection.ToList();
        return ToPercentSingle(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="int" /> a porcentajes de precisión simple.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="float" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo de la colección y
    /// <paramref name="max"/> son iguales.
    /// </exception>
    public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, in int max)
    {
        return ToPercentSingle(collection, 0, max);
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="int" /> a porcentajes de precisión simple.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="float" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="min">Valor que representará 0%.</param>
    /// <param name="max">Valor que representará 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
    /// iguales.
    /// </exception>
    public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, int min, int max)
    {
        ToPercent_Contract(collection, min, max);
        foreach (int j in collection) yield return (j - min) / (float)(max - min);
    }

    /// <summary>
    /// Convierte los valores de una colección de elementos
    /// <see cref="int" /> a porcentajes de precisión simple.
    /// </summary>
    /// <returns>
    /// Una colección de <see cref="float" /> con sus valores
    /// expresados en porcentaje.
    /// </returns>
    /// <param name="collection">Colección a procesar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el valor mínimo y máximo de la colección son iguales.
    /// </exception>
    public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection)
    {
        ToPercentSingle_Contract(collection);
        List<int>? enumerable = collection.ToList();
        return ToPercentSingle(enumerable, 0, enumerable.Max());
    }

    /// <summary>
    /// Obtiene una lista de los tipos de los objetos especificados.
    /// </summary>
    /// <param name="objects">
    /// Objetos a partir de los cuales generar la colección de tipos.
    /// </param>
    /// <returns>
    /// Una lista compuesta por los tipos de los objetos provistos.
    /// </returns>
    public static IEnumerable<Type> ToTypes(this IEnumerable objects)
    {
        foreach (object? j in objects) if (j is not null) yield return j.GetType();
    }

    /// <summary>
    /// Determina si cualquiera de los objetos es <see langword="null" />.
    /// </summary>
    /// <returns>
    /// Un enumerador con los índices de los objetos que son <see langword="null" />.
    /// </returns>
    /// <param name="collection">Colección de objetos a comprobar.</param>
    public static IEnumerable<int> WhichAreNull(this IEnumerable<object?> collection)
    {
        WhichAreNull_Contract(collection);
        int c = 0;
        foreach (object? j in collection)
        {
            if (j is null) yield return c;
            c++;
        }
    }
}
