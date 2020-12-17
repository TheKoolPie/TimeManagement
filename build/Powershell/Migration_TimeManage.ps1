param(
    [Parameter(Mandatory)]
    [string]
    $MigrationName
)

$projectDir = "..\..\src\Backend\TimeManagement.Api"
$projectPath = "$projectDir\TimeManagement.Api.csproj"
$migrationDir = "Context\Migrations\TimeManagement"
$contextName = "TimeManagementDbContext"

dotnet ef migrations add $MigrationName --context $contextName -o $migrationDir --startup-project $projectPath