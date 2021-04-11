dotnet publish -c Release |
    where { $_.Contains("publish") } |
    select -Last 1 {
        $published_path = $_.Substring($_.IndexOf("D:"))
        $desktop_path = [System.Environment]::GetFolderPath("Desktop")

        Get-ChildItem $published_path |
            where { $_.Extension -ne ".pdb" } | 
            Compress-Archive -DestinationPath $desktop_path\WindowStretch.zip
        
        Copy-Item $published_path $desktop_path -Recurse
        Rename-Item $desktop_path\publish WindowStretch
    }

# 70MB近くになる
# dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true /p:TrimMode=Link
# /p:IncludeNativeLibrariesForSelfExtract=true 

# なぜか最新のランタイムを要求される
# dotnet publish -r win-x64 -c Release --self-contained=false /p:PublishSingleFile=true
