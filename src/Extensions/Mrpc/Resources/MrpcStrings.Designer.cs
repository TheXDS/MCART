﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheXDS.MCART.Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class MrpcStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MrpcStrings() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TheXDS.MCART.Resources.MrpcStrings", typeof(MrpcStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El tipo {0} es inválido. Se esperaba una interfaz..
        /// </summary>
        internal static string ErrInterfaceExpected {
            get {
                return ResourceManager.GetString("ErrInterfaceExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El argumento &apos;{0}&apos; no es válido. No se puede convertir de &apos;{1}&apos; al tipo necesario &apos;{2}&apos;..
        /// </summary>
        internal static string ErrInvalidArgumentType {
            get {
                return ResourceManager.GetString("ErrInvalidArgumentType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No es posible transmitir un objeto{0}: No se trata de un tipo primitivo, serializable ni simple, no existe una representación de las propiedades del objeto ni una conversión a System.String que se pueda utilizar para transmitir el objeto..
        /// </summary>
        internal static string ErrUntransmittableObj {
            get {
                return ResourceManager.GetString("ErrUntransmittableObj", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a  de tipo {0}.
        /// </summary>
        internal static string OfType {
            get {
                return ResourceManager.GetString("OfType", resourceCulture);
            }
        }
    }
}
