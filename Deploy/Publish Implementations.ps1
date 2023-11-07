Param(
 [Parameter(Mandatory=$True)][string]$version,
 [Parameter(Mandatory=$True)][string]$apikey
)

#nuget list "CtorMock" -Source https://api.nuget.org/v3/index.json | ? {$_ -match $version}

$packages = "CtorMock.Moq", "CtorMock.FakeItEasy", "CtorMock.NSubstitute"

$packages | % { 
    & "$PSScriptRoot\Publish.ps1" -version $version -apikey $apikey -package $_ 
}
