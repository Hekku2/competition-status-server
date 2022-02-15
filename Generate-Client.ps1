<#
    .SYNOPSIS
    Generated API library for front end and doc.
    .DESCRIPTION
    Basically this just restarts the api, waits for it to start and runs
    openapi -command. This generates new client. After this, api is stopped.
#>
param(
)
$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$apiLocation = 'http://api/swagger/v1/swagger.json'
$apiLocation2 = 'http://api/swagger/v1/swagger.yaml'
$apiContainerName = 'api'
$frontEndContainerName = 'frontend'

docker-compose stop $apiContainerName
docker-compose build $apiContainerName
docker-compose up -d $apiContainerName
Start-Sleep -Seconds 10

docker-compose run --user "$(id -u):$(id -g)" $frontEndContainerName yarn openapi -i $apiLocation -o src/services/openapi
docker-compose run --user "$(id -u):$(id -g)" $frontEndContainerName yarn widdershins $apiLocation2 --language_tabs 'http:HTTP' -o /doc/openapi-doc.md

docker-compose stop $apiContainerName
