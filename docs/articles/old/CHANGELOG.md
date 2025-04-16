# ChangeLog de MCART
En este documento se describe un historial de cambios en general para distintas versiones secundarias de MCART. 

Para las versiones *0.1 series* hasta *0.5 series*, se provee de un esquema relativamente completo de los cambios más importantes al momento de su liberación. Para las versiones desde *0.6 series* en adelante, se presenta una versión considerablemente más resumida de los cambios. Esto es debido a que *0.6 series* fue la primera versión alojada en un servicio de control de versiones, como lo es git; a lo cual se debe agregar que muchos de los commits realizados en el proyecto pueden ser fixes parciales a fallos, cambios de poca reelevancia o de documentación, reorganizaciones menores de archivos, y en algunos casos únicamente bumps de versión.

Para las versiones *0.6 series* en adelante, para ver una lista mucho más completa y específica de los cambios, visite la [página de historial de commits](https://github.com/TheXDS/MCART/commits/master).

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

## MCART 0.10 Series
### HightLights
- Se ha reescrito una gran porción del código de MCART, además de reorganizar
  todos los archivos del proyecto.
- MCART ahora utiliza C# 8.0
- MCART ahora utiliza el `Microsoft.NET.Sdk` para compilar.
- MCART contiene múltiples Framework objetivo.
- MCART soporta oficialmente .Net Core 3
### Cambios
* Nuevo: Numerosos módulos de extensión de tipos.
* Nuevo: Concepto de extensiones de MCART (requisitos de dependencias
  relajados para los mismos)
* Nuevo: Funcionalidad básica de creación de ViewModels importada desde
  `TheXDS.Tritón`
* Nuevo: Múltiples clases auxiliares para trabajar bajo paradigmas MVVM
* Nuevo: Arquitectura de información de aplicaciones y ensamblados reescrita
  bajo principios SOLID.
* Nuevo: Conversión parcial del código a C# 8.0.
* Nuevo: Extensiones para FlowDocument.
* Nuevo: Script especial de publicación en NuGet.
* Nuevo: Clientes de red administrados para comandos.
* Nuevo: Clases de apertura de `Stream` a aprtir de un `System.Uri`.
* Nuevo: Envoltorios observables para todo tipo de colecciones.
* Nuevo: Algunos controles adicionales.
* Nuevo: Clases auxiliares para el manejo de argumentos de línea de comandos.
* Mejorado: Reorganización total del directorio del proyecto.
* Mejorado: Uso de tipos de referencia nulables.
* Mejorado: Cambios de Versioning
* Mejorado: Respeto estricto de SOLID/MVVM.
* Mejorado: Limpieza y actualización de archivos de proyecto.
* Mejorado: Cambios internos, mejoras y expansioens al Stack de redes.
* Mejorado: Reducción de complejidad ciclomática en varias funciones de MCART.
* Corregido: Múltiples problemas de confiabilidad misceláneos.
* Quitado: Comentarios de ReSharper

#### MCART 0.10.0.6
* Nuevo: Tipos universales básicos adicionales: ```I2DVector```, ```I3DVector```, ```Size```.
* Nuevo: App de ejemplo ```PictVGA```.

#### MCART 0.10.0.7
* Mejorado: Mejoras substanciales a ```TypeFactory```.

#### MCART 0.10.0.11
* Nuevo: Tipos de resolución de ```Uri```s.
* Nuevo: Clientes mejorados de comunicación de red basados en comandos.
* Mejorado: Cliente de ```LightChat``` (app de ejemplo).

#### MCART 0.10.0.12
* Nuevo: control ```BusyContainer``` para WPF.
* Nuevo: Envolturas observables para colecciones y listas.
* Nuevo: Proyecto de extensión para tipos complejos de Entity Framework.
* Nuevo: Otros tipos misc.
* Nuevo: Funciones de consola.
* Cambiado: Reescritura parcial transitoria de ```ViewModelFactory```.

#### MCART 0.10.0.13
* Nuevo: Clase que expone información sobre Microsoft Windows.
* Mejorado: Mejor segregación de métodos internos compartidos.
* Mejorado: Expansiones de los tipos universales de MCART.

#### MCART 0.10.0.14
* Mejorado: Scripts de compilación.
* Mejorado: Clases para trabajo con ViewModels
* Cambiado: Reorganización de algunas extensiones.

## MCART 0.11 Series
*0.11 series* tuvo su primer versión generada el 23 de septiembre de 2019. Se trata de una versión con un ciclo de vida corto comparado a otros releases.
### HightLigts
- Varios de los componentes *Legacy* de MCART han sido reubicados y marcados como tal.
- Ha ocurrido un cambio importante en la forma en la que se segregan y crean nuevos proyectos, esto en aras de reducir las dependencias y los tamaños de cada librería individual.
- Se ha decidico crear nuevas versiones de ciertos componentes que han sido declarados como *Legacy*. El trabajo es árduo, y podría no finalizarse por completo para la versión *0.12 series* de MCART.
- Se ha puesto fuerte énfasis en la limpieza de todo el código, quitando referencias residuales a ReSharper, y agregando toda la documentación faltante. Este también es un trabajo árduo, pero es desde esta versión que se inicia el compromiso.
- Algunos de los trabajos realizados durante esta versión ha sido resultado directo de las necesidades crecientes de [```TheXDS.Triton```](https://github.com/TheXDS/Triton), el cual es otro proyecto activo de TheXDS non-Corp.

### Cambios
* Nuevo: Numerosos módulos de extensión de tipos.
* Nuevo: Automatización de publicación de paquetes a [*NuGet*](https://www.nuget.org/packages?q=MCART) y a [*GitHub*](https://github.com/TheXDS/MCART/packages).
* Nuevo: Tipo ```Disposable``` (facilita la implementación de clases desechables).
* Nuevo: Clases y métodos de descripción de licencias SPDX comunes.
* Nuevo: Módulo de funciones estadísticas.
* Nuevo (WIP): MRPC (Morgan's Remote Procedure Call)
* Nuevo: Múltiples clases de conversión de tipos.
* Cambiado: Se ha deprecado por completo .Net Framework.  MCART ahora se construye exclusivamente para .Net Core 3.1 y .Net Standard 2.1 en los casos en donde sea posible.
* Cambiado: ```PluginSupport``` => ```PluginSupport.Legacy```
* Cambiado: SandCastle ha sido totalmente removido para reemplazarse por DocFX.

## MCART 0.12 Series
*0.12 series* tuvo su primer versión generada el 6 de diciembre de 2019. Se trata de una versión con un ciclo de vida corto comparado a otros releases.
### HightLigts
- Finalmente, MCART únicamente compila a objetivos .Net Core 3.1 y .NetStandard 2.1 donde sea aplicacble.
- Mucho del trabajo en esta versión se centraría en tratar de finalizar algunos de los nuevos componentes.

### Cambios
Nuevo: Finalización de componente ```TypeFactory```.
Corregido: Documentación con indentación innecesaria.
Deshabilitado: ```ViewModelFactory``` (aún no se remueve totalmente considerando posibles usos futuros)
