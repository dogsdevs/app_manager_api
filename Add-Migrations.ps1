param([parameter(mandatory)] [string] $MigrationName)

Write-Host "Adding PostgreSql migration" -ForegroundColor Green

$env:DB_PROVIDER = "postgresql"
dotnet ef migrations add $migrationName --project ./AppManager.Migrations.Postgresql -s ./AppManager.Api

Write-Host "Adding Sql Server migration" -ForegroundColor Green

$env:DB_PROVIDER = "sqlserver"
dotnet ef migrations add $migrationName --project ./AppManager.Migrations.SqlServer -s ./AppManager.Api