Param(
 [Parameter(Mandatory=$True)][string]$version,
 [Parameter(Mandatory=$True)][string]$apikey
)

$directory = resolve-path .
if(-not ($directory -match "CtorMock\\Deploy$"))
{
    Write-Host "Working directory must be \CtorMock\Deploy\"
    break
}

$packages = "CtorMock", "CtorMock.Moq", "CtorMock.FakeItEasy"

$packages | % {dotnet pack $directory\..\$_\$_.csproj -o $directory\Builds /p:PackageVersion=$version}

nuget setapikey $apikey -source https://api.nuget.org/v3/index.json

$packages | % {dotnet nuget push $directory\Builds\$_.$version.nupkg -s https://api.nuget.org/v3/index.json}

