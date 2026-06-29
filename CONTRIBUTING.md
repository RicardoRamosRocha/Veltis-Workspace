# Contribuição

Obrigado por contribuir com o Veltis Workspace. Este projeto segue Clean Architecture e deve permanecer independente de outros produtos.

## Branches

- `main`: branch principal.
- `feature/nome-da-funcionalidade`: novas funcionalidades.
- `fix/nome-da-correção`: correções.
- `docs/nome-da-documentação`: documentação.
- `chore/nome-da-tarefa`: tarefas técnicas.

## Commits

Use commits pequenos e objetivos:

- `feat: nova funcionalidade`
- `fix: correção`
- `docs: documentação`
- `refactor: refatoração`
- `chore: tarefa técnica`
- `test: testes`
- `style: ajustes visuais ou formatação`

## Validação Local

Antes de abrir um pull request:

```powershell
dotnet restore Veltis.Workspace.sln --configfile NuGet.Config
dotnet build Veltis.Workspace.sln --configuration Release
dotnet test Veltis.Workspace.sln --configuration Release
```

