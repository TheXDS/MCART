#! /bin/pwsh
docfx ..\docs\docfx.json

SOURCE_DIR = $PWD
TEMP_REPO_DIR = $env:TMP\mcart-gh-pages
if (!(Test-Path $TEMP_REPO_DIR)) {
    New-Item -Path $TEMP_REPO_DIR -ItemType "directory"
    git clone https://github.com/TheXDS/MCART.git --branch gh-pages $TEMP_REPO_DIR
}
cd $TEMP_REPO_DIR
git rm -r *
Copy-Item -Path $SOURCE_DIR/docs/_site/* -Destination . -Recurse -Force
git add . -A
git commit -m "Actualización de documentación"
git push origin gh-pages
cd $SOURCE_DIR
