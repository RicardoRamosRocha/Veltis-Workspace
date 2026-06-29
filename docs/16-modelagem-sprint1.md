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
- `CreatedAt`
- `UpdatedAt`

### ApplicationUser

O usuario passa a possuir `ProfessionId` como profissao principal. A entidade `UserProfession` prepara a evolucao futura para multiplas profissoes.

### Workspace

- `Id`
- `UserId`
- `Name`
- `Description`
- `CreatedAt`
- `UpdatedAt`

Cada usuario possui um workspace inicial.

## Modulos Web

- Dashboard inicial autenticado.
- CRUD administrativo de profissoes.
- CRUD administrativo de usuarios.
- CRUD administrativo de roles.

## Fora de Escopo

Motor de Agentes, Chat IA, Templates, Marketplace, Comunidade, Exportacao e Documentos Inteligentes permanecem para sprints futuras.
