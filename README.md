# App Manager

### Program.cs (Entry point)

```csharp
using AppManager;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppManager();


var app = builder.Build();

app.ApplyDbMigrations();

```

### Migrations

Para poder ejectuar las migraciones es necesario tener instalado Powershell

#### Option 1: Usando Brew

``
    brew install --cask powershell
``

#### Option 2: Descargando paquete

- Ve a la página oficial:
  https://learn.microsoft.com/powershell/scripting/install/installing-powershell-on-macos
- Descarga el .pkg de la última versión
- Instálalo como cualquier app de macOS
- Abre Terminal y ejecuta:
- ``pwsh``

Despues de instalado tenemos que dar permisos de ejecucion al script y luego ejecutamos

Bash:

```
chmod +x Add-Migrations.ps1
```

```
pwsh ./Add-Migrations.ps1 -MigrationName "InitialMigration"
```

Windows:

```
.\Add-Migrations.ps1 -MigrationName "InitialMigration"
```

Asegurate de tener estas variables configuradas en tu appsettings.json

```json
{
  "DB_PROVIDER": "postgresql",
  "ConnectionStrings": {
    "DataContextPostgreSql": "",
    "DataContextSqlServer": ""
  }
}
```