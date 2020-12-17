$projectDir = "..\..\src\Backend\TimeManagement.Api"
$projectPath = "$projectDir\TimeManagement.Api.csproj"
$contextName = "TimeManagementDbContext"

dotnet ef migrations remove --context $contextName --startup-project $projectPath