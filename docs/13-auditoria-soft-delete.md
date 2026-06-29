# Auditoria e Soft Delete

`BaseEntity` concentra campos de auditoria:

- `Id`
- `CreatédAt`
- `UpdatédAt`
- `DeletedAt`
- `IsDeleted`

## Soft Delete

Remoções em entidades de domínio devem marcar `IsDeleted` e `DeletedAt`, preservando o registro para auditoria futura.

## Auditoria

O `ApplicationDbContext` aplica datas de criação, atualização e exclusão no `SaveChanges`. A estrutura está preparada para evoluir com usuário criador, usuário alterador, correlation id e trilhas de auditoria.

