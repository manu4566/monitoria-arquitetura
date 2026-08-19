# Tasks: Interface Console Demonstração

**Input**: Design documents from `/specs/002-demo-console/`

**Prerequisites**: plan.md, spec.md, research.md, data-model.md, contracts/, quickstart.md

**Tests**: Tests are REQUIRED for ConsoleHelper utility methods (TDD mandatory per constitution). Program.cs UI orchestration validated manually via quickstart.md.

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `- [ ] [ID] [P?] [Story?] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3)
- Include exact file paths in descriptions

---

## Phase 1: Setup (Shared Infrastructure)

**Status**: ✅ Not needed - project already configured

The `ferramentas-ia/spec-kit/codigo-exemplo/projeto-base` project is fully set up with .NET 8, xUnit, all domain models, and services from feature 001. No setup tasks required.

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [X] T001 [P] Create ConsoleFeedbackView display model in src/MonitoriaBase/Models/ConsoleFeedbackView.cs
- [X] T002 [P] Create ConsoleAttemptView display model in src/MonitoriaBase/Models/ConsoleAttemptView.cs
- [X] T003 [P] Create SeedDataResult model in src/MonitoriaBase/Models/SeedDataResult.cs
- [X] T004 Create ConsoleHelper static utility class in src/MonitoriaBase/Helpers/ConsoleHelper.cs (empty shell with method signatures)

**Checkpoint**: Foundation ready - user story implementation can now begin

---

## Phase 3: User Story 1 - Menu Interativo de Navegação (Priority: P1) 🎯 MVP

**Goal**: Users can view a numbered menu, select options with keyboard input, navigate between features, and exit cleanly

**Independent Test**: Execute `dotnet run`, verify menu displays, select option "1", verify navigation works, select "0", verify exit

### Tests for User Story 1 (TDD - Write First, See Fail) ⚠️

- [X] T005 [P] [US1] Write test: TryParseMenuOption_ValidNumber_ReturnsTrue in tests/MonitoriaBase.Tests/ConsoleHelperTests.cs
- [X] T006 [P] [US1] Write test: TryParseMenuOption_InvalidNumber_ReturnsFalse in tests/MonitoriaBase.Tests/ConsoleHelperTests.cs
- [X] T007 [P] [US1] Write test: TryParseMenuOption_OutOfRange_ReturnsFalse in tests/MonitoriaBase.Tests/ConsoleHelperTests.cs
- [X] T008 [P] [US1] Write test: TryParseMenuOption_EmptyInput_ReturnsFalse in tests/MonitoriaBase.Tests/ConsoleHelperTests.cs

**Verify**: Run `dotnet test` - all 4 tests should FAIL (Red phase)

### Implementation for User Story 1

- [X] T009 [US1] Implement TryParseMenuOption method in src/MonitoriaBase/Helpers/ConsoleHelper.cs
- [X] T010 [US1] Implement DisplayHeader method in src/MonitoriaBase/Helpers/ConsoleHelper.cs
- [X] T011 [US1] Create SeedDemoData method in src/MonitoriaBase/Program.cs (seed 3 students, 3 exercises, 5 attempts, 2 feedbacks per data-model.md)
- [X] T012 [US1] Create DisplayMainMenu method in src/MonitoriaBase/Program.cs (4 options: submeter, consultar por aluno, consultar por status, sair)
- [X] T013 [US1] Create Main menu loop in src/MonitoriaBase/Program.cs (call SeedDemoData, display menu, handle input, route to handlers, exit on 0)
- [X] T014 [US1] Add error handling for invalid menu options in src/MonitoriaBase/Program.cs (display "Opção inválida..." message)
- [X] T015 [US1] Add top-level exception handler in src/MonitoriaBase/Program.cs (catch all exceptions, display user-friendly error, return to menu)

**Verify**: Run `dotnet test` - all 4 ConsoleHelper tests should PASS (Green phase)

**Checkpoint**: At this point, menu displays, navigation works, invalid input handled, exit works - MVP complete!

---

## Phase 4: User Story 2 - Demonstração de Submissão de Feedback (Priority: P2)

**Goal**: Users can submit feedback for pending exercise attempts with status choice (Aprovado/Precisa Revisão) and comment validation

**Independent Test**: Execute `dotnet run`, select "1. Submeter Feedback", choose attempt, choose status, enter valid comment, verify feedback created

### Tests for User Story 2 (TDD - Write First, See Fail) ⚠️

- [X] T016 [P] [US2] Write test: ValidateCommentLength_ValidLength_ReturnsTrue in tests/MonitoriaBase.Tests/ConsoleHelperTests.cs
- [X] T017 [P] [US2] Write test: ValidateCommentLength_TooShort_ReturnsFalse in tests/MonitoriaBase.Tests/ConsoleHelperTests.cs
- [X] T018 [P] [US2] Write test: ValidateCommentLength_TooLong_TruncatesAndReturnsTrue in tests/MonitoriaBase.Tests/ConsoleHelperTests.cs
- [X] T019 [P] [US2] Write test: ValidateCommentLength_EmptyInput_ReturnsFalse in tests/MonitoriaBase.Tests/ConsoleHelperTests.cs
- [X] T020 [P] [US2] Write test: TranslateStatus_Approved_ReturnsAprovado in tests/MonitoriaBase.Tests/ConsoleHelperTests.cs
- [X] T021 [P] [US2] Write test: TranslateStatus_NeedsRevision_ReturnsPrecisaRevisao in tests/MonitoriaBase.Tests/ConsoleHelperTests.cs

**Verify**: Run `dotnet test` - all 6 tests should FAIL (Red phase)

### Implementation for User Story 2

- [X] T022 [US2] Implement ValidateCommentLength method in src/MonitoriaBase/Helpers/ConsoleHelper.cs (10-5000 chars, truncate if >5000)
- [X] T023 [US2] Implement TranslateStatus method in src/MonitoriaBase/Helpers/ConsoleHelper.cs (Approved→"Aprovado", NeedsRevision→"Precisa Revisão")
- [X] T024 [US2] Create DisplayPendingAttempts method in src/MonitoriaBase/Program.cs (fetch pending attempts, map to sequential IDs, display formatted list)
- [X] T025 [US2] Create SelectAttemptId method in src/MonitoriaBase/Program.cs (parse user input, validate ID exists in mapping, return Guid)
- [X] T026 [US2] Create SelectEvaluationStatus method in src/MonitoriaBase/Program.cs (display "1. Aprovado / 2. Precisa Revisão", parse choice, return EvaluationStatus)
- [X] T027 [US2] Create GetCommentInput method in src/MonitoriaBase/Program.cs (prompt, read input, call ValidateCommentLength, retry on error)
- [X] T028 [US2] Create HandleSubmitFeedback method in src/MonitoriaBase/Program.cs (orchestrate: display attempts → select → choose status → get comment → call FeedbackService.SubmitFeedback → display success)
- [X] T029 [US2] Add HandleSubmitFeedback to menu routing in src/MonitoriaBase/Program.cs (call on option "1")
- [X] T030 [US2] Add already-evaluated validation in HandleSubmitFeedback in src/MonitoriaBase/Program.cs (check FeedbackService.GetFeedbackByAttemptId, display error if exists)

**Verify**: Run `dotnet test` - all 6 new ConsoleHelper tests should PASS (Green phase)

**Checkpoint**: At this point, User Story 1 AND 2 both work independently - can submit feedback!

---

## Phase 5: User Story 3 - Demonstração de Consulta de Feedbacks (Priority: P3)

**Goal**: Users can query feedbacks by student or by status with formatted display and "no results" handling

**Independent Test**: Execute `dotnet run`, select "2" or "3", verify lists display correctly, verify empty result message when no feedbacks found

### Tests for User Story 3 (TDD - Write First, See Fail) ⚠️

- [X] T031 [P] [US3] Write test: FormatDateTime_BrazilianFormat_ReturnsCorrectString in tests/MonitoriaBase.Tests/ConsoleHelperTests.cs

**Verify**: Run `dotnet test` - test should FAIL (Red phase)

### Implementation for User Story 3

- [X] T032 [US3] Implement FormatDateTime method in src/MonitoriaBase/Helpers/ConsoleHelper.cs (format as "dd/MM/yyyy HH:mm")
- [X] T033 [US3] Create DisplayStudentList method in src/MonitoriaBase/Program.cs (fetch all students, map to sequential IDs, display formatted list)
- [X] T034 [US3] Create SelectStudentId method in src/MonitoriaBase/Program.cs (parse user input, validate ID exists, return student Guid)
- [X] T035 [US3] Create DisplayFeedbackList method in src/MonitoriaBase/Program.cs (accept list of feedbacks, format each with exercise/student/status/comment/date, display)
- [X] T036 [US3] Create HandleQueryByStudent method in src/MonitoriaBase/Program.cs (display students → select → call FeedbackService.GetFeedbackByStudentId → display results or "Nenhum feedback encontrado")
- [X] T037 [US3] Create HandleQueryByStatus method in src/MonitoriaBase/Program.cs (display status options → select → call FeedbackService.GetFeedbackByStatus → display results or "Nenhum feedback encontrado")
- [X] T038 [US3] Add HandleQueryByStudent to menu routing in src/MonitoriaBase/Program.cs (call on option "2")
- [X] T039 [US3] Add HandleQueryByStatus to menu routing in src/MonitoriaBase/Program.cs (call on option "3")

**Verify**: Run `dotnet test` - FormatDateTime test should PASS (Green phase)

**Checkpoint**: All user stories should now be independently functional - full feature complete!

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [X] T040 [P] Run full build verification: `dotnet build` in ferramentas-ia/spec-kit/codigo-exemplo/projeto-base (verify 0 errors, 0 warnings)
- [X] T041 [P] Run full test suite: `dotnet test` in ferramentas-ia/spec-kit/codigo-exemplo/projeto-base (verify all 11 ConsoleHelper tests pass)
- [X] T042 Add XML documentation comments to all ConsoleHelper public methods in src/MonitoriaBase/Helpers/ConsoleHelper.cs
- [X] T043 Add "Pressione Enter para voltar ao menu..." prompt after each handler in src/MonitoriaBase/Program.cs
- [X] T044 Verify Console.Clear() called at start of each handler in src/MonitoriaBase/Program.cs (FR-012 compliance)
- [X] T045 Verify all error messages use ✓/✗ symbols in src/MonitoriaBase/Program.cs (FR-013 compliance, with ASCII fallback)
- [X] T046 Code review: Verify all methods < 50 lines (Clean Code standard from constitution)
- [X] T047 Code review: Verify PascalCase for methods, camelCase for variables (Clean Code standard from constitution)
- [X] T048 Code review: Verify guard clauses used for validation (Clean Code standard from constitution)
- [ ] T049 Manual validation: Run all quickstart.md scenarios (15+ scenarios covering US1, US2, US3, edge cases, success criteria) - READY FOR USER VALIDATION
- [X] T050 Performance validation: Verify startup < 2 seconds (SC-002 from spec.md)
- [ ] T051 Performance validation: Verify navigation flow < 60 seconds (SC-001 from spec.md) - READY FOR USER VALIDATION
- [ ] T052 Stability validation: Execute 10+ menu loops without crash (SC-005 from spec.md) - READY FOR USER VALIDATION

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: ✅ Already complete (project configured)
- **Foundational (Phase 2)**: No dependencies - can start immediately - BLOCKS all user stories
- **User Stories (Phase 3, 4, 5)**: All depend on Foundational phase completion
  - User stories can then proceed sequentially in priority order (P1 → P2 → P3)
  - Or in parallel if multiple developers available
- **Polish (Phase 6)**: Depends on all user stories being complete

### User Story Dependencies

- **User Story 1 (P1 - MVP)**: Depends on Phase 2 (Foundational) only
  - No dependencies on other user stories
  - Provides menu infrastructure for US2 and US3
- **User Story 2 (P2)**: Depends on Phase 2 and US1 (menu must exist for routing)
  - Can be developed after US1 complete
  - Independently testable via `dotnet run` → option "1"
- **User Story 3 (P3)**: Depends on Phase 2 and US1 (menu must exist for routing)
  - Can be developed after US1 complete
  - Can be developed in parallel with US2 (different handlers, no conflicts)
  - Independently testable via `dotnet run` → option "2" or "3"

### Within Each User Story (TDD Workflow)

1. **Write tests FIRST** (Red phase)
   - All tests for ConsoleHelper methods
   - Verify tests FAIL with clear error messages
2. **Implement to pass tests** (Green phase)
   - Implement ConsoleHelper methods
   - Implement handler methods in Program.cs
   - Verify tests PASS
3. **Refactor** (if needed)
   - Apply Clean Code principles
   - Extract repeated logic
   - Keep tests green

### Parallel Opportunities

**Phase 2 (Foundational)**: All 4 tasks can run in parallel
- T001, T002, T003, T004 (different files, no dependencies)

**Phase 3 Tests**: All 4 tests can be written in parallel
- T005, T006, T007, T008 (different test methods)

**Phase 4 Tests**: All 6 tests can be written in parallel
- T016, T017, T018, T019, T020, T021 (different test methods)

**Phase 6 Polish**: Many tasks can run in parallel
- T040 (build), T041 (test), T042 (docs), T046-T048 (code review) can overlap

---

## Parallel Example: User Story 2

```bash
# Launch all tests for User Story 2 together (write in parallel):
T016: "Write test: ValidateCommentLength_ValidLength_ReturnsTrue"
T017: "Write test: ValidateCommentLength_TooShort_ReturnsFalse"
T018: "Write test: ValidateCommentLength_TooLong_TruncatesAndReturnsTrue"
T019: "Write test: ValidateCommentLength_EmptyInput_ReturnsFalse"
T020: "Write test: TranslateStatus_Approved_ReturnsAprovado"
T021: "Write test: TranslateStatus_NeedsRevision_ReturnsPrecisaRevisao"

# All tests written → Run dotnet test → All should FAIL (Red phase confirmed)

# Then implement serially:
T022: "Implement ValidateCommentLength method"
T023: "Implement TranslateStatus method"
# Run dotnet test → All 6 tests should now PASS (Green phase confirmed)

# Continue with handler methods (must be serial - same file):
T024 → T025 → T026 → T027 → T028 → T029 → T030
```

---

## Implementation Strategy

### MVP First (User Story 1 Only - Fastest Path to Value)

1. ✅ Phase 1: Setup (already complete)
2. Complete Phase 2: Foundational (T001-T004) → ~30 minutes
3. Complete Phase 3: User Story 1 (T005-T015) → ~2 hours
4. **STOP and VALIDATE**: Test menu navigation manually
5. Demo working menu to stakeholders → Get feedback early!
6. Continue to US2 and US3 if validated

**Time to MVP**: ~2.5 hours

### Incremental Delivery (Recommended)

1. ✅ Phase 1: Setup → Already complete
2. Phase 2: Foundational → ~30 minutes
3. **Milestone 1**: Phase 3 (US1) → Menu navigation works → Demo! 🎯
4. **Milestone 2**: Phase 4 (US2) → Feedback submission works → Demo!
5. **Milestone 3**: Phase 5 (US3) → Query features work → Demo!
6. Phase 6: Polish → Final validation and cleanup
7. **RELEASE**: Full feature complete

Each milestone is independently valuable and demonstrable.

### Parallel Team Strategy (if 2+ developers)

**Not recommended for this feature** - Program.cs is the main file and will have conflicts. Better to work sequentially (US1 → US2 → US3) or pair program.

If parallel needed:
- Developer A: Phase 2 + Phase 3 (US1) → Menu foundation
- Developer B: Write all tests for US2 and US3 (T016-T021, T031) → Wait for A
- After US1 complete:
  - Developer A: Phase 4 (US2) → Feedback submission
  - Developer B: Phase 5 (US3) → Query features
- Both: Phase 6 (Polish) → Code review together

---

## Task Summary

| Phase | Task Count | Estimated Time | Dependencies |
|-------|-----------|----------------|--------------|
| Phase 1 (Setup) | 0 | 0 min | ✅ Complete |
| Phase 2 (Foundational) | 4 | 30 min | None |
| Phase 3 (US1 - MVP) | 11 (4 tests + 7 impl) | 2 hours | Phase 2 |
| Phase 4 (US2) | 14 (6 tests + 8 impl) | 3 hours | Phase 2, US1 |
| Phase 5 (US3) | 9 (1 test + 8 impl) | 2 hours | Phase 2, US1 |
| Phase 6 (Polish) | 13 | 2 hours | All US complete |
| **TOTAL** | **51 tasks** | **~9.5 hours** | Sequential |

### Test Coverage

- **Total tests**: 11 unit tests for ConsoleHelper
  - US1: 4 tests (TryParseMenuOption validation)
  - US2: 6 tests (ValidateCommentLength, TranslateStatus)
  - US3: 1 test (FormatDateTime)
- **Manual validation**: 15+ scenarios in quickstart.md (US1-US3, edge cases, success criteria)

### File Changes

- **Created**: 4 new files
  - `src/MonitoriaBase/Helpers/ConsoleHelper.cs` (utility class)
  - `src/MonitoriaBase/Models/ConsoleFeedbackView.cs` (display model)
  - `src/MonitoriaBase/Models/ConsoleAttemptView.cs` (display model)
  - `src/MonitoriaBase/Models/SeedDataResult.cs` (seed result)
  - `tests/MonitoriaBase.Tests/ConsoleHelperTests.cs` (11 unit tests)
- **Modified**: 1 file
  - `src/MonitoriaBase/Program.cs` (add menu loop, handlers, seed data)

### Success Criteria Validation

All success criteria from spec.md mapped to tasks:

- **SC-001** (Navigation < 60s): Validated in T051
- **SC-002** (Startup < 2s): Validated in T050
- **SC-003** (100% validation messages): Implemented in T014, T027, T030, T036, T037; verified in T049
- **SC-004** (Query correctness): Validated in T049 (quickstart scenarios)
- **SC-005** (10+ loops stable): Validated in T052

---

## Notes

- **TDD Mandatory**: Constitution requires Test-Driven Development (Principle III: NON-NEGOTIABLE)
  - Write tests FIRST for all ConsoleHelper methods
  - See tests FAIL (Red phase) before implementing
  - Implement to pass tests (Green phase)
  - Refactor while keeping tests green
- **Manual Validation**: Program.cs UI orchestration cannot be unit tested effectively
  - Use quickstart.md scenarios for comprehensive manual validation (T049)
  - This aligns with constitution: "Test business logic, not UI framework"
- **Clean Code**: All methods must be < 50 lines (verified in T046)
- **Portuguese**: All UI strings in Portuguese (FR-001, verified throughout implementation)
- **GUIDs Hidden**: Sequential IDs (1, 2, 3...) for user display, GUIDs only for service calls (data-model.md mapping strategy)
- **Commit Strategy**: Commit after each phase or logical group (e.g., after Phase 3 complete = MVP committed)
- **Constitution Alignment**: All tasks designed to comply with 5 core principles (SOLID, Clean Code, TDD, DI, Simplicity)
