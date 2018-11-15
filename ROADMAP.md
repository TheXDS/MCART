# Roadmap de MCART
### Introducción
En este documento, se describe el Roadmap y la planificación actual respecto a la dirección en la que se espera que el proyecto avance. Se incluirán los releases para los cuales exista una planificación futura, y se describirá lo que se planea alcanzar en ese Milestone específico.

Debido a que este roadmap describe releases futuros, está sujeta a grandes desviaciones de sus intenciones originales, además de grandes cambios.

## MCART 0.9 Series
Para la serie 0.9 de MCART, se pretende unificar todo el código de UI, además de actualizar el código antíguo de diálogos y controles. También reorganizar los espacios de nombre con el objetivo de finalmente eliminar los archivos monolíticos del directorio ```Modules```, separando toda su funcionalidad de acuerdo a una clasificación coherente de métodos de extensión / métodos auxiliares.

Este proceso será árduo, y durante los distintos releases de la serie 0.9 se podrían dar muchísimas rupturas de compatibilidad con la API.

Uno de los cambios que deseo realizar en esta versión es una rescritura de todo el soporte de generación de contraseñas, ya que el código anterior se basó en prácticas sub-óptimas, obsoletas e incluso inseguras.

## MCART 0.10 Series
La serie 0.10 marcará un hito muy importante, ya que se espera que esta serie sea la última en fase Pre-Alpha. Finalmente, podré considerar que el proyecto ha madurado lo suficiente para entrar en una fase de curación y de redacción de toda la documentación pertinente. En esta serie no espero realizar grandes cambios, sin embargo, actualmente hay ciertos elementos en consideración para su remoción, como ser el subsistema de controles para gráficas y datos. Este fue uno de los detonantes de la deprecación temporal de Gtk# y Win32 en la serie 0.8.

En la serie 0.10, procuraré reintegrar los proyectos de Gtk# y Win32, junto con una tentativa nueva adición: Eto.Forms, y con ello, el soporte para todas las plataformas de escritorio. Al incluir Eto.Forms, no pretendo deprecar ninguno de los demás proyectos, ya que los mismos contendrán código optimizado para su respectiva plataforma. En realidad, la idea es contar con una plataforma universal a opción del desarrollador.

## MCART 0.11 Series
También un hito muy importante, la serie 0.11 será oficialmente la Beta de MCART. A este punto, la API no debería cambiar, y únicamente se trabajará en correcciones de errores y limpieza del código. Aún así, podría tomarme la libertad de agregar nueva funcionalidad si así fuese necesario.

## MCART 1.0 y más allá
Al llegar a este punto, MCART finalmente será un *release* público y soportado. Serán necesarios ciertos cambios menores en la información de identificación de versión (tal y como está descrito en [TECHINFO.md](https://github.com/TheXDS/MCART/blob/master/TECHINFO.md)) y se espera que la API que se encuentre ya implementada no sufra de ningún cambio. Cualquier expansión que no rompa la compatibilidad de la API será parte de la serie 1.x.

Eventualmente, MCART podría iniciar un proceso de evolución en algo más grande y potente. Consideraré la magnitud de un cambio grande, y basado en su proporción, tomaré la decisión de incrementar el número menor de versión de MCART, para ser por ejemplo de la serie 1.1, o si el cambio es aún mayor y de caracter evolutivo, o rompe la compatibilidad de la API de manera significativa, entonces se iniciará el desarrollo de la serie 2.0 de MCART. 

Los números de versión a utilizar durante el desarrollo de la serie 2.x continuarán desde la serie 1.99, aunque aún estoy debatiendo la viabilidad de este número de versión.