# MCART
###### Librería de funciones varias para aplicaciones .Net/Mono
## Introducción
MCART es un conjunto de funciones, extensiones y módulos que he encontrado
útiles a lo largo de mis años de experiencia con lenguajes .Net,
particularmente con Visual Basic. Trata de añadir características que no se
encuentran fácilmente disponibles en .Net Framework, y además añade controles,
ventanas, recursos y otros objetos de utilidad.

Actualmente, se encuentra en una muy temprana fase Alpha, por lo que podría
tener bugs o problemas serios de rendimiento. He puesto mucho esfuerzo en
mantener un código funcional y, espero, libre de errores obvios. Sin embargo,
no puedo asegurar que MCART pueda ser utilizado en un paquete de software en su
estado actual.
## Composición del proyecto
MCART se compone de varios proyectos, y distintos proyectos de código
compartido entre las plataformas. En ellos reside la raíz de la mayoría de
características que pueden funcionar en diferentes entornos CIL. Hasta el
momento, la funcionalidad está comprobada con .Net Framework y Mono; lo que
debería ser suficiente para crear aplicaciones basadas en Win32, WPF, Gtk#,
consola e incluso sitios web desarrollados en ASP .Net.

Los demás proyectos apuntan a crear ensamblados para diferentes plataformas.
Actualmente, existen proyectos para crear ensamblados para Windows Presentation
Framework, Win32 y GtkSharp (Gtk#), además de dos proyectos que no son
específicos para una plataforma en particular, uno de los cuales es compatible
con .Net Standard y otro compilado para .Net Framework con la menor cantidad de
referencias posibles, denomidado CoreEdition. También, existen planes de
incorporar otros proyectos para soportar Universal Windows Platform (UWP) y, a
un futuro, Cocoa para macOs.
## Compilación
MCART requiere de un compilador compatible con C# 7.0, debido a ciertas
características especiales de contratos para argumentos. Eventualmente, es
posible que el desarrollo se mueva a versiones más recientes del lenguaje,
por ejemplo, cuando el soporte de C#7.1 sea mayor en diferentes entornos de
desarrollo.

MCART también requiere que [.Net SDK 2.0](https://www.microsoft.com/net/core)
esté instalado en el sistema. .Net SDK está disponible para Microsoft
Windows, Linux y macOs, e incluye el compilador necesario para construir los
proyectos.

La siguiente tabla indica los entornos de desarrollo en las cuales se ha
comprobado la compatibilidad para compilar MCART:

|Entorno de desarollo|Core|WPF|Gtk|Win32|.Net Std|Tests
|-|:-:|:-:|:-:|:-:|:-:|:-:
|Visual Studio 2017|X|X|X*|X|X|X
|Visual Studio 2015**|X|X|X*|X|X|X
|Visual Studio 2013**|X|X|X*|X
|Visual Studio 2010-
|Xamarin Studio 4 (Windows)
|MonoDevelop 7+|X||X|X|X
|MonoDevelop 5-
|SharpDevelop
|Rider 2017.1.2|X|X|X*|X|~|X

*Una marca indica que el IDE soporta el proyecto. Los IDE que no están listados
no han sido probados.*  
 ~: Soporte parcial  
 *: Requiere de componentes adicionales (Gtk#) instalados en el sistema  
**: Es necesario instalar un compilador para C# 7.0 o posterior (`PM> Install-Package Microsoft.Net.Compilers`)  
 +: Versiones posteriores  
 -: Versiones anteriores
