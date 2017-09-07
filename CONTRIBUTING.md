# Contribuciones a MCART
Agradezco que se tomen el tiempo de considerar hacer un aporte a MCART. MCART está pensado como un "repositorio" de funciones que puedan resultar útiles para otras personas. ¡Cualquier contribución que facilite el uso de C#/VB es bienvenida!
### Ejecutando pruebas en MCART
La solución incluye proyectos de prueba compatibles con UnitTesting de Microsoft Visual Studio. Debido a problemas de estabilidad y compatibilidad, los proyectos de prueba no se crearon utilizando NUnit Framework (lo que hubiese permitido que las pruebas funcionaran en MonoDevelop).

### Reportar errores
Al enviar un nuevo reporte de error, el reporte debe incluir:
* Versión y Branch de MCART utilizada.
* Plataforma utilizada.
* Función que ha causado el error.
Se necesita una ruta completa a la función, por ejemplo, `MCART.Resources.RTInfo.RTSupport(Assembly)`
* Si el elemento acepta parámetros, se deben incluir los parámetros utilizados.
* Pasos para reproducir el error. (lo más específicos posible)
* Resultado esperado.
* Excepción o resultado obtenido.

En caso de un error de visualización, se debe considerar lo siguiente:
* Que MCART se ejecute en un equipo estable, que no presente fallas en sí mismo.
* Que el error sea causado por un defecto en MCART.
* (Recomendado) que el reporte incluya una captura de pantalla del error, sin exceder los límites de tamaño y resolución de 320x200. Si el error requiere más de una imagen y/o una resuloción mayor a 320x200, se debería recurrir a un servicio de hosting externo para las imágenes.

### Nuevas características / funciones
Para brindar sugerencias sobre nuevas funciones, únicamente es necesario:
* Describir lo que la nueva característica debería hacer.
* Establecer una plataforma de destino. También, puede sugerirse una característica global.
* Que la característica no exista en .Net/Mono, o que se trate de una alternativa con mayor o mejor funcionalidad de la existente.
* Que la característica no entre en conflicto con otras ya implementadas en MCART.

Para contribuir con nuevas funciones a MCART, es necesario establecer:
* Plataforma de destino: Determinará el proyecto en el cual se deben crear aportaciones.
* Dependencia de otros componentes: Si bien MCART depende de componentes externos para algunos proyectos, como es el caso de Gtk# en Windows, la dependencia en componentes externos debe minimizarse, aunque será posible iniciar una discusión al respecto, basado en los motivos por los cuales MCART debe depender de un tercer componente en particular.

### Soporte para nuevas plataformas
¡Si eres un programador experimentado en alguna plataforma de .Net/Mono (por ejemplo, Cocoa en macOs), tu aporte de un nuevo proyecto de MCART para soportarla es totalmente bienvenido! La idea de MCART es soportar todas las plataformas posibles en las cuales se encuentre disponible CIL. Se recomienda crear una nueva plataforma si:
* Se trata de un aporte especial para una plataforma.
* Es una reimplementación de un formulario/diálogo/ventana/página/control/widget de MCART en una nueva plataforma.

### Notas adicionales
#### Gtk:
En Microsoft Windows, El proyecto Gtk requiere instalar Gtk# 2.12 o posterior, el cual se incluye en Xamarin Studio, y además está disponible como componente independiente.
[Gtk# para .Net Framework](http://www.mono-project.com/docs/gui/gtksharp/installer-for-net-framework/)
[Gtk# en NuGet](https://www.nuget.org/packages/GtkSharp)
[Código fuente de Gtk#](https://download.mono-project.com/sources/gtk-sharp212/)
#### Proyectos Bundles
## Para finalizar...
Este documento, junto con el resto de la documentación de MCART está sujeta a grandes cambios. es posible que aún tengas dudas respecto al proyecto. Recomiendo verificar con frecuencia si esta documentación ha cambiado. Para cualquier otra duda, pueden contactarme via [Facebook](https://facebook.com/cesar.morgan) o [e-mail](mailto:xds_xps_ivx@hotmail.com).
