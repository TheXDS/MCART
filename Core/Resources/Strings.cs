﻿//
//  Strings.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
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
#pragma warning disable CS1591 // TODO: agregar comentarios XML para las cadenas.
namespace MCART.Resources
{
    public static class Strings
    {
        #region Composición de texto
        private const string about = "Acerca de...";
        private const string AboutX = "Acerca de {0}";
        private const string IncludesX = "Incluye {0}";
        private const string IncludeX = "Incluya {0}";
        private const string OkX = "✓ {0}";
        private const string WarnX = @"/!\ {0}";
        private const string xCannotBeY = "{0} no puede ser {1}";

        /// <summary>
        /// Muestra una cadena con el texto "Acerca de {0}"
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>Un texto con el formato "Acerca de {0}"</returns>
        public static string About(string text) => string.Format(AboutX, text);
        /// <summary>
        /// Muestra una cadena con el texto "Acerca de..."
        /// </summary>
        /// <returns>El texto "Acerca de..."</returns>
        public static string About() => about;
        /// <summary>
        /// Muestra una cadena indicando que algo ocurrió correctamente.
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>Un texto con el formato "✓ {0}"</returns>
        /// <remarks>El texto generado por esta función podría no verse correctamente en un programa, consola o terminal que no sea Unicode.</remarks>
        public static string Ok(string text) => string.Format(OkX, text);
        /// <summary>
        /// Muestra una cadena con el formato de advertencia.
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>Un texto con el formato "/!\ {0}"</returns>
        public static string Warn(string text) => string.Format(WarnX, text);
        public static string Includes(string text) => string.Format(IncludesX, text);
        public static string Include(string text) => string.Format(IncludeX, text);
        /// <summary>
        /// Muestra una cadena con el texto "{0} no puede ser {1}".
        /// </summary>
        /// <param name="x">Elemento {0}</param>
        /// <param name="y">Elemento {1}</param>
        /// <returns>Un texto con el formato "{0} no puede ser {1}".</returns>
        public static string XCannotBeY(string x, string y) => string.Format(xCannotBeY, x, y);
        #endregion

        public const string Abort = "Abortar";
        public const string AllFiles = "Todos los archivos";
        public const string Alpha = "abcdefghijklmnopqrstuvwxyz";
        public const string Assmblies = "Ensamblados";
        public const string Bsy = "Ocupado";
        public const string BtnDel = "Eliminar";
        public const string BtnEdit = "Editar";
        public const string BtnNew = "Nuevo";
        public const string BtnSave = "Guardar";
        public const string CantCreateInstance = "No se pudo crear una nueva instancia del tipo {0}";
        public const string Chars = @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM,./;'][\=-1234567890~!@#$%^&*()_+|}{"":?><";
        public const string CINotSupported = "Las implementaciones personalizadas de {0} no están soportadas.";
        public const string CldntConnect = "No fue posible conectarse a {0}";
        public const string ClosdConn = "La conexión está cerrada";
        public const string Clse = "Cerrar";
        public const string Cncl = "Cancelar";
        public const string Copyright = "Copyright";
        public const string CorruptData = "Se han recibido datos corruptos";
        public const string CtrlCCancel = "Presione Ctrl+C para cancelar";
        public const string DataNotFound = "No se han encontrado los datos";
        public const string DBDoesntBelong = "La base de datos no pertenece a este programa";
        public const string Descr = "Descripción";
        public const string DirectoryIsFull = "El directorio está lleno";
        public const string Err = "Error";
        public const string ExcDoingX = "Se produjo una excepción realizando {0}";
        public const string FeatNotAvailable = "Característica no disponible";
        public const string FormatNotSupported = "El formato {0} no está soportado.";
        public const string FrmIncorreclyOpened = "Este formulario fue abierto de la manera incorrecta. Refiérase a la documentación de MCART para resolver este problema. Si ve este mensaje de error, póngase en contacto con el desarrollador de la aplicación.";
        public const string GUITest = "Prueba de interfaz gráfica";
        public const string HasUI = "Incluye UI";
        public const string HelpDir = @"\Help";
        public const string ImplInterfaces = "Interfaces implementadas";
        public const string IncorrectPWD = "La contraseña es incorrecta";
        public const string InterfaceExpected = "Se esperaba una interfaz";
        public const string InvalidDB = "El arhivo no contiene una base de datos válida";
        public const string InvalidPluginClass = "{0} no es una clase de Plugin válida ";
        public const string IsBeta = "Versión Beta";
        public const string IsUnsafe = "Inseguro (NO UTILIZAR)";
        public const string ItsLong = "Es muy larga";
        public const string LatinChars = "áéíóúüñäåöàèìòù";
        public const string LstEmpty = "La lista está vacía";
        public const string MandatoryField = "Este campo es obligatorio";
        public const string MCADBObjNotFound = "El objeto de tipo {0} con Uid '{1}' no existe en la base de datos";
        public const string Menu = "Menú";
        public const string MinMCARTV = "Versión mínima de MCART";
        public const string MissingArgument = "No se ha encontrado el argumento {0}";
        public const string MoreChars = @"ẁèỳùìòàǜǹẀÈỲÙÌÒÀǛǸẽẼỹỸũŨĩĨõÕãÃṽṼñÑ`~1!¡¹2@²űŰőŐ˝3#ĒǕŌĀ¯4$¤£5%€şḑģḩķļçņȩŗţ¸6ŵêŷûîôâŝĝĥĵẑĉŴÊŶÛÎÔÂŜĜĤĴẐĈ¼^7&ươƯƠ̛8*¾ęųįǫąĘŲĮǪĄ˛9(‘ĕŭĭŏăĔŬĬŎĂ˘0)’ẘẙůŮåÅ-_¥ẉẹṛṭỵụịọạṣḍḥḳḷṿḅṇṃ̣=+×÷qQwWeErR®tTþÞyYuUiIoOpP[{«“]}»”\|¬¦aAsSß§dDðÐfFgGhHjJkKœŒlLøØ;:¶°ẃéŕýúíóṕáśǵḱĺźćǘńḿ'ẅëẗÿüïöäḧẍẄËŸÜÏÖÄḦẌ""zZæÆxXcC©¢vVbBnNmMµ,<çÇ.>ˇěřťǔǐǒǎšďǧȟǰǩǩľžčǚň/?¿ʠⱳẻƭỷủỈỏƥảʂɗƒɠɦƙȥƈʋɓɲɱ̉";
        public const string MoreSymbs = "¡²³¤€¼½¾‘’¥×»«¬´¶¿Ç®ÞßÐØÆ©µ¹£÷¦¨°çþ§";
        public const string Needed = "necesario";
        public const string NewReg = "* Nuevo registro";
        public const string Nme = "Nombre";
        public const string NoData = "No hay datos";
        public const string NoLicense = "(sin licencia)";
        public const string NoRules = "No hay reglas activas";
        public const string NotEnoughData = "Los datos son insuficientes";
        public const string NotImplemented = "Función no implementada";
        public const string Numbers = "0123456789";
        public const string of_ = "de";
        public const string OfX = " de {0}";
        public const string OK = "Aceptar";
        public const string PluginDetails = "Detalles de plugins";
        public const string PluginDidntInit = "El plugin no se inicializó";
        public const string PluginNeeded = "Se necesita un Plugin";
        public const string PluginsInfo = "Información de plugins";
        public const string Plugins = "Plugins";
        public const string PlWait = "Por favor, espere...";
        public const string PwdConfirm = "Confirme la contraseña";
        public const string Pwd = "Contraseña";
        public const string PwdHint = "Indicio de contraseña";
        public const string PwNeeded = "Debe especificar una contraseña";
        public const string PwNotMatch = "Las contraseñas no coinciden.";
        public const string PwNtbmc = "La contraseña debe ser más compleja.";
        public const string PwTooLong = "La contraseña es excesivamente larga.";
        public const string PwTooShort = "La contraseña es muy corta.";
        public const string Rdy = "Listo";
        public const string search = "Buscar...";
        public const string Security = "Seguridad";
        public const string SetX = "Establecer {0}";
        public const string Specified = "especificado";
        public const string StackUnderflow = "Subflujo de pila";
        public const string Symbols = "`~!@#$%^&*()-_=+\\|]}[{'\";:/?.>,<";
        public const string TargetMCARTV = "Versión destino de MCART";
        public const string TestUI = "Probar UI...";
        public const string TheArgument = "El argumento";
        public const string TheAssembly = "El ensamblado";
        public const string TheClass = "La clase";
        public const string TheClient = "El cliente";
        public const string TheEvent = "El evento";
        public const string TheFile = "El archivo";
        public const string TheFunc = "La función";
        public const string TheGraphView = "El controlador de gráficas";
        public const string TheInterface = "La interfaz";
        public const string TheLicense = "La licencia";
        public const string TheObj = "El objeto";
        public const string ThePlugin = "El Plugin";
        public const string TheSrv = "El servidor";
        public const string TheTask = "La tarea";
        public const string TheType = "El tipo";
        public const string TheUid = "El Uid";
        public const string TheUsr = "El usuario";
        public const string TooFewArguments = "Muy pocos argumentos";
        public const string TooManyArguments = "Demasiados argumentos";
        public const string Total = "Total:";
        public const string Unk = "Desconocido";
        public const string UnknownType = "Tipo de objeto desconocido.";
        public const string UnspecLicense = "No se ha especificado una licencia, o la misma no ha sido incluída.";
        public const string UnsupportedVer = "Versión no soportada.";
        public const string Updt = "Actualizar";
        public const string UsrCncl = "Operación cancelada por el usuario.";
        public const string Usr = "Usuario";
        public const string Version = "MCART {0}";
        public const string Ver = "Versión";
        public const string XAlreadyExists = "{0} ya existe.";
        public const string XCncl = "Operación cancelada por {0}";
        public const string XDisabled = "{0} se ha deshabilitado";
        public const string XDoesntContainY = "{0} no contiene {1}";
        public const string XFoundError = "{0} ha encontrado un error";
        public const string XInvalid = "{0} no es válido";
        public const string XMissing = "Falta {0}";
        public const string XNotCancellable = "{0} no es cancelable.";
        public const string XNotFound = "{0} no ha sido encontrado.";
        public const string XNotIncluded = "{0} no se ha incluído.";
        public const string XNotInit = "{0} no se ha inicializado aún.";
        public const string XNotSelected = "No se ha seleccionado {0}.";
        public const string XReturnedInvalid = "{0} ha devuelto valores inválidos.";
        public const string XYQuotes = "{0} '{1}'";
    }
}
