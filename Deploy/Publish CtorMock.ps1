Param(
 [Parameter(Mandatory=$True)][string]$version,
 [Parameter(Mandatory=$True)][string]$apikey
)
$package = "CtorMock"

& "$PSScriptRoot\Publish.ps1" `
    -version $version `
    -apikey $apikey `
    -package $package
