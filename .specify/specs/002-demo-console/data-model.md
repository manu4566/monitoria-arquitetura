# Data Model: Interface Console Demonstração

**Feature**: 002-demo-console  
**Date**: 2026-08-04  
**Purpose**: Define data structures and relationships for console demonstration interface

## Overview

This feature implements a console UI layer on top of existing domain models (Student, Exercise, ExerciseAttempt, Feedback) from feature 001. No new persistent entities are created - only transient display/seed structures.

---

## Display Structures (Transient, Non-Persistent)

### ConsoleFeedbackView

**Purpose**: Aggregate view for displaying feedback with related entity names (not GUIDs)

**Structure**:
```csharp
public class ConsoleFeedbackView
{
    public Guid FeedbackId { get; init; }
    public string ExerciseName { get; init; }
    public string StudentName { get; init; }
    public string Status { get; init; } // "Aprovado" or "Precisa Revisão" (translated)
    public string Comment { get; init; }
    public string FormattedDate { get; init; } // "dd/MM/yyyy HH:mm"
}
```

**Rationale**:
- Aggregates data from Feedback, ExerciseAttempt, Student, Exercise for display
- Avoids exposing GUIDs to user (Spec Assumption)
- Pre-formats date and status for consistent Portuguese display
- Read-only (init-only properties) - display structure only

**Validation Rules**: None (display-only, data already validated by services)

**Relationships**: None (denormalized view, not persisted)

---

### ConsoleAttemptView

**Purpose**: Aggregate view for displaying exercise attempts with sequential IDs

**Structure**:
```csharp
public class ConsoleAttemptView
{
    public int DisplayId { get; init; } // Sequential 1, 2, 3... (not persisted)
    public Guid ActualAttemptId { get; init; } // Guid for service calls
    public string ExerciseName { get; init; }
    public string StudentName { get; init; }
    public string FormattedSubmissionDate { get; init; } // "dd/MM/yyyy HH:mm"
    public string Status { get; init; } // "Pendente", "Aprovado", "Precisa Revisão"
}
```

**Rationale**:
- Provides user-friendly sequential ID for selection (FR-007, Assumption)
- Maps DisplayId → ActualAttemptId for service invocations
- Pre-formats all display strings for consistent UI
- Filters to only show relevant attempts (e.g., Pendente for submission)

**Validation Rules**: None (display-only)

**Relationships**: None (denormalized view)

---

## Seed Data Structures

### SeedDataResult

**Purpose**: Return value from seed method, provides references for display

**Structure**:
```csharp
public class SeedDataResult
{
    public List<Student> Students { get; init; }
    public List<Exercise> Exercises { get; init; }
    public List<ExerciseAttempt> Attempts { get; init; }
    public List<Feedback> ExistingFeedbacks { get; init; }
}
```

**Rationale**:
- Encapsulates all seeded data for potential debugging/logging
- Provides counts for display (e.g., "3 alunos cadastrados")
- Not strictly necessary but improves testability (can verify seed correctness)

**Usage**:
```csharp
var seedResult = SeedDemoData(monitoriaService, feedbackService);
Console.WriteLine($"Demo inicializado: {seedResult.Students.Count} alunos, {seedResult.Attempts.Count} tentativas.");
```

---

### SeedData Configuration (Hardcoded Values)

**Students** (3 required by FR-003):
| ID (sequential) | Name | Registration Date |
|-----------------|------|------------------|
| 1 | Ana Silva | Today - 30 days |
| 2 | Carlos Souza | Today - 25 days |
| 3 | Maria Santos | Today - 20 days |

**Exercises** (3 required by FR-003):
| ID (sequential) | Name | Difficulty | Created Date |
|-----------------|------|-----------|-------------|
| 1 | Implementação de Lista Encadeada | Médio | Today - 15 days |
| 2 | Sistema de Autenticação JWT | Avançado | Today - 10 days |
| 3 | API REST com CRUD Completo | Intermediário | Today - 5 days |

**Exercise Attempts** (5 required by FR-003, mix of evaluated/pending):
| Display ID | Student | Exercise | Submitted | Status | Has Feedback |
|------------|---------|----------|-----------|--------|--------------|
| 1 | Ana Silva | Lista Encadeada | Today - 7 days | Aprovado | Yes (Approved) |
| 2 | Carlos Souza | Autenticação JWT | Today - 5 days | Precisa Revisão | Yes (NeedsRevision) |
| 3 | Ana Silva | API REST | Today - 3 days | Pendente | No |
| 4 | Maria Santos | Lista Encadeada | Today - 2 days | Pendente | No |
| 5 | Carlos Souza | API REST | Today - 1 day | Pendente | No |

**Existing Feedbacks** (2 pre-created to show both statuses):
| Attempt | Status | Comment |
|---------|--------|---------|
| Attempt 1 (Ana/Lista) | Approved | "Excelente implementação! Código limpo e bem testado." |
| Attempt 2 (Carlos/Auth) | NeedsRevision | "A lógica de validação precisa revisar casos de token expirado." |

**Rationale for Seed Design**:
- Mix of evaluated (2) and pending (3) attempts demonstrates both flows
- Both feedback statuses represented (Approved, NeedsRevision)
- Realistic Brazilian names and exercise topics (educational context)
- Recent dates (last 30 days) for realistic demo timeline
- Multiple attempts per student and per exercise (shows filtering works)

---

## Existing Domain Models (Reference Only)

These models are **already implemented** in feature 001. Listed here for completeness.

### Student (from feature 001)
- `Guid Id`
- `string Name`
- `DateTime RegistrationDate`

### Exercise (from feature 001)
- `Guid Id`
- `string Name`
- `string Difficulty`
- `DateTime CreatedAt`

### ExerciseAttempt (from feature 001)
- `Guid Id`
- `Guid StudentId`
- `Guid ExerciseId`
- `DateTime SubmittedAt`
- `string Status` (mutable - can be updated by FeedbackService)

### Feedback (from feature 001)
- `Guid Id`
- `Guid ExerciseAttemptId`
- `string Comment` (10-5000 chars)
- `EvaluationStatus Status` (Approved or NeedsRevision)
- `DateTime CreatedAt`

### EvaluationStatus (from feature 001)
```csharp
public enum EvaluationStatus
{
    Approved,
    NeedsRevision
}
```

---

## Data Flow Diagrams

### User Story 1: Menu Navigation
```
User Input (string) 
  → TryParse(int) 
  → Validate Range [0-3] 
  → Route to Handler Method
```

### User Story 2: Submit Feedback
```
1. Fetch all ExerciseAttempts (Status = "Pendente")
2. Map to ConsoleAttemptView with sequential DisplayId
3. Display list with Console.WriteLine
4. User selects DisplayId → resolve to Guid
5. User selects EvaluationStatus (1=Approved, 2=NeedsRevision)
6. User enters comment (string)
7. Validate comment length (10-5000)
8. Call FeedbackService.SubmitFeedback(Guid, string, EvaluationStatus)
9. Display success message with feedback details
```

### User Story 3: Query Feedbacks
```
By Student:
1. Fetch all Students
2. Display with sequential IDs
3. User selects Student DisplayId → resolve to Guid
4. Call FeedbackService.GetFeedbackByStudentId(Guid)
5. Map results to ConsoleFeedbackView
6. Display formatted list

By Status:
1. User selects status (1=Approved, 2=NeedsRevision)
2. Map selection to EvaluationStatus enum
3. Call FeedbackService.GetFeedbackByStatus(EvaluationStatus)
4. Map results to ConsoleFeedbackView
5. Display formatted list grouped by exercise
```

---

## Validation Rules Summary

| Field | Rule | Error Message (Portuguese) |
|-------|------|---------------------------|
| Menu Option | Integer 0-3 | "✗ Opção inválida. Digite um número entre 0 e 3." |
| Attempt DisplayId | Exists in current mapping | "✗ ID inválido. Escolha um número da lista." |
| Status Choice | Integer 1-2 | "✗ Escolha inválida. Digite 1 ou 2." |
| Comment | 10-5000 chars | "✗ Comentário deve ter entre 10 e 5000 caracteres." |
| Comment (>5000) | Auto-truncate + warning | "⚠ Comentário foi truncado para 5000 caracteres." |
| Student DisplayId | Exists in current mapping | "✗ ID inválido. Escolha um número da lista." |
| Empty Input | Not null/whitespace | "✗ Entrada não pode estar vazia. Tente novamente." |

**Validation Location**: Program.cs helper methods (extracted to ConsoleHelper for testing)

---

## State Management

**Session State**: None (stateless application)
- Each menu action fetches fresh data from services
- No caching, no session storage
- Seed data recreated on each program run

**Mapping State**: Temporary (method-scoped)
- `Dictionary<int, Guid>` created per display operation
- Scope: single method execution only
- Discarded after user selection

**Why No Persistence**: 
- Constitution Principle V: In-memory first, YAGNI
- Spec Assumption: "estado não persiste entre execuções"
- Educational focus: demonstrate service usage, not state management

---

## Dependencies on Feature 001

| Dependency | Type | Usage |
|------------|------|-------|
| `IMonitoriaService` | Service Interface | Fetch students, exercises, attempts for seed/display |
| `IFeedbackService` | Service Interface | Submit feedbacks, query by student/status |
| `Student` | Model | Display in lists, seed data creation |
| `Exercise` | Model | Display in lists, seed data creation |
| `ExerciseAttempt` | Model | Display in lists, map to views, seed data |
| `Feedback` | Model | Display in query results, seed data |
| `EvaluationStatus` | Enum | Map user input (1/2) to enum (Approved/NeedsRevision) |

**Integration Points**:
- Program.cs constructor-injects services (DI compliance)
- All service calls use Guid (internal) not DisplayId (UI)
- No direct database/storage access (services encapsulate)

---

## Testing Considerations

### Unit Tests (ConsoleHelper class)
- Input parsing (string → int, validation)
- Status translation (enum → Portuguese string)
- Date formatting (DateTime → "dd/MM/yyyy HH:mm")
- Comment validation (length checks, truncation)

### Integration Tests (NOT required per TDD principle)
- Full menu flow with seeded services (optional)
- Manual testing via quickstart.md scenarios

### What NOT to Test
- Console.WriteLine output (UI concern, not business logic)
- Service behavior (already tested in feature 001)
- User interaction timing (SC-001: manual validation)

---

## Summary

- **New Persistent Entities**: 0 (UI layer only)
- **New Display Structures**: 2 (ConsoleFeedbackView, ConsoleAttemptView)
- **Seed Configuration**: 3 students, 3 exercises, 5 attempts, 2 feedbacks
- **Dependencies**: 2 services (IMonitoriaService, IFeedbackService), 5 models (all from feature 001)
- **Validation Rules**: 7 input validations, all with Portuguese error messages
- **State Management**: Stateless (method-scoped mappings only)

---

**Next Artifact**: Create `quickstart.md` with manual validation scenarios for each user story.
