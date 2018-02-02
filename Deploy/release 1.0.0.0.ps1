$version = "1.0.0"
$directory = resolve-path .
if(-not ($directory -match "CtorMock\\Deploy$"))
{
    Write-Host "Must stand in directory \CtorMock\Deploy\"
    break
}

dotnet pack $directory\..\CtorMock\CtorMock.csproj -o $directory\Builds /p:PackageVersion=$version
