Param(
 [Parameter(Mandatory=$True)][string]$version,
 [Parameter(Mandatory=$True)][string]$apikey,
 [Parameter(Mandatory=$True)][string]$package
)

$directory = resolve-path .
if(-not ($directory -match "CtorMock\\Deploy$"))
{
    Write-Host "Working directory must be \CtorMock\Deploy\"
    break
}

dotnet pack $directory\..\$package\$package.csproj -c Release -o $directory\Builds /p:PackageVersion=$version

nuget setapikey $apikey -source https://api.nuget.org/v3/index.json

dotnet nuget push $directory\Builds\$package.$version.nupkg -s https://api.nuget.org/v3/index.json

