$debug = $false
$exclude = @('.git/*','.vs/*')
$include = @('*.config', '*.cs', '*.json', '*.md', '*.nfproj', '*.nuspec', '*.sln', '*.yml')
$templateToken = "template_nanoframework"

$solution = Read-Host -Prompt "Enter solution: "
if ($solution.Length -le 0) {
    Write-Output("Invalid solution name.")
    Exit
}

$files = Get-ChildItem -Exclude $exclude -Include $include -File -Recurse

foreach ($file in $files) {
    $content = Get-Content $file -Raw

    if ($content.Contains($templateToken)) {
        Write-Output("Updating $($file.FullName) ...")
        $content = $content -replace $templateToken, $solution
        if ($debug -ne $true) {
            Set-Content -Path $file.FullName -Value $content            
        }
    }

    if ($file.BaseName.StartsWith($templateToken)) {
        $newName = $file.Name -replace $templateToken, $solution
        Write-Output("Renaming $($file.FullName) to $($newName) ...")
        if ($debug -ne $true) {
            Rename-Item -Path $file.FullName -NewName $newName
        } 
    }
}

$directories = Get-ChildItem -Exclude '.*' -Directory -Recurse
foreach ($directory in $directories) {
    if ($directory.BaseName.StartsWith($templateToken)) {
        $newName = $directory.Name -replace $templateToken, $solution
        Write-Output("Moving $($directory.FullName) to $($newName) ...")
        if ($debug -ne $true) {
            Rename-Item -Path $directory.FullName -NewName $newName
        }
    }
}

if ($debug -ne $true) {
    Remove-Item .\rename-template.ps1
}