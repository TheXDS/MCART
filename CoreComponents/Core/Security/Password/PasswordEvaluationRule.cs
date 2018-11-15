/*
PasswordEvaluationRule.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

using TheXDS.MCART.Attributes;
using System;
using System.Security;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Define una regla de evaluación de contraseñas. Esta clase no puede heredarse.
    /// </summary>
    public sealed class PasswordEvaluationRule
    {
        PonderationLevel pond;
        readonly PwEvalFunc func;
        /// <summary>
        /// Activa o desactiva esta regla de evaluación.
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// Obtiene o establece un valor que determina si los puntos otorgados
        /// por esta regla serán parte del total, o si son puntos extra.
        /// </summary>
        public bool IsExtraPoints { get; set; }
        /// <summary>
        /// Obtiene el nombre de esta regla de evaluación.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Obtiene una descripción de esta regla de evaluación.
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// Obtiene o establece el valor de ponderación de la regla.
        /// </summary>
        /// <value>Valor de ponderación de la regla.</value>
#if !CLSCompliance && PreferExceptions
        [CLSCompliant(false)]
#endif
        public PonderationLevel Ponderation
        {
            get => pond;
            set
            {
#if !CLSCompliance && PreferExceptions
                if (!typeof(PonderationLevel).IsEnumDefined(value))
                    throw new ArgumentOutOfRangeException(
                        nameof(Ponderation),
                        St.XCannotBeY(nameof(Ponderation), value.ToString()));
#endif
                if (value == PonderationLevel.None) Enable = false;
                else pond = value;
            }
        }
        /// <summary>
        /// Ejecuta la evaluación de este <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="pwToEval">Contraseña a evaluar.</param>
        /// <returns>
        /// Un <see cref="PwEvalResult"/> con el resultado de la evaluación de
        /// la contraseña por parte de este <see cref="PasswordEvaluationRule"/>.
        /// </returns>
        public PwEvalResult Eval(SecureString pwToEval)
        {
            if (!Enable) throw new InvalidOperationException(St.XDisabled(St.TheObj));
            PwEvalResult k = func(pwToEval);
            if (!k.Result.IsBetween(0.0f, 1.0f)) throw new Exceptions.InvalidReturnValueException(func, k.Result);
            k.Result *= (float)pond;
            return k;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, string name) : this(evalFunc, name, null, PonderationLevel.Normal, true, false) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="description">Descripción de la regla.</param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, string name, string description) : this(evalFunc, name, description, PonderationLevel.Normal, true, false) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="ponderation">Ponderación a aplicar.</param>
        /// <param name="description">Descripción de la regla.</param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, string name, string description, PonderationLevel ponderation) : this(evalFunc, name, description, ponderation, true, false) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="description">Descripción de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, string name, string description, bool defaultEnable) : this(evalFunc, name, description, PonderationLevel.Normal, defaultEnable, false) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="description">Descripción de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        /// <param name="isExtra">
        /// Si se establece en <see langword="true"/>, el resultado de esta regla se
        /// tomará en cuenta como puntos extra.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, string name, string description, bool defaultEnable, bool isExtra) : this(evalFunc, name, description, PonderationLevel.Normal, defaultEnable, isExtra) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="ponderation">Ponderación a aplicar.</param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, string name, PonderationLevel ponderation) : this(evalFunc, name, null, ponderation, true, false) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="ponderation">Ponderación a aplicar.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, string name, PonderationLevel ponderation, bool defaultEnable) : this(evalFunc, name, null, ponderation, defaultEnable, false) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="ponderation">Ponderación a aplicar.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        /// <param name="isExtra">
        /// Si se establece en <see langword="true"/>, el resultado de esta regla se
        /// tomará en cuenta como puntos extra.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, string name, PonderationLevel ponderation, bool defaultEnable, bool isExtra) : this(evalFunc, name, null, ponderation, defaultEnable, isExtra) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, string name, bool defaultEnable) : this(evalFunc, name, null, PonderationLevel.Normal, defaultEnable, false) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        /// <param name="isExtra">
        /// Si se establece en <see langword="true"/>, el resultado de esta regla se
        /// tomará en cuenta como puntos extra.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, string name, bool defaultEnable, bool isExtra) : this(evalFunc, name, null, PonderationLevel.Normal, defaultEnable, isExtra) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="description">Descripción de la regla.</param>
        /// <param name="ponderation">Ponderación a aplicar.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        /// <param name="isExtra">
        /// Si se establece en <see langword="true"/>, el resultado de esta regla se
        /// tomará en cuenta como puntos extra.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, string name, string description, PonderationLevel ponderation, bool defaultEnable, bool isExtra)
        {
            if (evalFunc is null) throw new ArgumentNullException(nameof(evalFunc));
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
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, NameAttribute name) : this(evalFunc, name, null, null, null, null) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="description">Descripción de la regla.</param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, NameAttribute name, DescriptionAttribute description) : this(evalFunc, name, description, null, null, null) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="ponderation">Ponderación a aplicar.</param>
        /// <param name="description">Descripción de la regla.</param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, NameAttribute name, DescriptionAttribute description, PonderationAttribute ponderation) : this(evalFunc, name, description, ponderation, null, null) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="description">Descripción de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, NameAttribute name, DescriptionAttribute description, DefaultEnableAttribute defaultEnable) : this(evalFunc, name, description, null, defaultEnable, null) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="description">Descripción de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        /// <param name="isExtra">
        /// Si se establece en <see langword="true"/>, el resultado de esta regla se
        /// tomará en cuenta como puntos extra.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, NameAttribute name, DescriptionAttribute description, DefaultEnableAttribute defaultEnable, ExtraPointsAttribute isExtra) : this(evalFunc, name, description, null, defaultEnable, isExtra) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="ponderation">Ponderación a aplicar.</param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, NameAttribute name, PonderationAttribute ponderation) : this(evalFunc, name, null, ponderation, null, null) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="ponderation">Ponderación a aplicar.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, NameAttribute name, PonderationAttribute ponderation, DefaultEnableAttribute defaultEnable) : this(evalFunc, name, null, ponderation, defaultEnable, null) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="ponderation">Ponderación a aplicar.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        /// <param name="isExtra">
        /// Si se establece en <see langword="true"/>, el resultado de esta regla se
        /// tomará en cuenta como puntos extra.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, NameAttribute name, PonderationAttribute ponderation, DefaultEnableAttribute defaultEnable, ExtraPointsAttribute isExtra) : this(evalFunc, name, null, ponderation, defaultEnable, isExtra) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, NameAttribute name, DefaultEnableAttribute defaultEnable) : this(evalFunc, name, null, null, defaultEnable, null) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        /// <param name="isExtra">
        /// Si se establece en <see langword="true"/>, el resultado de esta regla se
        /// tomará en cuenta como puntos extra.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, NameAttribute name, DefaultEnableAttribute defaultEnable, ExtraPointsAttribute isExtra) : this(evalFunc, name, null, null, defaultEnable, isExtra) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">Función de evaluación.</param>
        /// <param name="name">Nombre de la regla.</param>
        /// <param name="description">Descripción de la regla.</param>
        /// <param name="ponderation">Ponderación a aplicar.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla estará activa de forma
        /// predeterminada.
        /// </param>
        /// <param name="isExtra">
        /// Si se establece en <see langword="true"/>, el resultado de esta regla se
        /// tomará en cuenta como puntos extra.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc, NameAttribute name, DescriptionAttribute description, PonderationAttribute ponderation, DefaultEnableAttribute defaultEnable, ExtraPointsAttribute isExtra)
        {
            if (evalFunc is null) throw new ArgumentNullException(nameof(evalFunc));
            if (!typeof(PonderationLevel).IsEnumDefined(ponderation.Value))
                throw new ArgumentOutOfRangeException(
                    nameof(ponderation),
                    St.XCannotBeY(nameof(ponderation), ((PonderationLevel)ponderation.Value).ToString()));
            func = evalFunc;
            Name = name?.Value ?? func.Method.Name;
            pond = (PonderationLevel)(ponderation?.Value ?? (int?)PonderationLevel.Normal);
            Description = description?.Value ?? string.Empty;
            Enable = defaultEnable?.Value ?? true;
            IsExtraPoints = !(isExtra is null);
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordEvaluationRule"/>.
        /// </summary>
        /// <param name="evalFunc">
        /// Función de evaluación. Debería contener todas sus propiedades
        /// indicadas como atributos.
        /// </param>
        public PasswordEvaluationRule(PwEvalFunc evalFunc) : this(evalFunc, evalFunc.GetAttr<NameAttribute>(), evalFunc.GetAttr<DescriptionAttribute>(), evalFunc.GetAttr<PonderationAttribute>(), evalFunc.GetAttr<DefaultEnableAttribute>(), evalFunc.GetAttr<ExtraPointsAttribute>()) { }
    }
}