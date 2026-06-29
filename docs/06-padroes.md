# Padrões

## Código

- Usar C# com nullable reference types habilitado.
- Preferir namespaces file-scoped.
- Manter Domain livre de detalhes de infraestrutura.
- Registrar dependências por camada usando classes `DependencyInjection`.
- Evitar acoplamento prematuro a módulos ainda não implementados.

## Branches

- `main`: branch principal.
- `feature/nome-da-funcionalidade`: novas funcionalidades.
- `fix/nome-da-correção`: correções.
- `docs/nome-da-documentação`: documentação.
- `chore/nome-da-tarefa`: tarefas técnicas.

## Commits

- `feat: nova funcionalidade`
- `fix: correção`
- `docs: documentação`
- `refactor: refatoração`
- `chore: tarefa técnica`
- `test: testes`
- `style: ajustes visuais ou formatação`

## Banco

- Usar PostgreSQL como banco relacional principal.
- Usar migrations do EF Core para evolução de schema.
- Manter schema dedicado `workspace`.
- Usar identificadores `Guid` para usuários e roles.

## Web

- Controllers MVC simples e focados.
- Views Razor com Tailwind CSS.
- Layout responsivo e preparado para dark mode.
- Formulários com antiforgery token.

## Segurança

- Identity como mecanismo base de autenticação.
- Cookies de autenticação configurados explicitamente.
- Validação server-side em todos os formulários.
- Configurações sensíveis via variáveis de ambiente em produção.

