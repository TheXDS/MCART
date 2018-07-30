# Información de errores conocidos
En la siguiente sección se describen los errores conocidos (y en algunos casos,
un workaround para los mismos) que existen dentro de MCART. Cuando el proyecto
finalmente vea un release, este archivo será eliminado, y se utilizará el
sistema de control de problemas existente en el servicio Git en donde el
proyecto se encuentre alojado; excepto si el servicio no incluye tal
característica.

La decisión de crear este archivo obedece a tres factores:
1) Actualmente, no existen contribuyentes a parte de su creador
2) Mientras el proyecto esté en fase pre-alpha, no vale la pena congestionar el repositorio con tickets de problemas.
3) Los problemas descritos aquí podrían llegar a permanecer a largo plazo durante el desarrollo de MCART.

Por lo general, únicamente los programadores de este proyecto aportarían
información de problemas conocidos a través de este archivo, ya que la
plataforma de tickets tiene como propósito brindarle esa posibilidad a
cualquier persona que no esté involucrada en este proyecto a un nivel tan
profundo.

## Stack de red de MCART
#### Problema:
El Stack de red de MCART para comunicaciones entre cliente y servidor no
funciona.
#### Detalles técnicos:
En algún punto del desarrollo de esta característica, hubo un problema en el
planteamiento de la forma en que se abren y administran las conexiones TCP.
Al abrir el puerto y escribir en el Stream subyacente, los bytes no se envían
hasta no cerrar la conexión, lo cual no es un escenario deseado.
#### Comentarios
El Stack de red ha sido reimplementado posiblemente unas 2 o 3 veces.

La primera versión utilizaba un código con métodos algo arcáicos para realizar
todas las comunicaciones, y fue tomado de un ejemplo algo viejo.

La segunda implementación fue utilizada en MCART 0.4, y aunque contenía
funcionalidad completa, carecía totalmente de métodos asíncronos.

La tercera implementación es una evolución del stack de red de MCART 0.4.3.0,
tomando en cuenta la traducción de todo el código de VB a C#. A este punto
las funciones aún no eran asíncronas, pero el Stack de red en general fue
mejorado en cuanto a robustez.

Para las últimas dos versiones de MCART, se tuvo como objetivo mejorar la
implementación del Stack de red para finalmente utilizar métodos asíncronos
y una mejor administración de las conexiones abiertas. En algún punto de esta
transición se perdió el control sobre cómo debía operar MCART sobre las
conexiones entrantes, y actualmente el Stack se encuentra en el estado
descrito.

La imposibilidad de obtener una revisión anterior de MCART (debido a no existir
un control de versiones para ningúna compilación de MCART anterior al 4 de
julio de 2017, y la pérdida de las copias de versiones anteriores) evitó que un
Backport fuese posible. Sin embargo, recientemente una copia funcional de
MCART 0.4.4.0 fue encontrada entre los archivos. Esta copia será decompilada y
analizada con el fin de obtener el componente de red que fue implementado, y un
Backport se ha planificado tentativamente para el build 0.8.7.0.