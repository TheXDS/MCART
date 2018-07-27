# Informaci�n de errores conocidos
En la siguiente secci�n se describen los errores conocidos (y en algunos casos,
un workaround para los mismos) que existen dentro de MCART. Cuando el proyecto
finalmente vea un release, este archivo ser� eliminado, y se utilizar� el
sistema de control de problemas existente en el servicio Git en donde el
proyecto se encuentre alojado; excepto si el servicio no incluye tal
caracter�stica.

La decisi�n de crear este archivo obedece a tres factores:
1) Actualmente, no existen contribuyentes a parte de su creador
2) Mientras el proyecto est� en fase pre-alpha, no vale la pena congestionar el repositorio con tickets de problemas.
3) Los problemas descritos aqu� podr�an llegar a permanecer a largo plazo durante el desarrollo de MCART.

Por lo general, �nicamente los programadores de este proyecto aportar�an
informaci�n de problemas conocidos a trav�s de este archivo, ya que la
plataforma de tickets tiene como prop�sito brindarle esa posibilidad a
cualquier persona que no est� involucrada en este proyecto a un nivel tan
profundo.

## Stack de red de MCART
#### Problema:
El Stack de red de MCART para comunicaciones entre cliente y servidor no
funciona.
#### Detalles t�cnicos:
En alg�n punto del desarrollo de esta caracter�stica, hubo un problema en el
planteamiento de la forma en que se abren y administran las conexiones TCP.
Al abrir el puerto y escribir en el Stream subyacente, los bytes no se env�an
hasta no cerrar la conexi�n, lo cual no es un escenario deseado.
#### Comentarios
El Stack de red ha sido reimplementado posiblemente unas 2 o 3 veces.

La primera versi�n utilizaba un c�digo con m�todos algo arc�icos para realizar
todas las comunicaciones, y fue tomado de un ejemplo algo viejo.

La segunda implementaci�n fue utilizada en MCART 0.4, y aunque conten�a
funcionalidad completa, carec�a totalmente de m�todos as�ncronos.

La tercera implementaci�n es una evoluci�n del stack de red de MCART 0.4.3.0,
tomando en cuenta la traducci�n de todo el c�digo de VB a C#. A este punto
las funciones a�n no eran as�ncronas, pero el Stack de red en general fue
mejorado en cuanto a robustez.

Para las �ltimas dos versiones de MCART, se tuvo como objetivo mejorar la
implementaci�n del Stack de red para finalmente utilizar m�todos as�ncronos
y una mejor administraci�n de las conexiones abiertas. En alg�n punto de esta
transici�n se perdi� el control sobre c�mo deb�a operar MCART sobre las
conexiones entrantes, y actualmente el Stack se encuentra en el estado
descrito.

La imposibilidad de obtener una revisi�n anterior de MCART (debido a no existir
un control de versiones para ning�na compilaci�n de MCART anterior al 4 de
julio de 2017, y la p�rdida de las copias de versiones anteriores) evit� que un
Backport fuese posible. Sin embargo, recientemente una copia funcional de
MCART 0.4.4.0 fue encontrada entre los archivos. Esta copia ser� decompilada y
analizada con el fin de obtener el componente de red que fue implementado, y un
Backport se ha planificado tentativamente para el build 0.8.7.0.