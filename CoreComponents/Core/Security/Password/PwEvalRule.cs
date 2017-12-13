//
//  PwEvalRule.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using MCART.Attributes;
using System;
using System.Security;
using St = MCART.Resources.Strings;

namespace MCART.Security.Password
{
    /// <summary>
    /// Define una regla de evaluación de contraseñas. Esta clase no puede heredarse.
    /// </summary>
    public sealed class PwEvalRule
    {
        PonderationLevel pond;
        readonly PwEvalFunc func;
        /// <summary>
        /// Activa o desactiva esta regla de evaluación.
        /// </summary>
        public bool Enable;
        /// <summary>
        /// Obtiene o establece un valor que determina si los puntos otorgados
        /// por esta regla serán parte del total, o si son puntos extra.
        /// </summary>
        public bool IsExtraPoints;
        /// <summary>
        /// Obtiene el nombre de esta regla de evaluación.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// Obtiene una descripción de esta regla de evaluación.
        /// </summary>
        public readonly string Description;
        /// <summary>
        /// Obtiene o establece el valor de ponderación de la regla.
        /// </summary>
        /// <value>Valor de ponderación de la regla.</value>
        public PonderationLevel Ponderation
        {
            get => pond;
            set
            {
                if (!typeof(PonderationLevel).IsEnumDefined(value))
                    throw new ArgumentOutOfRangeException(
                        nameof(Ponderation),
                        St.XCannotBeY(nameof(Ponderation), value.ToString()));
                if (value == PonderationLevel.None) Enable = false;
                else pond = value;
            }
        }
        /// <summary>
        /// Ejecuta la evaluación de este <see cref="PwEvalRule"/>.
        /// </summary>
        /// <param name="pwToEval">Contraseña a evaluar.</param>
        /// <returns></returns>
        public PwEvalResult Eval(SecureString pwToEval)
        {
            if (!Enable) throw new InvalidOperationException(
                St.XDisabled(St.TheObj));
            PwEvalResult k = func(pwToEval);
            if (!k.Result.IsBetween(0.0f, 1.0f)) throw new
                 Exceptions.InvalidReturnValueException(func, k.Result);
            k.Result *= (float)pond;
            return k;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PwEvalRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="ponderation">Ponderación a aplicar.</param>
        /// <param name="description">Descripción de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <c>true</c>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        /// <param name="isExtra">
        /// Si se establece en <c>true</c>, el resultado de esta regla se
        /// tomará en cuenta como puntos extra.
        /// </param>
        public PwEvalRule(
            PwEvalFunc evalFunc,
            string name,
            PonderationLevel ponderation = PonderationLevel.Normal,
            string description = null,
            bool defaultEnable = true,
            bool isExtra = false)
        {
            if (evalFunc.IsNull()) throw new ArgumentNullException(nameof(evalFunc));
            if (!typeof(PonderationLevel).IsEnumDefined(ponderation))
                throw new ArgumentOutOfRangeException(
                    nameof(ponderation),
                    St.XCannotBeY(nameof(ponderation), ponderation.ToString()));
            func = evalFunc;
            Name = name;
            pond = ponderation;
            Description = description;
            Enable = defaultEnable;
            IsExtraPoints = isExtra;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PwEvalRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="ponderation">Ponderación a aplicar.</param>
        /// <param name="description">Descripción de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <c>true</c>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        /// <param name="isExtra">
        /// Si se establece en <c>true</c>, el resultado de esta regla se
        /// tomará en cuenta como puntos extra.
        /// </param>
        public PwEvalRule(
            PwEvalFunc evalFunc,
            NameAttribute name,
            PonderationAttribute ponderation,
            DescriptionAttribute description = null,
            DefaultEnableAttribute defaultEnable = null,
            ExtraPointsAttribute isExtra = null)
        {
            if (evalFunc.IsNull()) throw new ArgumentNullException(nameof(evalFunc));
            if (!typeof(PonderationLevel).IsEnumDefined(ponderation.Value))
                throw new ArgumentOutOfRangeException(
                    nameof(ponderation),
                    St.XCannotBeY(nameof(ponderation), ((PonderationLevel)ponderation.Value).ToString()));
            func = evalFunc;
            Name = name?.Value ?? func.Method.Name;
            pond = (PonderationLevel)(ponderation?.Value ?? (int?)PonderationLevel.Normal);
            Description = description?.Value ?? string.Empty;
            Enable = defaultEnable?.Value ?? true;
            IsExtraPoints = !isExtra.IsNull();
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PwEvalRule"/>.
        /// </summary>
        /// <param name="evalFunc">
        /// Función de evaluación. Debería contener todas sus propiedades
        /// indicadas como atributos.
        /// </param>
        public PwEvalRule(PwEvalFunc evalFunc)
        {
            if (evalFunc.IsNull()) throw new ArgumentNullException(nameof(evalFunc));
            func = evalFunc;
            Name = Objects.GetAttr<NameAttribute>(func)?.Value ?? func.Method.Name;
            pond = (PonderationLevel)(Objects.GetAttr<PonderationAttribute>(func)?.Value ?? (int?)PonderationLevel.Normal);
            if (!typeof(PonderationLevel).IsEnumDefined(pond))
                throw new ArgumentOutOfRangeException(
                    nameof(PonderationAttribute),
                    St.XCannotBeY(nameof(PonderationAttribute), pond.ToString()));
            Description = Objects.GetAttr<DescriptionAttribute>(func)?.Value ?? string.Empty;
            Enable = Objects.GetAttr<DefaultEnableAttribute>(func)?.Value ?? true;
            IsExtraPoints = !Objects.GetAttr<ExtraPointsAttribute>(func).IsNull();
        }
    }
}