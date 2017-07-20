# MCART
### Librería de funciones varias para aplicaciones .Net/Mono
MCART es un conjunto de funciones, extensiones y módulos que he encontrado útiles a lo largo de mis años de experiencia con lenguajes .Net, particularmente con Visual Basic. Trata de añadir características que no se encuentran fácilmente disponibles en .Net Framework, y además añade controles, ventanas, recursos y otros objetos de utilidad.

Actualmente, se encuentra en una muy temprana fase Alpha, por lo que podría tener bugs o problemas serios de rendimiento. He puesto mucho esfuerzo en mantener un código funcional y, espero, libre de errores obvios. Sin embargo, no puedo asegurar que MCART pueda ser utilizado en un paquete de software en su estado actual.

MCART se compone de varios proyectos, y un proyecto de código compartido, Core. Aquí reside la raíz de la mayoría de funciones básicas que pueden funcionar en diferentes entornos CIL. Hasta el momento, la funcionalidad está comprobada con .Net Framework y Mono; lo que debería ser suficiente para crear aplicaciones basadas en WPF y Gtk#.

Los demás proyectos apuntan a crear ensamblados para diferentes plataformas. Actualmente, existe un proyecto para crear un ensamblado para Windows Presentation Framework (WPF) y otro para GtkSharp (Gtk#), y existen planes de incorporar otros proyectos para soportar Universal Windows Platform (UWP), Windows Forms (Win32) y a un futuro, Cocoa para macOs.

## Compilación
La siguiente tabla indica los entornos de desarrollo y las plataformas en las cuales MCART puede ser compilado:

| Entorno de desarollo | WPF | Gtk | Win32 (TODO) | UWP (TODO) |
| --- | :---: | :---: | :---: | :---: |
| Visual Studio 2015+ (Windows) | X | X* | X | X |
| Visual Studio 2013 (Windows)** | X | X* | X | |
| Visual Studio 2010- (Windows) | | | | |
| Visual Studio 2010- (Linux via Wine) | | | | |
| Xamarin Studio 4+ (Windows) | | X | X | |
| MonoDevelop 5+ (Linux) | | X | X | |
| SharpDevelop (Windows) | | | | |

 *: Requiere de componentes adicionales (Gtk#) instalados en el sistema.  
**: Es necesario instalar un compilador para C# 6.0 (`PM> Install-Package Microsoft.Net.Compilers`)  
 +: Versiones posteriores.  
 -: Versiones anteriores.
