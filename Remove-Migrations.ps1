
param([parameter(mandatory)] [string] $MigrationName)

Write-Host "Removing PostgreSql migration" -ForegroundColor Green

$env:DB_PROVIDER = "postgresql"
dotnet ef migrations remove  $migrationName --project ./AppManager.Migrations.Postgresql -s ./AppManager.Api

Write-Host "Removing Sql Server migration" -ForegroundColor Green

$env:DB_PROVIDER = "sqlserver"
dotnet ef migrations remove $migrationName --project ./AppManager.Migrations.SqlServer -s ./AppManager.Api