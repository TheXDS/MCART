﻿//
//  Colors.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can Redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

#pragma warning disable CS1591 // TODO: agregar comentarios XML para los colores.

using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Incluye una colección de colores adicionales.
    /// </summary>
    public static partial class Colors
    {
        /// <summary>
        /// Devuelve un color aleatorio.
        /// </summary>
        /// <returns>
        /// Un <see cref="Color"/> seleccionado aleatoriamente.
        /// </returns>
        public static Color Pick()
        {
            return (Color)typeof(Colors).GetProperties().Pick().GetValue(null);
        }

        public static Color AliceBlue => new Color(240, 248, 255);
        public static Color AntiqueWhite1 => new Color(255, 239, 219);
        public static Color AntiqueWhite2 => new Color(238, 223, 204);
        public static Color AntiqueWhite3 => new Color(205, 192, 176);
        public static Color AntiqueWhite4 => new Color(139, 131, 120);
        public static Color AntiqueWhite => new Color(250, 235, 215);
        public static Color Aquamarine2 => new Color(118, 238, 198);
        public static Color Aquamarine4 => new Color(69, 139, 116);
        public static Color Aquamarine => new Color(127, 255, 212);
        public static Color Azure2 => new Color(224, 238, 238);
        public static Color Azure3 => new Color(193, 205, 205);
        public static Color Azure4 => new Color(131, 139, 139);
        public static Color Azure => new Color(240, 255, 255);
        public static Color Banana => new Color(227, 207, 87);
        public static Color Beige => new Color(245, 245, 220);
        public static Color Bisque2 => new Color(238, 213, 183);
        public static Color Bisque3 => new Color(205, 183, 158);
        public static Color Bisque4 => new Color(139, 125, 107);
        public static Color Bisque => new Color(255, 228, 196);
        public static Color Black => new Color(0, 0, 0);
        public static Color BlanchedAlmond => new Color(255, 235, 205);
        public static Color Blue2 => new Color(0, 0, 238);
        public static Color Blue => new Color(0, 0, 255);
        public static Color BlueViolet => new Color(138, 43, 226);
        public static Color Brick => new Color(156, 102, 31);
        public static Color Brown1 => new Color(255, 64, 64);
        public static Color Brown2 => new Color(238, 59, 59);
        public static Color Brown3 => new Color(205, 51, 51);
        public static Color Brown4 => new Color(139, 35, 35);
        public static Color Brown => new Color(165, 42, 42);
        public static Color BurlyWood1 => new Color(255, 211, 155);
        public static Color BurlyWood2 => new Color(238, 197, 145);
        public static Color BurlyWood3 => new Color(205, 170, 125);
        public static Color BurlyWood4 => new Color(139, 115, 85);
        public static Color BurlyWood => new Color(222, 184, 135);
        public static Color BurntSienna => new Color(138, 54, 15);
        public static Color BurnTumber => new Color(138, 51, 36);
        public static Color CadetBlue1 => new Color(152, 245, 255);
        public static Color CadetBlue2 => new Color(142, 229, 238);
        public static Color CadetBlue3 => new Color(122, 197, 205);
        public static Color CadetBlue4 => new Color(83, 134, 139);
        public static Color CadetBlue => new Color(95, 158, 160);
        public static Color CadmiumOrange => new Color(255, 97, 3);
        public static Color CadmiumYellow => new Color(255, 153, 18);
        public static Color Carrot => new Color(237, 145, 33);
        public static Color Chartreuse2 => new Color(118, 238, 0);
        public static Color Chartreuse3 => new Color(102, 205, 0);
        public static Color Chartreuse4 => new Color(69, 139, 0);
        public static Color Chartreuse => new Color(127, 255, 0);
        public static Color Chocolate1 => new Color(255, 127, 36);
        public static Color Chocolate2 => new Color(238, 118, 33);
        public static Color Chocolate3 => new Color(205, 102, 29);
        public static Color Chocolate => new Color(210, 105, 30);
        public static Color CobaltGreen => new Color(61, 145, 64);
        public static Color Cobalt => new Color(61, 89, 171);
        public static Color ColdGrey => new Color(128, 138, 135);
        public static Color Coral2 => new Color(238, 106, 80);
        public static Color Coral3 => new Color(205, 91, 69);
        public static Color Coral4 => new Color(139, 62, 47);
        public static Color Coral => new Color(255, 127, 80);
        public static Color CornFlowerBlue => new Color(100, 149, 237);
        public static Color CornSilk2 => new Color(238, 232, 205);
        public static Color CornSilk3 => new Color(205, 200, 177);
        public static Color CornSilk4 => new Color(139, 136, 120);
        public static Color CornSilk => new Color(255, 248, 220);
        public static Color Crimson => new Color(220, 20, 60);
        public static Color Cyan2 => new Color(0, 238, 238);
        public static Color Cyan3 => new Color(0, 205, 205);
        public static Color Cyan => new Color(0, 255, 255);
        public static Color DarkBlue => new Color(0, 0, 139);
        public static Color DarkCyan => new Color(0, 139, 139);
        public static Color DarkGoldenRod1 => new Color(255, 185, 15);
        public static Color DarkGoldenRod2 => new Color(238, 173, 14);
        public static Color DarkGoldenRod3 => new Color(205, 149, 12);
        public static Color DarkGoldenRod4 => new Color(139, 101, 8);
        public static Color DarkGoldenRod => new Color(184, 134, 11);
        public static Color DarkGray => new Color(169, 169, 169);
        public static Color DarkGreen => new Color(0, 100, 0);
        public static Color DarkKhaki => new Color(189, 183, 107);
        public static Color DarkMagenta => new Color(139, 0, 139);
        public static Color DarkOliveGreen1 => new Color(202, 255, 112);
        public static Color DarkOliveGreen2 => new Color(188, 238, 104);
        public static Color DarkOliveGreen3 => new Color(162, 205, 90);
        public static Color DarkOliveGreen4 => new Color(110, 139, 61);
        public static Color DarkOliveGreen => new Color(85, 107, 47);
        public static Color DarkOrange1 => new Color(255, 127, 0);
        public static Color DarkOrange2 => new Color(238, 118, 0);
        public static Color DarkOrange3 => new Color(205, 102, 0);
        public static Color DarkOrange4 => new Color(139, 69, 0);
        public static Color DarkOrange => new Color(255, 140, 0);
        public static Color DarkOrchid1 => new Color(191, 62, 255);
        public static Color DarkOrchid2 => new Color(178, 58, 238);
        public static Color DarkOrchid3 => new Color(154, 50, 205);
        public static Color DarkOrchid4 => new Color(104, 34, 139);
        public static Color DarkOrchid => new Color(153, 50, 204);
        public static Color DarkRed => new Color(139, 0, 0);
        public static Color DarkSalmon => new Color(233, 150, 122);
        public static Color DarkseaGreen1 => new Color(193, 255, 193);
        public static Color DarkseaGreen2 => new Color(180, 238, 180);
        public static Color DarkseaGreen3 => new Color(155, 205, 155);
        public static Color DarkseaGreen4 => new Color(105, 139, 105);
        public static Color DarkseaGreen => new Color(143, 188, 143);
        public static Color DarkSlateBlue => new Color(72, 61, 139);
        public static Color DarkSlateGray1 => new Color(151, 255, 255);
        public static Color DarkSlateGray2 => new Color(141, 238, 238);
        public static Color DarkSlateGray3 => new Color(121, 205, 205);
        public static Color DarkSlateGray4 => new Color(82, 139, 139);
        public static Color DarkSlateGray => new Color(47, 79, 79);
        public static Color DarkTurquoise => new Color(0, 206, 209);
        public static Color DarkViolet => new Color(148, 0, 211);
        public static Color DeepPink2 => new Color(238, 18, 137);
        public static Color DeepPink3 => new Color(205, 16, 118);
        public static Color DeepPink4 => new Color(139, 10, 80);
        public static Color DeepPink => new Color(255, 20, 147);
        public static Color DeepSkyBlue2 => new Color(0, 178, 238);
        public static Color DeepSkyBlue3 => new Color(0, 154, 205);
        public static Color DeepSkyBlue4 => new Color(0, 104, 139);
        public static Color DeepSkyBlue => new Color(0, 191, 255);
        public static Color DimGray => new Color(105, 105, 105);
        public static Color DodgerBlue2 => new Color(28, 134, 238);
        public static Color DodgerBlue3 => new Color(24, 116, 205);
        public static Color DodgerBlue4 => new Color(16, 78, 139);
        public static Color DodgerBlue => new Color(30, 144, 255);
        public static Color EggShell => new Color(252, 230, 201);
        public static Color EmeraldGreen => new Color(0, 201, 87);
        public static Color FireBrick1 => new Color(255, 48, 48);
        public static Color FireBrick2 => new Color(238, 44, 44);
        public static Color FireBrick3 => new Color(205, 38, 38);
        public static Color FireBrick4 => new Color(139, 26, 26);
        public static Color FireBrick => new Color(178, 34, 34);
        public static Color Flesh => new Color(255, 125, 64);
        public static Color FloralWhite => new Color(255, 250, 240);
        public static Color ForestGreen => new Color(34, 139, 34);
        public static Color Gainsboro => new Color(220, 220, 220);
        public static Color GhostWhite => new Color(248, 248, 255);
        public static Color Gold2 => new Color(238, 201, 0);
        public static Color Gold3 => new Color(205, 173, 0);
        public static Color Gold4 => new Color(139, 117, 0);
        public static Color GoldenRod1 => new Color(255, 193, 37);
        public static Color GoldenRod2 => new Color(238, 180, 34);
        public static Color GoldenRod3 => new Color(205, 155, 29);
        public static Color GoldenRod4 => new Color(139, 105, 20);
        public static Color GoldenRod => new Color(218, 165, 32);
        public static Color Gold => new Color(255, 215, 0);
        public static Color Gray => new Color(128, 128, 128);
        public static Color Green2 => new Color(0, 238, 0);
        public static Color Green3 => new Color(0, 205, 0);
        public static Color Green4 => new Color(0, 139, 0);
        public static Color Green => new Color(0, 128, 0);
        public static Color GreenYellow => new Color(173, 255, 47);
        public static Color Honeydew2 => new Color(224, 238, 224);
        public static Color Honeydew3 => new Color(193, 205, 193);
        public static Color Honeydew4 => new Color(131, 139, 131);
        public static Color Honeydew => new Color(240, 255, 240);
        public static Color HotPink1 => new Color(255, 110, 180);
        public static Color HotPink2 => new Color(238, 106, 167);
        public static Color HotPink3 => new Color(205, 96, 144);
        public static Color HotPink4 => new Color(139, 58, 98);
        public static Color HotPink => new Color(255, 105, 180);
        public static Color IceWhite => new Color(0xed, 0xfa, 0xff);
        public static Color IndianRed1 => new Color(255, 106, 106);
        public static Color IndianRed2 => new Color(238, 99, 99);
        public static Color IndianRed3 => new Color(205, 85, 85);
        public static Color IndianRed4 => new Color(139, 58, 58);
        public static Color IndianRed => new Color(176, 23, 31);
        public static Color Indigo => new Color(75, 0, 130);
        public static Color IronOxideGlass => new Color(0x21, 0x9c, 0x6a);
        public static Color Ivory2 => new Color(238, 238, 224);
        public static Color Ivory3 => new Color(205, 205, 193);
        public static Color Ivory4 => new Color(139, 139, 131);
        public static Color IvoryBlack => new Color(41, 36, 33);
        public static Color Ivory => new Color(255, 255, 240);
        public static Color Khaki1 => new Color(255, 246, 143);
        public static Color Khaki2 => new Color(238, 230, 133);
        public static Color Khaki3 => new Color(205, 198, 115);
        public static Color Khaki4 => new Color(139, 134, 78);
        public static Color Khaki => new Color(240, 230, 140);
        public static Color LavenderBlush2 => new Color(238, 224, 229);
        public static Color LavenderBlush3 => new Color(205, 193, 197);
        public static Color LavenderBlush4 => new Color(139, 131, 134);
        public static Color LavenderBlush => new Color(255, 240, 245);
        public static Color Lavender => new Color(230, 230, 250);
        public static Color LawnGreen => new Color(124, 252, 0);
        public static Color Lemonchiffon2 => new Color(238, 233, 191);
        public static Color Lemonchiffon3 => new Color(205, 201, 165);
        public static Color Lemonchiffon4 => new Color(139, 137, 112);
        public static Color Lemonchiffon => new Color(255, 250, 205);
        public static Color LightBlue1 => new Color(191, 239, 255);
        public static Color LightBlue2 => new Color(178, 223, 238);
        public static Color LightBlue3 => new Color(154, 192, 205);
        public static Color LightBlue4 => new Color(104, 131, 139);
        public static Color LightBlue => new Color(173, 216, 230);
        public static Color LightCoral => new Color(240, 128, 128);
        public static Color Lightcyan2 => new Color(209, 238, 238);
        public static Color Lightcyan3 => new Color(180, 205, 205);
        public static Color Lightcyan4 => new Color(122, 139, 139);
        public static Color Lightcyan => new Color(224, 255, 255);
        public static Color LightGoldenRod1 => new Color(255, 236, 139);
        public static Color LightGoldenRod2 => new Color(238, 220, 130);
        public static Color LightGoldenRod3 => new Color(205, 190, 112);
        public static Color LightGoldenRod4 => new Color(139, 129, 76);
        public static Color LightGoldenRodYellow => new Color(250, 250, 210);
        public static Color LightGreen => new Color(144, 238, 144);
        public static Color LightGrey => new Color(211, 211, 211);
        public static Color LightPink1 => new Color(255, 174, 185);
        public static Color LightPink2 => new Color(238, 162, 173);
        public static Color LightPink3 => new Color(205, 140, 149);
        public static Color LightPink4 => new Color(139, 95, 101);
        public static Color LightPink => new Color(255, 182, 193);
        public static Color LightSalmon2 => new Color(238, 149, 114);
        public static Color LightSalmon3 => new Color(205, 129, 98);
        public static Color LightSalmon4 => new Color(139, 87, 66);
        public static Color LightSalmon => new Color(255, 160, 122);
        public static Color LightSeaGreen => new Color(32, 178, 170);
        public static Color LightSkyBlue1 => new Color(176, 226, 255);
        public static Color LightSkyBlue2 => new Color(164, 211, 238);
        public static Color LightSkyBlue3 => new Color(141, 182, 205);
        public static Color LightSkyBlue4 => new Color(96, 123, 139);
        public static Color LightSkyBlue => new Color(135, 206, 250);
        public static Color LightSlateBlue => new Color(132, 112, 255);
        public static Color LightSlateGray => new Color(119, 136, 153);
        public static Color LightSteelBlue1 => new Color(202, 225, 255);
        public static Color LightSteelBlue2 => new Color(188, 210, 238);
        public static Color LightSteelBlue3 => new Color(162, 181, 205);
        public static Color LightSteelBlue4 => new Color(110, 123, 139);
        public static Color LightSteelBlue => new Color(176, 196, 222);
        public static Color LightYellow2 => new Color(238, 238, 209);
        public static Color LightYellow3 => new Color(205, 205, 180);
        public static Color LightYellow4 => new Color(139, 139, 122);
        public static Color LightYellow => new Color(255, 255, 224);
        public static Color LimeGreen => new Color(50, 205, 50);
        public static Color Lime => new Color(0, 255, 0);
        public static Color Linen => new Color(250, 240, 230);
        public static Color Magenta2 => new Color(238, 0, 238);
        public static Color Magenta3 => new Color(205, 0, 205);
        public static Color Magenta => new Color(255, 0, 255);
        public static Color MagicPurple => new Color(0x5d, 0x00, 0xff);
        public static Color ManganeseBlue => new Color(3, 168, 158);
        public static Color Maroon1 => new Color(255, 52, 179);
        public static Color Maroon2 => new Color(238, 48, 167);
        public static Color Maroon3 => new Color(205, 41, 144);
        public static Color Maroon4 => new Color(139, 28, 98);
        public static Color Maroon => new Color(128, 0, 0);
        public static Color MediumAquamarine => new Color(102, 205, 170);
        public static Color MediumBlue => new Color(0, 0, 205);
        public static Color MediumOrchid1 => new Color(224, 102, 255);
        public static Color MediumOrchid2 => new Color(209, 95, 238);
        public static Color MediumOrchid3 => new Color(180, 82, 205);
        public static Color MediumOrchid4 => new Color(122, 55, 139);
        public static Color MediumOrchid => new Color(186, 85, 211);
        public static Color MediumPurple1 => new Color(171, 130, 255);
        public static Color MediumPurple2 => new Color(159, 121, 238);
        public static Color MediumPurple3 => new Color(137, 104, 205);
        public static Color MediumPurple4 => new Color(93, 71, 139);
        public static Color MediumPurple => new Color(147, 112, 219);
        public static Color MediumSeaGreen => new Color(60, 179, 113);
        public static Color MediumSlateBlue => new Color(123, 104, 238);
        public static Color MediumSpringGreen => new Color(0, 250, 154);
        public static Color MediumTurquoise => new Color(72, 209, 204);
        public static Color MediumVioletRed => new Color(199, 21, 133);
        public static Color Melon => new Color(227, 168, 105);
        public static Color MidnightBlue => new Color(25, 25, 112);
        public static Color MintCream => new Color(245, 255, 250);
        public static Color Mint => new Color(189, 252, 201);
        public static Color MistyRose2 => new Color(238, 213, 210);
        public static Color MistyRose3 => new Color(205, 183, 181);
        public static Color MistyRose4 => new Color(139, 125, 123);
        public static Color MistyRose => new Color(255, 228, 225);
        public static Color Moccasin => new Color(255, 228, 181);
        public static Color NavajoWhite2 => new Color(238, 207, 161);
        public static Color NavajoWhite3 => new Color(205, 179, 139);
        public static Color NavajoWhite4 => new Color(139, 121, 94);
        public static Color NavajoWhite => new Color(255, 222, 173);
        public static Color Navy => new Color(0, 0, 128);
        public static Color OldLace => new Color(253, 245, 230);
        public static Color Olivedrab1 => new Color(192, 255, 62);
        public static Color Olivedrab2 => new Color(179, 238, 58);
        public static Color Olivedrab3 => new Color(154, 205, 50);
        public static Color Olivedrab4 => new Color(105, 139, 34);
        public static Color Olivedrab => new Color(107, 142, 35);
        public static Color Olive => new Color(128, 128, 0);
        public static Color Orange2 => new Color(238, 154, 0);
        public static Color Orange3 => new Color(205, 133, 0);
        public static Color Orange4 => new Color(139, 90, 0);
        public static Color Orange => new Color(255, 165, 0);
        public static Color OrangeRed2 => new Color(238, 64, 0);
        public static Color OrangeRed3 => new Color(205, 55, 0);
        public static Color OrangeRed4 => new Color(139, 37, 0);
        public static Color OrangeRed => new Color(255, 69, 0);
        public static Color Orchid1 => new Color(255, 131, 250);
        public static Color Orchid2 => new Color(238, 122, 233);
        public static Color Orchid3 => new Color(205, 105, 201);
        public static Color Orchid4 => new Color(139, 71, 137);
        public static Color Orchid => new Color(218, 112, 214);
        public static Color PaleGoldenRod => new Color(238, 232, 170);
        public static Color PaleGreen2 => new Color(154, 255, 154);
        public static Color PaleGreen3 => new Color(124, 205, 124);
        public static Color PaleGreen4 => new Color(84, 139, 84);
        public static Color PaleGreen => new Color(152, 251, 152);
        public static Color PaleTurquoise2 => new Color(187, 255, 255);
        public static Color PaleTurquoise3 => new Color(150, 205, 205);
        public static Color PaleTurquoise4 => new Color(102, 139, 139);
        public static Color PaleTurquoise => new Color(174, 238, 238);
        public static Color PaleVioletRed1 => new Color(255, 130, 171);
        public static Color PaleVioletRed2 => new Color(238, 121, 159);
        public static Color PaleVioletRed3 => new Color(205, 104, 137);
        public static Color PaleVioletRed4 => new Color(139, 71, 93);
        public static Color PaleVioletRed => new Color(219, 112, 147);
        public static Color PapayaWhip => new Color(255, 239, 213);
        public static Color PeachPuff2 => new Color(238, 203, 173);
        public static Color PeachPuff3 => new Color(205, 175, 149);
        public static Color PeachPuff4 => new Color(139, 119, 101);
        public static Color PeachPuff => new Color(255, 218, 185);
        public static Color Peacock => new Color(51, 161, 201);
        public static Color Peru => new Color(205, 133, 63);
        public static Color Pink1 => new Color(255, 181, 197);
        public static Color Pink2 => new Color(238, 169, 184);
        public static Color Pink3 => new Color(205, 145, 158);
        public static Color Pink4 => new Color(139, 99, 108);
        public static Color Pink => new Color(255, 192, 203);
        public static Color Plum1 => new Color(255, 187, 255);
        public static Color Plum2 => new Color(238, 174, 238);
        public static Color Plum3 => new Color(205, 150, 205);
        public static Color Plum4 => new Color(139, 102, 139);
        public static Color Plum => new Color(221, 160, 221);
        public static Color Poop => new Color(0x69, 0x46, 0x24);
        public static Color PowderBlue => new Color(176, 224, 230);
        public static Color Purple1 => new Color(155, 48, 255);
        public static Color Purple2 => new Color(145, 44, 238);
        public static Color Purple3 => new Color(125, 38, 205);
        public static Color Purple4 => new Color(85, 26, 139);
        public static Color Purple => new Color(128, 0, 128);
        public static Color RacingYellow => new Color(0xff, 0xcc, 0x00);
        public static Color Raspberry => new Color(135, 38, 87);
        public static Color RawSienna => new Color(199, 97, 20);
        public static Color Red2 => new Color(238, 0, 0);
        public static Color Red3 => new Color(205, 0, 0);
        public static Color Red => new Color(255, 0, 0);
        public static Color RosyBrown1 => new Color(255, 193, 193);
        public static Color RosyBrown2 => new Color(238, 180, 180);
        public static Color RosyBrown3 => new Color(205, 155, 155);
        public static Color RosyBrown4 => new Color(139, 105, 105);
        public static Color RosyBrown => new Color(188, 143, 143);
        public static Color RoyalBlue1 => new Color(72, 118, 255);
        public static Color RoyalBlue2 => new Color(67, 110, 238);
        public static Color RoyalBlue3 => new Color(58, 95, 205);
        public static Color RoyalBlue4 => new Color(39, 64, 139);
        public static Color RoyalBlue => new Color(65, 105, 225);
        public static Color SaddleBrown => new Color(139, 69, 19);
        public static Color Salmon2 => new Color(238, 130, 98);
        public static Color Salmon3 => new Color(205, 112, 84);
        public static Color Salmon4 => new Color(139, 76, 57);
        public static Color Salmon => new Color(255, 140, 105);
        public static Color SandyBrown => new Color(244, 164, 96);
        public static Color SapGreen => new Color(48, 128, 20);
        public static Color SeaGreen2 => new Color(78, 238, 148);
        public static Color SeaGreen3 => new Color(67, 205, 128);
        public static Color SeaGreen4 => new Color(84, 255, 159);
        public static Color SeaGreen => new Color(46, 139, 87);
        public static Color SeaShell2 => new Color(238, 229, 222);
        public static Color SeaShell3 => new Color(205, 197, 191);
        public static Color SeaShell4 => new Color(139, 134, 130);
        public static Color SeaShell => new Color(255, 245, 238);
        public static Color Sepia => new Color(94, 38, 18);
        public static Color Sgibeet => new Color(142, 56, 142);
        public static Color SgibrightGray => new Color(197, 193, 170);
        public static Color Sgichartreuse => new Color(113, 198, 113);
        public static Color SgiDarkGray => new Color(85, 85, 85);
        public static Color SgiGray12 => new Color(30, 30, 30);
        public static Color SgiGray16 => new Color(40, 40, 40);
        public static Color SgiGray32 => new Color(81, 81, 81);
        public static Color SgiGray36 => new Color(91, 91, 91);
        public static Color SgiGray52 => new Color(132, 132, 132);
        public static Color SgiGray56 => new Color(142, 142, 142);
        public static Color SgiGray72 => new Color(183, 183, 183);
        public static Color SgiGray76 => new Color(193, 193, 193);
        public static Color SgiGray92 => new Color(234, 234, 234);
        public static Color SgiGray96 => new Color(244, 244, 244);
        public static Color SgiLightBlue => new Color(125, 158, 192);
        public static Color SgiLightGray => new Color(170, 170, 170);
        public static Color SgiOlivedrab => new Color(142, 142, 56);
        public static Color SgiSalmon => new Color(198, 113, 113);
        public static Color SgiSlateBlue => new Color(113, 113, 198);
        public static Color Sgiteal => new Color(56, 142, 142);
        public static Color Sienna1 => new Color(255, 130, 71);
        public static Color Sienna2 => new Color(238, 121, 66);
        public static Color Sienna3 => new Color(205, 104, 57);
        public static Color Sienna4 => new Color(139, 71, 38);
        public static Color Sienna => new Color(160, 82, 45);
        public static Color Silver => new Color(192, 192, 192);
        public static Color SkyBlue1 => new Color(135, 206, 255);
        public static Color SkyBlue2 => new Color(126, 192, 238);
        public static Color SkyBlue3 => new Color(108, 166, 205);
        public static Color SkyBlue4 => new Color(74, 112, 139);
        public static Color SkyBlue => new Color(0x00, 0xaf, 0xff);
        public static Color SlateBlue1 => new Color(131, 111, 255);
        public static Color SlateBlue2 => new Color(122, 103, 238);
        public static Color SlateBlue3 => new Color(105, 89, 205);
        public static Color SlateBlue4 => new Color(71, 60, 139);
        public static Color SlateBlue => new Color(106, 90, 205);
        public static Color SlateGray1 => new Color(198, 226, 255);
        public static Color SlateGray2 => new Color(185, 211, 238);
        public static Color SlateGray3 => new Color(159, 182, 205);
        public static Color SlateGray4 => new Color(108, 123, 139);
        public static Color SlateGray => new Color(112, 128, 144);
        public static Color Snow2 => new Color(238, 233, 233);
        public static Color Snow3 => new Color(205, 201, 201);
        public static Color Snow4 => new Color(139, 137, 137);
        public static Color Snow => new Color(255, 250, 250);
        public static Color SpringGreen1 => new Color(0, 238, 118);
        public static Color SpringGreen2 => new Color(0, 205, 102);
        public static Color SpringGreen3 => new Color(0, 139, 69);
        public static Color SpringGreen => new Color(0, 255, 127);
        public static Color SteelBlue1 => new Color(99, 184, 255);
        public static Color SteelBlue2 => new Color(92, 172, 238);
        public static Color SteelBlue3 => new Color(79, 148, 205);
        public static Color SteelBlue4 => new Color(54, 100, 139);
        public static Color SteelBlue => new Color(70, 130, 180);
        public static Color Tan1 => new Color(255, 165, 79);
        public static Color Tan2 => new Color(238, 154, 73);
        public static Color Tan4 => new Color(139, 90, 43);
        public static Color Tan => new Color(210, 180, 140);
        public static Color Teal => new Color(0, 128, 128);
        public static Color Thistle1 => new Color(255, 225, 255);
        public static Color Thistle2 => new Color(238, 210, 238);
        public static Color Thistle3 => new Color(205, 181, 205);
        public static Color Thistle4 => new Color(139, 123, 139);
        public static Color Thistle => new Color(216, 191, 216);
        public static Color Tomato2 => new Color(238, 92, 66);
        public static Color Tomato3 => new Color(205, 79, 57);
        public static Color Tomato4 => new Color(139, 54, 38);
        public static Color Tomato => new Color(255, 99, 71);
        public static Color Turquoise1 => new Color(0, 245, 255);
        public static Color Turquoise2 => new Color(0, 229, 238);
        public static Color Turquoise3 => new Color(0, 197, 205);
        public static Color Turquoise4 => new Color(0, 134, 139);
        public static Color TurquoiseBlue => new Color(0, 199, 140);
        public static Color Turquoise => new Color(64, 224, 208);
        public static Color Violet => new Color(238, 130, 238);
        public static Color VioletRed1 => new Color(255, 62, 150);
        public static Color VioletRed2 => new Color(238, 58, 140);
        public static Color VioletRed3 => new Color(205, 50, 120);
        public static Color VioletRed4 => new Color(139, 34, 82);
        public static Color VioletRed => new Color(208, 32, 144);
        public static Color WarmGrey => new Color(128, 128, 105);
        public static Color Wheat1 => new Color(255, 231, 186);
        public static Color Wheat2 => new Color(238, 216, 174);
        public static Color Wheat3 => new Color(205, 186, 150);
        public static Color Wheat4 => new Color(139, 126, 102);
        public static Color Wheat => new Color(245, 222, 179);
        public static Color White => new Color(255, 255, 255);
        public static Color Whitesmoke => new Color(245, 245, 245);
        public static Color WoodYellow => new Color(0xf2, 0xe7, 0xc4);
        public static Color Yellow2 => new Color(238, 238, 0);
        public static Color Yellow3 => new Color(205, 205, 0);
        public static Color Yellow4 => new Color(139, 139, 0);
        public static Color Yellow => new Color(255, 255, 0);
    }
}