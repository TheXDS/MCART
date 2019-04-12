param (
    [switch]$Debug,
    [switch]$Symbols,
    [switch]$Help,
    [switch]$Clean
)

if ($Help)
{
    Return
}

if ($Debug)
{
    $Folder = "Debug"
    if ($Symbols)
    {
        $Subfix = ".symbols"
    }
}
else
{
    $Folder = "Release"
    $Subfix = [String]::Empty
    if ($Symbols)
    {
        Write-Warning "Se ignorará la bandera -Symbols para la publicación de paquetes Release."
    }
}

if ($Clean)
{
    Remove-Item ..\build\$Folder -Force -Recurse
}

if (![System.IO.Directory]::Exists("..\build\$Folder"))
{
    dotnet build -p:Configuration=$Folder
}

foreach ($j in ls ..\build\$Folder\*$Subfix.nupkg)
{
    nuget push $j.FullName.ToString() -Source https://nuget.org
}