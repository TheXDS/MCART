@echo off
setlocal enabledelayedexpansion

for /f "usebackq tokens=*" %%i in (`"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -prerelease -products * -requires Microsoft.Component.MSBuild -property installationPath`) do (
  set InstallDir=%%i
)

set tool="!InstallDir!\MSBuild\Current\Bin\MSBuild.exe"
if not exist !tool! (
  set tool="!InstallDir!\MSBuild\15.0\Bin\MSBuild.exe"
  if not exist !tool! (
    echo No fue posible detectar la versión de MSBuild a utilizar.
    exit /b 2
  )
)

nuget restore
!tool! %*