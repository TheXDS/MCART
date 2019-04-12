param (
    [switch]$Debug,
    [switch]$Symbols,
    [switch]$Clean,
    [string]$Repository="https://nuget.org",
    [System.Version]$Version,
    [string]$PrVersion=[String]::Empty,
	[switch]$List,
    [switch]$Help
)

if ($Help)
{
    "Herramineta de publicación en NuGet de MCART"
    ""
    "Esta herramienta publica los paquetes de NuGet generados luego de compilar MCART. De forma predeterminada, los paquetes se publicarán en https://nuget.org"
    ""
    "Opciones:"
    " -Debug                     Publica la versión de depuración en lugar de Release."
    " -Symbols                   Publica los paquetes con símbolos de depuración en lugar de los normales."
    " -Clean                     Limpia el directorio de trabajo para evitar enviar paquetes antíguos."
    " -Repository <repositorio>  Publica los paquetes en el repositorio especificado."
    " -Version <versión>         Publica una versión específica de los paquetes."
    " -PrVersion <versión>       Agrega un subfijo de versión preeliminar a la búsqueda."
    " -List                      Listar la operación a realizar en lugar de ejecutar NuGet."
    " -Help                      Muestra esta ayuda."
    ""
    "Ejemplos:"
    "$($MyInvocation.MyCommand.Name)                 Publicar los paquetes Release en nuget."
    "$($MyInvocation.MyCommand.Name) -Debug -Symbols Publicar los paquetes Debug con símbolos en nuget."
    Return
}

$Subfix = $Version.ToString()
if ($PrVersion)
{
    $Subfix += "-" + $PrVersion
}

if ($Debug)
{
    $Folder = "Debug"
    if ($Symbols)
    {
        $Subfix += ".symbols"
    }
}
else
{
    $Folder = "Release"
    if ($Symbols)
    {
        Write-Warning "Se ignorará la bandera -Symbols para la publicación de paquetes Release."
    }
}

if ($Clean -and [System.IO.Directory]::Exists("..\build\$Folder"))
{
    if ($Version)
    {
        Write-Warning "No debería utilizar el switch -Version junto con -Clean."
    }
    Remove-Item ..\build\$Folder -Force -Recurse
}

if (![System.IO.Directory]::Exists("..\build\$Folder"))
{
    dotnet build -p:Configuration=$Folder
}

foreach ($j in Get-ChildItem ..\build\$Folder\*$Subfix.nupkg)
{
    if (!$Symbols -and $j.FullName.EndsWith(".symbols.nupkg"))
    {
        continue
    }

	if ($List)
	{
		Write-Output $j.FullName
	}
	else
	{
		nuget push $j.FullName -Source $Repository
	}
}