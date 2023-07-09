$migration = $args[0]

$command = "dotnet ef migrations add $migration --startup-project ../Invoice.Api.Data.SqlServer/Invoice.Api.Data.SqlServer.csproj -o ../Invoice.Api.Data.SqlServer/Migrations"

Invoke-Command $command