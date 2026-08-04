---
description: "Lista de tarefas para implementação da feature de Sistema de Avaliação e Feedback"
---

# Tasks: Sistema de Avaliação e Feedback de Exercícios

**Input**: Documentos de design de `/specs/avaliacao-feedback/`

**Pré-requisitos**: plan.md, spec.md, research.md, data-model.md, contracts/IFeedbackService.cs, quickstart.md

**Testes**: TDD OBRIGATÓRIO conforme constitution.md - todos os testes devem ser escritos ANTES da implementação

**Organização**: Tarefas agrupadas por história de usuário para permitir implementação e teste independente de cada história

## Formato: `[ID] [P?] [Story] Descrição`

- **[P]**: Pode executar em paralelo (arquivos diferentes, sem dependências)
- **[Story]**: A qual história de usuário esta tarefa pertence (ex: US1, US2)
- Inclui caminhos de arquivo exatos nas descrições

## Convenções de Caminho

- **Projeto único**: `src/`, `tests/` na raiz do repositório
- Caminho base: `ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/`

---

## Phase 1: Setup (Infraestrutura Compartilhada)

**Propósito**: Estrutura do projeto já existe - nenhuma tarefa de setup necessária

*Nota: O projeto MonitoriaBase já está configurado com .NET 8, xUnit e estrutura de pastas em `ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/`*

**Checkpoint**: Projeto pronto para desenvolvimento da feature

---

## Phase 2: Foundational (Pré-requisitos Bloqueantes)

**Propósito**: Infraestrutura core que DEVE estar completa antes que QUALQUER história de usuário possa ser implementada

**⚠️ CRÍTICO**: Nenhum trabalho de história de usuário pode começar até que esta fase esteja completa

- [X] T001 [P] Criar enum EvaluationStatus em ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/src/MonitoriaBase/Models/EvaluationStatus.cs
- [X] T002 [P] Criar interface IFeedbackService em ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/src/MonitoriaBase/Services/IFeedbackService.cs (copiar de contracts/)
- [X] T003 Criar classe Feedback com validações em ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/src/MonitoriaBase/Models/Feedback.cs

**Checkpoint**: Base pronta - implementação de histórias de usuário pode começar em paralelo

---

## Phase 3: User Story 1 - Monitor Submete Feedback (Prioridade: P1) 🎯 MVP

**Goal**: Permitir que monitores forneçam feedback estruturado sobre tentativas de exercício concluídas, incluindo comentários e status de avaliação

**Teste Independente**: 
1. Criar um aluno
2. Criar um exercício
3. Marcar exercício como concluído (ExerciseAttempt)
4. Submeter feedback com comentário e status
5. Verificar que feedback foi registrado e status do ExerciseAttempt foi atualizado

### Testes para User Story 1 (TDD - OBRIGATÓRIO)

> **NOTA: Escrever estes testes PRIMEIRO, garantir que FALHAM antes da implementação**

- [X] T004 [P] [US1] Criar arquivo FeedbackServiceTests.cs em ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/tests/MonitoriaBase.Tests/FeedbackServiceTests.cs
- [X] T005 [P] [US1] Teste: SubmitFeedback_WithValidDataAndApprovedStatus_CreatesFeedbackAndUpdatesAttemptStatus em FeedbackServiceTests.cs
- [X] T006 [P] [US1] Teste: SubmitFeedback_WithValidDataAndNeedsRevisionStatus_CreatesFeedbackAndUpdatesAttemptStatus em FeedbackServiceTests.cs
- [X] T007 [P] [US1] Teste: SubmitFeedback_WithNonExistentAttemptId_ThrowsArgumentException em FeedbackServiceTests.cs
- [X] T008 [P] [US1] Teste: SubmitFeedback_WithCommentTooShort_ThrowsArgumentException em FeedbackServiceTests.cs
- [X] T009 [P] [US1] Teste: SubmitFeedback_WithCommentTooLong_ThrowsArgumentException em FeedbackServiceTests.cs
- [X] T010 [P] [US1] Teste: GetFeedbackByAttemptId_WithExistingFeedback_ReturnsFeedback em FeedbackServiceTests.cs
- [X] T011 [P] [US1] Teste: GetFeedbackByAttemptId_WithNonExistentAttempt_ReturnsNull em FeedbackServiceTests.cs

### Implementação para User Story 1

> **NOTA: Executar `dotnet test` após cada teste escrito para confirmar FALHA (Red)**

- [X] T012 [US1] Implementar FeedbackService.SubmitFeedback em ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/src/MonitoriaBase/Services/FeedbackService.cs
- [X] T013 [US1] Implementar validação de ExerciseAttempt existente em FeedbackService.SubmitFeedback
- [X] T014 [US1] Implementar atualização de Status do ExerciseAttempt em FeedbackService.SubmitFeedback
- [X] T015 [US1] Implementar armazenamento em memória (Dictionary<Guid, Feedback>) em FeedbackService
- [X] T016 [US1] Implementar FeedbackService.GetFeedbackByAttemptId em ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/src/MonitoriaBase/Services/FeedbackService.cs
- [X] T017 [US1] Adicionar injeção de dependência de IMonitoriaService no construtor de FeedbackService

**Checkpoint**: Executar `dotnet test` - todos os testes de User Story 1 devem passar (Green). User Story 1 deve estar totalmente funcional e testável independentemente.

---

## Phase 4: User Story 2 - Aluno Visualiza Feedback Recebido (Prioridade: P2)

**Goal**: Permitir que alunos vejam todo o feedback que receberam em suas tentativas de exercício, incluindo filtros por status

**Teste Independente**:
1. Criar um aluno e exercícios
2. Criar múltiplas tentativas de exercício
3. Submeter múltiplos feedbacks para diferentes tentativas
4. Consultar feedback por ID do aluno
5. Filtrar feedback por status
6. Verificar que todas as entradas são retornadas corretamente

### Testes para User Story 2 (TDD - OBRIGATÓRIO)

> **NOTA: Escrever estes testes PRIMEIRO, garantir que FALHAM antes da implementação**

- [X] T018 [P] [US2] Teste: GetFeedbackByStudentId_WithMultipleFeedbacks_ReturnsAllFeedbacks em FeedbackServiceTests.cs
- [X] T019 [P] [US2] Teste: GetFeedbackByStudentId_WithNoFeedbacks_ReturnsEmptyList em FeedbackServiceTests.cs
- [X] T020 [P] [US2] Teste: GetFeedbackByStudentId_WithNonExistentStudent_ReturnsEmptyList em FeedbackServiceTests.cs
- [X] T021 [P] [US2] Teste: GetFeedbackByStatus_WithApprovedStatus_ReturnsOnlyApprovedFeedbacks em FeedbackServiceTests.cs
- [X] T022 [P] [US2] Teste: GetFeedbackByStatus_WithNeedsRevisionStatus_ReturnsOnlyNeedsRevisionFeedbacks em FeedbackServiceTests.cs
- [X] T023 [P] [US2] Teste: GetFeedbackByStatus_WithNoMatchingFeedbacks_ReturnsEmptyList em FeedbackServiceTests.cs

### Implementação para User Story 2

> **NOTA: Executar `dotnet test` após cada teste escrito para confirmar FALHA (Red)**

- [X] T024 [US2] Implementar FeedbackService.GetFeedbackByStudentId em ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/src/MonitoriaBase/Services/FeedbackService.cs
- [X] T025 [US2] Implementar lógica de busca por StudentId através de ExerciseAttempts em GetFeedbackByStudentId
- [X] T026 [US2] Implementar FeedbackService.GetFeedbackByStatus em ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/src/MonitoriaBase/Services/FeedbackService.cs
- [X] T027 [US2] Implementar lógica de filtro por EvaluationStatus em GetFeedbackByStatus

**Checkpoint**: Executar `dotnet test` - todos os testes de User Story 1 E 2 devem passar. Ambas histórias devem funcionar independentemente.

---

## Phase 5: Polish & Validação Final

**Propósito**: Melhorias que afetam múltiplas histórias de usuário e validação completa

- [X] T028 [P] Executar `dotnet build` e garantir compilação sem warnings
- [X] T029 [P] Executar `dotnet test` e garantir 100% de testes passando
- [X] T030 Validar cenários do quickstart.md manualmente ou via testes de aceitação
- [X] T031 [P] Revisar código para conformidade com SOLID principles (constitution.md)
- [X] T032 [P] Revisar código para conformidade com Clean Code standards (constitution.md)
- [X] T033 [P] Adicionar comentários XML documentation nos métodos públicos de IFeedbackService
- [X] T034 [P] Verificar que todo código está em inglês e documentação em português
- [X] T035 Executar code review final verificando checklist da constitution.md

**Checkpoint**: Feature completa, testada e pronta para merge

---

## Dependências & Ordem de Execução

### Dependências de Fase

- **Setup (Phase 1)**: Sem dependências - projeto já existe
- **Foundational (Phase 2)**: Sem dependências - BLOQUEIA todas as histórias de usuário
- **User Stories (Phase 3+)**: Todas dependem da conclusão da fase Foundational
  - Histórias de usuário podem prosseguir em paralelo (se houver recursos)
  - Ou sequencialmente em ordem de prioridade (P1 → P2)
- **Polish (Fase Final)**: Depende de todas as histórias de usuário desejadas estarem completas

### Dependências de User Story

- **User Story 1 (P1)**: Pode começar após Foundational (Phase 2) - Sem dependências de outras histórias
- **User Story 2 (P2)**: Pode começar após Foundational (Phase 2) - Independente mas usa SubmitFeedback de US1 para setup de testes

### Dentro de Cada User Story

1. **Testes PRIMEIRO** (TDD não-negociável):
   - Escrever todos os testes da história
   - Executar `dotnet test` - confirmar FALHA (Red)
2. **Implementação**:
   - Implementar código mínimo para passar testes
   - Executar `dotnet test` após cada mudança - confirmar SUCESSO (Green)
3. **Refatoração**:
   - Limpar código mantendo testes verdes
   - Aplicar princípios SOLID e Clean Code
4. **História completa antes de mover para próxima prioridade**

### Oportunidades de Paralelização

- Todas as tarefas da Foundational marcadas com [P] podem executar em paralelo
- Dentro de US1: Todos os testes marcados com [P] podem ser escritos em paralelo (T004-T011)
- Dentro de US2: Todos os testes marcados com [P] podem ser escritos em paralelo (T018-T023)
- Tarefas de Polish marcadas com [P] podem executar em paralelo (T028-T035)
- **User Story 1 e User Story 2 podem ser trabalhadas em paralelo por membros diferentes do time após Phase 2**

---

## Exemplo de Paralelização: User Story 1

Se você tem 3 desenvolvedores disponíveis após completar a fase Foundational:

**Developer 1** (Testes):
- T004, T005, T006, T007 (escrever testes em paralelo)

**Developer 2** (Testes):
- T008, T009, T010, T011 (escrever testes em paralelo)

**Developer 3** (Esperar testes ficarem prontos, então implementar):
- T012 → T013 → T014 → T015 → T016 → T017 (sequencial, guiado pelos testes)

Alternativamente, após Phase 2, um dev pode trabalhar inteiramente em US1 (T004-T017) enquanto outro trabalha em US2 (T018-T027) em paralelo.

---

## Exemplo de Paralelização: User Story 2

**Developer 1** (Testes):
- T018, T019, T020 (escrever testes em paralelo)

**Developer 2** (Testes):
- T021, T022, T023 (escrever testes em paralelo)

**Developer 3** (Implementação após testes):
- T024 → T025 → T026 → T027 (sequencial)

---

## Critérios de Sucesso da Feature

- [ ] ✅ Todos os 29 testes passam (`dotnet test` retorna 0 falhas)
- [ ] ✅ Cobertura de 100% dos métodos públicos de FeedbackService
- [ ] ✅ Código compila sem warnings (`dotnet build`)
- [ ] ✅ Todos os cenários do quickstart.md validados
- [ ] ✅ Princípios SOLID verificados em code review
- [ ] ✅ Clean Code standards atendidos (nomes descritivos, métodos curtos)
- [ ] ✅ TDD workflow seguido rigorosamente (Red → Green → Refactor)
- [ ] ✅ Interfaces definidas antes de implementações
- [ ] ✅ Injeção de dependência por construtor
- [ ] ✅ Armazenamento apenas em memória (sem dependências externas)
- [ ] ✅ Código em inglês, documentação em português
- [ ] ✅ Feedback é imutável (sem operações de Update)
- [ ] ✅ Feedback atualiza Status do ExerciseAttempt corretamente

---

## Estratégia de Implementação

### MVP Scope (Entregar Primeiro)
- **Phase 2**: Foundational (T001-T003) - Base necessária
- **Phase 3**: User Story 1 (T004-T017) - Feature core mínima viável
- **Validação**: Executar cenários 1, 2 e 3 do quickstart.md

**Entrega de valor**: Monitores podem submeter feedback estruturado para tentativas de exercício

### Iteração 2 (Completar Feature)
- **Phase 4**: User Story 2 (T018-T027) - Visualização de feedback
- **Phase 5**: Polish (T028-T035) - Qualidade e validação final
- **Validação**: Executar cenário 4 do quickstart.md e todos os testes

**Entrega de valor**: Alunos podem consultar e filtrar todo o feedback recebido

### Abordagem Recomendada
1. Completar Phase 2 (Foundational) primeiro
2. Implementar US1 completa (TDD: Red → Green → Refactor)
3. Validar US1 independentemente com quickstart scenarios 1-3
4. Implementar US2 completa (TDD: Red → Green → Refactor)
5. Validar US2 independentemente com quickstart scenario 4
6. Executar Phase 5 (Polish) para refinamento final
7. Code review usando checklist da constitution.md

---

## Notas de Implementação

### Convenções de Teste (xUnit)
- Padrão de nome: `MethodName_Scenario_ExpectedResult`
- Usar `[Fact]` para testes simples
- Usar `[Theory]` com `[InlineData]` para testes parametrizados
- Arrange-Act-Assert pattern obrigatório
- Um assert por teste (quando possível)

### Convenções de Código
- PascalCase: Classes, métodos, propriedades
- camelCase: Variáveis e parâmetros
- Interfaces: Prefixo `I` (ex: `IFeedbackService`)
- Guard clauses no início dos métodos para validações
- Construtor para inicialização e validação de entidades
- Properties com `init` para imutabilidade

### Armazenamento em Memória
- Usar `Dictionary<Guid, Feedback>` para lookup rápido por ID
- Manter referência ao `IMonitoriaService` para acessar ExerciseAttempts
- Não usar coleções thread-safe (não necessário para contexto educacional)

### Validações Obrigatórias
- ExerciseAttemptId não pode ser Guid.Empty
- Comment não pode ser null ou vazio
- Comment deve ter entre 10 e 5000 caracteres
- ExerciseAttempt deve existir no sistema (via IMonitoriaService)
- Lançar `ArgumentException` com mensagem clara para validações falhadas

---

**Total de Tasks**: 35
- Foundational: 3 tasks
- User Story 1: 14 tasks (7 testes + 7 implementação)
- User Story 2: 10 tasks (6 testes + 4 implementação)
- Polish: 8 tasks

**Estimativa**: 2-3 dias para desenvolvedor experiente seguindo TDD rigorosamente

**MVP Mínimo**: Phases 2 + 3 = 17 tasks (Base + US1)
