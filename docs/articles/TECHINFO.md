# Información técnica de desarrollo
En este archivo, se detallan algunas consideraciones técnicas al trabajar en
MCART. Para un eficiente flujo de trabajo, por favor, lee todo este documento.

### Estructura de directorios de solución
La solución contiene una estructura de directorios organizados para el tipo de
proyecto, de acuerdo al alcance de soporte del mismo. Por ejemplo, para
librerías de soporte a una plataforma de UI específica, existe la carpeta
`Platforms`, donde actualmente existe el proyecto `WPF`, *MCART para WPF.*

La siguiente tabla lista los directorios actualmente definidos:

Directorio          | Contenido del directorio
---                 | ---
./Art               | Archivos de recursos de arte. Contiene íconos e imágenes de MCART.
./Build             | Versiones compiladas y empaquetadas de las librerías distribuibles de MCART.
./docs              | Documentación estándar, y proyecto de DocFX para generar documentación de API.
./Publish           | Repositorio de paquetes de NuGet de MCART que permite configurar orígenes locales.
./src/Examples      | Varios ejemplos sobre el uso de MCART.
./src/Extensions    | Extensiones especiales con funcionalidad muy específica y/o posibilidad de referencias externas.
./src/Lib           | Librerías auxiliares genéricas de MCART para la creación de ensamblados para distintas plataformas.
./src/MCART         | Proyecto principal.
./src/Platform      | Proyectos de plataforma de UI.
./src/Shared        | Proyectos de código compartido.
./src/Targets       | Archivos de configuración de compilación.
./src/Tests         | Pruebas unitarias y de integración.
./src/Utils         | Utilerías de desarrollo varias.
./vs-snippets       | Snippets de código para instalar en Visual Studio.

### Números de versión
MCART sigue los estándares de números de versión de NuGet, utilizando una
cadena de versión Major.Minor.Revision.Build, adjuntando para pas versiones
pre-release un subfijo

### Constantes globales de compilación
El archivo `./src/Targets/GlobalDirectives.targets` contiene un conjunto de constantes de
compilación definidas para toda la solución. Esto, con el propósito de evitar
los problemas que pueden surgir al configurar individualmente cada proyecto, y
permitir compartir eficientemente dichas constantes.

La siguiente tabla incluye las constantes existentes y el efecto que producen
al activarse:

Constante             | Efecto
---                   | ---
AntiFreeze            | Habilita a las funciones que así lo permitan eventualmente detener una enumeración potencialmente infinita cuando esta haya sido mal utilizada.
BufferedIO            | Algunas funciones de entrada/salida incluyen una implementación opcional con búffer. Activar esta opción habilita las lecturas y escrituras con búffer.
CheckDanger           | Obliga a las funciones que lo permitan a limitar el uso de clases o funciones peligrosas (marcadas con el atributo `DangerousAttribute`)
CLSCompliance         | Obliga a utilizar implementaciones que cumplen con CLS (Common Language Standard). Se recomienda encarecidamente activar esta constante.
DynamicLoading        | Habilita la carga de clases y/o miembros de clases por medio de ```System.Reflection``` de elementos contenidos dentro de MCART.
ExtrasBuiltIn         | Incluir en el ensamblado de MCART ejemplos e implementaciones estándar básicas de las interfaces o clases abstractas para las cuales se pueda proveer.
FloatDoubleSpecial    | `float` y `double` son tipos numéricos que pueden contener valores especiales, como ser NaN o infinito. Al activar esta constante, se habilitan métodos especiales que pueden trabajar con estos valores.
McartAsPlugin         | Habilita la carga de plugins definidos dentro de MCART.
NativeNumbers         | Permite utilizar implementaciones conscientes de la cultura nativa para algunas funciones que trabajan con símbolos numéricos, utilizando los dígitos numéricos locales.
PreferExceptions      | Cuando ciertas funciones deban manejar información inválida, activar esta constante causa que se arrojen excepciones en lugar de continuar con código alternativo (activar esta bandera puede ser un dolor de cabeza, pero resulta en código más seguro).
RatherDRY             | Indica que, a pesar de disminuir la optimización del código, se debe respetar el principio DRY (Don't Repeat Yourself) al implementar sobrecargas cuyo cuerpo sea exactamente igual salvo por los tipos de argumentos.
SaferPasswords        | Permite que algunas funciones de seguridad requieran, comprueben o generen contraseñas más seguras, a expensas de compatibilidad con los métodos de entrada disponibles o con el equipo.
StrictMCARTVersioning | Cuando se realicen comprobaciones de compatibilidad con MCART, activar esta constante causa que dichas comprobaciones sean más estrictas. Se recomienda mantener esta constante habilitada.
