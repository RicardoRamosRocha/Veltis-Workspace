# GitHub

O repositorio sugerido para publicacao e `Veltis-Workspace`.

## Branches

- `main`: branch principal e fonte de releases.
- `feature/nome-da-funcionalidade`: desenvolvimento de novas funcionalidades.
- `fix/nome-da-correcao`: correcoes de defeitos.
- `docs/nome-da-documentacao`: alteracoes documentais.
- `chore/nome-da-tarefa`: tarefas tecnicas e manutencao.

## Pull Requests

Pull requests devem conter resumo, validacao executada e escopo claro. Mudancas sem relacao com o objetivo do PR devem ser evitadas.

## Workflows

O workflow `.github/workflows/build.yml` valida .NET 8 com restore, build e testes.
