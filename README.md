# Veltis Workspace

Veltis Workspace e uma plataforma SaaS de produtividade profissional baseada em inteligencia artificial. Esta Sprint 0 entrega a fundacao tecnica independente do produto, sem reaproveitamento de codigo de outros projetos.

## Stack

- .NET 8 LTS
- ASP.NET Core MVC
- Entity Framework Core
- PostgreSQL
- ASP.NET Core Identity
- Tailwind CSS
- JavaScript
- Docker
- GitHub Actions
- Clean Architecture

## GitHub

Repositorio sugerido: `Veltis-Workspace`.

Branches:

- `main`: branch principal.
- `feature/nome-da-funcionalidade`: novas funcionalidades.
- `fix/nome-da-correcao`: correcoes.
- `docs/nome-da-documentacao`: documentacao.
- `chore/nome-da-tarefa`: tarefas tecnicas.

Commits:

- `feat: nova funcionalidade`
- `fix: correcao`
- `docs: documentacao`
- `refactor: refatoracao`
- `chore: tarefa tecnica`
- `test: testes`
- `style: ajustes visuais ou formatacao`

## Estrutura

```text
src/
  Veltis.Workspace.Web
  Veltis.Workspace.Application
  Veltis.Workspace.Domain
  Veltis.Workspace.Infrastructure
tests/
docs/
database/
assets/
.github/
```

## Execucao local

1. Suba o PostgreSQL:

```powershell
docker compose up -d postgres
```

2. Restaure e compile:

```powershell
dotnet restore Veltis.Workspace.sln --configfile NuGet.Config
dotnet build Veltis.Workspace.sln
```

3. Aplique migrations quando forem geradas:

```powershell
dotnet ef database update --project src/Veltis.Workspace.Infrastructure --startup-project src/Veltis.Workspace.Web
```

4. Execute a aplicacao:

```powershell
dotnet run --project src/Veltis.Workspace.Web
```

## Docker

```powershell
docker compose up --build
```

A aplicacao fica disponivel em `http://localhost:8080`.

## Autenticacao

A fundacao usa ASP.NET Core Identity com `ApplicationUser` e `ApplicationRole`, pronta para evoluir com permissoes, papeis, politicas e auditoria.

O seed inicial de roles e administrador fica desligado por padrao. Para habilitar, configure `Seed__RunOnStartup=true` e informe `Seed__AdminUser__Email` e `Seed__AdminUser__Password` por variavel de ambiente ou secret.

## Infraestrutura Compartilhada

A Sprint 0.5 adiciona infraestrutura compartilhada para Result Pattern, paginacao, auditoria, soft delete, repositorios genericos, Unit of Work, seed inicial, tratamento global de erros e contratos futuros para IA.

## Documentacao

Os documentos da Sprint 0 estao em `docs/` e cobrem visao geral, arquitetura, stack, roadmap, backlog, padroes e estrutura.
