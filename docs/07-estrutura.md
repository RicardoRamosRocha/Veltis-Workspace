# Estrutura

```text
src/
  Veltis.Workspace.Domain/
    Agents/
    Billing/
    Common/
    Constants/
    Entities/
    FeatureFlags/
    Identity/
    Notifications/
    Observability/
    Permissions/
    Settings/
    Tenancy/
  Veltis.Workspace.Application/
    Agents/
    Ai/
    Common/
      Events/
      Interfaces/
      Pagination/
      Results/
      Validation/
    Dashboard/
    FeatureFlags/
    Notifications/
    Permissions/
    Professions/
    Workspaces/
  Veltis.Workspace.Infrastructure/
    Events/
    Identity/
    Persistence/
    Services/
    Tenancy/
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

`Domain` concentra entidades e conceitos centrais. `Application` define contratos e orquestrará casos de uso. `Infrastructure` implementa persistência, Identity e detalhes externos. `Web` compõe a aplicação e entrega a interface MVC.

## Convenções

Novos módulos devem nascer primeiro no domínio e aplicação quando representam regra de negócio. Implementações de banco, serviços externos e detalhes operacionais devem ficar na infraestrutura. Views e controllers permanecem na camada Web.

