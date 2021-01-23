
if [ "$1" == "" ]; then
    echo 'missing packagename, example CtorMock' 
    exit 1
fi

if [ "$2" == "" ]; then
    echo 'missing version, example 1.0.8'
     exit 2
fi

if [ "$3" == "" ]; then
    echo 'missing apikey'
    exit 3
fi

package="$1"
version="$2"
apikey="$3"

dotnet pack ../$package/$package.csproj -c Release -o ../Builds /p:PackageVersion=$version
dotnet nuget push ../Builds/$package.$version.nupkg -s https://api.nuget.org/v3/index.json -k $apikey
git tag "$version"
