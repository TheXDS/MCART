/*
Colors.cs

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

using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Incluye una colección de colores adicionales.
    /// </summary>
    public static class Colors
    {
        /// <summary>
        /// Devuelve un color aleatorio.
        /// </summary>
        /// <returns>
        /// Un <see cref="Color"/> seleccionado aleatoriamente.
        /// </returns>
        public static Color Pick() => (Color)typeof(Colors).GetProperties().Pick().GetValue(null)!;

        /// <summary>
        /// Obtiene el color azul alicia.
        /// </summary>
        public static Color AliceBlue => new(240, 248, 255);

        /// <summary>
        /// Obtiene el color blanco antíguo en su tonalidad alternativa 1.
        /// </summary>
        public static Color AntiqueWhite1 => new(255, 239, 219);

        /// <summary>
        /// Obtiene el color blanco antíguo en su tonalidad alternativa 2.
        /// </summary>
        public static Color AntiqueWhite2 => new(238, 223, 204);

        /// <summary>
        /// Obtiene el color blanco antíguo en su tonalidad alternativa 3.
        /// </summary>
        public static Color AntiqueWhite3 => new(205, 192, 176);

        /// <summary>
        /// Obtiene el color blanco antíguo en su tonalidad alternativa 4.
        /// </summary>
        public static Color AntiqueWhite4 => new(139, 131, 120);

        /// <summary>
        /// Obtiene el color blanco antíguo.
        /// </summary>
        public static Color AntiqueWhite => new(250, 235, 215);

        /// <summary>
        /// Obtiene el color aguamarina en su tonalidad alternativa 2.
        /// </summary>
        public static Color Aquamarine2 => new(118, 238, 198);

        /// <summary>
        /// Obtiene el color aguamarina en su tonalidad alternativa 4.
        /// </summary>
        public static Color Aquamarine4 => new(69, 139, 116);

        /// <summary>
        /// Obtiene el color aguamarina.
        /// </summary>
        public static Color Aquamarine => new(127, 255, 212);

        /// <summary>
        /// Obtiene el color azur en su tonalidad alternativa 2.
        /// </summary>
        public static Color Azure2 => new(224, 238, 238);

        /// <summary>
        /// Obtiene el color azur en su tonalidad alternativa 3.
        /// </summary>
        public static Color Azure3 => new(193, 205, 205);

        /// <summary>
        /// Obtiene el color azur en su tonalidad alternativa 4.
        /// </summary>
        public static Color Azure4 => new(131, 139, 139);

        /// <summary>
        /// Obtiene el color azur.
        /// </summary>
        public static Color Azure => new(240, 255, 255);

        /// <summary>
        /// Obtiene el color banana.
        /// </summary>
        public static Color Banana => new(227, 207, 87);

        /// <summary>
        /// Obtiene el color beige.
        /// </summary>
        public static Color Beige => new(245, 245, 220);

        /// <summary>
        /// Obtiene el color "Bisque 2"
        /// </summary>
        public static Color Bisque2 => new(238, 213, 183);

        /// <summary>
        /// Obtiene el color "Bisque 3"
        /// </summary>
        public static Color Bisque3 => new(205, 183, 158);

        /// <summary>
        /// Obtiene el color "Bisque 4"
        /// </summary>
        public static Color Bisque4 => new(139, 125, 107);

        /// <summary>
        /// Obtiene el color "Bisque"
        /// </summary>
        public static Color Bisque => new(255, 228, 196);

        /// <summary>
        /// Obtiene el color negro.
        /// </summary>
        public static Color Black => new(0, 0, 0);

        /// <summary>
        /// Obtiene el color "Blanched Almond"
        /// </summary>
        public static Color BlanchedAlmond => new(255, 235, 205);

        /// <summary>
        /// Obtiene el color azul en su tonalidad alternativa 2.
        /// </summary>
        public static Color Blue2 => new(0, 0, 238);

        /// <summary>
        /// Obtiene el color azul.
        /// </summary>
        public static Color Blue => new(0, 0, 255);

        /// <summary>
        /// Obtiene el color azul violeta.
        /// </summary>
        public static Color BlueViolet => new(138, 43, 226);

        /// <summary>
        /// Obtiene el color ladrillo.
        /// </summary>
        public static Color Brick => new(156, 102, 31);

        /// <summary>
        /// Obtiene el color café en su tonalidad alternativa 1.
        /// </summary>
        public static Color Brown1 => new(255, 64, 64);

        /// <summary>
        /// Obtiene el color café en su tonalidad alternativa 2.
        /// </summary>
        public static Color Brown2 => new(238, 59, 59);

        /// <summary>
        /// Obtiene el color café en su tonalidad alternativa 3.
        /// </summary>
        public static Color Brown3 => new(205, 51, 51);

        /// <summary>
        /// Obtiene el color café en su tonalidad alternativa 4.
        /// </summary>
        public static Color Brown4 => new(139, 35, 35);

        /// <summary>
        /// Obtiene el color café.
        /// </summary>
        public static Color Brown => new(165, 42, 42);

        /// <summary>
        /// Obtiene el color madera densa en su tonalidad alternativa 1.
        /// </summary>
        public static Color BurlyWood1 => new(255, 211, 155);

        /// <summary>
        /// Obtiene el color madera densa en su tonalidad alternativa 2.
        /// </summary>
        public static Color BurlyWood2 => new(238, 197, 145);

        /// <summary>
        /// Obtiene el color madera densa en su tonalidad alternativa 3.
        /// </summary>
        public static Color BurlyWood3 => new(205, 170, 125);

        /// <summary>
        /// Obtiene el color madera densa en su tonalidad alternativa 4.
        /// </summary>
        public static Color BurlyWood4 => new(139, 115, 85);

        /// <summary>
        /// Obtiene el color madera densa.
        /// </summary>
        public static Color BurlyWood => new(222, 184, 135);

        /// <summary>
        /// Obtiene el color siena quemada.
        /// </summary>
        public static Color BurntSienna => new(138, 54, 15);

        /// <summary>
        /// Obtiene el color marrón rojizo.
        /// </summary>
        public static Color BurnTumber => new(138, 51, 36);

        /// <summary>
        /// Obtiene el color azul cadete en su tonalidad alternativa 1.
        /// </summary>
        public static Color CadetBlue1 => new(152, 245, 255);

        /// <summary>
        /// Obtiene el color azul cadete en su tonalidad alternativa 2.
        /// </summary>
        public static Color CadetBlue2 => new(142, 229, 238);

        /// <summary>
        /// Obtiene el color azul cadete en su tonalidad alternativa 3
        /// </summary>
        public static Color CadetBlue3 => new(122, 197, 205);

        /// <summary>
        /// Obtiene el color azul cadete en su tonalidad alternativa 4.
        /// </summary>
        public static Color CadetBlue4 => new(83, 134, 139);

        /// <summary>
        /// Obtiene el color azul cadete.
        /// </summary>
        public static Color CadetBlue => new(95, 158, 160);

        /// <summary>
        /// Obtiene el color naranja cadmio.
        /// </summary>
        public static Color CadmiumOrange => new(255, 97, 3);

        /// <summary>
        /// Obtiene el color amarillo cadmio.
        /// </summary>
        public static Color CadmiumYellow => new(255, 153, 18);

        /// <summary>
        /// Obtiene el color zanahoria.
        /// </summary>
        public static Color Carrot => new(237, 145, 33);

        /// <summary>
        /// Obtiene el color verde chartreuse en su tonalidad alternativa 2.
        /// </summary>
        public static Color Chartreuse2 => new(118, 238, 0);

        /// <summary>
        /// Obtiene el color verde chartreuse en su tonalidad alternativa 3.
        /// </summary>
        public static Color Chartreuse3 => new(102, 205, 0);

        /// <summary>
        /// Obtiene el color verde chartreuse en su tonalidad alternativa 4.
        /// </summary>
        public static Color Chartreuse4 => new(69, 139, 0);

        /// <summary>
        /// Obtiene el color verde chartreuse.
        /// </summary>
        public static Color Chartreuse => new(127, 255, 0);

        /// <summary>
        /// Obtiene el color chocolate en su tonalidad alternativa 1.
        /// </summary>
        public static Color Chocolate1 => new(255, 127, 36);

        /// <summary>
        /// Obtiene el color chocolate en su tonalidad alternativa 2.
        /// </summary>
        public static Color Chocolate2 => new(238, 118, 33);

        /// <summary>
        /// Obtiene el color chocolate en su tonalidad alternativa 3.
        /// </summary>
        public static Color Chocolate3 => new(205, 102, 29);

        /// <summary>
        /// Obtiene el color chocolate.
        /// </summary>
        public static Color Chocolate => new(210, 105, 30);

        /// <summary>
        /// Obtiene el color verde cobalto.
        /// </summary>
        public static Color CobaltGreen => new(61, 145, 64);

        /// <summary>
        /// Obtiene el color azul cobalto.
        /// </summary>
        public static Color Cobalt => new(61, 89, 171);

        /// <summary>
        /// Obtiene el color gris frío.
        /// </summary>
        public static Color ColdGrey => new(128, 138, 135);

        /// <summary>
        /// Obtiene el color coral en su tonalidad alternativa 2.
        /// </summary>
        public static Color Coral2 => new(238, 106, 80);

        /// <summary>
        /// Obtiene el color coral en su tonalidad alternativa 3.
        /// </summary>
        public static Color Coral3 => new(205, 91, 69);

        /// <summary>
        /// Obtiene el color coral en su tonalidad alternativa 4.
        /// </summary>
        public static Color Coral4 => new(139, 62, 47);

        /// <summary>
        /// Obtiene el color coral.
        /// </summary>
        public static Color Coral => new(255, 127, 80);

        /// <summary>
        /// Obtiene el color azul aciano.
        /// </summary>
        public static Color CornFlowerBlue => new(100, 149, 237);

        /// <summary>
        /// Obtiene el color seda de maíz en su tonalidad alternativa 2.
        /// </summary>
        public static Color CornSilk2 => new(238, 232, 205);

        /// <summary>
        /// Obtiene el color seda de maíz en su tonalidad alternativa 3.
        /// </summary>
        public static Color CornSilk3 => new(205, 200, 177);

        /// <summary>
        /// Obtiene el color seda de maíz en su tonalidad alternativa 4.
        /// </summary>
        public static Color CornSilk4 => new(139, 136, 120);

        /// <summary>
        /// Obtiene el color seda de maíz.
        /// </summary>
        public static Color CornSilk => new(255, 248, 220);

        /// <summary>
        /// Obtiene el color carmesí.
        /// </summary>
        public static Color Crimson => new(220, 20, 60);

        /// <summary>
        /// Obtiene el color cyan en su tonalidad alternativa 2.
        /// </summary>
        public static Color Cyan2 => new(0, 238, 238);

        /// <summary>
        /// Obtiene el color cyan en su tonalidad alternativa 3.
        /// </summary>
        public static Color Cyan3 => new(0, 205, 205);

        /// <summary>
        /// Obtiene el color cyan.
        /// </summary>
        public static Color Cyan => new(0, 255, 255);

        /// <summary>
        /// Obtiene el color azul oscuro.
        /// </summary>
        public static Color DarkBlue => new(0, 0, 139);

        /// <summary>
        /// Obtiene el color cyan oscuro.
        /// </summary>
        public static Color DarkCyan => new(0, 139, 139);

        /// <summary>
        /// Obtiene el color vara de oro oscuro en su tonalidad alternativa 1.
        /// </summary>
        public static Color DarkGoldenRod1 => new(255, 185, 15);

        /// <summary>
        /// Obtiene el color vara de oro oscuro en su tonalidad alternativa 2.
        /// </summary>
        public static Color DarkGoldenRod2 => new(238, 173, 14);

        /// <summary>
        /// Obtiene el color vara de oro oscuro en su tonalidad alternativa 3.
        /// </summary>
        public static Color DarkGoldenRod3 => new(205, 149, 12);

        /// <summary>
        /// Obtiene el color vara de oro oscuro en su tonalidad alternativa 4.
        /// </summary>
        public static Color DarkGoldenRod4 => new(139, 101, 8);

        /// <summary>
        /// Obtiene el color vara de oro oscuro.
        /// </summary>
        public static Color DarkGoldenRod => new(184, 134, 11);

        /// <summary>
        /// Obtiene el color girs oscuro.
        /// </summary>
        public static Color DarkGray => new(169, 169, 169);

        /// <summary>
        /// Obtiene el color verde oscuro.
        /// </summary>
        public static Color DarkGreen => new(0, 100, 0);

        /// <summary>
        /// Obtiene el color khaki oscuro.
        /// </summary>
        public static Color DarkKhaki => new(189, 183, 107);

        /// <summary>
        /// Obtiene el color magenta oscuro.
        /// </summary>
        public static Color DarkMagenta => new(139, 0, 139);

        /// <summary>
        /// Obtiene el color verde olivo oscuro en su tonalidad alternativa 1.
        /// </summary>
        public static Color DarkOliveGreen1 => new(202, 255, 112);

        /// <summary>
        /// Obtiene el color verde olivo oscuro en su tonalidad alternativa 2.
        /// </summary>
        public static Color DarkOliveGreen2 => new(188, 238, 104);

        /// <summary>
        /// Obtiene el color verde olivo oscuro en su tonalidad alternativa 3.
        /// </summary>
        public static Color DarkOliveGreen3 => new(162, 205, 90);

        /// <summary>
        /// Obtiene el color verde olivo oscuro en su tonalidad alternativa 4.
        /// </summary>
        public static Color DarkOliveGreen4 => new(110, 139, 61);

        /// <summary>
        /// Obtiene el color verde olivo oscuro.
        /// </summary>
        public static Color DarkOliveGreen => new(85, 107, 47);

        /// <summary>
        /// Obtiene el color naranja oscuro en su tonalidad alternativa 1.
        /// </summary>
        public static Color DarkOrange1 => new(255, 127, 0);

        /// <summary>
        /// Obtiene el color naranja oscuro en su tonalidad alternativa 2.
        /// </summary>
        public static Color DarkOrange2 => new(238, 118, 0);

        /// <summary>
        /// Obtiene el color naranja oscuro en su tonalidad alternativa 3.
        /// </summary>
        public static Color DarkOrange3 => new(205, 102, 0);

        /// <summary>
        /// Obtiene el color naranja oscuro en su tonalidad alternativa 4.
        /// </summary>
        public static Color DarkOrange4 => new(139, 69, 0);

        /// <summary>
        /// Obtiene el color naranja oscuro.
        /// </summary>
        public static Color DarkOrange => new(255, 140, 0);

        /// <summary>
        /// Obtiene el color orquídea oscuro en su tonalidad alternativa 1.
        /// </summary>
        public static Color DarkOrchid1 => new(191, 62, 255);

        /// <summary>
        /// Obtiene el color orquídea oscuro en su tonalidad alternativa 2.
        /// </summary>
        public static Color DarkOrchid2 => new(178, 58, 238);

        /// <summary>
        /// Obtiene el color orquídea oscuro en su tonalidad alternativa 3.
        /// </summary>
        public static Color DarkOrchid3 => new(154, 50, 205);

        /// <summary>
        /// Obtiene el color orquídea oscuro en su tonalidad alternativa 4.
        /// </summary>
        public static Color DarkOrchid4 => new(104, 34, 139);

        /// <summary>
        /// Obtiene el color orquídea oscuro.
        /// </summary>
        public static Color DarkOrchid => new(153, 50, 204);

        /// <summary>
        /// Obtiene el color rojo oscuro.
        /// </summary>
        public static Color DarkRed => new(139, 0, 0);

        /// <summary>
        /// Obtiene el color salmón oscuro.
        /// </summary>
        public static Color DarkSalmon => new(233, 150, 122);

        /// <summary>
        /// Obtiene el color verde marino oscuro en su tonalidad alternativa 1.
        /// </summary>
        public static Color DarkseaGreen1 => new(193, 255, 193);

        /// <summary>
        /// Obtiene el color verde marino oscuro en su tonalidad alternativa 2.
        /// </summary>
        public static Color DarkseaGreen2 => new(180, 238, 180);

        /// <summary>
        /// Obtiene el color verde marino oscuro en su tonalidad alternativa 3.
        /// </summary>
        public static Color DarkseaGreen3 => new(155, 205, 155);

        /// <summary>
        /// Obtiene el color verde marino oscuro en su tonalidad alternativa 4.
        /// </summary>
        public static Color DarkseaGreen4 => new(105, 139, 105);

        /// <summary>
        /// Obtiene el color verde marino oscuro.
        /// </summary>
        public static Color DarkSeaGreen => new(143, 188, 143);

        /// <summary>
        /// Obtiene el color azul pizarra oscuro.
        /// </summary>
        public static Color DarkSlateBlue => new(72, 61, 139);

        /// <summary>
        /// Obtiene el color gris pizarra oscuro en su tonalidad alternativa 1.
        /// </summary>
        public static Color DarkSlateGray1 => new(151, 255, 255);

        /// <summary>
        /// Obtiene el color gris pizarra oscuro en su tonalidad alternativa 2.
        /// </summary>
        public static Color DarkSlateGray2 => new(141, 238, 238);

        /// <summary>
        /// Obtiene el color gris pizarra oscuro en su tonalidad alternativa 3.
        /// </summary>
        public static Color DarkSlateGray3 => new(121, 205, 205);

        /// <summary>
        /// Obtiene el color gris pizarra oscuro en su tonalidad alternativa 4.
        /// </summary>
        public static Color DarkSlateGray4 => new(82, 139, 139);

        /// <summary>
        /// Obtiene el color gris pizarra oscuro.
        /// </summary>
        public static Color DarkSlateGray => new(47, 79, 79);

        /// <summary>
        /// Obtiene el color turqueza oscuro.
        /// </summary>
        public static Color DarkTurquoise => new(0, 206, 209);

        /// <summary>
        /// Obtiene el color violeta oscuro.
        /// </summary>
        public static Color DarkViolet => new(148, 0, 211);

        /// <summary>
        /// Obtiene el color rosa profundo en su tonalidad alternativa 2.
        /// </summary>
        public static Color DeepPink2 => new(238, 18, 137);

        /// <summary>
        /// Obtiene el color rosa profundo en su tonalidad alternativa 3.
        /// </summary>
        public static Color DeepPink3 => new(205, 16, 118);

        /// <summary>
        /// Obtiene el color rosa profundo en su tonalidad alternativa 4.
        /// </summary>
        public static Color DeepPink4 => new(139, 10, 80);

        /// <summary>
        /// Obtiene el color rosa profundo.
        /// </summary>
        public static Color DeepPink => new(255, 20, 147);

        /// <summary>
        /// Obtiene el color azul cielo profundo en su tonalidad alternativa 2.
        /// </summary>
        public static Color DeepSkyBlue2 => new(0, 178, 238);

        /// <summary>
        /// Obtiene el color azul cielo profundo en su tonalidad alternativa 3.
        /// </summary>
        public static Color DeepSkyBlue3 => new(0, 154, 205);

        /// <summary>
        /// Obtiene el color azul cielo profundo en su tonalidad alternativa 4.
        /// </summary>
        public static Color DeepSkyBlue4 => new(0, 104, 139);

        /// <summary>
        /// Obtiene el color azul cielo profundo.
        /// </summary>
        public static Color DeepSkyBlue => new(0, 191, 255);

        /// <summary>
        /// Obtiene el color gris tenue.
        /// </summary>
        public static Color DimGray => new(105, 105, 105);

        /// <summary>
        /// Obtiene el color azul gandul en su tonalidad alternativa 2.
        /// </summary>
        public static Color DodgerBlue2 => new(28, 134, 238);

        /// <summary>
        /// Obtiene el color azul gandul en su tonalidad alternativa 3.
        /// </summary>
        public static Color DodgerBlue3 => new(24, 116, 205);

        /// <summary>
        /// Obtiene el color azul gandul en su tonalidad alternativa 4.
        /// </summary>
        public static Color DodgerBlue4 => new(16, 78, 139);

        /// <summary>
        /// Obtiene el color azul gandul.
        /// </summary>
        public static Color DodgerBlue => new(30, 144, 255);

        /// <summary>
        /// Obtiene el color cáscara de huevo.
        /// </summary>
        public static Color EggShell => new(252, 230, 201);

        /// <summary>
        /// Obtiene el color verde esmeralda.
        /// </summary>
        public static Color EmeraldGreen => new(0, 201, 87);

        /// <summary>
        /// Obtiene el color rojo ladrillo en su tonalidad alternativa 4.
        /// </summary>
        public static Color FireBrick1 => new(255, 48, 48);

        /// <summary>
        /// Obtiene el color rojo ladrillo en su tonalidad alternativa 2.
        /// </summary>
        public static Color FireBrick2 => new(238, 44, 44);

        /// <summary>
        /// Obtiene el color rojo ladrillo en su tonalidad alternativa 3.
        /// </summary>
        public static Color FireBrick3 => new(205, 38, 38);

        /// <summary>
        /// Obtiene el color rojo ladrillo en su tonalidad alternativa 4.
        /// </summary>
        public static Color FireBrick4 => new(139, 26, 26);

        /// <summary>
        /// Obtiene el color rojo ladrillo.
        /// </summary>
        public static Color FireBrick => new(178, 34, 34);

        /// <summary>
        /// Obtiene el color carne.
        /// </summary>
        public static Color Flesh => new(255, 125, 64);

        /// <summary>
        /// Obtiene el color blanco floral.
        /// </summary>
        public static Color FloralWhite => new(255, 250, 240);

        /// <summary>
        /// Obtiene el color verde bosque.
        /// </summary>
        public static Color ForestGreen => new(34, 139, 34);

        /// <summary>
        /// Obtiene el color gris gainsboro.
        /// </summary>
        public static Color Gainsboro => new(220, 220, 220);

        /// <summary>
        /// Obtiene el color blanco fantasma.
        /// </summary>
        public static Color GhostWhite => new(248, 248, 255);

        /// <summary>
        /// Obtiene el color oro en su tonalidad alternativa 2.
        /// </summary>
        public static Color Gold2 => new(238, 201, 0);

        /// <summary>
        /// Obtiene el color oro en su tonalidad alternativa 3.
        /// </summary>
        public static Color Gold3 => new(205, 173, 0);

        /// <summary>
        /// Obtiene el color oro en su tonalidad alternativa 4.
        /// </summary>
        public static Color Gold4 => new(139, 117, 0);

        /// <summary>
        /// Obtiene el color vara de oro en su tonalidad alternativa 1.
        /// </summary>
        public static Color GoldenRod1 => new(255, 193, 37);

        /// <summary>
        /// Obtiene el color vara de oro en su tonalidad alternativa 2.
        /// </summary>
        public static Color GoldenRod2 => new(238, 180, 34);

        /// <summary>
        /// Obtiene el color vara de oro en su tonalidad alternativa 3.
        /// </summary>
        public static Color GoldenRod3 => new(205, 155, 29);

        /// <summary>
        /// Obtiene el color vara de oro en su tonalidad alternativa 4.
        /// </summary>
        public static Color GoldenRod4 => new(139, 105, 20);

        /// <summary>
        /// Obtiene el color vara de oro.
        /// </summary>
        public static Color GoldenRod => new(218, 165, 32);

        /// <summary>
        /// Obtiene el color oro.
        /// </summary>
        public static Color Gold => new(255, 215, 0);

        /// <summary>
        /// Obtiene el color gris.
        /// </summary>
        public static Color Gray => new(127, 127, 127);

        /// <summary>
        /// Obtiene el color gris real.
        /// </summary>
        public static Color TrueGray => new(0.5f, 0.5f, 0.5f);

        /// <summary>
        /// Obtiene el color verde en su tonalidad alternativa 2.
        /// </summary>
        public static Color Green2 => new(0, 238, 0);

        /// <summary>
        /// Obtiene el color verde en su tonalidad alternativa 3.
        /// </summary>
        public static Color Green3 => new(0, 205, 0);

        /// <summary>
        /// Obtiene el color verde en su tonalidad alternativa 4.
        /// </summary>
        public static Color Green4 => new(0, 139, 0);

        /// <summary>
        /// Obtiene el color verde.
        /// </summary>
        public static Color Green => new(0, 128, 0);

        /// <summary>
        /// Obtiene el color amarillo verdoso.
        /// </summary>
        public static Color GreenYellow => new(173, 255, 47);

        /// <summary>
        /// Obtiene el color gota de miel en su tonalidad alternativa 2.
        /// </summary>
        public static Color Honeydew2 => new(224, 238, 224);

        /// <summary>
        /// Obtiene el color gota de miel en su tonalidad alternativa 3.
        /// </summary>
        public static Color Honeydew3 => new(193, 205, 193);

        /// <summary>
        /// Obtiene el color gota de miel en su tonalidad alternativa 4.
        /// </summary>
        public static Color Honeydew4 => new(131, 139, 131);

        /// <summary>
        /// Obtiene el color gota de miel.
        /// </summary>
        public static Color Honeydew => new(240, 255, 240);

        /// <summary>
        /// Obtiene el color rosa intenso en su tonalidad alternativa 1.
        /// </summary>
        public static Color HotPink1 => new(255, 110, 180);

        /// <summary>
        /// Obtiene el color rosa intenso en su tonalidad alternativa 2.
        /// </summary>
        public static Color HotPink2 => new(238, 106, 167);

        /// <summary>
        /// Obtiene el color rosa intenso en su tonalidad alternativa 3.
        /// </summary>
        public static Color HotPink3 => new(205, 96, 144);

        /// <summary>
        /// Obtiene el color rosa intenso en su tonalidad alternativa 4.
        /// </summary>
        public static Color HotPink4 => new(139, 58, 98);

        /// <summary>
        /// Obtiene el color rosa intenso.
        /// </summary>
        public static Color HotPink => new(255, 105, 180);

        /// <summary>
        /// Obtiene el color blanco hielo.
        /// </summary>
        public static Color IceWhite => new(0xed, 0xfa, 0xff);

        /// <summary>
        /// Obtiene el color rojo indio en su tonalidad alternativa 1.
        /// </summary>
        public static Color IndianRed1 => new(255, 106, 106);

        /// <summary>
        /// Obtiene el color rojo indio en su tonalidad alternativa 2.
        /// </summary>
        public static Color IndianRed2 => new(238, 99, 99);

        /// <summary>
        /// Obtiene el color rojo indio en su tonalidad alternativa 3.
        /// </summary>
        public static Color IndianRed3 => new(205, 85, 85);

        /// <summary>
        /// Obtiene el color rojo indio en su tonalidad alternativa 4.
        /// </summary>
        public static Color IndianRed4 => new(139, 58, 58);

        /// <summary>
        /// Obtiene el color rojo indio.
        /// </summary>
        public static Color IndianRed => new(176, 23, 31);

        /// <summary>
        /// Obtiene el color índigo.
        /// </summary>
        public static Color Indigo => new(75, 0, 130);

        /// <summary>
        /// Obtiene el color óxido de hierro vidrioso.
        /// </summary>
        public static Color IronOxideGlass => new(0x21, 0x9c, 0x6a);

        /// <summary>
        /// Obtiene el color marfil en su tonalidad alternativa 2.
        /// </summary>
        public static Color Ivory2 => new(238, 238, 224);

        /// <summary>
        /// Obtiene el color marfil en su tonalidad alternativa 3.
        /// </summary>
        public static Color Ivory3 => new(205, 205, 193);

        /// <summary>
        /// Obtiene el color marfil en su tonalidad alternativa 4.
        /// </summary>
        public static Color Ivory4 => new(139, 139, 131);

        /// <summary>
        /// Obtiene el color negro marfil.
        /// </summary>
        public static Color IvoryBlack => new(41, 36, 33);

        /// <summary>
        /// Obtiene el color marfil.
        /// </summary>
        public static Color Ivory => new(255, 255, 240);

        /// <summary>
        /// Obtiene el color khaki en su tonalidad alternativa 1.
        /// </summary>
        public static Color Khaki1 => new(255, 246, 143);

        /// <summary>
        /// Obtiene el color khaki en su tonalidad alternativa 2.
        /// </summary>
        public static Color Khaki2 => new(238, 230, 133);

        /// <summary>
        /// Obtiene el color khaki en su tonalidad alternativa 3.
        /// </summary>
        public static Color Khaki3 => new(205, 198, 115);

        /// <summary>
        /// Obtiene el color khaki en su tonalidad alternativa 4.
        /// </summary>
        public static Color Khaki4 => new(139, 134, 78);

        /// <summary>
        /// Obtiene el color khaki.
        /// </summary>
        public static Color Khaki => new(240, 230, 140);

        /// <summary>
        /// Obtiene el color rubor lavanda en su tonalidad alternativa 2.
        /// </summary>
        public static Color LavenderBlush2 => new(238, 224, 229);

        /// <summary>
        /// Obtiene el color rubor lavanda en su tonalidad alternativa 3.
        /// </summary>
        public static Color LavenderBlush3 => new(205, 193, 197);

        /// <summary>
        /// Obtiene el color rubor lavanda en su tonalidad alternativa 4.
        /// </summary>
        public static Color LavenderBlush4 => new(139, 131, 134);

        /// <summary>
        /// Obtiene el color rubor lavanda.
        /// </summary>
        public static Color LavenderBlush => new(255, 240, 245);

        /// <summary>
        /// Obtiene el color lavanda.
        /// </summary>
        public static Color Lavender => new(230, 230, 250);

        /// <summary>
        /// Obtiene el color verde pasto.
        /// </summary>
        public static Color LawnGreen => new(124, 252, 0);

        /// <summary>
        /// Obtiene el color gasa de limón en su tonalidad alternativa 2.
        /// </summary>
        public static Color Lemonchiffon2 => new(238, 233, 191);

        /// <summary>
        /// Obtiene el color gasa de limón en su tonalidad alternativa 3.
        /// </summary>
        public static Color Lemonchiffon3 => new(205, 201, 165);

        /// <summary>
        /// Obtiene el color gasa de limón en su tonalidad alternativa 4.
        /// </summary>
        public static Color Lemonchiffon4 => new(139, 137, 112);

        /// <summary>
        /// Obtiene el color gasa de limón.
        /// </summary>
        public static Color Lemonchiffon => new(255, 250, 205);

        /// <summary>
        /// Obtiene el color azul claro en su tonalidad alternativa 1.
        /// </summary>
        public static Color LightBlue1 => new(191, 239, 255);

        /// <summary>
        /// Obtiene el color azul claro en su tonalidad alternativa 2.
        /// </summary>
        public static Color LightBlue2 => new(178, 223, 238);

        /// <summary>
        /// Obtiene el color azul claro en su tonalidad alternativa 3.
        /// </summary>
        public static Color LightBlue3 => new(154, 192, 205);

        /// <summary>
        /// Obtiene el color azul claro en su tonalidad alternativa 4.
        /// </summary>
        public static Color LightBlue4 => new(104, 131, 139);

        /// <summary>
        /// Obtiene el color azul claro.
        /// </summary>
        public static Color LightBlue => new(173, 216, 230);

        /// <summary>
        /// Obtiene el color coral claro.
        /// </summary>
        public static Color LightCoral => new(240, 128, 128);

        /// <summary>
        /// Obtiene el color cyan claro en su tonalidad alternativa 2.
        /// </summary>
        public static Color LightCyan2 => new(209, 238, 238);

        /// <summary>
        /// Obtiene el color cyan claro en su tonalidad alternativa 3.
        /// </summary>
        public static Color LightCyan3 => new(180, 205, 205);

        /// <summary>
        /// Obtiene el color cyan claro en su tonalidad alternativa 4.
        /// </summary>
        public static Color LightCyan4 => new(122, 139, 139);

        /// <summary>
        /// Obtiene el color cyan claro.
        /// </summary>
        public static Color LightCyan => new(224, 255, 255);

        /// <summary>
        /// Obtiene el color vara de oro claro en su tonalidad alternativa 1.
        /// </summary>
        public static Color LightGoldenRod1 => new(255, 236, 139);

        /// <summary>
        /// Obtiene el color vara de oro claro en su tonalidad alternativa 2.
        /// </summary>
        public static Color LightGoldenRod2 => new(238, 220, 130);

        /// <summary>
        /// Obtiene el color vara de oro claro en su tonalidad alternativa 3.
        /// </summary>
        public static Color LightGoldenRod3 => new(205, 190, 112);

        /// <summary>
        /// Obtiene el color vara de oro claro en su tonalidad alternativa 4.
        /// </summary>
        public static Color LightGoldenRod4 => new(139, 129, 76);

        /// <summary>
        /// Obtiene el color amarillo vara de oro claro.
        /// </summary>
        public static Color LightGoldenRodYellow => new(250, 250, 210);

        /// <summary>
        /// Obtiene el color verde claro.
        /// </summary>
        public static Color LightGreen => new(144, 238, 144);

        /// <summary>
        /// Obtiene el color gris claro.
        /// </summary>
        public static Color LightGrey => new(211, 211, 211);

        /// <summary>
        /// Obtiene el color rosa claro en su tonalidad alternativa 1.
        /// </summary>
        public static Color LightPink1 => new(255, 174, 185);

        /// <summary>
        /// Obtiene el color rosa claro en su tonalidad alternativa 2.
        /// </summary>
        public static Color LightPink2 => new(238, 162, 173);

        /// <summary>
        /// Obtiene el color rosa claro en su tonalidad alternativa 3.
        /// </summary>
        public static Color LightPink3 => new(205, 140, 149);

        /// <summary>
        /// Obtiene el color rosa claro en su tonalidad alternativa 4.
        /// </summary>
        public static Color LightPink4 => new(139, 95, 101);

        /// <summary>
        /// Obtiene el color rosa claro.
        /// </summary>
        public static Color LightPink => new(255, 182, 193);

        /// <summary>
        /// Obtiene el color salmón claro en su tonalidad alternativa 2.
        /// </summary>
        public static Color LightSalmon2 => new(238, 149, 114);

        /// <summary>
        /// Obtiene el color salmón claro en su tonalidad alternativa 3.
        /// </summary>
        public static Color LightSalmon3 => new(205, 129, 98);

        /// <summary>
        /// Obtiene el color salmón claro en su tonalidad alternativa 4.
        /// </summary>
        public static Color LightSalmon4 => new(139, 87, 66);

        /// <summary>
        /// Obtiene el color salmón claro.
        /// </summary>
        public static Color LightSalmon => new(255, 160, 122);

        /// <summary>
        /// Obtiene el color verde marino claro.
        /// </summary>
        public static Color LightSeaGreen => new(32, 178, 170);

        /// <summary>
        /// Obtiene el color azul cielo claro en su tonalidad alternativa 1.
        /// </summary>
        public static Color LightSkyBlue1 => new(176, 226, 255);

        /// <summary>
        /// Obtiene el color azul cielo claro en su tonalidad alternativa 2.
        /// </summary>
        public static Color LightSkyBlue2 => new(164, 211, 238);

        /// <summary>
        /// Obtiene el color azul cielo claro en su tonalidad alternativa 3.
        /// </summary>
        public static Color LightSkyBlue3 => new(141, 182, 205);

        /// <summary>
        /// Obtiene el color azul cielo claro en su tonalidad alternativa 4.
        /// </summary>
        public static Color LightSkyBlue4 => new(96, 123, 139);

        /// <summary>
        /// Obtiene el color azul cielo claro.
        /// </summary>
        public static Color LightSkyBlue => new(135, 206, 250);

        /// <summary>
        /// Obtiene el color azul pizarra claro.
        /// </summary>
        public static Color LightSlateBlue => new(132, 112, 255);

        /// <summary>
        /// Obtiene el color gris pizarra claro.
        /// </summary>
        public static Color LightSlateGray => new(119, 136, 153);

        /// <summary>
        /// Obtiene el color azul acero claro en su tonalidad alternativa 1.
        /// </summary>
        public static Color LightSteelBlue1 => new(202, 225, 255);

        /// <summary>
        /// Obtiene el color azul acero claro en su tonalidad alternativa 2.
        /// </summary>
        public static Color LightSteelBlue2 => new(188, 210, 238);

        /// <summary>
        /// Obtiene el color azul acero claro en su tonalidad alternativa 3.
        /// </summary>
        public static Color LightSteelBlue3 => new(162, 181, 205);

        /// <summary>
        /// Obtiene el color azul acero claro en su tonalidad alternativa 4.
        /// </summary>
        public static Color LightSteelBlue4 => new(110, 123, 139);

        /// <summary>
        /// Obtiene el color azul acero claro.
        /// </summary>
        public static Color LightSteelBlue => new(176, 196, 222);

        /// <summary>
        /// Obtiene el color amarillo claro en su tonalidad alternativa 2.
        /// </summary>
        public static Color LightYellow2 => new(238, 238, 209);

        /// <summary>
        /// Obtiene el color amarillo claro en su tonalidad alternativa 3.
        /// </summary>
        public static Color LightYellow3 => new(205, 205, 180);

        /// <summary>
        /// Obtiene el color amarillo claro en su tonalidad alternativa 4.
        /// </summary>
        public static Color LightYellow4 => new(139, 139, 122);

        /// <summary>
        /// Obtiene el color amarillo claro.
        /// </summary>
        public static Color LightYellow => new(255, 255, 224);

        /// <summary>
        /// Obtiene el color verde lima.
        /// </summary>
        public static Color LimeGreen => new(50, 205, 50);

        /// <summary>
        /// Obtiene el color lima.
        /// </summary>
        public static Color Lime => new(0, 255, 0);

        /// <summary>
        /// Obtiene el color lino.
        /// </summary>
        public static Color Linen => new(250, 240, 230);

        /// <summary>
        /// Obtiene el color magenta en su tonalidad alternativa 2.
        /// </summary>
        public static Color Magenta2 => new(238, 0, 238);

        /// <summary>
        /// Obtiene el color magenta en su tonalidad alternativa 3.
        /// </summary>
        public static Color Magenta3 => new(205, 0, 205);

        /// <summary>
        /// Obtiene el color magenta.
        /// </summary>
        public static Color Magenta => new(255, 0, 255);

        /// <summary>
        /// Obtiene el color púrpura mágico.
        /// </summary>
        public static Color MagicPurple => new(0x5d, 0x00, 0xff);

        /// <summary>
        /// Obtiene el color azul manganeso.
        /// </summary>
        public static Color ManganeseBlue => new(3, 168, 158);

        /// <summary>
        /// Obtiene el color marrón en su tonalidad alternativa 1.
        /// </summary>
        public static Color Maroon1 => new(255, 52, 179);

        /// <summary>
        /// Obtiene el color marrón en su tonalidad alternativa 2.
        /// </summary>
        public static Color Maroon2 => new(238, 48, 167);

        /// <summary>
        /// Obtiene el color marrón en su tonalidad alternativa 3.
        /// </summary>
        public static Color Maroon3 => new(205, 41, 144);

        /// <summary>
        /// Obtiene el color marrón en su tonalidad alternativa 4.
        /// </summary>
        public static Color Maroon4 => new(139, 28, 98);

        /// <summary>
        /// Obtiene el color marrón.
        /// </summary>
        public static Color Maroon => new(128, 0, 0);

        /// <summary>
        /// Obtiene el color aguamarina medio.
        /// </summary>
        public static Color MediumAquamarine => new(102, 205, 170);

        /// <summary>
        /// Obtiene el color azul medio.
        /// </summary>
        public static Color MediumBlue => new(0, 0, 205);

        /// <summary>
        /// Obtiene el color orquídea medio en su tonalidad alternativa 1.
        /// </summary>
        public static Color MediumOrchid1 => new(224, 102, 255);

        /// <summary>
        /// Obtiene el color orquídea medio en su tonalidad alternativa 2.
        /// </summary>
        public static Color MediumOrchid2 => new(209, 95, 238);

        /// <summary>
        /// Obtiene el color orquídea medio en su tonalidad alternativa 3.
        /// </summary>
        public static Color MediumOrchid3 => new(180, 82, 205);

        /// <summary>
        /// Obtiene el color orquídea medio en su tonalidad alternativa 4.
        /// </summary>
        public static Color MediumOrchid4 => new(122, 55, 139);

        /// <summary>
        /// Obtiene el color orquídea medio.
        /// </summary>
        public static Color MediumOrchid => new(186, 85, 211);

        /// <summary>
        /// Obtiene el color púrpura medio en su tonalidad alternativa 1.
        /// </summary>
        public static Color MediumPurple1 => new(171, 130, 255);

        /// <summary>
        /// Obtiene el color púrpura medio en su tonalidad alternativa 2.
        /// </summary>
        public static Color MediumPurple2 => new(159, 121, 238);

        /// <summary>
        /// Obtiene el color púrpura medio en su tonalidad alternativa 3.
        /// </summary>
        public static Color MediumPurple3 => new(137, 104, 205);

        /// <summary>
        /// Obtiene el color púrpura medio en su tonalidad alternativa 4.
        /// </summary>
        public static Color MediumPurple4 => new(93, 71, 139);

        /// <summary>
        /// Obtiene el color púrpura medio.
        /// </summary>
        public static Color MediumPurple => new(147, 112, 219);

        /// <summary>
        /// Obtiene el color verde marino medio.
        /// </summary>
        public static Color MediumSeaGreen => new(60, 179, 113);

        /// <summary>
        /// Obtiene el color azul pizarra medio.
        /// </summary>
        public static Color MediumSlateBlue => new(123, 104, 238);

        /// <summary>
        /// Obtiene el color verde primavera medio.
        /// </summary>
        public static Color MediumSpringGreen => new(0, 250, 154);

        /// <summary>
        /// Obtiene el color turqueza medio.
        /// </summary>
        public static Color MediumTurquoise => new(72, 209, 204);

        /// <summary>
        /// Obtiene el color rojo violeta medio.
        /// </summary>
        public static Color MediumVioletRed => new(199, 21, 133);

        /// <summary>
        /// Obtiene el color melón.
        /// </summary>
        public static Color Melon => new(227, 168, 105);

        /// <summary>
        /// Obtiene el color azul medianoche.
        /// </summary>
        public static Color MidnightBlue => new(25, 25, 112);

        /// <summary>
        /// Obtiene el color menta crema.
        /// </summary>
        public static Color MintCream => new(245, 255, 250);

        /// <summary>
        /// Obtiene el color menta.
        /// </summary>
        public static Color Mint => new(189, 252, 201);

        /// <summary>
        /// Obtiene el color rosa brumosa en su tonalidad alternativa 2.
        /// </summary>
        public static Color MistyRose2 => new(238, 213, 210);

        /// <summary>
        /// Obtiene el color rosa brumosa en su tonalidad alternativa 3.
        /// </summary>
        public static Color MistyRose3 => new(205, 183, 181);

        /// <summary>
        /// Obtiene el color rosa brumosa en su tonalidad alternativa 4.
        /// </summary>
        public static Color MistyRose4 => new(139, 125, 123);

        /// <summary>
        /// Obtiene el color rosa brumosa.
        /// </summary>
        public static Color MistyRose => new(255, 228, 225);

        /// <summary>
        /// Obtiene el color mocasín.
        /// </summary>
        public static Color Moccasin => new(255, 228, 181);

        /// <summary>
        /// Obtiene el color blanco navajo en su tonalidad alternativa 2.
        /// </summary>
        public static Color NavajoWhite2 => new(238, 207, 161);

        /// <summary>
        /// Obtiene el color blanco navajo en su tonalidad alternativa 3.
        /// </summary>
        public static Color NavajoWhite3 => new(205, 179, 139);

        /// <summary>
        /// Obtiene el color blanco navajo en su tonalidad alternativa 4.
        /// </summary>
        public static Color NavajoWhite4 => new(139, 121, 94);

        /// <summary>
        /// Obtiene el color blanco navajo.
        /// </summary>
        public static Color NavajoWhite => new(255, 222, 173);

        /// <summary>
        /// Obtiene el color azul marino.
        /// </summary>
        public static Color Navy => new(0, 0, 128);

        /// <summary>
        /// Obtiene el color encaje antíguo.
        /// </summary>
        public static Color OldLace => new(253, 245, 230);

        /// <summary>
        /// Obtiene el color verde oliva en su tonalidad alternativa 1.
        /// </summary>
        public static Color OliveDrab1 => new(192, 255, 62);

        /// <summary>
        /// Obtiene el color verde oliva en su tonalidad alternativa 2.
        /// </summary>
        public static Color OliveDrab2 => new(179, 238, 58);

        /// <summary>
        /// Obtiene el color verde oliva en su tonalidad alternativa 3.
        /// </summary>
        public static Color OliveDrab3 => new(154, 205, 50);

        /// <summary>
        /// Obtiene el color verde oliva en su tonalidad alternativa 4.
        /// </summary>
        public static Color OliveDrab4 => new(105, 139, 34);

        /// <summary>
        /// Obtiene el color verde oliva.
        /// </summary>
        public static Color OliveDrab => new(107, 142, 35);

        /// <summary>
        /// Obtiene el color olivo.
        /// </summary>
        public static Color Olive => new(128, 128, 0);

        /// <summary>
        /// Obtiene el color naranja en su tonalidad alternativa 2.
        /// </summary>
        public static Color Orange2 => new(238, 154, 0);

        /// <summary>
        /// Obtiene el color naranja en su tonalidad alternativa 3.
        /// </summary>
        public static Color Orange3 => new(205, 133, 0);

        /// <summary>
        /// Obtiene el color naranja en su tonalidad alternativa 4.
        /// </summary>
        public static Color Orange4 => new(139, 90, 0);

        /// <summary>
        /// Obtiene el color naranja.
        /// </summary>
        public static Color Orange => new(255, 165, 0);

        /// <summary>
        /// Obtiene el color naranja rojizo en su tonalidad alternativa 2.
        /// </summary>
        public static Color OrangeRed2 => new(238, 64, 0);

        /// <summary>
        /// Obtiene el color naranja rojizo en su tonalidad alternativa 3.
        /// </summary>
        public static Color OrangeRed3 => new(205, 55, 0);

        /// <summary>
        /// Obtiene el color naranja rojizo en su tonalidad alternativa 4.
        /// </summary>
        public static Color OrangeRed4 => new(139, 37, 0);

        /// <summary>
        /// Obtiene el color naranja rojizo.
        /// </summary>
        public static Color OrangeRed => new(255, 69, 0);

        /// <summary>
        /// Obtiene el color orquídea en su tonalidad alternativa 1.
        /// </summary>
        public static Color Orchid1 => new(255, 131, 250);

        /// <summary>
        /// Obtiene el color orquídea en su tonalidad alternativa 2.
        /// </summary>
        public static Color Orchid2 => new(238, 122, 233);

        /// <summary>
        /// Obtiene el color orquídea en su tonalidad alternativa 3.
        /// </summary>
        public static Color Orchid3 => new(205, 105, 201);

        /// <summary>
        /// Obtiene el color orquídea en su tonalidad alternativa 4.
        /// </summary>
        public static Color Orchid4 => new(139, 71, 137);

        /// <summary>
        /// Obtiene el color orquídea.
        /// </summary>
        public static Color Orchid => new(218, 112, 214);

        /// <summary>
        /// Obtiene el color vara de oro pálido.
        /// </summary>
        public static Color PaleGoldenRod => new(238, 232, 170);

        /// <summary>
        /// Obtiene el color verde pálido en su tonalidad alternativa 2.
        /// </summary>
        public static Color PaleGreen2 => new(154, 255, 154);

        /// <summary>
        /// Obtiene el color verde pálido en su tonalidad alternativa 3.
        /// </summary>
        public static Color PaleGreen3 => new(124, 205, 124);

        /// <summary>
        /// Obtiene el color verde pálido en su tonalidad alternativa 4.
        /// </summary>
        public static Color PaleGreen4 => new(84, 139, 84);

        /// <summary>
        /// Obtiene el color verde pálido.
        /// </summary>
        public static Color PaleGreen => new(152, 251, 152);

        /// <summary>
        /// Obtiene el color turqueza pálido en su tonalidad alternativa 2.
        /// </summary>
        public static Color PaleTurquoise2 => new(187, 255, 255);

        /// <summary>
        /// Obtiene el color turqueza pálido en su tonalidad alternativa 3.
        /// </summary>
        public static Color PaleTurquoise3 => new(150, 205, 205);

        /// <summary>
        /// Obtiene el color turqueza pálido en su tonalidad alternativa 4.
        /// </summary>
        public static Color PaleTurquoise4 => new(102, 139, 139);

        /// <summary>
        /// Obtiene el color turqueza pálido.
        /// </summary>
        public static Color PaleTurquoise => new(174, 238, 238);

        /// <summary>
        /// Obtiene el color rojo violeta pálido en su tonalidad alternativa 1.
        /// </summary>
        public static Color PaleVioletRed1 => new(255, 130, 171);

        /// <summary>
        /// Obtiene el color rojo violeta pálido en su tonalidad alternativa 2.
        /// </summary>
        public static Color PaleVioletRed2 => new(238, 121, 159);

        /// <summary>
        /// Obtiene el color rojo violeta pálido en su tonalidad alternativa 3.
        /// </summary>
        public static Color PaleVioletRed3 => new(205, 104, 137);

        /// <summary>
        /// Obtiene el color rojo violeta pálido en su tonalidad alternativa 4.
        /// </summary>
        public static Color PaleVioletRed4 => new(139, 71, 93);

        /// <summary>
        /// Obtiene el color rojo violeta pálido.
        /// </summary>
        public static Color PaleVioletRed => new(219, 112, 147);

        /// <summary>
        /// Obtiene el color papaya.
        /// </summary>
        public static Color PapayaWhip => new(255, 239, 213);

        /// <summary>
        /// Obtiene el color melocotón en su tonalidad alternativa 2.
        /// </summary>
        public static Color PeachPuff2 => new(238, 203, 173);

        /// <summary>
        /// Obtiene el color melocotón en su tonalidad alternativa 3.
        /// </summary>
        public static Color PeachPuff3 => new(205, 175, 149);

        /// <summary>
        /// Obtiene el color melocotón en su tonalidad alternativa 4.
        /// </summary>
        public static Color PeachPuff4 => new(139, 119, 101);

        /// <summary>
        /// Obtiene el color melocotón.
        /// </summary>
        public static Color PeachPuff => new(255, 218, 185);

        /// <summary>
        /// Obtiene el color pavoreal.
        /// </summary>
        public static Color Peacock => new(51, 161, 201);

        /// <summary>
        /// Obtiene el color perú.
        /// </summary>
        public static Color Peru => new(205, 133, 63);

        /// <summary>
        /// Obtiene el color rosa en su tonalidad alternativa 1.
        /// </summary>
        public static Color Pink1 => new(255, 181, 197);

        /// <summary>
        /// Obtiene el color rosa en su tonalidad alternativa 2.
        /// </summary>
        public static Color Pink2 => new(238, 169, 184);

        /// <summary>
        /// Obtiene el color rosa en su tonalidad alternativa 3.
        /// </summary>
        public static Color Pink3 => new(205, 145, 158);

        /// <summary>
        /// Obtiene el color rosa en su tonalidad alternativa 4.
        /// </summary>
        public static Color Pink4 => new(139, 99, 108);

        /// <summary>
        /// Obtiene el color rosa.
        /// </summary>
        public static Color Pink => new(255, 192, 203);

        /// <summary>
        /// Obtiene el color ciruela en su tonalidad alternativa 1.
        /// </summary>
        public static Color Plum1 => new(255, 187, 255);

        /// <summary>
        /// Obtiene el color ciruela en su tonalidad alternativa 2.
        /// </summary>
        public static Color Plum2 => new(238, 174, 238);

        /// <summary>
        /// Obtiene el color ciruela en su tonalidad alternativa 3.
        /// </summary>
        public static Color Plum3 => new(205, 150, 205);

        /// <summary>
        /// Obtiene el color ciruela en su tonalidad alternativa 4.
        /// </summary>
        public static Color Plum4 => new(139, 102, 139);

        /// <summary>
        /// Obtiene el color ciruela.
        /// </summary>
        public static Color Plum => new(221, 160, 221);

        /// <summary>
        /// Obtiene el color excremento.
        /// </summary>
        public static Color Poop => new(0x69, 0x46, 0x24);

        /// <summary>
        /// Obtiene el color azul polvo.
        /// </summary>
        public static Color PowderBlue => new(176, 224, 230);

        /// <summary>
        /// Obtiene el color púrpura en su tonalidad alternativa 1.
        /// </summary>
        public static Color Purple1 => new(155, 48, 255);

        /// <summary>
        /// Obtiene el color púrpura en su tonalidad alternativa 2.
        /// </summary>
        public static Color Purple2 => new(145, 44, 238);

        /// <summary>
        /// Obtiene el color púrpura en su tonalidad alternativa 3.
        /// </summary>
        public static Color Purple3 => new(125, 38, 205);

        /// <summary>
        /// Obtiene el color púrpura en su tonalidad alternativa 4.
        /// </summary>
        public static Color Purple4 => new(85, 26, 139);

        /// <summary>
        /// Obtiene el color púrpura.
        /// </summary>
        public static Color Purple => new(128, 0, 128);

        /// <summary>
        /// Obtiene el color amarillo de carreras.
        /// </summary>
        public static Color RacingYellow => new(0xff, 0xcc, 0x00);

        /// <summary>
        /// Obtiene el color frambuesa.
        /// </summary>
        public static Color Raspberry => new(135, 38, 87);

        /// <summary>
        /// Obtiene el color siena puro.
        /// </summary>
        public static Color RawSienna => new(199, 97, 20);

        /// <summary>
        /// Obtiene el color rojo en su tonalidad alternativa 2.
        /// </summary>
        public static Color Red2 => new(238, 0, 0);

        /// <summary>
        /// Obtiene el color rojo en su tonalidad alternativa 3.
        /// </summary>
        public static Color Red3 => new(205, 0, 0);

        /// <summary>
        /// Obtiene el color rojo.
        /// </summary>
        public static Color Red => new(255, 0, 0);

        /// <summary>
        /// Obtiene el color café rosado en su tonalidad alternativa 1.
        /// </summary>
        public static Color RosyBrown1 => new(255, 193, 193);

        /// <summary>
        /// Obtiene el color café rosado en su tonalidad alternativa 2.
        /// </summary>
        public static Color RosyBrown2 => new(238, 180, 180);

        /// <summary>
        /// Obtiene el color café rosado en su tonalidad alternativa 3.
        /// </summary>
        public static Color RosyBrown3 => new(205, 155, 155);

        /// <summary>
        /// Obtiene el color café rosado en su tonalidad alternativa 4.
        /// </summary>
        public static Color RosyBrown4 => new(139, 105, 105);

        /// <summary>
        /// Obtiene el color café rosado.
        /// </summary>
        public static Color RosyBrown => new(188, 143, 143);

        /// <summary>
        /// Obtiene el color azul royal en su tonalidad alternativa 1.
        /// </summary>
        public static Color RoyalBlue1 => new(72, 118, 255);

        /// <summary>
        /// Obtiene el color azul royal en su tonalidad alternativa 2.
        /// </summary>
        public static Color RoyalBlue2 => new(67, 110, 238);

        /// <summary>
        /// Obtiene el color azul royal en su tonalidad alternativa 3.
        /// </summary>
        public static Color RoyalBlue3 => new(58, 95, 205);

        /// <summary>
        /// Obtiene el color azul royal en su tonalidad alternativa 4.
        /// </summary>
        public static Color RoyalBlue4 => new(39, 64, 139);

        /// <summary>
        /// Obtiene el color azul royal.
        /// </summary>
        public static Color RoyalBlue => new(65, 105, 225);

        /// <summary>
        /// Obtiene el color montura marrón.
        /// </summary>
        public static Color SaddleBrown => new(139, 69, 19);

        /// <summary>
        /// Obtiene el color salmón en su tonalidad alternativa 2.
        /// </summary>
        public static Color Salmon2 => new(238, 130, 98);

        /// <summary>
        /// Obtiene el color salmón en su tonalidad alternativa 3.
        /// </summary>
        public static Color Salmon3 => new(205, 112, 84);

        /// <summary>
        /// Obtiene el color salmón en su tonalidad alternativa 4.
        /// </summary>
        public static Color Salmon4 => new(139, 76, 57);

        /// <summary>
        /// Obtiene el color salmón.
        /// </summary>
        public static Color Salmon => new(255, 140, 105);

        /// <summary>
        /// Obtiene el color marrón arena.
        /// </summary>
        public static Color SandyBrown => new(244, 164, 96);

        /// <summary>
        /// Obtiene el color verde savia.
        /// </summary>
        public static Color SapGreen => new(48, 128, 20);

        /// <summary>
        /// Obtiene el color verde marino en su tonalidad alternativa 2.
        /// </summary>
        public static Color SeaGreen2 => new(78, 238, 148);

        /// <summary>
        /// Obtiene el color verde marino en su tonalidad alternativa 3.
        /// </summary>
        public static Color SeaGreen3 => new(67, 205, 128);

        /// <summary>
        /// Obtiene el color verde marino en su tonalidad alternativa 4.
        /// </summary>
        public static Color SeaGreen4 => new(84, 255, 159);

        /// <summary>
        /// Obtiene el verde marino.
        /// </summary>
        public static Color SeaGreen => new(46, 139, 87);

        /// <summary>
        /// Obtiene el color concha marina en su tonalidad alternativa 2.
        /// </summary>
        public static Color SeaShell2 => new(238, 229, 222);

        /// <summary>
        /// Obtiene el color concha marina en su tonalidad alternativa 3.
        /// </summary>
        public static Color SeaShell3 => new(205, 197, 191);

        /// <summary>
        /// Obtiene el color concha marina en su tonalidad alternativa 4.
        /// </summary>
        public static Color SeaShell4 => new(139, 134, 130);

        /// <summary>
        /// Obtiene el color concha marina.
        /// </summary>
        public static Color SeaShell => new(255, 245, 238);

        /// <summary>
        /// Obtiene el color sepia.
        /// </summary>
        public static Color Sepia => new(94, 38, 18);

        /// <summary>
        /// Obtiene el color remolacha de la paleta de SGI.
        /// </summary>
        public static Color SgiBeet => new(142, 56, 142);

        /// <summary>
        /// Obtiene el color gris brillante de la paleta de SGI.
        /// </summary>
        public static Color SgiBrightGray => new(197, 193, 170);

        /// <summary>
        /// Obtiene el color chartreuse de la paleta de SGI.
        /// </summary>
        public static Color SgiChartreuse => new(113, 198, 113);

        /// <summary>
        /// Obtiene el color gris oscuro de la paleta de SGI.
        /// </summary>
        public static Color SgiDarkGray => new(85, 85, 85);

        /// <summary>
        /// Obtiene el color gris en tonalidad #12 de la paleta de SGI.
        /// </summary>
        public static Color SgiGray12 => new(30, 30, 30);

        /// <summary>
        /// Obtiene el color gris en tonalidad #16 de la paleta de SGI.
        /// </summary>
        public static Color SgiGray16 => new(40, 40, 40);

        /// <summary>
        /// Obtiene el color gris en tonalidad #32 de la paleta de SGI.
        /// </summary>
        public static Color SgiGray32 => new(81, 81, 81);

        /// <summary>
        /// Obtiene el color gris en tonalidad #36 de la paleta de SGI.
        /// </summary>
        public static Color SgiGray36 => new(91, 91, 91);

        /// <summary>
        /// Obtiene el color gris en tonalidad #52 de la paleta de SGI.
        /// </summary>
        public static Color SgiGray52 => new(132, 132, 132);

        /// <summary>
        /// Obtiene el color gris en tonalidad #56 de la paleta de SGI.
        /// </summary>
        public static Color SgiGray56 => new(142, 142, 142);

        /// <summary>
        /// Obtiene el color gris en tonalidad #72 de la paleta de SGI.
        /// </summary>
        public static Color SgiGray72 => new(183, 183, 183);

        /// <summary>
        /// Obtiene el color gris en tonalidad #76 de la paleta de SGI.
        /// </summary>
        public static Color SgiGray76 => new(193, 193, 193);

        /// <summary>
        /// Obtiene el color gris en tonalidad #92 de la paleta de SGI.
        /// </summary>
        public static Color SgiGray92 => new(234, 234, 234);

        /// <summary>
        /// Obtiene el color gris en tonalidad #96 de la paleta de SGI.
        /// </summary>
        public static Color SgiGray96 => new(244, 244, 244);

        /// <summary>
        /// Obtiene el color azul claro de la paleta de SGI.
        /// </summary>
        public static Color SgiLightBlue => new(125, 158, 192);

        /// <summary>
        /// Obtiene el color girs claro de la paleta de SGI.
        /// </summary>
        public static Color SgiLightGray => new(170, 170, 170);

        /// <summary>
        /// Obtiene el color olivo de la paleta de SGI.
        /// </summary>
        public static Color SgiOliveDrab => new(142, 142, 56);

        /// <summary>
        /// Obtiene el color salmón de la paleta de SGI.
        /// </summary>
        public static Color SgiSalmon => new(198, 113, 113);

        /// <summary>
        /// Obtiene el color azul pizarra de la paleta de SGI.
        /// </summary>
        public static Color SgiSlateBlue => new(113, 113, 198);

        /// <summary>
        /// Obtiene el color verde azulado de la paleta de SGI.
        /// </summary>
        public static Color Sgiteal => new(56, 142, 142);

        /// <summary>
        /// Obtiene el color siena en su tonalidad alternativa 1.
        /// </summary>
        public static Color Sienna1 => new(255, 130, 71);

        /// <summary>
        /// Obtiene el color siena en su tonalidad alternativa 2.
        /// </summary>
        public static Color Sienna2 => new(238, 121, 66);

        /// <summary>
        /// Obtiene el color siena en su tonalidad alternativa 3.
        /// </summary>
        public static Color Sienna3 => new(205, 104, 57);

        /// <summary>
        /// Obtiene el color siena en su tonalidad alternativa 4.
        /// </summary>
        public static Color Sienna4 => new(139, 71, 38);

        /// <summary>
        /// Obtiene el color siena.
        /// </summary>
        public static Color Sienna => new(160, 82, 45);

        /// <summary>
        /// Obtiene el color plateado.
        /// </summary>
        public static Color Silver => new(192, 192, 192);

        /// <summary>
        /// Obtiene el color azul cielo en su tonalidad alternativa 1.
        /// </summary>
        public static Color SkyBlue1 => new(135, 206, 255);

        /// <summary>
        /// Obtiene el color azul cielo en su tonalidad alternativa 2.
        /// </summary>
        public static Color SkyBlue2 => new(126, 192, 238);

        /// <summary>
        /// Obtiene el color azul cielo en su tonalidad alternativa 3.
        /// </summary>
        public static Color SkyBlue3 => new(108, 166, 205);

        /// <summary>
        /// Obtiene el color azul cielo en su tonalidad alternativa 4.
        /// </summary>
        public static Color SkyBlue4 => new(74, 112, 139);

        /// <summary>
        /// Obtiene el color azul cielo.
        /// </summary>
        public static Color SkyBlue => new(0x00, 0xaf, 0xff);

        /// <summary>
        /// Obtiene el color azul pizarra en su tonalidad alternativa 1.
        /// </summary>
        public static Color SlateBlue1 => new(131, 111, 255);

        /// <summary>
        /// Obtiene el color azul pizarra en su tonalidad alternativa 2.
        /// </summary>
        public static Color SlateBlue2 => new(122, 103, 238);

        /// <summary>
        /// Obtiene el color azul pizarra en su tonalidad alternativa 3.
        /// </summary>
        public static Color SlateBlue3 => new(105, 89, 205);

        /// <summary>
        /// Obtiene el color azul pizarra en su tonalidad alternativa 4.
        /// </summary>
        public static Color SlateBlue4 => new(71, 60, 139);

        /// <summary>
        /// Obtiene el color azul pizarra.
        /// </summary>
        public static Color SlateBlue => new(106, 90, 205);

        /// <summary>
        /// Obtiene el color gris pizarra en su tonalidad alternativa 1.
        /// </summary>
        public static Color SlateGray1 => new(198, 226, 255);

        /// <summary>
        /// Obtiene el color gris pizarra en su tonalidad alternativa 2.
        /// </summary>
        public static Color SlateGray2 => new(185, 211, 238);

        /// <summary>
        /// Obtiene el color gris pizarra en su tonalidad alternativa 3.
        /// </summary>
        public static Color SlateGray3 => new(159, 182, 205);

        /// <summary>
        /// Obtiene el color gris pizarra en su tonalidad alternativa 4.
        /// </summary>
        public static Color SlateGray4 => new(108, 123, 139);

        /// <summary>
        /// Obtiene el color gris pizarra.
        /// </summary>
        public static Color SlateGray => new(112, 128, 144);

        /// <summary>
        /// Obtiene el color nieve en su tonalidad alternativa 2.
        /// </summary>
        public static Color Snow2 => new(238, 233, 233);

        /// <summary>
        /// Obtiene el color nieve en su tonalidad alternativa 3.
        /// </summary>
        public static Color Snow3 => new(205, 201, 201);

        /// <summary>
        /// Obtiene el color nieve en su tonalidad alternativa 4.
        /// </summary>
        public static Color Snow4 => new(139, 137, 137);

        /// <summary>
        /// Obtiene el color nieve.
        /// </summary>
        public static Color Snow => new(255, 250, 250);

        /// <summary>
        /// Obtiene el color verde primavera en su tonalidad alternativa 1.
        /// </summary>
        public static Color SpringGreen1 => new(0, 238, 118);

        /// <summary>
        /// Obtiene el color verde primavera en su tonalidad alternativa 2.
        /// </summary>
        public static Color SpringGreen2 => new(0, 205, 102);

        /// <summary>
        /// Obtiene el color verde primavera en su tonalidad alternativa 3.
        /// </summary>
        public static Color SpringGreen3 => new(0, 139, 69);

        /// <summary>
        /// Obtiene el color verde primavera.
        /// </summary>
        public static Color SpringGreen => new(0, 255, 127);

        /// <summary>
        /// Obtiene el color azul acero en su tonalidad alternativa 1.
        /// </summary>
        public static Color SteelBlue1 => new(99, 184, 255);

        /// <summary>
        /// Obtiene el color azul acero en su tonalidad alternativa 2.
        /// </summary>
        public static Color SteelBlue2 => new(92, 172, 238);

        /// <summary>
        /// Obtiene el color azul acero en su tonalidad alternativa 3.
        /// </summary>
        public static Color SteelBlue3 => new(79, 148, 205);

        /// <summary>
        /// Obtiene el color azul acero en su tonalidad alternativa 4.
        /// </summary>
        public static Color SteelBlue4 => new(54, 100, 139);

        /// <summary>
        /// Obtiene el color azul acero.
        /// </summary>
        public static Color SteelBlue => new(70, 130, 180);

        /// <summary>
        /// Obtiene el color bronceado en su tonalidad alternativa 1.
        /// </summary>
        public static Color Tan1 => new(255, 165, 79);

        /// <summary>
        /// Obtiene el color bronceado en su tonalidad alternativa 2.
        /// </summary>
        public static Color Tan2 => new(238, 154, 73);

        /// <summary>
        /// Obtiene el color bronceado en su tonalidad alternativa 4.
        /// </summary>
        public static Color Tan4 => new(139, 90, 43);

        /// <summary>
        /// Obtiene el color bronceado.
        /// </summary>
        public static Color Tan => new(210, 180, 140);

        /// <summary>
        /// Obtiene el color verde azulado.
        /// </summary>
        public static Color Teal => new(0, 128, 128);

        /// <summary>
        /// Obtiene el color cardo en su tonalidad alternativa 1.
        /// </summary>
        public static Color Thistle1 => new(255, 225, 255);

        /// <summary>
        /// Obtiene el color cardo en su tonalidad alternativa 2.
        /// </summary>
        public static Color Thistle2 => new(238, 210, 238);

        /// <summary>
        /// Obtiene el color cardo en su tonalidad alternativa 3.
        /// </summary>
        public static Color Thistle3 => new(205, 181, 205);

        /// <summary>
        /// Obtiene el color cardo en su tonalidad alternativa 4.
        /// </summary>
        public static Color Thistle4 => new(139, 123, 139);

        /// <summary>
        /// Obtiene el color cardo.
        /// </summary>
        public static Color Thistle => new(216, 191, 216);

        /// <summary>
        /// Obtiene el color tomate en su tonalidad alternativa 2.
        /// </summary>
        public static Color Tomato2 => new(238, 92, 66);

        /// <summary>
        /// Obtiene el color tomate en su tonalidad alternativa 3.
        /// </summary>
        public static Color Tomato3 => new(205, 79, 57);

        /// <summary>
        /// Obtiene el color tomate en su tonalidad alternativa 4.
        /// </summary>
        public static Color Tomato4 => new(139, 54, 38);

        /// <summary>
        /// Obtiene el color tomate.
        /// </summary>
        public static Color Tomato => new(255, 99, 71);

        /// <summary>
        /// Obtiene el color azul turqueza en su tonalidad alternativa 1.
        /// </summary>
        public static Color Turquoise1 => new(0, 245, 255);

        /// <summary>
        /// Obtiene el color azul turqueza en su tonalidad alternativa 2.
        /// </summary>
        public static Color Turquoise2 => new(0, 229, 238);

        /// <summary>
        /// Obtiene el color azul turqueza en su tonalidad alternativa 3.
        /// </summary>
        public static Color Turquoise3 => new(0, 197, 205);

        /// <summary>
        /// Obtiene el color azul turqueza en su tonalidad alternativa 4.
        /// </summary>
        public static Color Turquoise4 => new(0, 134, 139);

        /// <summary>
        /// Obtiene el color azul turqueza.
        /// </summary>
        public static Color TurquoiseBlue => new(0, 199, 140);

        /// <summary>
        /// Obtiene el color Turqueza.
        /// </summary>
        public static Color Turquoise => new(64, 224, 208);

        /// <summary>
        /// Obtiene el color violeta.
        /// </summary>
        public static Color Violet => new(238, 130, 238);

        /// <summary>
        /// Obtiene el color rojo violeta en su tonalidad alternativa 1.
        /// </summary>
        public static Color VioletRed1 => new(255, 62, 150);

        /// <summary>
        /// Obtiene el color rojo violeta en su tonalidad alternativa 2.
        /// </summary>
        public static Color VioletRed2 => new(238, 58, 140);

        /// <summary>
        /// Obtiene el color rojo violeta en su tonalidad alternativa 3.
        /// </summary>
        public static Color VioletRed3 => new(205, 50, 120);

        /// <summary>
        /// Obtiene el color rojo violeta en su tonalidad alternativa 4.
        /// </summary>
        public static Color VioletRed4 => new(139, 34, 82);

        /// <summary>
        /// Obtiene el color rojo violeta.
        /// </summary>
        public static Color VioletRed => new(208, 32, 144);

        /// <summary>
        /// Obtiene el color gris cálido.
        /// </summary>
        public static Color WarmGrey => new(128, 128, 105);

        /// <summary>
        /// Obtiene el color trigo en su tonalidad alternativa 1.
        /// </summary>
        public static Color Wheat1 => new(255, 231, 186);

        /// <summary>
        /// Obtiene el color trigo en su tonalidad alternativa 2.
        /// </summary>
        public static Color Wheat2 => new(238, 216, 174);

        /// <summary>
        /// Obtiene el color trigo en su tonalidad alternativa 3.
        /// </summary>
        public static Color Wheat3 => new(205, 186, 150);

        /// <summary>
        /// Obtiene el color trigo en su tonalidad alternativa 4.
        /// </summary>
        public static Color Wheat4 => new(139, 126, 102);

        /// <summary>
        /// Obtiene el color trigo.
        /// </summary>
        public static Color Wheat => new(245, 222, 179);

        /// <summary>
        /// Obtiene el color blanco.
        /// </summary>
        public static Color White => new(255, 255, 255);

        /// <summary>
        /// Obtiene el color blanco humo.
        /// </summary>
        public static Color Whitesmoke => new(245, 245, 245);

        /// <summary>
        /// Obtiene el color amarillo madero.
        /// </summary>
        public static Color WoodYellow => new(0xf2, 0xe7, 0xc4);

        /// <summary>
        /// Obtiene el color amarillo en su tonalidad alternativa 2.
        /// </summary>
        public static Color Yellow2 => new(238, 238, 0);

        /// <summary>
        /// Obtiene el color amarillo en su tonalidad alternativa 3.
        /// </summary>
        public static Color Yellow3 => new(205, 205, 0);

        /// <summary>
        /// Obtiene el color amarillo en su tonalidad alternativa 4.
        /// </summary>
        public static Color Yellow4 => new(139, 139, 0);

        /// <summary>
        /// Obtiene el color amarillo.
        /// </summary>
        public static Color Yellow => new(255, 255, 0);
    }
}