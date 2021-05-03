cd $PSScriptRoot

$publish_base_path = [System.Environment]::GetFolderPath("Desktop")
$publish_path = "$publish_base_path\WindowStretch"
$archive_file = "$publish_base_path\WindowStretch.zip"

Remove-Item $publish_path -Recurse -Force
Remove-Item $archive_file

dotnet publish -o $publish_path -c Release -p:ApplicationManifest=app.manifest -p:Platform="x64"

Get-ChildItem $publish_path |
    where { $_.Extension -ne ".pdb" } | 
    Compress-Archive -DestinationPath $archive_file

# 70MB近くになる
# dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true /p:TrimMode=Link
# /p:IncludeNativeLibrariesForSelfExtract=true 

# なぜか最新のランタイムを要求される
# dotnet publish -r win-x64 -c Release --self-contained=false /p:PublishSingleFile=true
