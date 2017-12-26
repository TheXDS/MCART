# Contribuciones a MCART
Agradezco que se tomen el tiempo de considerar hacer un aporte a MCART. MCART
está pensado como un "repositorio" de funciones que puedan resultar útiles para
otras personas. ¡Cualquier contribución que facilite el uso de C#/VB es
bienvenida!
### Ejecutando pruebas en MCART
La solución incluye proyectos de prueba compatibles con xUnit. Esto permite que
las pruebas puedan ser ejecutadas en cualquier IDE compatible. Cabe destacar que
actualmente, solo es posible realizar pruebas sobre las funciones de MCART, y no
directamente sobre elementos de UI. Con este fin, también se incluyen algunas
aplicaciones de ejemplo denominadas TestApp, que permiten realizar pruebas sobre
los elementos de UI de MCART.
### Reportar errores
Al enviar un nuevo reporte de error, el reporte debe incluir:
* Versión y Branch de MCART utilizada.
* Plataforma utilizada.
* Función que ha causado el error. Se necesita una ruta completa a la función,
por ejemplo, `MCART.Resources.RTInfo.RTSupport(Assembly)`
* Si el elemento acepta parámetros, se deben incluir los parámetros utilizados.
* Pasos para reproducir el error. (lo más específicos posible)
* Resultado esperado.
* Excepción o resultado obtenido.

En caso de un error de visualización, se debe considerar lo siguiente:
* Que MCART se ejecute en un equipo estable, que no presente fallas en sí
mismo.
* Que el error sea causado por un defecto en MCART.
* (Recomendado) que el reporte incluya una captura de pantalla del error, sin
exceder los límites de tamaño y resolución de 320x200. Si el error requiere más
de una imagen y/o una resuloción mayor a 320x200, se debería recurrir a un
servicio de hosting externo para las imágenes.

**Los errores generados por Unity Testing no son errores necesariamente, sino
que se deben considerar como diseños de implementación.**
### Nuevas características / funciones
Para brindar sugerencias sobre nuevas funciones, únicamente es necesario:
* Describir lo que la nueva característica debería hacer.
* Establecer una plataforma de destino. También, puede sugerirse una característica global.
* Que la característica no exista en .Net/Mono, o que se trate de una alternativa con mayor o mejor funcionalidad de la existente.
* Que la característica no entre en conflicto con otras ya implementadas en MCART.
* Preferiblemente, utilizar Unity Testing para describir la nueva función.

Para contribuir con nuevas funciones a MCART, es necesario establecer:
* **Plataforma de destino:** Determinará el proyecto en el cual se deben crear
aportaciones.
* **Dependencia de otros componentes:** Si bien MCART depende de componentes 
externos para algunos proyectos, como es el caso de Gtk# en Windows, la
dependencia en componentes externos debe minimizarse, aunque será posible
iniciar una discusión al respecto, basado en los motivos por los cuales MCART
debe depender de un tercer componente en particular.
### Soporte para nuevas plataformas
¡Si eres un programador experimentado en alguna plataforma de .Net/Mono (por ejemplo, Cocoa en macOs), tu aporte de un nuevo proyecto de MCART para soportarla es totalmente bienvenido! La idea de MCART es soportar todas las plataformas posibles en las cuales se encuentre disponible CIL. Se recomienda crear una nueva plataforma si:
* Se trata de un aporte especial para una plataforma.
* Es una reimplementación de un formulario/diálogo/ventana/página/control/widget de MCART en una nueva plataforma.

Personalmente, debo admitir que probablemente MCART para WPF sea la versión con
más avances, ya que es mi campo de mayor experiencia. Sin embargo, pretendo que
los otros proyectos no se queden atrás.
### Notas adicionales
#### Gtk:
El proyecto incluye paquetes de NuGet de Gtk# 3.22, por lo que en Microsoft
Windows® es necesario comprobar que se cuente con una copia de los respectivos
archivos DLL de GTK+ 3 en el directorio principal de la aplicación, junto al
ensamblado de MCART para Gtk#. Las distintas distribuciones de Linux
generalmente ya incluyen GTK+ 3, por lo que únicamente es necesario comprobar
que los paquetes correspondientes hayan sido instalados y se encuentren 
actualizados.

[Gtk# en NuGet](https://www.nuget.org/packages/GtkSharp)  
[Información para descargar e instalar Gtk+3 en Micrisift Windows®](https://www.gtk.org/download/windows.php)
#### C# 7.0
Respecto a la decisión de utilizar una versión tan nueva del lenguaje, C# 7.0
ofrece ciertas características de lenguaje sobre las cuales MCART depende.
Aunque los cambios necesarios para utilizar una versión anterior (C# 6.0 
específicamente) son relativamente pocos, la idea es poder reducir la
complejidad del código, aprovechando las características disponibles. Incluso,
se planea en un futuro cercano utilizar C# 7.1 cuando otros entornos de
desarrollo, aparte de Visual Studio, lo adopten.

Es posible utilizar la aplicación `dotnet` incluída en el .Net SDK 2.0 para
compilar código de C#7.0 (e incluso C# 7.1) en cualquier plataforma soportada.

[.Net SDK 2.0 (Windows)](https://www.microsoft.com/download/details.aspx?id=19988)  
[.Net SDK Getting Started (todas las plataformas)](https://www.microsoft.com/net/core)
#### Proyectos de ejemplos
Se incluyen algunas aplicaciones sencillas de prueba en la solución, su
propósito es el de probar ciertas características que requieren de una UI, y
que no podrían ser probadas directamente con xUnit. En caso de contribuir a 
MCART, se debe tomar en cuenta esta distinción antes de crear pruebas o un
proyecto de ejemplo.

Estos proyectos también pueden utilizarse con una implementación de referencia,
sin embargo, no deben considerarse como seguras y efectivas ninguna de las
prácticas que puedan parecer inusuales. Reitero, estos son proyectos de ejemplo
o de prueba, y están pensados únicamente para demostrar la API y realizar
pruebas de la UI.
## Para finalizar...
Este documento, junto con el resto de la documentación de MCART está sujeta a 
grandes cambios. es posible que aún tengas dudas respecto al proyecto. 
Recomiendo verificar con frecuencia si esta documentación ha cambiado. Para 
cualquier otra duda, pueden contactarme via 
[e-mail](mailto:xds_xps_ivx@hotmail.com).
