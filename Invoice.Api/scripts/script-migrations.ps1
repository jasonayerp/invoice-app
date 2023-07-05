$command = "dotnet ef migrations script --idempotent -o Invoice.Api.Data.Migrations/Scripts/idempotent.sql"

Invoke-Command $command