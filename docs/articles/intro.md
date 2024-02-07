# Acerca de MCART

## (Pre-)historia
Hace mucho tiempo, cuando comenc� a aprender programaci�n *circa* 1999~2000,
tuve la experiencia de iniciarme en la programaci�n en lenguajes como GWBASIC y
QuickBASIC. Estos son lenguajes que no ofrec�an muchas herramientas ni
funcionalidad incorporada de forma predeterminada, lo cual era relativamente
com�n para los lenguajes legacy de la �poca.

Con el tiempo, empec� a desarrollar peque�os programas o peque�os juegos con
una interactividad bastante limitada, todo esto en QuickBASIC. Debido a que QB
era un entorno tan limitado, tuve que crear distintas funciones que realizaban
peque�as tareas �tiles en mis programas. Desde funciones sencillas de centrado
de texto en pantalla, hasta funciones algo complejas que dibujaban cuadros de
texto para la entrada de datos, con todo y soporte para tabulacion,
desplazamiento y caracteres de contrase�a.

Originalmente llamado XDS!SUBS, ese proyecto se convirti� en mi repositorio de
funciones utilitarias de prop�sito general, y aunque no pudo ver la luz
p�blica, consider� ese proyecto como un gran logro personal. Todo esto en 2004,
a la edad de 11 a�os.

Hice algunos programas utilizando funcionalidad de XDS!SUBS. Uno de los m�s
impresionantes fue Basic Shell 3, el cual simplemente era una capa gr�fica
entre MS-DOS y el usuario. simplemente agregaba soporte para rat�n y algunos
botones con comandos de MS-DOS listos para ejecutar, adem�s de correr en modo
gr�fico (modo de pantalla 12) y ofrecer algunos comandos especiales para el
cambio de colores o para cambiar el modo de dibujo de BS3.

Eventualmente, aprend� Visual Basic. En aquel entonces, inici� con VBA en
Microsoft Office 2003 y luego pase a VB6. Intent� *portear* mis funciones de
XDS!SUBS a Visual Basic, y obviamente, la gran mayor�a de ellas no podrian
funcionar, ya que eran funciones graficas que lidiaban con el modo texto u
otros modos directos de dibujo para entornos MS-DOS.

Algunos de mis experimentos en Visual Basic se beneficiaron de mis funciones,
como ser peque�as calculadoras, y herramientas que lidiaban con n�meros
binarios. Incluso, intent� inventar un formato de almac�n de contrase�as
llamado DUF, el cual era irrisoriamente inseguro.

## Empezando a tomar forma
Es importante resaltar que, en este punto, XDS!SUBS dej� de tener ese nombre.
Utilic� los archivos de lo que fue XDS!SUBS como un lugar de experimentacion,
en donde intentaba ideas alocadas o implementaba retos autoimpuestos para
asegurarme de entender bien ciertos conceptos. Estaba migrando mis habilidades
de VB6 al relativamente nuevo VB.Net (la version de 2002, en el a�o 2006).

El proyecto a�n no ten�a un nombre como tal. Sin embargo, siendo un fan del
automovilismo, tuve la idea de llamar a este gran lugar para todo tipo de
funciones con el nombre de mi circuito de carreras favorito: N�rburgring.

N�rburgring no ten�a un objetivo en particular. Simplemente era un lugar donde
guardar c�digo e intentar ideas interesantes. Inclu�a una app de pruebas en
donde simplemente se demostraba el uso de las funciones incorporadas,
permitiendo a un nuevo desarrollador tomarlas como ejemplo y utilizarlas dentro
de sus programas.

Entre las funciones disponibles en N�rburgring se inclu�a un int�rprete del
lenguaje de programaci�n esot�rico [BrainFuck](https://en.wikipedia.org/wiki/Brainfuck),
una clase adaptador para realizar encriptado con AES, funciones de Checksum,
*parsers* de binario a tipos num�ricos, una clase para lectura/escritura del
anteriormente mencionado formato de archivo DUF (database of users file), un
motor de base de datos extremadamente rudimentario basado en archivos XML,
entre otra funcionalidad, alguna de la cual estaba incompleta/rota o exist�a
en .Net Framework.

Luego de varias revisiones y versiones, eliminaci�n de los experimentos
extra�os e inusuales y de toda la funcionalidad para la cual exist�a una
alternativa nativa en el BCL de .Net Framework, lo que ser�a finalmente MCART
empez� a tomar forma, alrededor del a�o 2010 mientras finalizaba el colegio.

Hoy en d�a, tengo un repositorio para experimentos extra�os e inusuales,
[Vulcanium](https://github.com/TheXDS/Vulcanium/).

## Enfocando el proyecto
La ultima versi�n repleta de funcionalidad de dudosa utilidad fue la
[0.3.3.80](https://github.com/TheXDS/MCART-Classic). En este punto finalmente
decid� lo que mi proyecto deb�a ser. Finalmente, decid� que podr�a crear una
librer�a de prop�sito general que realmente pudiera proveer a los
desarrolladores de algo �til. La version 0.4 finalmente llevaria el nombre de
MCART (Morgan's CLR Advanced Runtime), aunque a�n era referenciada por el
nombre N�rburgring.

MCART 0.4 se reescribi� por completo, quitando una gran cantidad de
funcionalidad que no era �til, o para la cual exist�a una alternativa viable en
.Net Framework. Con el pasar de nuevas versiones, se fueron agregando funciones
verdaderamente �tiles, adem�s de quitar las que no lo eran.

En 0.6 hubo un salto importante: MCART se reescribi� en C#. En 0.7 hubo otra
reescritura que trajo consigo una reestructuraci�n del c�digo. Luego, en 0.8
finalmente decid� publicar MCART como un paquete de [NuGet](https://www.nuget.org/packages/TheXDS.MCART/).

Durante el enfoque, proyectos para soportar y extender frameworks de UI fueron
creados y luego deprecados. Esto se debi� a que MCART ha sido siempre un
proyecto de pasi�n, no tanto uno que se comercialize o con el cual se hubiesen
creado apps para el mercado o el consumo p�blico (aunque, en esp�ritu esa era
la idea).

## Hoy en d�a
MCART sigue en desarrollo activo. Mi trabajo como desarrollador me impiden tal
vez hacer el progreso que me gustar�a, pero siempre procuro ocasionalmente
abrir el proyecto, revisar el c�digo, implementar alguna funcionalidad nueva,
escribir m�s pruebas unitarias, o simplemente apreciar la evoluci�n del
proyecto desde su g�nesis.

Espero que alg�n d�a pueda ver MCART en el punto en que lo envisiono: como una
librer�a de prop�sito general que los desarrolladores puedan utilizar para
agilizar sus flujos de trabajo, y proveerles de las herramientas que necesiten
para crear todo tipo de aplicaciones, sin importar el tama�o.