#!/usr/local/bin/pwsh
$env:ASPNETCORE_ENVIRONMENT="Development"
$project=Join-Path $PSScriptRoot "WHVM.Web.csproj"

Start-Process dotnet -ArgumentList "run","--project $project" -NoNewWindow