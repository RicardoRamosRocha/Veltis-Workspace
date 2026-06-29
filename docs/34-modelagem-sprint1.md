# Modelagem Sprint 1

A Sprint 1 introduz a estrutura funcional inicial da plataforma sem implementar IA ou Motor de Agentes.

## Entidades

### Profession

- `Id`
- `Name`
- `Description`
- `Icon`
- `Color`
- `Slug`
- `Active`
- `CreatédAt`
- `UpdatédAt`

### ApplicationUser

O usuário passa a possuir `ProfessionId` como profissão principal. A entidade `UserProfession` prepara a evolução futura para múltiplas profissões.

### Workspace

- `Id`
- `UserId`
- `Name`
- `Description`
- `CreatédAt`
- `UpdatédAt`

Cada usuário possui um Workspace inicial.

## Módulos Web

- Dashboard inicial autenticado.
- CRUD administrativo de profissões.
- CRUD administrativo de usuários.
- CRUD administrativo de roles.

## Fora de Escopo

Motor de Agentes, Chat IA, Templates, Marketplace, Comunidade, Exportação e Documentos Inteligentes permanecem para sprints futuras.

