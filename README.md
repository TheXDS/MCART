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

## Introduction
MCART is a set of functions, extensions, and modules that I have found
useful throughout my years of experience with .Net languages,
particularly with Visual Basic. It aims to add features that are not
easily available in the .Net Framework, and also adds controls,
windows, resources, and other utility objects.

Currently, it is in a very early Alpha phase, so it may
have bugs or serious performance issues. I have put a lot of effort into
maintaining functional code and, hopefully, free of obvious errors. However,
I cannot guarantee that MCART can be used in a software package in its
current state.

## Project Composition
MCART consists of several projects, and different shared code
projects across platforms. Here lies the root of most
features that can work in different CIL environments. The
functionality has been tested with .Net 6.0; which should be sufficient
to create applications based on Win32, WPF, Gtk#, console, and even
websites developed in ASP .Net.

## Releases
MCART is available on NuGet.

Release | Link
--- | ---
Latest stable version: | [![Stable Version](https://img.shields.io/nuget/v/TheXDS.MCART)](https://www.nuget.org/packages/TheXDS.MCART/)
Latest development version: | [![Development version]([https://buildstats.info/nuget/TheXDS.MCART?includePreReleases=true](https://img.shields.io/nuget/vpre/TheXDS.MCART))](https://www.nuget.org/packages/TheXDS.MCART/)

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
**Package reference**  
```sh
<PackageReference Include="TheXDS.MCART" Version="0.18.0" />
```

## Compilation
To compile MCART, the [SDK for .NET 8.0](https://dotnet.microsoft.com/) or a later version with a targeting pack for .NET 8.0 must be installed on the system.

### Compiling MCART
```sh
dotnet build ./src/MCART.sln
```
The binaries will be found in the `Build` folder at the root of the repository.

### Executing tests
```sh
dotnet test ./src/MCART.sln
```
#### Coverage Report
It is possible to obtain a local code coverage report. To do this, it is necessary to install [`ReportGenerator`](https://github.com/danielpalme/ReportGenerator), which will read the results of the test execution and generate a web page with the coverage results.

To install `ReportGenerator`, run:
```sh
dotnet tool install -g dotnet-reportgenerator-globaltool
```
After installing `ReportGenerator`, it will be possible to run the following command:
```sh
dotnet test ./src/MCART.sln --collect:"XPlat Code Coverage" --results-directory:./Build/Tests ; reportgenerator.exe -reports:./Build/Tests/*/coverage.cobertura.xml -targetdir:./Build/Coverage/
```
The coverage results will be stored in `./Build/Coverage`

## Contribute
[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/W7W415UCHY)

If MCART has been useful to you, or if you are interested in donating to support the development of the project, feel free to make a donation via [PayPal](https://paypal.me/thexds), [Ko-fi](https://ko-fi.com/W7W415UCHY), or contact me directly.

Unfortunately, I cannot offer other means of donation at the moment because my country (Honduras) is not supported by any platform.
