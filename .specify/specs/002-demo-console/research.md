# Research: Interface Console Demonstração

**Feature**: 002-demo-console  
**Date**: 2026-08-04  
**Purpose**: Document architectural decisions and research findings for console demonstration interface

## Research Tasks Completed

### 1. Console Application Architecture for .NET 8

**Decision**: Use simple imperative Program.cs with menu loop and method extraction for each feature

**Rationale**:
- Constitution mandates simplicity (Principle V: "YAGNI - Don't build for the future")
- Educational context requires readable, straightforward code
- Console application doesn't need MVC, MVVM, or complex patterns
- Method extraction provides sufficient organization without over-engineering

**Alternatives Considered**:
- ❌ **Command Pattern with ICommand interface**: Adds unnecessary abstraction for 4 menu options
- ❌ **MVC Console Framework**: No educational benefit, increases complexity for learners
- ❌ **Mediator Pattern**: Overkill for direct service invocations

**Best Practices Applied**:
- Extract each menu option to dedicated method (e.g., `HandleSubmitFeedback()`, `HandleQueryByStudent()`)
- Keep Program.cs focused on orchestration (menu loop + input validation)
- Use guard clauses for validation (Clean Code principle)
- Follow single responsibility: one method = one menu action

---

### 2. Seed Data Strategy

**Decision**: Create static seed method `SeedDemoData()` called in Main before menu loop

**Rationale**:
- Constitution requires in-memory storage (Principle V)
- Spec requires "no mínimo 3 alunos, 3 exercícios, 5 tentativas" (FR-003)
- Data must be repeatable for demos (same data every run)
- No persistence between executions (Assumption: "estado não persiste")

**Implementation Approach**:
```csharp
// Pseudo-code structure
static void SeedDemoData(IMonitoriaService monitoriaService, IFeedbackService feedbackService)
{
    // Create 3 students
    var student1 = monitoriaService.RegisterStudent(...);
    var student2 = monitoriaService.RegisterStudent(...);
    var student3 = monitoriaService.RegisterStudent(...);
    
    // Create 3 exercises
    var exercise1 = monitoriaService.CreateExercise(...);
    var exercise2 = monitoriaService.CreateExercise(...);
    var exercise3 = monitoriaService.CreateExercise(...);
    
    // Create 5 attempts (2 evaluated, 3 pending)
    var attempt1 = monitoriaService.SubmitExerciseAttempt(...);
    var attempt2 = monitoriaService.SubmitExerciseAttempt(...);
    var attempt3 = monitoriaService.SubmitExerciseAttempt(...);
    var attempt4 = monitoriaService.SubmitExerciseAttempt(...);
    var attempt5 = monitoriaService.SubmitExerciseAttempt(...);
    
    // Create 2 feedbacks (to show both Approved and NeedsRevision)
    feedbackService.SubmitFeedback(attempt1.Id, "Excelente trabalho!", EvaluationStatus.Approved);
    feedbackService.SubmitFeedback(attempt2.Id, "Precisa revisar a lógica do loop", EvaluationStatus.NeedsRevision);
}
```

**Data Realism**:
- Use realistic Brazilian names for students (Ana Silva, Carlos Souza, Maria Santos)
- Use real exercise topics (Implementação de Lista Encadeada, Sistema de Autenticação, API REST)
- Vary submission dates (last 7 days) for realistic timeline

---

### 3. User Input Handling & Validation

**Decision**: Use `Console.ReadLine()` with TryParse for numeric inputs, string validation for text

**Rationale**:
- Built-in .NET methods, no external dependencies (Constitution constraint)
- TryParse prevents crashes on invalid input (edge case: "abc" instead of number)
- Guard clauses for null/empty checks (Clean Code standard)

**Validation Patterns**:

**Numeric Menu Option**:
```csharp
string? input = Console.ReadLine();
if (!int.TryParse(input, out int choice))
{
    Console.WriteLine("✗ Opção inválida. Digite um número.");
    continue;
}
```

**Comment Length Validation** (FR-005):
```csharp
string? comment = Console.ReadLine();
if (string.IsNullOrWhiteSpace(comment) || comment.Length < 10)
{
    Console.WriteLine("✗ Comentário deve ter entre 10 e 5000 caracteres. Tente novamente.");
    continue;
}
if (comment.Length > 5000)
{
    comment = comment.Substring(0, 5000);
    Console.WriteLine("⚠ Comentário foi truncado para 5000 caracteres.");
}
```

**Edge Cases Addressed**:
- Empty input (Enter without text): Treat as invalid, prompt again
- Null input (Ctrl+Z on Windows): Treat as exit signal
- Whitespace-only: Trim and validate length
- Very long input: Truncate with warning (edge case from spec)

---

### 4. Display Formatting & User Experience

**Decision**: Use helper methods for consistent formatting, no external UI libraries

**Rationale**:
- Constitution prohibits external dependencies in core logic
- Spec requires "Interface puramente textual" (Assumption)
- Unicode support for ✓/✗ with ASCII fallback (Assumption: "fallback para 'OK' e 'ERRO'")

**Formatting Helpers**:

**DisplayHeader()**:
```csharp
static void DisplayHeader(string title)
{
    Console.Clear(); // FR-012: limpar console entre telas
    Console.WriteLine("========================================");
    Console.WriteLine($"  {title}");
    Console.WriteLine("========================================");
    Console.WriteLine();
}
```

**FormatDateTime()**:
```csharp
static string FormatDateTime(DateTime dt)
{
    return dt.ToString("dd/MM/yyyy HH:mm"); // Edge case: formato brasileiro
}
```

**DisplayFeedback()** (for ConsoleFeedbackView):
```csharp
static void DisplayFeedback(Feedback feedback, string exerciseName, string studentName)
{
    Console.WriteLine($"  Exercício: {exerciseName}");
    Console.WriteLine($"  Aluno: {studentName}");
    Console.WriteLine($"  Status: {TranslateStatus(feedback.Status)}");
    Console.WriteLine($"  Comentário: {feedback.Comment}");
    Console.WriteLine($"  Data: {FormatDateTime(feedback.CreatedAt)}");
    Console.WriteLine();
}

static string TranslateStatus(EvaluationStatus status)
{
    return status == EvaluationStatus.Approved ? "Aprovado" : "Precisa Revisão";
}
```

**Best Practices**:
- Separate formatting logic from business logic (SRP)
- Consistent spacing and alignment
- Portuguese labels throughout (spec requirement)
- Visual feedback with symbols (✓/✗) per FR-013

---

### 5. Sequential ID Mapping (GUID to Int)

**Decision**: Create in-memory `Dictionary<int, Guid>` mapping for user-friendly IDs

**Rationale**:
- Spec assumption: "GUIDs não expostos ao usuário - IDs sequenciais (1, 2, 3...)"
- Internal entities use Guid (existing implementation from feature 001)
- Console UI needs simple numbers for usability (FR-007)

**Implementation**:
```csharp
// In each display method
var attempts = monitoriaService.GetAllExerciseAttempts();
var mapping = new Dictionary<int, Guid>();
int id = 1;
foreach (var attempt in attempts.Where(a => a.Status == "Pendente"))
{
    mapping[id] = attempt.Id;
    Console.WriteLine($"  {id}. Exercício: {attempt.ExerciseName} | Aluno: {attempt.StudentName} | Data: {FormatDateTime(attempt.SubmittedAt)}");
    id++;
}

// When user selects ID
if (mapping.TryGetValue(selectedId, out Guid attemptGuid))
{
    // Use attemptGuid for service calls
}
```

**Trade-offs**:
- ✅ Simplifies user interaction (type "1" instead of GUID)
- ✅ Aligns with educational focus (less cognitive load)
- ❌ Creates temporary mapping (acceptable, no persistence required)

---

### 6. Error Handling & Graceful Degradation

**Decision**: Try-catch at top level (Main loop) with user-friendly error messages

**Rationale**:
- Constitution requires no crashes (Quality Gate: all tests pass)
- Spec SC-005: "10 loops sem crash" (stability requirement)
- Educational context: errors should teach, not confuse

**Error Handling Strategy**:
```csharp
while (!exit)
{
    try
    {
        DisplayMainMenu();
        // ... menu logic ...
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"✗ Erro de validação: {ex.Message}");
        Console.WriteLine("Pressione Enter para continuar...");
        Console.ReadLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Erro inesperado: {ex.Message}");
        Console.WriteLine("Por favor, reporte este erro ao desenvolvedor.");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
    }
}
```

**Edge Cases**:
- Service throws ArgumentException: Display validation message, return to menu
- Unexpected exception: Display generic error, don't crash (SC-005)
- Ctrl+C signal: Caught in edge case handling (graceful shutdown)

---

### 7. Testing Strategy

**Decision**: Unit test Program.cs logic by extracting testable methods to static helper class

**Rationale**:
- Constitution mandates TDD (Principle III: NON-NEGOTIABLE)
- Static Program.Main is hard to test directly
- Extract business logic to testable static helper class `ConsoleHelper`

**Test Coverage Plan**:
```csharp
// ConsoleHelper.cs (new file)
public static class ConsoleHelper
{
    public static bool TryParseMenuOption(string? input, int min, int max, out int option) { }
    public static string TranslateStatus(EvaluationStatus status) { }
    public static string FormatDateTime(DateTime dt) { }
    public static bool ValidateCommentLength(string? comment, out string validatedComment) { }
}

// ConsoleHelperTests.cs
[Fact]
public void TryParseMenuOption_ValidNumber_ReturnsTrue() { }

[Fact]
public void TryParseMenuOption_InvalidNumber_ReturnsFalse() { }

[Fact]
public void TranslateStatus_Approved_ReturnsAprovado() { }

[Fact]
public void FormatDateTime_BrazilianFormat_ReturnsCorrectFormat() { }

[Fact]
public void ValidateCommentLength_TooShort_ReturnsFalse() { }

[Fact]
public void ValidateCommentLength_TooLong_TruncatesAndReturnsTrue() { }
```

**What NOT to test**:
- Console.WriteLine output (integration concern, not unit test)
- User interaction flow (manual QA via quickstart.md scenarios)
- Service behavior (already tested in feature 001)

---

## Summary of Key Decisions

| Decision Area | Choice | Rationale |
|---------------|--------|-----------|
| **Architecture** | Simple Program.cs with extracted methods | Constitution: YAGNI, simplicity first |
| **Seed Data** | Static method with hardcoded realistic data | Repeatable demos, no persistence needed |
| **Input Handling** | Console.ReadLine + TryParse + guard clauses | Built-in .NET, no dependencies, Clean Code |
| **Formatting** | Helper methods with Portuguese labels | SRP, consistent UX, spec requirement |
| **ID Mapping** | Dictionary<int, Guid> per display | User-friendly IDs, internal Guid preserved |
| **Error Handling** | Top-level try-catch with clear messages | Stability (SC-005), educational value |
| **Testing** | Extract logic to ConsoleHelper static class | TDD compliance, testable without mocking Console |

---

## Open Questions (None)

All unknowns from spec have been resolved through research. No clarifications needed.

---

## Next Steps

Proceed to **Phase 1: Design & Contracts**:
1. Create `data-model.md` with ConsoleFeedbackView and SeedData structures
2. Define `contracts/` if needed (likely none - UI layer, not service layer)
3. Create `quickstart.md` with manual test scenarios for each user story
