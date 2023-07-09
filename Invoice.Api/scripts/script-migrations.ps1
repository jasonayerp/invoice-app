$command = "dotnet ef migrations script --idempotent -o ../Invoice.Api.Data.SqlServer/Migrations/Scripts/Migrations.sql"

Invoke-Command $command