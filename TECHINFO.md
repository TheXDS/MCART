# Información técnica de desarrollo
En este archivo, se detallan algunas consideraciones técnicas al trabajar en
MCART. Para un eficiente flujo de trabajo, por favor, lee todo este documento.
### Creando nuevos proyectos
Los proyectos comparten un conjunto de constantes de compilación. Debido a
algunas limitaciones presentadas por Visual Studio, no existe un editor visual
de estas configuraciones, y no existe un mecanismo elegante para administrar
las configuraciones globales. Por ende, es necesario agregar el siguiente nodo
a la definición de cada proyecto:
```xml
  <PropertyGroup Condition="'$(SolutionDir)' == '' or '$(SolutionDir)' == '*undefined*'">
    <SolutionDir>..\..\</SolutionDir>
  </PropertyGroup>
  <Import Project="$(SolutionDir)CommonSettings.targets" />
```
Al agregar este bloque de código a los proyectos, es posible cambiar las
constantes de compilación globales editando el archivo
`CommonSettings.targets`.

**Nota:** las constantes aplicables al proyecto deben definirse antes de
agregar este nodo a la definición dle proyecto, para evitar que Visual Studio
copie las constantes globales al archivo del proyecto.
### Números de versión
La información genérica de los ensamblados de MCART se encuentra en el proyecto
compartido *AssemblyInfo*, el cual contiene únicamente un archivo con las
respectivas definiciones de atributos de versión, copyright, compañía,
trademark y nombre del producto. Al crear nuevos proyectos, es necesario
incluir una referencia a este proyecto para evitar mantener copias innecesarias
de los atributos, y centralizar la información de versiones y de
identificación.

Durante la fase Pre-release de MCART, la versión mayor tendrá un valor de cero,
siendo necesario referirse al componente menor para evaluar el estado de avance
del proyecto. Eventualmente, al existir un Release, el comportamiento de los
distintos números que componen la versión pasará a representar el estado de
manera normal.

Luego de ocurrir un Release final, los últimos componentes pasarán a ser la
fecha codificada de compilación de los ensamblados.

**Notas adicionales para NetStandard:**
Debido a la forma en la que dichos atributos se encuentran almacenados en un
proyecto de este tipo, será necesario actualizar manualmente la información de
ensamblado de *NetStandard* al compilar. Por favor, no olvides realizar estos
cambios y recompilar *NetStandard* por separado.
### Constantes globales de compilación
El archivo `CommonSettings.targets` contiene un conjunto de constantes de
compilación definidas para toda la solución. Esto, con el propósito de evitar
los problemas que pueden surgir al configurar individualmente cada proyecto, y
permitir compartir eficientemente dichas constantes.

La siguiente tabla incluye las constantes existentes y el efecto que producen
al activarse:

Constante | Efecto
--- | ---
BufferedIO | Algunas funciones de entrada/salida incluyen una implementación opcional con búffer. Activar esta opción habilita las lecturas y escrituras con búffer.
CheckDanger | Obliga a las funciones que lo permitan a limitar el uso de clases o funciones peligrosas (marcadas con el atributo `DangerousAttribute`)
CLSCompliance | Obliga a utilizar implementaciones que cumplen con CLS (Common Language Standard). Se recomienda encarecidamente activar esta constante.
ExtrasBuiltIn | Incluir en el ensamblado de MCART ejemplos e implementaciones estándar básicas de las interfaces o clases abstractas para las cuales se pueda proveer.
FloatDoubleSpecial | `float` y `double` son tipos numéricos que pueden contener valores especiales, como ser NaN o infinito. Al activar esta constante, se habilitan métodos especiales que pueden trabajar con estos valores.
NativeNumbers | Permite utilizar implementaciones conscientes de la cultura nativa para algunas funciones que trabajan con símbolos numéricos, utilizando los dígitos numéricos locales.
PreferExceptions | Cuando ciertas funciones deban manejar información inválida, activar esta constante causa que se arrojen excepciones en lugar de continuar con código alternativo (activar esta bandera puede ser un dolor de cabeza, pero resulta en código más seguro).
RatherDRY | Indica que, a pesar de disminuir la optimización del código, se debe respetar el principio DRY (Don't Repeat Yourself) al implementar sobrecargas cuyo cuerpo sea exactamente igual.
SaferPasswords | Permite que algunas funciones de seguridad requieran, comprueben o generen contraseñas más seguras, a expensas de compatibilidad con los métodos de entrada disponibles o con el equipo.
StrictMCARTVersioning | Cuando se realicen comprobaciones de compatibilidad con MCART, activar esta constante causa que dichas comprobaciones sean más estrictas. Se recomienda mantener esta constante habilitada.
### Consideraciones especiales para Gtk
Debido a las limitadas herramientas disponibles para trabajar con proyectos
basados en Gtk, algunas tareas deben ser realizadas manualmente, indistintamente
del entorno de desarrollo o el IDE utilizado.

Anteriormente, Gtk fue basado en Gtk# 2, por lo que era posible editar la
interfaz gráfica utilizando el editor integrado en MonoDevelop y Xamarin Studio,
Stetic. Ahora, Gtk se basa en Gtk# 3, por lo que este editor no se encuentra
disponible. Para diseñar la interfaz gráfica, se utiliza Glade, y los archivos
resultantes son almacenados junto a su archivo de código. Algunas convenciones
son muy importantes para lograr que todo funcione bien:

* La ruta del archivo .glade debe coincidir con el nombre completo del tipo del Widget/ventana asociado (es decir, debe coincidir con la ruta de espacios de nombre).
* El nivel superior del Widget/ventana en el archivo .glade debe tener como Id el nombre del tipo del Widget/ventana asociado.
* La opción de compilación del archivo .glade debe establecerse en EmbeddedResource.
* El código asociado al Widget/ventana debe contener una plantilla como la siguente:
```csharp
        #region Construcción de ventana
        /// <summary>
        /// Crea una nueva instancia de la clase <see cref="Ventana"/>.
        /// </summary>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="Ventana"/>.
        /// </returns>
        public static Ventana Create()
        {
            var t = typeof(Ventana);
            Builder builder = new Builder(t.Assembly, $"{t.FullName}.glade", null);
            return new Ventana(builder, builder.GetObject(t.Name).Handle);
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Ventana"/>.
        /// </summary>
        /// <param name="builder">Builder.</param>
        /// <param name="handle">Handle.</param>
        public Ventana(Builder builder, IntPtr handle) : base(handle)
        {
            builder.Autoconnect(this);

            /*
             * Otras inicializaciones de la clase aquí...
             */
        }
        #endregion

        #region Widgets        
#pragma warning disable CS0649
#pragma warning disable CS0169
        [Builder.Object] Label lblHola;
        [Builder.Object] Entry txtNombre;
        [Builder.Object] Button btnAceptar;
        /*
         * Widgets utilizados aquí, con el atributo [Builder.Object].
         * No es necesario incluir aquellos que no poseen interacción alguna.
         */
#pragma warning restore CS0649
#pragma warning restore CS0169
        #endregion
```
