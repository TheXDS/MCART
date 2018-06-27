//
//  LightChat.cs
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2017 César Morgan
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace LightChat
{
    /// <summary>
    /// Describe a un usuario registrado del protocolo 
    /// <see cref="LightChat"/>.
    /// </summary>
    public class UserRegistry
    {
        byte[] pwd;
        /// <summary>
        /// Establece el hash de contraseña del usuario.
        /// </summary>
        public byte[] Password { set => pwd = value; }
        /// <summary>
        /// Indica si este usuario ha sido baneado.
        /// </summary>
        public bool Banned;
        /// <summary>
        /// Comprueba la contraseña.
        /// </summary>
        /// <param name="pw">contraseña a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si la contraseña coincide, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public bool CheckPw(byte[] pw) => pwd == pw;
    }
}