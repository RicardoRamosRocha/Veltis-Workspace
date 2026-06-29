# Estrutura

```text
src/
  Veltis.Workspace.Domain/
    Common/
    Constants/
    Identity/
  Veltis.Workspace.Application/
    Common/
      Interfaces/
      Pagination/
      Results/
      Validation/
  Veltis.Workspace.Infrastructure/
    Identity/
    Persistence/
    Services/
  Veltis.Workspace.Web/
    Controllers/
    Middleware/
    Models/
    Services/
    Views/
    wwwroot/
tests/
  Veltis.Workspace.Tests/
docs/
database/
assets/
.github/
  ISSUE_TEMPLATE/
  workflows/
```

## Responsabilidades

`Domain` concentra entidades e conceitos centrais. `Application` define contratos e orquestrara casos de uso. `Infrastructure` implementa persistencia, Identity e detalhes externos. `Web` compoe a aplicacao e entrega a interface MVC.

## Convencoes

Novos modulos devem nascer primeiro no dominio e aplicacao quando representam regra de negocio. Implementacoes de banco, servicos externos e detalhes operacionais devem ficar na infraestrutura. Views e controllers permanecem na camada Web.
