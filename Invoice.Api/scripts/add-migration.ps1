$migration = $args[0]

$command = "dotnet ef migrations add $migration --startup-project Invoice.Api/Invoice.Api.csproj --project Invoice.Api.Data.Migrations/Invoice.Api.Data.Migrations.csproj"

Invoke-Command $command