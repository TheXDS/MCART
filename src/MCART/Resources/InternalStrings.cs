/*
InternalStrings.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

using System;

namespace TheXDS.MCART.Resources
{
#pragma warning disable CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente
    /// <summary>
    /// Contiene cadenas específicas para uso interno de MCART.
    /// </summary>
    public static class InternalStrings
    {
        public const string GpVNoData = " (Sin datos)";
        public const string NullPwEvalRule = "Regla nula";
        public const string NullPwEvalRule2 = "Esta regla no evaluará la contraseña, sino que devolverá un valor constante que ayuda a balancear el puntaje de otras reglas en el total.";
        public const string ObsoleteCheckDoc = "Este método no está soportado. Por favor, lea la documentación de MCART acerca de esta clase.";
        public const string PwLatinEvalRule = "Caracteres latinos";
        public const string PwLcaseEvalRule = "Minúsculas";
        public const string PwLenghtEvalRule = "Longitud de contraseña";
        public const string PwLenghtEvalRule2 = "Esta regla evalúa la longitud de la contraseña, para asegurarse que contenga de {0} a {1} caracteres.";
        public const string PwNumbersEvalRule = "Números";
        public const string PwOtherSymbsEvalRule = "Otros símbolos";
        public const string PwOtherUTFEvalRule = "Otros caracteres Unicode";
        public const string PwSymbolsEvalRule = "Símbolos";
        public const string PwUcaseEvalRule = "Mayúsculas";
        public const string xBuilder = "Esta regla evalúa que la contraseña contenga {0}";

        public const string SafePw = "Contraseña segura";
        public const string ComplexPw = "Contraseña compleja";
        public const string PinNumber = "Número PIN";
        public const string CryptoPw = "Contraseña criptográfica";
        public const string User = "Usuario";
        public const string Password = "Contraseña";
        public const string ConfirmPassword = "Confirmar contraseña";
        public const string PasswordHint = "Indicio de contraseña";
        public const string EmptyPassword = "Introduzca una contraseña para continuar.";
        public const string CommonEvaluator = "Evaluador de reglas comunes.";
        public const string CommonEvaluatorDesc = "Se evalúa la contraseña contra reglas comunmente utilizadas, como ser longitud, uso de mayúsculas y minúsculas, letras y símbolos.";
        public const string AvoidCommonPasswords = "Evite el uso de contraseñas comunes.";
        public const string AvoidYears = "Evite utilizar el año actual en la contraseña.";
        public const string ExtendedEvaluator = "Evaluador extendido de contraseñas";
        public const string ComplexEvaluator = "Evaluador complejo de contraseñas";

        public const string TypeName = "Nombre del tipo";
        public const string Namespace = "Espacio de nombres";
        public const string DeclaringAssembly = "Ensamblado de declaración";
        public const string PublicMembers = "Miembros públicos";
        public const string PublicInstanceMembers = "Miembros públicos de instancia";
        public const string PublicStaticMembers = "Miembros públicos estáticos";
        public const string Methods = "Métodos";
        public const string Properties = "Propiedades";
        public const string Interfaces = "Interfaces implementadas";
        public const string BaseTypes = "Tipos base";
        public const string DynamicType = "Tipo definido en tiempo de ejecución";
        public const string DefaultValue = "Valor predeterminado";

        public const string Instantiable = "Tipo instanciable";
        public const string IsStatic = "Clase estática";
        public const string Serializable = "Tipo serializable";
        public const string GenericArgs = "Argumentos genéricos";
        public const string GenericType = "Tipo genérico";
        public const string IsGenericBuilt = "Tipo genérico construido";
        public const string Abstract = "Tipo abstracto";
        public const string IsSealed = "Tipo sellado";
        public const string IsPrimitive = "Tipo primitivo";
        public const string IsValueType = "Tipo de valor";
        public const string IsInterface = "Es una interfaz";
        public const string IsClass = "Es una clase";
        public const string IsEnum = "Es una enumeración";
        public const string NewInstance = "Valor de nueva instancia";
        public const string NoInfoExposed = "La aplicación actual no expone información de identificación.";

        public const string Bytes = "Bytes";
        public const string KB = "KB";
        public const string MB = "MB";
        public const string GB = "GB";
        public const string TB = "TB";
        public const string PB = "PB";
        public const string EB = "EB";
        public const string ZB = "ZB";
        public const string YB = "YB";
        public const string KiB = "KiB";
        public const string MiB = "MiB";
        public const string GiB = "GiB";
        public const string TiB = "TiB";
        public const string PiB = "PiB";
        public const string EiB = "EiB";
        public const string ZiB = "ZiB";
        public const string YiB = "YiB";

        public const string ErrorShowingPluginInfo = "Error mostrando la información del plugin.";
        public const string LegacyComponent = "Este es un componente legado.";
        public const string ErrorCircularOperationDetected = "Operación circular detectada.";
        public const string ErrorEnumerableTypeExpected = "Se esperaba un tipo de colección enumerable.";

        public static string ErrorXAlreadyRegistered(string x) => $"{x} ya ha sido registrado(a).";
        public static string ErrorXIsReadOnly(string x) => $"{x} es de sólo lectura.";
        public static string ErrorXClassNotInstantiableWithArgs(string x) => $"La clase '{x}' no pudo ser instanciada con los parámetros de constructor especificados.";
        public static string UnkErrLoadingRes(string res, string ex) => $"Error desconocido al cargar recurso {res}\n{ex}";
        public static string ErrorDeclMustHaveGuidAttr(Type o) => $"La declaración del tipo {o.Name} requiere un atributo de Guid.";

        public static string LegacyComponentUseInstead(object alternative) => $"{LegacyComponent} Utilice en su lugar {alternative}";
        public static string LegacyComponentUseInstead<T>() => LegacyComponentUseInstead(typeof(T));


        public const string UseLicUriInstead = "Utilice LicenseUriAttribute en su lugar.";
    }
}