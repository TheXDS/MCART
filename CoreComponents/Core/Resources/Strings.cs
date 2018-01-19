//
//  Strings.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
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

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Contiene cadenas de texto genéricas, además de funciones de composición de texto.
    /// </summary>
    public static class Strings
    {
        #region Composición de texto
        /// <summary>
        /// Devuelve una cadena con el texto "Acerca de
        /// {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "Acerca de {<paramref name="text"/>}".
        /// </returns>
        public static string About(string text) => $"Acerca de {text}";
        /// <summary>
        /// Devuelve una cadena con el texto "No se pudo crear una nueva
        /// instancia del tipo {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "No se pudo crear una nueva instancia del
        /// tipo {<paramref name="text"/>}".
        /// </returns>
        public static string CantCreateInstance(string text) => $"No se pudo crear una nueva instancia del tipo {text}.";
        /// <summary>
        /// Devuelve una cadena con el texto "Las implementaciones
        /// personalizadas de {<paramref name="text"/>} no están soportadas.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "Las implementaciones personalizadas de
        /// {<paramref name="text"/>} no están soportadas.".
        /// </returns>
        public static string CINotSupported(string text) => $"Las implementaciones personalizadas de {text} no están soportadas.";
        /// <summary>
        /// Devuelve una cadena con el texto "No fue posible conectarse a 
        /// {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "No fue posible conectarse a
        /// {<paramref name="text"/>}".
        /// </returns>
        public static string CldntConnect(string text) => $"No fue posible conectarse a {text}";
        /// <summary>
        /// Devuelve una cadena con el texto "Se produjo una excepción
        /// realizando {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "Se produjo una excepción realizando 
        /// {<paramref name="text"/>}".
        /// </returns>
        public static string ExcDoingX(string text) => $"Se produjo una excepción realizando {text}";
        /// <summary>
        /// Devuelve una cadena con el texto "El formato
        /// {<paramref name="text"/>} no está soportado.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "El formato {<paramref name="text"/>} no
        /// está soportado.".
        /// </returns>
        public static string FormatNotSupported(string text) => $"El formato {text} no está soportado.";
        /// <summary>
        /// Devuelve una cadena con el texto "Incluya {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "Incluya {<paramref name="text"/>}".
        /// </returns>
        public static string Include(string text) => $"Incluya {text}";
        /// <summary>
        /// Devuelve una cadena con el texto "Incluye {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "Incluye {<paramref name="text"/>}".
        /// </returns>
        public static string Includes(string text) => $"Incluye {text}";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} no es
        /// una clase de Plugin válida.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} no es una clase
        /// de Plugin válida.".
        /// </returns>
        public static string InvalidPluginClass(string text) => $"{text} no es una clase de Plugin válida.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} no
        /// tiene una firma válida.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} no tiene una
        /// firma válida.".
        /// </returns>
        public static string InvalidSignature(string text) => $"{text} no tiene una firma válida.";
        /// <summary>
        /// Devuelve una cadena con el texto "La llamada al método
        /// '{<paramref name="method"/>}' es peligrosa.".
        /// </summary>
        /// <param name="method">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "La llamada al método
        /// '{<paramref name="method"/>}' es peligrosa.".
        /// </returns>
        public static string MethodIsDangerous(string method) => $"La llamada al método '{method}' es peligrosa.";
        /// <summary>
        /// Devuelve una cadena con el texto "El uso de la clase 
        /// '{<paramref name="class"/>}' es peligroso.".
        /// </summary>
        /// <param name="class">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "El uso de la clase 
        /// '{<paramref name="class"/>}' es peligroso.".
        /// </returns>
        public static string ClassIsDangerous(string @class) => $"El uso de la clase '{@class}' es peligroso.";
        /// <summary>
        /// Devuelve una cadena con el texto "Falta el argumento
        /// {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "Falta el argumento
        /// {<paramref name="text"/>}".
        /// </returns>
        public static string MissingArgument(string text) => $"Falta el argumento {text}";
        /// <summary>
        /// Devuelve una cadena con el texto "de {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "de {<paramref name="text"/>}".
        /// </returns>
        public static string OfX(string text) => $" de {text}";
        /// <summary>
        /// Devuelve una cadena con el texto "✓ {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "✓ {<paramref name="text"/>}".
        /// </returns>
        /// <remarks>
        /// El texto generado por esta función podría no verse correctamente en
        /// un programa, consola o terminal que no sea Unicode.
        /// </remarks>
        public static string Ok(string text) => $"✓ {text}";
        /// <summary>
        /// Devuelve una cadena con el texto "El plugin {<paramref name="plg"/>} no contiene interacciones.".
        /// </summary>
        /// <param name="plg">Nombre del plugin.</param>
        /// <returns>
        /// Una cadena con el texto "El plugin {<paramref name="plg"/>} no contiene interacciones.".
        /// </returns>
        public static string PluginHasNoInters(string plg) => $"El plugin {plg} no contiene interacciones.";
        /// <summary>
        /// Devuelve una cadena con el texto "Se necesita un Plugin de tipo {<paramref name="plg"/>}".
        /// </summary>
        /// <param name="plg">Nombre del plugin.</param>
        /// <returns>
        /// Una cadena con el texto "Se necesita un Plugin de tipo {<paramref name="plg"/>}".
        /// </returns>
        public static string PluginXNeeded(string plg) => $"Se necesita un Plugin de tipo {plg}";
        /// <summary>
        /// Devuelve una cadena con el texto "Establecer {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "Establecer {<paramref name="text"/>}".
        /// </returns>
        public static string SetX(string text) => $"Establecer {text}";
        /// <summary>
        /// Devuelve una cadena con el texto "/!\ {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "/!\ {<paramref name="text"/>}".
        /// </returns>
        public static string Warn(string text) => $"/!\\ {text}";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} ha sido aceptado.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} ha sido aceptado.".
        /// </returns>
        public static string XAccepted(string text) => $"{text} ha sido aceptado.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} ya existe.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} ya existe".
        /// </returns>
        public static string XAlreadyExists(string text) => $"{text} ya existe.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} ya se ha iniciado.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} ya se ha iniciado.".
        /// </returns>
        public static string XAlreadyStarted(string text) => $"{text} ya se ha iniciado.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="x"/>} no puede 
        /// ser {<paramref name="y"/>}".
        /// </summary>
        /// <param name="x">Elemento x.</param>
        /// <param name="y">Elemento y.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="x"/>} no puede ser
        /// {<paramref name="y"/>}".
        /// </returns>
        public static string XCannotBeY(string x, string y) => $"{x} no puede ser {y}";
        /// <summary>
        /// Devuelve una cadena con el texto "Operación cancelada por 
        /// {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "Operación cancelada por 
        /// {<paramref name="text"/>}".
        /// </returns>
        public static string XCncl(string text) => $"Operación cancelada por {text}.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} se ha 
        /// deshabilitado.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} se ha
        /// deshabilitado.".
        /// </returns>
        public static string XDisabled(string text) => $"{text} se ha deshabilitado.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} se ha desconectado.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} se ha desconectado.".
        /// </returns>
        public static string XDisconnected(string text) => $"{text} se ha desconectado.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="X"/>} no
        /// contiene {<paramref name="Y"/>}".
        /// </summary>
        /// <param name="X">Cadena X</param>
        /// <param name="Y">Cadena Y</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="X"/>} no contiene
        /// {<paramref name="Y"/>}".
        /// </returns>
        public static string XDoesntContainY(string X, string Y) => $"{X} no contiene {Y}";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} ha encontrado un error.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} ha encontrado un error.".
        /// </returns>
        public static string XFoundError(string text) => $"{text} ha encontrado un error.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} se encuentra en fase beta.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} se encuentra en fase beta.".
        /// </returns>
        public static string XIsBeta(string text) => $"{text} se encuentra en fase beta.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} no es válido.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} no es válido.".
        /// </returns>
        public static string XIsInvalid(string text) => $"{text} no es válido.";
        /// <summary>
        /// Devuelve una cadena con el texto "Falta {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "Falta {<paramref name="text"/>}".
        /// </returns>
        public static string XMissing(string text) => $"Falta {text}";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} no es cancelable".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} no es cancelable".
        /// </returns>
        public static string XNotCancellable(string text) => $"{text} no es cancelable.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} no ha sido encontrado.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} no ha sido encontrado.".
        /// </returns>
        public static string XNotFound(string text) => $"{text} no ha sido encontrado.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} no se ha incluído.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} no se ha incluído.".
        /// </returns>
        public static string XNotIncluded(string text) => $"{text} no se ha incluído.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} no se ha inicializado aún.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} no se ha inicializado aún.".
        /// </returns>
        public static string XNotInit(string text) => $"{text} no se ha inicializado aún.";
        /// <summary>
        /// Devuelve una cadena con el texto "No se ha seleccionado {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "No se ha seleccionado {<paramref name="text"/>}".
        /// </returns>
        public static string XNotSelected(string text) => $"No se ha seleccionado {text}.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>}, o".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>}, o".
        /// </returns>
        public static string XOr(string text) => $"{text}, o";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} ha sido
        /// rechazado.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} ha sido
        /// rechazado.".
        /// </returns>
        public static string XRejected(string text) => $"{text} ha sido rechazado.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} ha
        /// devuelto valores inválidos.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} ha devuelto
        /// valores inválidos.".
        /// </returns>
        public static string XReturnedInvalid(string text) => $"{text} ha devuelto valores inválidos.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} se ha iniciado.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} se ha iniciado.".
        /// </returns>
        public static string XStarted(string text) => $"{text} se ha iniciado.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="text"/>} se ha detenido.".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="text"/>} se ha detenido.".
        /// </returns>
        public static string XStopped(string text) => $"{text} se ha detenido.";
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="unquoted"/>}
        /// '{<paramref name="quoted"/>}'".
        /// </summary>
        /// <param name="unquoted">Elemento x.</param>
        /// <param name="quoted">Elemento y.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="unquoted"/>}
        /// '{<paramref name="quoted"/>}'".
        /// </returns>
        public static string XYQuotes(string unquoted, string quoted) => $"{unquoted} '{quoted}'";
        #endregion

#pragma warning disable CS1591 // Las cadenas generalmente no requieren de descripción.

        public const string Abort = "Abortar";
        public const string about = "Acerca de...";
        public const string AllFiles = "Todos los archivos";
        public const string Alpha = "abcdefghijklmnopqrstuvwxyz";
        public const string Assmblies = "Ensamblados";
        public const string Bsy = "Ocupado";
        public const string BtnDel = "Eliminar";
        public const string BtnEdit = "Editar";
        public const string BtnNew = "Nuevo";
        public const string BtnSave = "Guardar";
        public const string Chars = @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM,./;'][\=-1234567890~!@#$%^&*()_+|}{"":?><";
        public const string ClientConnected = "Se ha conectado un nuevo cliente.";
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
        public const string FeatNotAvailable = "Característica no disponible";
        public const string FrmIncorreclyOpened = "Este diálogo fue abierto de forma incorrecta. Refiérase a la documentación de MCART para resolver este problema. Si ve este mensaje de error, póngase en contacto con el desarrollador de la aplicación.";
        public const string GUITest = "Prueba de interfaz gráfica";
        public const string HasUI = "Incluye UI";
        public const string HelpDir = @"\Help";
        public const string ImplInterfaces = "Interfaces implementadas";
        public const string InterfaceExpected = "Se esperaba una interfaz";
        public const string InvalidData = "Los datos proporcionados no son válidos.";
        public const string InvalidDB = "El arhivo no contiene una base de datos válida";
        public const string InvalidInfo = "La información no es válida.";
        public const string InvalidPassword = "La contraseña no es válida.";
        public const string invalidPluginClass = "La clase no es un Plugin válido.";
        public const string IsBeta = "Versión Beta";
        public const string IsUnsafe = "Inseguro (NO UTILIZAR)";
        public const string ItsLong = "Es muy larga";
        public const string LatinChars = "áéíóúüñäåöàèìòùç";
        public const string LstEmpty = "La lista está vacía";
        public const string MandatoryField = "Este campo es obligatorio";
        public const string Menu = "Menú";        
        public const string MinMCARTV = "Versión mínima de MCART";
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
        public const string Of = "de";
        public const string OK = "Aceptar";
        public const string PluginDetails = "Detalles de plugins";
        public const string PluginDidntInit = "El plugin no se inicializó";
        public const string PluginInfo1 = "Seleccione un plugin para comprobar la compatibilidad.";
        public const string PluginInfo2 = "El plugin es compatible con esta versión de MCART.";
        public const string PluginInfo3 = "El plugin NO es compatible con esta versión de MCART.";
        public const string PluginInfo4 = "No se pudo determinar la compatibilidad del plugin.";
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
        public const string Search = "Buscar...";
        public const string Security = "Seguridad";
        public const string Specified = "especificado";
        public const string StackUnderflow = "Subflujo de pila";
        public const string Symbols = "`~!@#$%^&*()-_=+\\|]}[{'\";:/?.>,<";
        public const string TargetMCARTV = "Versión destino de MCART";
        public const string TestUI = "Probar UI...";
        public const string TheArgument = "El argumento";
        public const string TheAssembly = "El ensamblado";
        public const string TheClass = "La clase";
        public const string TheClient = "El cliente";
        public const string TheConn = "La conexión";
        public const string TheEvent = "El evento";
        public const string TheFile = "El archivo";
        public const string TheFunc = "La función";
        public const string TheGraphView = "El controlador de gráficas";
        public const string TheInterface = "La interfaz";
        public const string TheLicense = "La licencia";
        public const string TheListener = "El escucha";
        public const string TheMethod = "El método";
        public const string TheObj = "El objeto";
        public const string ThePlugin = "El Plugin";
        public const string TheProtocol = "El protocolo";
        public const string TheResource = "El recurso";
        public const string TheSrv = "El servidor";
        public const string TheTask = "La tarea";
        public const string TheType = "El tipo";
        public const string TheUid = "El Uid";
        public const string TheUsr = "El usuario";
        public const string TheValue = "El valor";
        public const string Timeout = "Se ha agotado el tiempo de espera.";
        public const string TooFewArguments = "Muy pocos argumentos";
        public const string TooManyArguments = "Demasiados argumentos";
        public const string Total = "Total:";
        public const string Unk = "Desconocido";
        public const string UnknownType = "Tipo de objeto desconocido.";
        public const string UnspecLicense = "No se ha especificado una licencia, o la misma no ha sido incluída.";
        public const string UnsupportedVer = "Versión no soportada.";
        public const string UnusableObject = "El objeto no puede ser utilizado.";
        public const string Updt = "Actualizar";
        public const string UsrCncl = "Operación cancelada por el usuario.";
        public const string Usr = "Usuario";
        public const string Ver = "Versión";

#pragma warning restore CS1591

    }
}