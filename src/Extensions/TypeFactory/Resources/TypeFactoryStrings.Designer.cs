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
    internal class TypeFactoryStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal TypeFactoryStrings() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TheXDS.MCART.Resources.TypeFactoryStrings", typeof(TypeFactoryStrings).Assembly);
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
        ///   Busca una cadena traducida similar a Se esperaba una definición de método desde una interfaz..
        /// </summary>
        internal static string ErrIFaceMethodExpected {
            get {
                return ResourceManager.GetString("ErrIFaceMethodExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El tipo no implementa la interfaz &apos;{0}&apos;..
        /// </summary>
        internal static string ErrIfaceNotImpl {
            get {
                return ResourceManager.GetString("ErrIfaceNotImpl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La propiedad no contiene accesor de lectura..
        /// </summary>
        internal static string ErrPropCannotBeRead {
            get {
                return ResourceManager.GetString("ErrPropCannotBeRead", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya se ha definido un campo de almacenamiento para la propiedad..
        /// </summary>
        internal static string ErrPropFieldAlreadyDefined {
            get {
                return ResourceManager.GetString("ErrPropFieldAlreadyDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya se ha definido un Getter para la propiedad..
        /// </summary>
        internal static string ErrPropGetterAlreadyDefined {
            get {
                return ResourceManager.GetString("ErrPropGetterAlreadyDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya se ha definido un Setter para la propiedad..
        /// </summary>
        internal static string ErrPropSetterAlreadyDefined {
            get {
                return ResourceManager.GetString("ErrPropSetterAlreadyDefined", resourceCulture);
            }
        }
    }
}
