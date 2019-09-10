#!/usr/local/bin/pwsh
$env:ASPNETCORE_ENVIRONMENT="Development"
$project=Join-Path $PSScriptRoot "WHVM.Web.csproj"

& dotnet run --project $project