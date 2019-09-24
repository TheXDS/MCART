#! /bin/pwsh
$path = vswhere -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe | select-object -first 1
if (!$path) {
  return
}

docfx ..\docs\docfx.json

SOURCE_DIR=$PWD
TEMP_REPO_DIR=$env:TMP\mcart-gh-pages
if ([System.IO.Directory]::Exists($TEMP_REPO_DIR)) {
    Remove-Item -Path $TEMP_REPO_DIR -Recurse -Force
}
New-Item -Path $TEMP_REPO_DIR -ItemType "directory"
git clone https://github.com/TheXDS/MCART.git --branch gh-pages $TEMP_REPO_DIR
cd $TEMP_REPO_DIR
git rm -r *
Copy-Item -Path $SOURCE_DIR/docs/_site/* -Destination . -Recurse
git add . -A
git commit -m "Actualización de documentación"
git push origin gh-pages
cd $SOURCE_DIR
Remove-Item -Path $TEMP_REPO_DIR -Recurse -Force