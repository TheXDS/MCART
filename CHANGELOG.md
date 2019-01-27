## Nürburgring 0.1.11.5
- Versión inicial

## Nürburgring 0.2.0.10
- Primera implementación de concepto de plugins
- Corregido un problema con ```VMRunner``` al finalizar una máquina virtual
- Corregida parcialmente la inoperatibilidad de ```Bintools2```
- Desactivación de código no compilable (*DUFParser v1.0*)

## Nürburgring 0.2.0.11
- Cambios menores

## Nürburgring 0.2.1.12
- Corregidos problemas de estabilidad al cargar Plugins
- Más correcciones a la inoperatibilidad de ```Bintools2```
- Reactivación de *DUFParser v1.0* mediante una capa de compatibilidad con el plugin ```AESCrypter```

## Nürburgring 0.2.2.13
- Implementación de funciones Shift en ```Bintools2```
- Cambios menores

## Nürburgring 0.2.3.18
- Reactivación de funciones inoperativas de *DUFParser v2.0*
- Mejoras en la implementación de plugins
- Cambios menores

## Nürburgring 0.2.4.33
- Soporte de plugins reescrito
- Cambios menores

## Nürburgring 0.3.0.40
- Implementación de arquitectura AnyCPU en todos los módulos
- Correción de devolucion de valores incorrectos en algunas funciones

## Nürburgring 0.3.1.75
- Inicios de implementación de Copyright (bloquear información confidencial de la API de los plugins o el DLL principal)
- Mejoras considerables en la implementación de plugins.
- El DLL principal puede ser cargado como plugin si contiene clases cargables.
- El DLL principal ahora también presenta su información en formato de Plugin.
- Correción de errores en plugins.
- Correción en cuadro de diálogo de detalles de plugin.
- Cambios menores.
- Removal of lame dedication note attached to Credits.txt

## Nürburgring 0.3.1.77
- Implementación de algunas funciones adicionales.
- Reescritura de funciones de consola y video para máquinas virtuales.
- Reescritura de algunas funciones de soporte de teclado y de emulación de máquinas virtuales.

## Nürburgring 0.3.2.78
- Limpieza de código, ahora en lugar de módulos, se utilizan Namespaces.
- Mejoras en algunas funciones misceláneas.

## Nürburging 0.3.3.80
- Nueva política: Ahora, el código será obfuscado para la versión protegida.
- Incorporación de plugins de motor aleatorio.
- Cambios menores.

## Nürburgring 0.4.0.0
- Reescritura total de MCA Runtime
- Ahora, este software se identifica como **MCART**
- Eliminación de abundante código innecesario
- Otra reescritura total del soporte de plugins
- Eliminación de plugins innecesarios
- Incorporación preeliminar de un administrador de base de datos basado en árbol XML
- Incorporación preeliminar de protocolos de red
- Incorporación de funciones matemáticas
- Incorporación de diversas herramientas de interfaz gráfica
- El proyecto ahora está basado en WPF y .NET Framework 4.6.1
- Inclusión de una biblioteca de controles WPF
- Inclusión de una biblioterca de formularios comunes WPF
- Inclusión de una biblioteca de excepciones
- Inclusión de una biblioteca de atributos
- Inclusión de una biblioteca para la creación de eventos
- Inclusión de una biblioteca de tipos nuevos
- Inclusión de una biblioteca de extensiones para tipos de .NET Framework
- Remoción total del soporte de máquinas virtuales
- Remoción total de funciones obsoletas

## Nürburgring 0.4.1.0
- Reescritura de algunos controles y plugins
- Nueva interfaz ITaskReporter
- Mejor organización de los módulos
- Otras mejoras varias

## Nürburgring 0.4.2.0
- Incorporación de funciones de comprobación de versión de Runtime
- Eliminación de fase de carga XAML de los controles y formularios personalizados
- Las funciones gráficas ahora pueden acceder a todos los filtros de imagen cargados para la aplicación y/o .NET Framework
- Nuevas funciones y clases de seguridad
- Enormes mejoras al cuadro de contraseña
- Otras mejoras varias

## Nürburgring 0.4.3.0
* Nuevo: control ```NavigationBar```
* Nuevo: módulo ```ValueConverters```
* Nuevo: las bases de datos ahora pueden contener información Tag
* Nuevo: Propiedades adicionales para ```Networking.ClientServer.Server```
* Nuevo: Extensiones adicionales de la clase ```List(Of T)```
* Mejorado: Limpieza de recursos de ```Networking.ClientServer.Server```
* Mejorado: Extensión y mejoras varias al soporte de plugins
* Arreglado: ```AboutMCART``` debería funcionar correctamente.
* Arreglado: Problemas con el indicador de seguridad en ```PasswordDialog```
* Reemplazado: ```MCAGraph``` > ```LightGraph```
* Movido: ```Objects``` ahora es un módulo independiente en el proyecto (```ObjectManagement```)
* Inhabilitado: ```MCADBServer```
* Algunas nuevas funciones, atributos, argumentos de eventos, excepciones y recursos misceláneos.
* También han sido agregadas algunas etiquetas XML de documentación.

## Nürburgring 0.4.3.1
* Arreglado: Problema de control de búsqueda de NavigationBar
* Modificado: Cambios a los constructores de StringVisibilityConverter

## Nürburgring 0.4.3.2
* Nuevo: eventos adicionales para ```NavigationBar```
* Mejorado: ```GraphView``` tiene soporte para tipos adicionales de gráficas, modo histograma y búffer de máximo/mínimo
* Arreglado: problemas de confiabilidad de ```GraphView```
* Quitado: código de carga de XAML innecesario que aún formaba parte de los controles
* Otras mejoras y correcciones varias.

## Nürburgring 0.4.4.0
* Nuevo: Módulo de recursos de ícono
* Nuevo: Clase abstracta ```System.Threading.Reporters.TaskReporter```
* Nuevo: Clase ```System.Threading.Reporters.ConsoleReporter```
* Mejorado: Significativa expansión de ```ITaskReporter```
* Cambiado: refactorización y renombrado de multiples propiedades de ```BarStepper```
* Algunas nuevas funciones, atributos, argumentos de eventos, excepciones y recursos misceláneos, además una profunda limpieza de archivos innecesarios

## MCART 0.5.0.0
* Nuevo: Soporte preeliminar para campos llave secundarios, campos de autoincremento y campos automáticos para ```MCADB```.
* Nuevo: Para desarrolladores, existen nuevas opciones de carga de bases de datos de ```MCADB``` para realizar tareas de depuración.
* Nuevo: Nuevas funciones de administración de objetos: ```GetTypes(Of T)(AppDomain)```, ```ToTypes(Object())```, ```NewInstance(Of T)(Type, Object())```
* Nuevo: Funciones ```PackString(String, IO.Stream)```, ```UnpackString(IO.Stream)```, ```IsEither(Object, Object())```, ```IsNeither(Object, Object())```
* Mejorado: las bases de datos de ```MCADB``` utilizan el *GUID* de la aplicación en lugar del nombre del ensamblado.
* Cambiado: Todos los controles y formularios ahora se encuentran contenidos dentro de su propio Namespace.
* Cambiado: Más movimientos de código de ejemplo a un módulo de ejemplos independiente. Los archivos fuente de MCART son más pequeños.
* Arreglado: Error de XML de compilación de documentación
* Misc.: Uso de características de VB15
* Otras mejoras y correcciones varias.

## MCART 0.6 Series
### HightLights
- Todo el código se ha mudado a C# 7.0 desde VB15
- Se ha adoptado la licencia GNU GPL3
- Todos los módulos han sido reorganizados
### Cambios
* Nuevo: El proyecto ahora es multiplataforma
* Nuevo: Soporte para Mono Runtime
* Nuevo: Soporte para .Net Standard (Core 2.0)
* Nuevo: Soporte para Linux/macOs por medio de Mono Runtime
* Nuevo: Librería de UI en *gtk* (incluye widgets y ventanas)
* Nuevo: Librería de UI en *Windows Forms* (incluye controles y formularios)
* Nuevo: Librería de UI en *WPF* (incluye controles y ventanas)
* Nuevo: Proyectos de Unit Testing compatibles con Visual Studio
* Mejorado: Reescritura total de MCART
* Quitado: ```MCADB```

## MCART 0.7 Series
### HightLights
- MCART ha cambiado algunas de sus implementaciones para respetar los principios de SOLID.
### Cambios
* Nuevo: Proyecto de documentación basado en *SandCastle*.
* Mejorado: Soporte de plugins parcialmente rescrito.
* Cambiado: En lugar de Unit Testing de Visual Studio, se utiliza *xUnit*.

## MCART 0.8 Series
### HightLights
- MCART está cambiando sus implementaciones para cumplir al 100% con la especificación CLS.
- MCART ahora será publicado en *NuGet*.
- MCART se basa en C# 7.3.
### Cambios
* Nuevo: Soporte para objetos de extracción de recursos incrustados
* Nuevo: Librería matemática
* Nuevo: Extensiones Fluent para trabajar con objetos de documento (```System.Windows.Documents```)
* Nuevo: Tipo ```Range<T>```, extensiones misc. para soporte del nuevo tipo.
* Nuevo: Extensiones para ```MemberInfo```
* Mejorado: Reescritura parcial de MCART
* Mejorado: Soporte de plugins parcialmente rescrito (nuevamente).
* Cambiado: Reorganización de los espacios de nombre.
* Cambiado: Todos los espacios de nombres respetan la convención estándar de la especificación CLS.
* Cambiado: Información de ensamblado de los ejemplos de MCART unificados.
* Quitado: ```TaskReporter```
* Deprecado: *Gtk* y *Win32* han sido deprecados temporalmente.

#### MCART 0.8.7.0
* Arreglado: Stack de red problemático.

#### MCART 0.8.7.2
* Cambiado: Stack de red (lado del cliente) parcialmente rescrito.

#### MCART 0.8.8.1
* Cambiado: Stack de red (lado del servidor) parcialmente rescrito.

#### MCART 0.8.9.0
* Mejorado: Cambios de API de stack de red

#### MCART 0.8.10.0
* Nuevo: Diálogo ```AboutBox```
* Nuevo: Página ```AboutPage```
* Mejorado: Los diálogos utilizarán en la medida de lo posible la arquitectura *ViewModel*

## MCART 0.9 Series
### HightLights
- MCART está cambiando sus implementaciones para cumplir al 100% con la especificación CLS.
- MCART está mudando sus implementaciones de UI al esquema MVVM.
- Algunas características planeadas para MCART han sido reconsideradas y nuevos proyectos hermanos han sido creados para acomodarlas.
- Se han reorganizado grandes bloques monolíticos de tipos en archivos independientes más manejables.
- Se están optimizando las firmas de funciones para acelerar el desempeño de MCART bajo la iniciativa *Performante*
### Cambios
* Nuevo: Implementaciones MVVM para cuadros de diálogo y páginas.
* Nuevo: ```AssemblyDataExposer```.
* Nuevo: Nuevos convertidores de tipo.
* Nuevo: Clase estática especial para funciones internas.
* Nuevo: Clases y estructuras básicas para crear aplicaciones bajo el esquema MVVM.
* Nuevo: Página para detalles de Plugin.
* Nuevo: Página para detalles de Tipos.
* Nuevo: Múltiples funciones de extensión para cadenas, enumeraciones, tareas y tipos.
* Mejorado: Sistema de generación de contraseñas parcialmente rescrito.
* Mejorado: Rescritura parcial de la clase ```ClientBase```.
* Corregido: Problemas de confiabilidad del protocolo ```LightChat```.