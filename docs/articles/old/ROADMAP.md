# Roadmap de MCART
### Introducción
En este documento, se describe el Roadmap y la planificación actual respecto a
la dirección en la que se espera que el proyecto avance. Se incluirán los
releases para los cuales exista una planificación futura, y se describirá lo
que se planea alcanzar en ese Milestone específico.

Debido a que este roadmap describe releases futuros, está sujeta a grandes
desviaciones de sus intenciones originales, además de grandes cambios.

## MCART 0.9 Series
Para la serie 0.9 de MCART, se pretende unificar todo el código de UI, además
de actualizar el código antíguo de diálogos y controles. También reorganizar
los espacios de nombre con el objetivo de finalmente eliminar los archivos
monolíticos del directorio `Modules`, separando toda su funcionalidad de
acuerdo a una clasificación coherente de métodos de extensión / métodos
auxiliares.

Este proceso será árduo, y durante los distintos releases de la serie 0.9 se
podrían dar muchísimas rupturas de compatibilidad con la API.

Uno de los cambios que deseo realizar en esta versión es una rescritura de todo
el soporte de generación de contraseñas, ya que el código anterior se basó en
prácticas sub-óptimas, obsoletas e incluso inseguras.

## MCART 0.10 Series
La serie 0.10 marcará un hito muy importante, ya que se espera que esta serie
sea la última en fase Pre-Alpha. Finalmente, podré considerar que el proyecto
ha madurado lo suficiente para entrar en una fase de curación y de redacción de
toda la documentación pertinente. En esta serie no espero realizar grandes
cambios, sin embargo, actualmente hay ciertos elementos en consideración para
su remoción, como ser el subsistema de controles para gráficas y datos. Este
fue uno de los detonantes de la deprecación temporal de Gtk# y Win32 en la
serie 0.8.

En la serie 0.10, procuraré reintegrar los proyectos de Gtk# y Win32, junto con
una tentativa nueva adición: Eto.Forms, y con ello, el soporte para todas las
plataformas de escritorio. Al incluir Eto.Forms, no pretendo deprecar ninguno
de los demás proyectos, ya que los mismos contendrán código optimizado para su
respectiva plataforma. En realidad, la idea es contar con una plataforma
universal a opción del desarrollador.

Pretendo también considerar la opción de mudar el código a C# 8.0, lo cual
podría modificar este roadmap considerablemente.

## MCART 0.11 Series
La serie 0.11 de MCART verá grandes cambios y ajustes al sistema de proyectos
de Visual Studio. Existe la posibilidad de ruptura de compatibilidad con otros
entornos de desarrollo, pero estudiaré cuidadosamente los efectos que estas
actualizaciones conlleven.

Afortunadamente, .Net Core 3.0 y el Microsoft.NET.Sdk ofrecen desarrollo y
compilación de código en múltiples plataformas sin necesidad de contar con un
IDE, por lo que esta será la dirección que tomaré.

Cabe mencionar que, a la fecha de escritura de este Roadmap, el proyecto se ha
reorganizado considerablemente. No estoy completamente seguro si esta será la
forma final del directorio del proyecto, pero las pruebas preliminares parecen
indicar un resultado satisfactorio.

Los cambios que pretendo hacer durante la serie 0.11 incluyen una revisión
exhaustiva del sistema de Plugins y un análisis de confiabilidad de redes y
comunicaciones, además que espero implementar Networking con *Peers* además de
las implementaciones existentes de cliente/servidor.

## MCART 0.12 Series **(NO CUMPLIDO)**
Finalmente integraré los nuevos proyectos de UI, además de cualquier librería
que pueda ser de utilidad. Pretendo también que sea la última versión de MCART
en fase Alpha. La API no debería sufrir cambios de ruptura de compatibilidad, a
menos que existan errores o problemas que obliguen a hacerlo.

Cabe destacar que, de identificarse nuevos múltiples puntos de mejora y cambios
que puedan producir ruptura de compatibilidad, este Roadmap será actualizado
para reflejar dicho evento, y podría planificarse la creación de la serie 0.13.

## MCART 0.13 Series
Las últimas dos versiones de MCART vieron un significativo cambio en la
dirección del proyecto, requiriendo al menos dos versiónes preeliminares
adicionales. En la serie 0.13 se pretende re-encaminar el proyecto en la
dirección apropiada, contando con cambios significativos en cuanto a limpieza y
estabilidad se refiere.

Han sido necesarias muchas reorganizaciones del código, y espero que en 0.13
series por fin haya un árbol de archivos coherente y funcional.

Uno de los grandes atascos existentes en MCART al día en que se creó 0.13, fue
la enorme existencia de características en Backlog, componentes grandes que
reemplazarían a otros que son legados, y otros cambios importantes en cuanto a
recursos, especialmente de cadenas. Dichos componentes deben tener una
arquitectura definida antes de cerrar el ciclo 0.13.

## MCART 0.14 Series
Al día de creación de 0.13 Series, se supuso que 0.14 sería la última versión
preliminar de MCART. Mientras el plan se mantenga, en la serie 0.14 se pulirán
los componentes nuevos que han sido arrastrados en Backlog desde 0.11 y 0.12.

También existe la posibilidad de finalmente traer de vuelta los proyectos de
Gtk# y Windows Forms, además de reconsiderar la integración de Cocoa y
Eto.Forms, en cuyo caso posiblemente deba considerarse la creación del ciclo
0.15 Series, para lo cual se actualizará este documento en el momento adecuado.

## MCART 1.0 y más allá
Durante la fase de Beta de MCART, el número de versión disponible dentro de las
aplicaciones será 1.0.0.0, sin embargo, debido a que NuGet soporta subfijos de
número de versión, se utilizarán estos para identificar de forma más eficaz las
diferentes versiones Beta de MCART 1.0.

Al llegar a este punto, MCART finalmente será un *release* público y soportado.
Eventualmente, MCART podría iniciar un proceso de evolución en algo más grande
y potente. Consideraré la magnitud de un cambio grande, y basado en su
proporción, tomaré la decisión de incrementar el número menor de versión de
MCART, para ser por ejemplo de la serie 1.1, o si el cambio es aún mayor y de
caracter evolutivo, o rompe la compatibilidad de la API de manera
significativa, entonces se iniciará el desarrollo de la serie 2.0 de MCART. 
