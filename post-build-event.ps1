param (
    [string]$OutputDir   = ".\artifacts",
    [string]$NugetPackageName,
    [string]$NugetPackageVersion
)
$XbyKenticoCmsCsprojPath = $env:XbyKenticoCmsCsprojPath
$XbyKenticoCmsProcessName = $env:XbyKenticoCmsProcessName
$XbyKenticoCmsCsprojProjectFolderPath = [System.IO.Path]::GetDirectoryName("$XbyKenticoCmsCsprojPath")

Write-Host "==> Deploying NuGet package to CMS"
Write-Host "Artifacts Dir            : $OutputDir"
Write-Host "Package ID               : $NugetPackageName"
Write-Host "Package version          : $NugetPackageVersion"
Write-Host "Kettico csproj path      : $XbyKenticoCmsCsprojPath"
Write-Host "Kettico csproj folder    : $XbyKenticoCmsCsprojProjectFolderPath"
Write-Host "Kettico process name     : $XbyKenticoCmsProcessName"

# close all running application instances
$process = Get-Process -Name $XbyKenticoCmsProcessName -ErrorAction SilentlyContinue
if ($process) {
    Write-Host "Existing Process info: $($process.Name)"
    Write-Host "-------------------"
    $process | Format-List
    Write-Host "-------------------"
    Stop-Process -Name $process.Name -Force
}

# install package and build kentico CMS project
Set-Location $XbyKenticoCmsCsprojProjectFolderPath
dotnet add package $NugetPackageName -v $NugetPackageVersion -s "$OutputDir"
dotnet build $XbyKenticoCmsCsprojPath

# run application
Start-Process -FilePath "powershell.exe" -ArgumentList "-Command ""dotnet run"""
