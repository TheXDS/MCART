<table>
<tr>
<td>
<h1 align="center">
<img src="https://raw.githubusercontent.com/TheXDS/MCART/master/Art/MCART.png" width="96px">
MCART
</h1>
</td>
<td>

[![CodeFactor](https://www.codefactor.io/repository/github/thexds/mcart/badge)](https://www.codefactor.io/repository/github/thexds/mcart)
[![codecov](https://codecov.io/gh/TheXDS/MCART/branch/master/graph/badge.svg?token=B3WZ7C4VTS)](https://codecov.io/gh/TheXDS/MCART)
[![Build MCART](https://github.com/TheXDS/MCART/actions/workflows/build.yml/badge.svg)](https://github.com/TheXDS/MCART/actions/workflows/build.yml)
[![Publish MCART](https://github.com/TheXDS/MCART/actions/workflows/publish.yml/badge.svg)](https://github.com/TheXDS/MCART/actions/workflows/publish.yml)
[![Issues](https://img.shields.io/github/issues/TheXDS/MCART)](https://github.com/TheXDS/MCART/issues)
[![MIT](https://img.shields.io/github/license/TheXDS/MCART)](https://mit-license.org/)

</td>
</tr>
</table>

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
características que pueden funcionar en diferentes entornos CIL. La
funcionalidad está comprobada con .Net 6.0; lo que debería ser suficiente
para crear aplicaciones basadas en Win32, WPF, Gtk#, consola e incluso sitios
web desarrollados en ASP .Net.

## Releases
MCART se encuentra disponible en NuGet y en mi repositorio privado de GitHub.

Release | Link
--- | ---
Última versión estable: | [![Versión estable](https://buildstats.info/nuget/TheXDS.MCART)](https://www.nuget.org/packages/TheXDS.MCART/)
Última versión de desarrollo: | [![Versión de desarrollo](https://buildstats.info/nuget/TheXDS.MCART?includePreReleases=true)](https://www.nuget.org/packages/TheXDS.MCART/)

**Package Manager**  
```sh
Install-Package TheXDS.MCART
```

**.NET CLI**  
```sh
dotnet add package TheXDS.MCART
```

**Paket CLI**  
```sh
paket add TheXDS.MCART
```

**Referencia de paquete**  
```xml
<PackageReference Include="TheXDS.MCART" Version="0.17.0" />
```

**Ventana interactiva (CSI)**  
```
#r "nuget: TheXDS.MCART, 0.17.0"
```

#### Repositorio de GitHub
Para obtener los paquetes de MCART directamente desde GitHub, es necesario
agregar mi repositorio privado. Para lograr esto, solo es necesario
ejecutar en una terminal:
```sh
nuget sources add -Name "TheXDS GitHub Repo" -Source https://nuget.pkg.github.com/TheXDS/index.json
```

## Compilación
MCART requiere de un compilador compatible con C# 10.0, debido a ciertas
características especiales del lenguaje que ayudan a disminuir la
complejidad del código.

MCART también requiere que [.Net SDK 6.0](https://dotnet.microsoft.com/) esté
instalado en el sistema.

### Compilando MCART
```sh
dotnet build ./src/MCART.sln
```
Los binarios se encontrarán en la carpeta `Build` en la raíz del repositorio.

### Ejecutando pruebas
```sh
dotnet test ./src/MCART.sln
```
#### Reporte de cobertura
Es posible obtener un reporte de la cobertura de código de manera local. Para ello, es necesario instalar 
[`ReportGenerator`](https://github.com/danielpalme/ReportGenerator) , que leerá los resultados de la ejecución de las pruebas, y generará una página web con el resultado de la cobertura.

Para instalar `ReportGenerator` ejecuta:
```sh
dotnet tool install -g dotnet-reportgenerator-globaltool
```
Luego de haber instalado `ReportGenerator`, será posible ejecutar el siguiente comando:
```sh
dotnet test ./src/MCART.sln --collect:"XPlat Code Coverage" --results-directory:./Build/Tests ; reportgenerator.exe -reports:./Build/Tests/*/coverage.cobertura.xml -targetdir:./Build/Coverage/
```
Los resultados de la cobertura se almacenarán en `./Build/Coverage`

## Contribuir
[![Buy Me A Coffee](https://cdn.buymeacoffee.com/buttons/default-orange.png)](https://www.buymeacoffee.com/xdsxpsivx)

Si MCART te ha sido de utilidad, o te interesa donar para fomentar el
desarrollo del proyecto, siéntete libre de hacer una donación por medio de
[PayPal](https://paypal.me/thexds), [BuyMeACoffee](https://www.buymeacoffee.com/xdsxpsivx)
o ponte en contacto directamente conmigo.

Lamentablemente, no puedo ofrecer otros medios de donación por el momento
debido a que mi país (Honduras) no es soportado por ninguna plataforma.