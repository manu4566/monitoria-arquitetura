# Plano de Implementação: Sistema de Avaliação e Feedback de Exercícios

**Branch**: `avaliacao-feedback` | **Date**: 2026-07-29 | **Espec**: [.specify/specs/avaliacao-feedback/spec.md](.specify/specs/avaliacao-feedback/spec.md)

## Resumo

Este plano detalha a implementação do recurso de "Sistema de Avaliação e Feedback de Exercícios" para a plataforma MonitoriaBase. Alunos poderão ter suas submissões de exercícios (ExerciseAttempt) avaliadas de forma estruturada por monitores genéricos. O feedback consistirá em um comentário em texto livre (mínimo de 10 e máximo de 5000 caracteres) e um status de avaliação (Enum: Approved ou NeedsRevision). Cada tentativa de exercício possui seu feedback próprio (associação de 1-para-1). A criação de um feedback atualizará diretamente o status do ExerciseAttempt correspondente. Forneceremos uma nova interface `IFeedbackService` e sua implementação `FeedbackService` seguindo as diretrizes de TDD, código limpo e arquitetura SOLID estabelecidas na constituição do projeto.

## Contexto Técnico

**Language/Version**: C# com .NET 8

**Primary Dependencies**: Nenhuma dependência externa no core da aplicação. Dependências de teste incluem xUnit.

**Storage**: Em memória usando coleções padrão do .NET (`List<Feedback>`, `Dictionary<Guid, Feedback>`).

**Testing**: xUnit para testes de unidade e testes baseados em cenários.

**Target Platform**: .NET 8 Class Library / Console Application.

**Project Type**: Class Library e testes xUnit integrados.

**Performance Goals**: Baixa latência por operar inteiramente em memória e de forma integrada na aplicação local do educando.

**Constraints**:
- Executará localmente sem infraestrutura externa (sem bancos de dados, sem Docker).
- Totalmente em inglês para o código (classes, métodos, variáveis) e em português para a documentação e artefatos de planejamento.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **SOLID Principles**:
  - SRP: `Feedback` representará apenas dados do feedback. `FeedbackService` tratará as validações de regras de negócio e manipulação dos dados armazenados em memória.
  - OCP: A alteração no status de `ExerciseAttempt` pode ser estendida sem alterar a lógica original se fizermos uso de delegação de contrato, mas neste escopo simplificado podemos interagir diretamente de modo focado no serviço correspondente via interface, ou adicionar lógica em cooperação de dependência.
  - LSP: As implementações de `IFeedbackService` serão perfeitamente substituíveis por dublês de testes.
  - ISP: A interface `IFeedbackService` conterá apenas os métodos relevantes para submissão e consulta de feedbacks, sem inflar interfaces existentes como `IMonitoriaService`.
  - DIP: Novas implementações dependerão de abstrações (`IFeedbackService`, `IMonitoriaService`). O `FeedbackService` receberá as dependências injetadas via construtor (como o `IMonitoriaService` ou os repositórios compartilhados).
- **Clean Code Standards**:
  - Padrão de nomes em inglês no código e PascalCase para tipos/membros, camelCase para variáveis/parâmetros. Nomes com intenção clara: `GetFeedbackByStudentId`, `SubmitFeedback`.
  - Métodos focados e de tamanho limitado, com cláusulas de guarda (*guard clauses*) no início para validações rápidas.
- **TDD (Non-Negotiable)**:
  - Todo o desenvolvimento de código de produção será precedido por testes de unidade falhando (Red), seguindo para passagem (Green) e subsequente refatoração.
- **Interface Segregation & Dependency Injection**:
  - Criação prévia das interfaces (`IFeedbackService` e `IEvaluationStatus` se houver) antes da implementação em código de produção. Injeção de dependência por construtor garantida.
- **Simplicity & In-Memory First**:
  - Armazenamento puramente em coleções `List<Feedback>` ou dicionários em memória, sem o uso de bancos de dados relacionais ou externos.

## Estrutura do Projeto

### Documentação (está feature)

```text
.specify/specs/avaliacao-feedback/
├── plan.md              # Este arquivo (Plano de Implementação)
├── research.md          # Pesquisa e fundamentação de decisões
├── data-model.md        # Detalhes de classes e enumerações .NET 8
├── quickstart.md        # Cenários executáveis e guia rápido (Given/When/Then)
└── contracts/           # Pasta contendo os contratos de interfaces antes da implementação
```

### Código Fonte (raiz do repositório)

```text
ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/
├── MonitoriaBase.sln
├── src/
│   └── MonitoriaBase/
│       ├── MonitoriaBase.csproj
│       ├── Program.cs
│       ├── Models/
│       │   ├── Student.cs
│       │   ├── Exercise.cs
│       │   ├── ExerciseAttempt.cs
│       │   ├── Feedback.cs
│       │   └── EvaluationStatus.cs
│       └── Services/
│           ├── IMonitoriaService.cs
│           ├── MonitoriaService.cs
│           ├── IFeedbackService.cs
│           └── FeedbackService.cs
└── tests/
    └── MonitoriaBase.Tests/
        ├── MonitoriaBase.Tests.csproj
        ├── MonitoriaServiceTests.cs
        └── FeedbackServiceTests.cs
```

**Structure Decision**: Utilizaremos a estrutura padrão de Class Library unificada do projeto base localizada em `ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/` para acomodar os novos tipos e serviços, seguindo rigorosamente a separação existente de arquivos.

## Rastreamento de Complexidade

Nenhuma violação aos princípios e guias da constituição foi introduzida neste design. Todo o modelo permanece sob o princípio de simplicidade (*YAGNI* e *In-Memory First*).
