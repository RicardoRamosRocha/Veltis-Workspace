# Veltis Workspace

Veltis Workspace é uma plataforma SaaS de produtividade profissional baseada em inteligência artificial. Esta Sprint 0 entrega a fundação técnica independente do produto, sem reaproveitamento de código de outros projetos.

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

Repositório sugerido: `Veltis-Workspace`.

Branches:

- `main`: branch principal.
- `feature/nome-da-funcionalidade`: novas funcionalidades.
- `fix/nome-da-correção`: correções.
- `docs/nome-da-documentação`: documentação.
- `chore/nome-da-tarefa`: tarefas técnicas.

Commits:

- `feat: nova funcionalidade`
- `fix: correção`
- `docs: documentação`
- `refactor: refatoração`
- `chore: tarefa técnica`
- `test: testes`
- `style: ajustes visuais ou formatação`

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

## Execução local

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

4. Execute a aplicação:

```powershell
dotnet run --project src/Veltis.Workspace.Web
```

## Docker

```powershell
docker compose up --build
```

A aplicação fica disponível em `http://localhost:8080`.

## Autenticação

A fundação usa ASP.NET Core Identity com `ApplicationUser` e `ApplicationRole`, pronta para evoluir com permissões, papéis, políticas e auditoria.

O seed inicial de roles e administrador fica desligado por padrão. Para habilitar, configure `Seed__RunOnStartup=true` e informe `Seed__AdminUser__Email` e `Seed__AdminUser__Password` por variável de ambiente ou secret.

## Infraestrutura Compartilhada

A Sprint 0.5 adiciona infraestrutura compartilhada para Result Pattern, paginação, auditoria, soft delete, repositórios genéricos, Unit of Work, seed inicial, tratamento global de erros e contratos futuros para IA.

## Sprint 1

A fundação funcional inicial inclui cadastro de profissões, relação usuário-profissão, Workspace automático por usuário, dashboard inicial e CRUD administrativo de profissões, usuários e roles.

## Sprint 0.8

A fundação SaaS Enterprise prepara multi-tenant, feature flags, permissões granulares, billing base, observabilidade, auditoria, cache, storage, e-mail, filas, eventos e contratos de IA sem implementar IA real.

## Documentação

Os documentos da Sprint 0 estão em `docs/` e cobrem visão geral, arquitetura, stack, roadmap, backlog, padrões e estrutura.

