# Contributing

Obrigado por contribuir com o Veltis Workspace. Este projeto segue Clean Architecture e deve permanecer independente de outros produtos.

## Branches

- `main`: branch principal.
- `feature/nome-da-funcionalidade`: novas funcionalidades.
- `fix/nome-da-correcao`: correcoes.
- `docs/nome-da-documentacao`: documentacao.
- `chore/nome-da-tarefa`: tarefas tecnicas.

## Commits

Use commits pequenos e objetivos:

- `feat: nova funcionalidade`
- `fix: correcao`
- `docs: documentacao`
- `refactor: refatoracao`
- `chore: tarefa tecnica`
- `test: testes`
- `style: ajustes visuais ou formatacao`

## Validacao Local

Antes de abrir um pull request:

```powershell
dotnet restore Veltis.Workspace.sln --configfile NuGet.Config
dotnet build Veltis.Workspace.sln --configuration Release
dotnet test Veltis.Workspace.sln --configuration Release
```
