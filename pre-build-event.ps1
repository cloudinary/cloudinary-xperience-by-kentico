param (
    [Parameter(Mandatory = $true)]
    [string]$ClientDir
)

Write-Host "==> Running npm build"
Write-Host "Path                     : $ClientDir"

if (-not (Test-Path $ClientDir)) {
    throw "Client directory not found: $ClientDir"
}

Push-Location $ClientDir

try {
    npm install
    npm run build
}
finally {
    Pop-Location
}
