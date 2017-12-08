# Informaci�n t�cnica de desarrollo
En este archivo, se detallan algunas consideraciones t�cnicas al trabajar en
MCART. Para un eficiente flujo de trabajo, por favor, lee todo este documento.
### Creando nuevos proyectos
Los proyectos comparten un conjunto de constantes de compilaci�n. Debido a
algunas limitaciones presentadas por Visual Studio, no existe un editor visual
de estas configuraciones, y no existe un mecanismo elegante para administrar
las configuraciones globales. Por ende, es necesario agregar el siguiente nodo
a la definici�n de cada proyecto:
```xml
  <PropertyGroup Condition="'$(SolutionDir)' == '' or '$(SolutionDir)' == '*undefined*'">
    <SolutionDir>..\..\</SolutionDir>
  </PropertyGroup>
  <Import Project="$(SolutionDir)CommonSettings.targets" />
```
Al agregar este bloque de c�digo a los proyectos, es posible cambiar las
constantes de compilaci�n globales editando el archivo
`CommonSettings.targets`.
### N�meros de versi�n
La informaci�n gen�rica de los ensamblados de MCART se encuentra en el proyecto
compartido *AssemblyInfo*, el cual contiene �nicamente un archivo con las
respectivas definiciones de atributos de versi�n, copyright, compa��a,
trademark y nombre del producto. Al crear nuevos proyectos, es necesario
incluir una referencia a este proyecto para evitar mantener copias innecesarias
de los atributos, y centralizar la informaci�n de versiones y de
identificaci�n.

Durante la fase Pre-release de MCART, la versi�n mayor tendr� un valor de cero,
siendo necesario referirse al componente menor para evaluar el estado de avance
del proyecto. Eventualmente, al existir un Release, el comportamiento de los
distintos n�meros que componen la versi�n pasar� a representar el estado de
manera normal.

Luego de ocurrir un Release final, los �ltimos componentes pasar�n a ser la
fecha codificada de compilaci�n de los ensamblados.

**Notas adicionales para NetStandard:**
Debido a la forma en la que dichos atributos se encuentran almacenados en un
proyecto de este tipo, ser� necesario actualizar manualmente la informaci�n de
ensamblado de *NetStandard* al compilar. Por favor, no olvides realizar estos
cambios y recompilar *NetStandard* por separado.
### Constantes globales de compilaci�n
El archivo `CommonSettings.targets` contiene un conjunto de constantes de
compilaci�n definidas para toda la soluci�n. Esto, con el prop�sito de evitar
los problemas que pueden surgir al configurar individualmente cada proyecto, y
permitir compartir eficientemente dichas constantes.

La siguiente tabla incluye las constantes existentes y el efecto que producen
al activarse:

Constante | Efecto
--- | ---
BufferedIO | Algunas funciones de entrada/salida incluyen una implementaci�n opcional con b�ffer. Activar esta opci�n habilita las lecturas y escrituras con b�ffer.
ExtrasBuiltIn | Incluir en el ensamblado de MCART ejemplos e implementaciones est�ndar b�sicas de las interfaces o clases abstractas para las cuales se pueda proveer.
FloatDoubleSpecial | `float` y `double` son tipos num�ricos que pueden contener valores especiales, como ser NaN o infinito. Al activar esta constante, se habilitan m�todos especiales que pueden trabajar con estos valores.
NativeNumbers | Permite utilizar implementaciones conscientes de la cultura nativa para algunas funciones que trabajan con s�mbolos num�ricos, utilizando los d�gitos num�ricos locales.
PreferExceptions | Cuando ciertas funciones deban manejar informaci�n inv�lida, activar esta constante causa que se arrojen excepciones en lugar de continuar con c�digo alternativo (activar esta bandera puede ser un dolor de cabeza, pero resulta en c�digo m�s seguro).
RatherDRY | Indica que, a pesar de disminuir la optimizaci�n del c�digo, se debe respetar el principio DRY (Don't Repeat Yourself) al implementar sobrecargas cuyo cuerpo sea exactamente igual.
StrictMCARTVersioning | Cuando se realicen comprobaciones de compatibilidad con MCART, activar esta constante causa que dichas comprobaciones sean m�s estrictas. Se recomienda mantener esta constante habilitada.