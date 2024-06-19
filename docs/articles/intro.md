# Acerca de MCART

## (Pre-)historia
Hace mucho tiempo, cuando comencé a aprender programación *circa* 1999~2000,
tuve la experiencia de iniciarme en la programación en lenguajes como GWBASIC y
QuickBASIC. Estos son lenguajes que no ofrecían muchas herramientas ni
funcionalidad incorporada de forma predeterminada, lo cual era relativamente
común para los lenguajes legacy de la época.

Con el tiempo, empecé a desarrollar pequeńos programas o pequeńos juegos con
una interactividad bastante limitada, todo esto en QuickBASIC. Debido a que QB
era un entorno tan limitado, tuve que crear distintas funciones que realizaban
pequeńas tareas útiles en mis programas. Desde funciones sencillas de centrado
de texto en pantalla, hasta funciones algo complejas que dibujaban cuadros de
texto para la entrada de datos, con todo y soporte para tabulacion,
desplazamiento y caracteres de contraseńa.

Originalmente llamado XDS!SUBS, ese proyecto se convirtió en mi repositorio de
funciones utilitarias de propósito general, y aunque no pudo ver la luz
pública, consideré ese proyecto como un gran logro personal. Todo esto en 2004,
a la edad de 11 ańos.

Hice algunos programas utilizando funcionalidad de XDS!SUBS. Uno de los más
impresionantes fue Basic Shell 3, el cual simplemente era una capa gráfica
entre MS-DOS y el usuario. simplemente agregaba soporte para ratón y algunos
botones con comandos de MS-DOS listos para ejecutar, además de correr en modo
gráfico (modo de pantalla 12) y ofrecer algunos comandos especiales para el
cambio de colores o para cambiar el modo de dibujo de BS3.

Eventualmente, aprendí Visual Basic. En aquel entonces, inicié con VBA en
Microsoft Office 2003 y luego pase a VB6. Intenté *portear* mis funciones de
XDS!SUBS a Visual Basic, y obviamente, la gran mayoría de ellas no podrian
funcionar, ya que eran funciones graficas que lidiaban con el modo texto u
otros modos directos de dibujo para entornos MS-DOS.

Algunos de mis experimentos en Visual Basic se beneficiaron de mis funciones,
como ser pequeńas calculadoras, y herramientas que lidiaban con números
binarios. Incluso, intenté inventar un formato de almacén de contraseńas
llamado DUF, el cual era irrisoriamente inseguro.

## Empezando a tomar forma
Es importante resaltar que, en este punto, XDS!SUBS dejó de tener ese nombre.
Utilicé los archivos de lo que fue XDS!SUBS como un lugar de experimentacion,
en donde intentaba ideas alocadas o implementaba retos autoimpuestos para
asegurarme de entender bien ciertos conceptos. Estaba migrando mis habilidades
de VB6 al relativamente nuevo VB.Net (la version de 2002, en el ańo 2006).

El proyecto aún no tenía un nombre como tal. Sin embargo, siendo un fan del
automovilismo, tuve la idea de llamar a este gran lugar para todo tipo de
funciones con el nombre de mi circuito de carreras favorito: Nürburgring.

Nürburgring no tenía un objetivo en particular. Simplemente era un lugar donde
guardar código e intentar ideas interesantes. Incluía una app de pruebas en
donde simplemente se demostraba el uso de las funciones incorporadas,
permitiendo a un nuevo desarrollador tomarlas como ejemplo y utilizarlas dentro
de sus programas.

Entre las funciones disponibles en Nürburgring se incluía un intérprete del
lenguaje de programación esotérico [BrainFuck](https://en.wikipedia.org/wiki/Brainfuck),
una clase adaptador para realizar encriptado con AES, funciones de Checksum,
*parsers* de binario a tipos numéricos, una clase para lectura/escritura del
anteriormente mencionado formato de archivo DUF (database of users file), un
motor de base de datos extremadamente rudimentario basado en archivos XML,
entre otra funcionalidad, alguna de la cual estaba incompleta/rota o existía
en .Net Framework.

Luego de varias revisiones y versiones, eliminación de los experimentos
extrańos e inusuales y de toda la funcionalidad para la cual existía una
alternativa nativa en el BCL de .Net Framework, lo que sería finalmente MCART
empezó a tomar forma, alrededor del ańo 2010 mientras finalizaba el colegio.

Hoy en día, tengo un repositorio para experimentos extrańos e inusuales,
[Vulcanium](https://github.com/TheXDS/Vulcanium/).

## Enfocando el proyecto
La ultima versión repleta de funcionalidad de dudosa utilidad fue la
[0.3.3.80](https://github.com/TheXDS/MCART-Classic). En este punto finalmente
decidí lo que mi proyecto debía ser. Finalmente, decidí que podría crear una
librería de propósito general que realmente pudiera proveer a los
desarrolladores de algo útil. La version 0.4 finalmente llevaria el nombre de
MCART (Morgan's CLR Advanced Runtime), aunque aún era referenciada por el
nombre Nürburgring.

MCART 0.4 se reescribió por completo, quitando una gran cantidad de
funcionalidad que no era útil, o para la cual existía una alternativa viable en
.Net Framework. Con el pasar de nuevas versiones, se fueron agregando funciones
verdaderamente útiles, además de quitar las que no lo eran.

En 0.6 hubo un salto importante: MCART se reescribió en C#. En 0.7 hubo otra
reescritura que trajo consigo una reestructuración del código. Luego, en 0.8
finalmente decidí publicar MCART como un paquete de [NuGet](https://www.nuget.org/packages/TheXDS.MCART/).

Durante el enfoque, proyectos para soportar y extender frameworks de UI fueron
creados y luego deprecados. Esto se debió a que MCART ha sido siempre un
proyecto de pasión, no tanto uno que se comercialize o con el cual se hubiesen
creado apps para el mercado o el consumo público (aunque, en espíritu esa era
la idea).

## Hoy en día
MCART sigue en desarrollo activo. Mi trabajo como desarrollador me impiden tal
vez hacer el progreso que me gustaría, pero siempre procuro ocasionalmente
abrir el proyecto, revisar el código, implementar alguna funcionalidad nueva,
escribir más pruebas unitarias, o simplemente apreciar la evolución del
proyecto desde su génesis.

Espero que algún día pueda ver MCART en el punto en que lo envisiono: como una
librería de propósito general que los desarrolladores puedan utilizar para
agilizar sus flujos de trabajo, y proveerles de las herramientas que necesiten
para crear todo tipo de aplicaciones, sin importar el tamańo.