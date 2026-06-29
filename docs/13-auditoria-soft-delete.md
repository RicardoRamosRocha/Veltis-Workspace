# Auditoria e Soft Delete

`BaseEntity` concentra campos de auditoria:

- `Id`
- `CreatedAt`
- `UpdatedAt`
- `DeletedAt`
- `IsDeleted`

## Soft Delete

Remocoes em entidades de dominio devem marcar `IsDeleted` e `DeletedAt`, preservando o registro para auditoria futura.

## Auditoria

O `ApplicationDbContext` aplica datas de criacao, atualizacao e exclusao no `SaveChanges`. A estrutura esta preparada para evoluir com usuario criador, usuario alterador, correlation id e trilhas de auditoria.
