<div align="center">
<h1>
<img src="https://raw.githubusercontent.com/TheXDS/MCART/master/Art/MCART.png" alt="MCART" align="middle" heigth="64px" width="64px">
MCART
</h1>
</div>

[![Versión estable](https://buildstats.info/nuget/TheXDS.MCART)](https://www.nuget.org/packages/TheXDS.MCART/)
[![Versión de desarrollo](https://buildstats.info/nuget/TheXDS.MCART?includePreReleases=true)](https://www.nuget.org/packages/TheXDS.MCART/)
[![CodeFactor](https://www.codefactor.io/repository/github/thexds/mcart/badge)](https://www.codefactor.io/repository/github/thexds/mcart)

## Introducción
MCART es un conjunto de funciones, extensiones y módulos que he encontrado
útiles a lo largo de mis años de experiencia con lenguajes .Net,
particularmente con Visual Basic. Trata de añadir características que no se
encuentran fácilmente disponibles en .Net Framework, y además añade controles,
ventanas, recursos y otros objetos de utilidad.

Actualmente, se encuentra en una muy temprana fase Alpha, por lo que podría
tener bugs o problemas serios de rendimiento. He puesto mucho esfuerzo en
mantener un código funcional y, espero, libre de errores obvios. Sin embargo,
no puedo asegurar que MCART pueda ser utilizado en un paquete de software en su
estado actual.

## Composición del proyecto
MCART se compone de varios proyectos, y distintos proyectos de código
compartido entre las plataformas. En ellos reside la raíz de la mayoría de
características que pueden funcionar en diferentes entornos CIL. Hasta el
momento, la funcionalidad está comprobada con .Net Framework y Mono; lo que
debería ser suficiente para crear aplicaciones basadas en Win32, WPF, Gtk#,
consola e incluso sitios web desarrollados en ASP .Net.

## Releases
MCART se encuentra disponible en NuGet.

**Package Manager**  
`PM> Install-Package TheXDS.MCART`

**.NET CLI**  
`> dotnet add package TheXDS.MCART`

**Paket CLI**  
`> paket add TheXDS.MCART`

## Compilación
MCART requiere de un compilador compatible con C# 8.0, debido a ciertas
características especiales del lenguaje que ayudan a disminuir la
complejidad del código.

MCART también requiere que [.Net SDK](https://www.microsoft.com/net/core) 3.1
esté instalado en el sistema.

#### Componentes adicionales
[.Net SDK Getting Started (todas las plataformas)](https://www.microsoft.com/net/core)  
