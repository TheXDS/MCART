#! /bin/pwsh
try {
    docfx ..\docs\docfx.json
}
catch {
    Write-Error "No se pudo generar la documentación de ayuda." -CategoryReason "Parece que DocFx no está instalado." -Category NotInstalled
    return
}
$SOURCE_DIR = $PWD
$TEMP_REPO_DIR = "$env:TMP\mcart-gh-pages"
if (!(Test-Path $TEMP_REPO_DIR)) {
    New-Item -Path $TEMP_REPO_DIR -ItemType "directory"
    git clone https://github.com/TheXDS/MCART.git --branch gh-pages $TEMP_REPO_DIR
}
Set-Location $TEMP_REPO_DIR
git rm -r *
Copy-Item -Path $SOURCE_DIR/docs/_site/* -Destination . -Recurse -Force
git add . -A
git commit -m "Actualización de documentación"
git push origin gh-pages
Set-Location $SOURCE_DIR
