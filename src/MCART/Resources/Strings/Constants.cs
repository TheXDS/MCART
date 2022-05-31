/*
Constants.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Resources.Strings
{
    /// <summary>
    /// Contiene una colección de constantes de cadena.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Obtiene una cadena con todos los caracteres alfabéticos en minúscula.
        /// </summary>
        public const string AlphaLc = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Obtiene una cadena con todos los caracteres alfabéticos en mayúscula.
        /// </summary>
        public const string AlphaUc = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Obtiene una cadena con todos los dígitos numéricos.
        /// </summary>
        public const string Numbers = "0123456789";

        /// <summary>
        /// Obtiene una cadena con todos los signos de puntuación comunes.
        /// </summary>
        public const string Symbols = "`~!@#$%^&*()-_=+\\|]}[{'\";:/?.>,<";

        /// <summary>
        /// Obtiene una cadena con todos los caracteres alfanuméricos y signos
        /// de puntuación comunes.
        /// </summary>
        public const string Chars = AlphaLc + AlphaUc + Numbers + Symbols;

        /// <summary>
        /// Obtiene un cadena con distintos caracteres utilizados en distintos
        /// alfabetos latinos.
        /// </summary>
        public const string LatinChars = "áéíóúüñäåöàèìòùâêîôûç";

        /// <summary>
        /// Obtiene una cadena con distintos caracteres misceláneos.
        /// </summary>
        public const string MoreChars = @"ẁèỳùìòàǜǹẀÈỲÙÌÒÀǛǸẽẼỹỸũŨĩĨõÕãÃṽṼñÑ`~1!¡¹2@²űŰőŐ˝3#ĒǕŌĀ¯4$¤£5%€şḑģḩķļçņȩŗţ¸6ŵêŷûîôâŝĝĥĵẑĉŴÊŶÛÎÔÂŜĜĤĴẐĈ¼^7&ươƯƠ̛8*¾ęųįǫąĘŲĮǪĄ˛9(‘ĕŭĭŏăĔŬĬŎĂ˘0)’ẘẙůŮåÅ-_¥ẉẹṛṭỵụịọạṣḍḥḳḷṿḅṇṃ̣=+×÷qQwWeErR®tTþÞyYuUiIoOpP[{«“]}»”\|¬¦aAsSß§dDðÐfFgGhHjJkKœŒlLøØ;:¶°ẃéŕýúíóṕáśǵḱĺźćǘńḿ'ẅëẗÿüïöäḧẍẄËŸÜÏÖÄḦẌ""zZæÆxXcC©¢vVbBnNmMµ,<çÇ.>ˇěřťǔǐǒǎšďǧȟǰǩǩľžčǚň/?¿ʠⱳẻƭỷủỈỏƥảʂɗƒɠɦƙȥƈʋɓɲɱ̉";

        /// <summary>
        /// Obtiene una cadena con símbolos misceláneos.
        /// </summary>
        public const string MoreSymbs = "¡²³¤€¼½¾‘’¥×»«¬´¶¿Ç®ÞßÐØÆ©µ¹£÷¦¨°çþ§";
    }
}