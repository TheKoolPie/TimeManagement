$projectDir = "..\..\src\Backend\TimeManagement.Api"
$projectPath = "$projectDir\TimeManagement.Api.csproj"
$contextName = "ApplicationDbContext"

dotnet ef migrations remove --context $contextName --startup-project $projectPath