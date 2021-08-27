/*
Constants.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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