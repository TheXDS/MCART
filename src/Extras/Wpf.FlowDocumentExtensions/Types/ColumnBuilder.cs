/*
ColumnBuilder.cs

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

namespace TheXDS.MCART.FlowDocumentExtensions.Types;

/// <summary>
/// Clase que describe a un selector de datos para generar una
/// columna de datos dentro de una tabla.
/// </summary>
/// <typeparam name="TObject">
/// Tipo de objeto desde el cual se extraerá la información de la
/// columna.
/// </typeparam>
/// <typeparam name="TItem">
/// Tipo de elemento a devolver para esta columna.
/// </typeparam>
public class ColumnBuilder<TObject, TItem> : IColumnBuilder<TObject>
{
    /// <summary>
    /// Título de la columna.
    /// </summary>
    public string Header { get; }

    /// <summary>
    /// Contenido de la fila actual a establecer en esta columna.
    /// </summary>
    /// <param name="obj">Objeto del cual extraer el contenido.</param>
    /// <returns>
    /// El contenido en formato de cadena a colocar dentro de la celda 
    /// correspondiente a esta columna en la fila actual.
    /// </returns>
    public string Content(TObject obj) => Selector?.Invoke(obj)?.ToString() ?? obj?.ToString() ?? string.Empty;

    /// <summary>
    /// Obtiene un estilo a utilizar sobre la celda en esta columna de la fila actual.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>
    /// Un estilo a aplicar a la celda generada, o
    /// <see langword="null"/> si no es posible obtener un estilo
    /// válido.
    /// </returns>
    public ICellStyle? Style(object? item) => item is TItem tItem ? CurrentStyle(tItem) : null;

    /// <summary>
    /// Función de selección de datos.
    /// </summary>
    public Func<TObject, TItem> Selector { get; }

    /// <summary>
    /// Obtiene una colección con los estilos registrados para esta columna.
    /// </summary>
    public IList<CellStyle<TItem>> Styles { get; } = new List<CellStyle<TItem>>();

    /// <summary>
    /// Obtiene un estilo a utilizar para el elemento actual.
    /// </summary>
    /// <param name="currentItem">
    /// Valor contra el cual evaluar los estilos.
    /// </param>
    /// <returns>
    /// Un estilo que pueda ser aplicado al valor, o
    /// <see langword="null"/> si ningún estilo puede ser aplicado.
    /// </returns>
    public CellStyle<TItem>? CurrentStyle(TItem currentItem) => Styles.FirstOrDefault(p => p.StyleApplies?.Invoke(currentItem) ?? true);

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ColumnBuilder{TObject, TItem}"/>.
    /// </summary>
    /// <param name="header">
    /// Título de la columna.
    /// </param>
    /// <param name="selector">
    /// Función de selección de datos.
    /// </param>
    public ColumnBuilder(string header, Func<TObject, TItem> selector)
    {
        Header = header;
        Selector = selector;
    }
}
