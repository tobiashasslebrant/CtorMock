#!/bin/bash

packagename=""
version=""
apikey=""
build="false"
push="false"
gitversion="false"

print_usage() {
  printf "
  Usage: publish.sh -n packageName -v version [-a apikey] [-b] [-p] [-g]
    -n packageName  name of the project to be build into a package
    -v version      semantic version of package
    -a apiKey       apikey used when pushing to remote nuget repository
    -b              builds the package artifact
    -p              push the package artifact to remote nuget repository
    -g              creates a git label with current version
    
    example for local build: 
        publish.sh -n CtorMock -v 1.0.0-test -b
    
    example for building and pushing new version: 
        publish.sh -n CtorMock -v 1.0.11 -a <secret_api_key> -bpg
  "
}

while getopts 'n:v:a:bpg' flag; do
  case "${flag}" in
    n) packagename="${OPTARG}" ;;
    v) version="${OPTARG}" ;;
    a) apikey="${OPTARG}" ;;
    b) build='true' ;;
    p) push='true' ;;
    *) print_usage
       exit 1 ;;
  esac
done


if [ "$packagename" == "" ]; then
    echo 'missing packagename, example CtorMock' 
    exit 1
fi

if [ "$version" == "" ]; then
    echo 'missing version, example 1.0.8'
     exit 2
fi

if [[ "$build" == "false" && "$push" == "false" ]]; then
    echo "Doing nothing"
fi

if [ "$build" == "true" ]; then
    dotnet pack ../$packagename/$packagename.csproj -c Release -o ../Builds /p:PackageVersion=$version
fi

if [ "$push" == "true" ]; then
    if [ "$apikey" == "" ]; then
        echo 'missing apikey'
        exit 3
    fi
    
    dotnet nuget push ../Builds/$packagename.$version.nupkg -s https://api.nuget.org/v3/index.json -k $apikey
    git tag "$packagename-$version"
    sed -i "/$apikey/d" .zsh_history
fi
