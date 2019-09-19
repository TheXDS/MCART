/*
CmdLineParser.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using TheXDS.MCART.Comparison;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.StringExtensions;

namespace TheXDS.MCART.Component
{


    /// <summary>
    ///     Clase que permite administrar y exponer de forma intuitiva las
    ///     opciones de línea de comandos con las que se inicia una aplicación.
    /// </summary>
    public sealed class CmdLineParser: ICmdLineParser
    {
        private static readonly List<Argument> _allArguments = Objects.FindAllObjects<Argument>().ToList();
        private readonly HashSet<Argument> _args = new HashSet<Argument>(new TypeComparer());
        private readonly List<string> _commands = new List<string>();
        private readonly List<string> _invalid = new List<string>();

        private bool Append(Argument v, string? value)
        {
            switch (v.Kind)
            {
                case Argument.ValueKind.Flag:
                    if (value != null) return false;
                    break;
                case Argument.ValueKind.Optional:
                    if (value != null) v.Value = value;
                    break;
                case Argument.ValueKind.ValueRequired:
                case Argument.ValueKind.Required:
                    if (value is null) return false;
                    v.Value = value;
                    break;
            }
            _args.Add(v);
            return true;
        }
        private bool? TryLong(string j, string marker)
        {
            if (j.StartsWith(marker) && j.Length > marker.Length)
            {
                var tokens = j.Split(new[] { '=', ':' }, 2);
                var arg = tokens[0].Substring(marker.Length).ToLower();
                if (AvailableArguments.FirstOrDefault(p => p.LongName.ToLower() == arg) is Argument v)
                {
                    return Append(v, tokens.Length == 2 ? tokens[1] : null);
                }
            }
            return null;
        }
        private bool? TryShortGroup(string j, string marker)
        {
            if (j.StartsWith(marker) && j.Length > 1)
            {
                foreach (var p in AvailableArguments)
                {
                    if (p.ShortName.HasValue && (p.Kind == Argument.ValueKind.Flag || p.Kind == Argument.ValueKind.Optional) && j.Contains(p.ShortName.Value))
                    {
                        _args.Add(p);
                        j = j.Replace(p.ShortName.ToString(), string.Empty);
                    }
                }
                return j.Length == 0;
            }
            return null;
        }
        private bool? TryShort(string j, string marker)
        {
            if (j.StartsWith(marker) && j.Length > 1)
            {
                if (AvailableArguments.FirstOrDefault(p => p.ShortName == j[1]) is Argument v)
                {
                    return Append(v, j.Length > 2 ? j.Substring(2) : null);
                }
            }
            return null;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="CmdLineParser"/>.
        /// </summary>
        public CmdLineParser() : this(Environment.GetCommandLineArgs())
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="CmdLineParser"/>.
        /// </summary>
        /// <param name="cmdLine">
        ///     Argumentos de lanzamiento de la aplicación.
        /// </param>
        public CmdLineParser(string cmdLine) : this(cmdLine.Split(' '))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="CmdLineParser"/>.
        /// </summary>
        /// <param name="args">
        ///     Argumentos de lanzamiento de la aplicación.
        /// </param>
        public CmdLineParser(string[] args) : this(_allArguments.AsReadOnly(), args)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="CmdLineParser"/>.
        /// </summary>
        /// <param name="arguments">
        ///     Colección de argumentos aceptados.
        /// </param>
        /// <param name="args">
        ///     Argumentos de lanzamiento de la aplicación.
        /// </param>
        public CmdLineParser(IEnumerable<Argument> arguments, string[] args)
        {
            AvailableArguments = arguments;
            foreach (var j in args)
            {
                var l =
                    TryLong(j, "--") ??
                    TryShortGroup(j, "-") ??
                    TryShort(j, "-") ??
                    (Environment.OSVersion.Platform == PlatformID.Win32NT ? (TryLong(j, "/") ?? TryShort(j, "/")) : null);
                if (l is null)
                {
                    _commands.Add(j);
                }
                else if (!l.Value)
                {
                    _invalid.Add(j);
                }
            }
        }

        /// <summary>
        ///     Registra un nuevo argumento para obtener desde los argumentos
        ///     de la línea de comandos de esta aplicación.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de argumento a registrar.
        /// </typeparam>
        public static void Register<T>() where T : Argument, new()
        {
            _allArguments.Add(new T());
        }

        /// <summary>
        ///     Obtiene un valor que indica si un argumento ha sido
        ///     especificado.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo que describe al argumento.
        /// </typeparam>
        /// <returns>
        ///     <see langword="true"/> si el argumento ha sido especificado en
        ///     la línea de comandos, <see langword="false"/> en caso
        ///     contrario.
        /// </returns>
        public bool IsPresent<T>() where T : Argument, new()
        {
            return _args.Any(p => p.GetType() == typeof(T));
        }

        /// <summary>
        ///     Obtiene el valor especificado en la línea de comandos para el
        ///     argumento.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo que describe al argumento para el cual se obtendrá el
        ///     valor especificado en la línea de comandos.
        /// </typeparam>
        /// <returns>
        ///     El valor del argumento, <see cref="string.Empty"/> si el
        ///     argumento no expone un valor, o <see langword="null"/>  si el
        ///     argumento no fue especificado en la línea de comandos.
        /// </returns>
        public string? Value<T>() where T : Argument, new()
        {
            var arg = _args.FirstOrDefault(p => p.GetType() == typeof(T));
            if (arg is null) return null;
            return arg.Value ?? arg.Default?.OrEmpty();
        }

        /// <summary>
        ///     Obtiene uns instancia presente de argumento.
        /// </summary>
        /// <typeparam name="T">
        ///     Argumento a obtener.
        /// </typeparam>
        /// <returns>
        ///     Una instancia de argumento especificado en la línea de
        ///     comandos, o <see langword="null"/> si el argumento no ha sido
        ///     especificado.
        /// </returns>
        public T? Arg<T>() where T : Argument, new() => _args.OfType<T>().FirstOrDefault();

        /// <summary>
        ///     Enumera las opciones inválidas especificadas.
        /// </summary>
        public IEnumerable<string> Invalid => _invalid.AsReadOnly();

        /// <summary>
        ///     Enumera los argumentos obligatorios que hagan falta en la línea de comandos.
        /// </summary>
        public IEnumerable<Argument> Missing
        {
            get
            {
                foreach (var j in AvailableArguments.Where(p => p.Kind == Argument.ValueKind.Required))
                {
                    if (!_args.Any(p => p.GetType() == j.GetType()))
                    {
                        yield return j;
                    }
                }
            }
        }

        /// <summary>
        ///     Enumera todos los argumentos disponibles para ser procesados
        ///     por este <see cref="CmdLineParser"/>.
        /// </summary>
        public IEnumerable<Argument> AvailableArguments { get; private set; }

        /// <summary>
        ///     Enumera los argumentos presentes en la línea de comandos.
        /// </summary>
        public IEnumerable<Argument> Present => _args;

        /// <summary>
        ///     Enumera los comandos que han sido pasados como argumentos.
        /// </summary>
        public IEnumerable<string> Commands => _commands.AsReadOnly();

        /// <summary>
        ///     Convierte implícitamente un arreglo de <see cref="string"/> en
        ///     un <see cref="CmdLineParser"/>.
        /// </summary>
        /// <param name="args">
        ///     Argumentos de línea de comandos pasados a la aplicación.
        /// </param>
        public static implicit operator CmdLineParser(string[] args) => new CmdLineParser(args);
    }
}
