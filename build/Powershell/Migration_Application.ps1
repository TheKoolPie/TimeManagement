param(
    [Parameter(Mandatory)]
    [string]
    $MigrationName
)

$projectDir = "..\..\src\Backend\TimeManagement.Api"
$projectPath = "$projectDir\TimeManagement.Api.csproj"
$migrationDir = "Context\Migrations\Authentication"
$contextName = "ApplicationDbContext"

dotnet ef migrations add $MigrationName --context $contextName -o $migrationDir --startup-project $projectPath