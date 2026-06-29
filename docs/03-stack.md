# Stack

## Backend

- .NET 8 LTS
- ASP.NET Core MVC
- ASP.NET Core Identity
- Entity Framework Core
- Npgsql Entity Framework Core Provider

## Banco de Dados

- PostgreSQL 16 para desenvolvimento local simples com usuário `postgres`
- PostgreSQL 16 em Docker com usuário `veltis` e senha de exemplo `veltis_dev_password`
- Volume persistente para dados
- Schema dedicado `workspace`

## Connection String de Desenvolvimento

No ambiente Development local, a aplicação lê a conexão em `src/Veltis.Workspace.Web/appsettings.Development.json` por meio de `Configuration.GetConnectionString("DefaultConnection")`.

Opção A — desenvolvimento local simples:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=veltis_workspace;Username=postgres;Password=SUA_SENHA_DO_POSTGRES"
  }
}
```

Opção B — Docker:

```text
Host=postgres;Port=5432;Database=veltis_workspace;Username=veltis;Password=veltis_dev_password
```

Senhas reais não devem ser versionadas no repositório.

## Frontend

- Razor Views
- Tailwind CSS
- JavaScript vanilla para interações pequenas
- Layout responsivo com dark mode preparado

## Operação

- Dockerfile multi-stage
- Docker Compose com web e PostgreSQL
- GitHub Actions para restore, build, test e publish

