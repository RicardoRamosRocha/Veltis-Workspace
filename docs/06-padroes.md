# Padroes

## Codigo

- Usar C# com nullable reference types habilitado.
- Preferir namespaces file-scoped.
- Manter Domain livre de detalhes de infraestrutura.
- Registrar dependencias por camada usando classes `DependencyInjection`.
- Evitar acoplamento prematuro a modulos ainda nao implementados.

## Branches

- `main`: branch principal.
- `feature/nome-da-funcionalidade`: novas funcionalidades.
- `fix/nome-da-correcao`: correcoes.
- `docs/nome-da-documentacao`: documentacao.
- `chore/nome-da-tarefa`: tarefas tecnicas.

## Commits

- `feat: nova funcionalidade`
- `fix: correcao`
- `docs: documentacao`
- `refactor: refatoracao`
- `chore: tarefa tecnica`
- `test: testes`
- `style: ajustes visuais ou formatacao`

## Banco

- Usar PostgreSQL como banco relacional principal.
- Usar migrations do EF Core para evolucao de schema.
- Manter schema dedicado `workspace`.
- Usar identificadores `Guid` para usuarios e roles.

## Web

- Controllers MVC simples e focados.
- Views Razor com Tailwind CSS.
- Layout responsivo e preparado para dark mode.
- Formularios com antiforgery token.

## Seguranca

- Identity como mecanismo base de autenticacao.
- Cookies de autenticacao configurados explicitamente.
- Validacao server-side em todos os formularios.
- Configuracoes sensiveis via variaveis de ambiente em producao.
