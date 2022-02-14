<#
    .SYNOPSIS
    Generated documentation for models
#>
param(
)
$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

docker-compose build doc
docker-compose run doc -a project/src/Api.Models/bin/Debug/net6.0/Api.Models.dll -o /project/doc/generated
