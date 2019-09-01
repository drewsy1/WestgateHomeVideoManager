@ECHO OFF

SET ASPNETCORE_ENVIRONMENT="Development"
SET PROJECT="%~dp0WHVM.Web.csproj"

IF "%1"=="Production" (
    SET ASPNETCORE_ENVIRONMENT="Production"
)

dotnet run --project %PROJECT%