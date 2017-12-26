# Bienvenido a la documentación de API de MCART

Bienvenido. Aquí encontrará una guía sobre toda la API disponible en MCART, una librería avanzada y biblioteca de controles/ventanas/widgets/helpers para aplicaciones de la plataforma .Net, como ser WinForms, WPF, Gtk#, .Net Core 2.0 y .Net Standard.



## Introducción

MCART es un conjunto de funciones, extensiones y módulos que he encontrado útiles a lo largo de mis años de experiencia con lenguajes .Net, particularmente con Visual Basic. Trata de añadir características que no se encuentran fácilmente disponibles en .Net Framework, y además añade controles, ventanas, recursos y otros objetos de utilidad.


Actualmente, se encuentra en una muy temprana fase Alpha, por lo que podría tener bugs o problemas serios de rendimiento. He puesto mucho esfuerzo en mantener un código funcional y, espero, libre de errores obvios. Sin embargo, no puedo asegurar que MCART pueda ser utilizado en un paquete de software en su estado actual.



## Composición del proyecto

MCART se compone de varios proyectos, y distintos proyectos de código compartido entre las plataformas. En ellos reside la raíz de la mayoría de características que pueden funcionar en diferentes entornos CIL. Hasta el momento, la funcionalidad está comprobada con .Net Framework y Mono; lo que debería ser suficiente para crear aplicaciones basadas en Win32, WPF, Gtk#, consola e incluso sitios web desarrollados en ASP .Net.


Los demás proyectos apuntan a crear ensamblados para diferentes plataformas. Actualmente, existen proyectos para crear ensamblados para Windows Presentation Framework, Win32 y GtkSharp (Gtk#), además de dos proyectos que no son específicos para una plataforma en particular, uno de los cuales es compatible con .Net Standard y otro compilado para .Net Framework con la menor cantidad de referencias posibles, denomidado CoreEdition. También, existen planes de incorporar otros proyectos para soportar Universal Windows Platform (UWP) y, a un futuro, Cocoa para macOs.



## See Also


#### Other Resources
<a href="a43a94b2-1336-49d7-b56e-160b8ed4dd09">Historial de versiones</a><br />