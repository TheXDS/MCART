# Información técnica de desarrollo
En este archivo, se detallan algunas consideraciones técnicas al trabajar en
MCART. Para un eficiente flujo de trabajo, por favor, lee todo este documento.
### Creando nuevos proyectos
Los proyectos comparten un conjunto de constantes de compilación. Debido a
algunas limitaciones presentadas por Visual Studio, no existe un editor visual
de estas configuraciones, y no existe un mecanismo elegante para administrar
las configuraciones globales. Por ende, es necesario agregar el siguiente nodo
a la definición de cada proyecto:
```xml
  <PropertyGroup Condition="'$(SolutionDir)' == '' or '$(SolutionDir)' == '*undefined*'">
    <SolutionDir>..\..\</SolutionDir>
  </PropertyGroup>
  <Import Project="$(SolutionDir)CommonSettings.targets" />
```
Al agregar este bloque de código a los proyectos, es posible editar las
constantes de compilación globales editando el archivo
`CommonSettings.targets`.
### Constantes globales de compilación
El archivo `CommonSettings.targets` contiene un conjunto de constantes de
compilación definidas para toda la solución. Esto, con el propósito de evitar
los problemas que pueden surgir al configurar individualmente cada proyecto, y
permitir compartir eficientemente dichas constantes.

La siguiente tabla incluye las constantes existentes y el efecto que producen
al activarse:

Constante | Efecto
--- | ---
BufferedIO | Algunas funciones de entrada/salida incluyen una implementación opcional con búffer. Activar esta opción habilita las lecturas y escrituras con búffer.
ExtrasBuiltIn | Incluir en el ensamblado de MCART ejemplos e implementaciones estándar básicas de las interfaces o clases abstractas para las cuales se pueda proveer.
FloatDoubleSpecial | `float` y `double` son tipos numéricos que pueden contener valores especiales, como ser NaN o infinito. Al activar esta constante, se habilitan métodos especiales que pueden trabajar con estos valores.
NativeNumbers | Permite utilizar implementaciones conscientes de la cultura nativa para algunas funciones que trabajan con símbolos numéricos, utilizando los dígitos numéricos locales.
PreferExceptions | Cuando ciertas funciones deban manejar información inválida, activar esta constante causa que se arrojen excepciones en lugar de continuar con código alternativo (activar esta bandera puede ser un dolor de cabeza, pero resulta en código más seguro).
RatherDRY | Indica que, a pesar de disminuir la optimización del código, se debe respetar el principio DRY (Don't Repeat Yourself) al implementar sobrecargas cuyo cuerpo sea exactamente igual.
StrictMCARTVersioning | Cuando se realicen comprobaciones de compatibilidad con MCART, activar esta constante causa que dichas comprobaciones sean más estrictas. Se recomienda mantener esta constante habilitada.