# Contracts: Interface Console Demonstração

**Feature**: 002-demo-console  
**Date**: 2026-08-04

## No New Interface Contracts

This feature implements a **console UI layer** that consumes existing service interfaces from feature 001. It does not expose new public APIs, libraries, or services to external consumers.

## Consumed Interfaces (from Feature 001)

The console application depends on these existing contracts:

### IMonitoriaService
```csharp
public interface IMonitoriaService
{
    Student RegisterStudent(string name);
    Exercise CreateExercise(string name, string difficulty);
    ExerciseAttempt SubmitExerciseAttempt(Guid studentId, Guid exerciseId);
    IReadOnlyList<Student> GetAllStudents();
    IReadOnlyList<Exercise> GetAllExercises();
    IReadOnlyList<ExerciseAttempt> GetAllExerciseAttempts();
    ExerciseAttempt? GetExerciseAttempt(Guid attemptId);
    void UpdateExerciseAttemptStatus(Guid attemptId, string status);
}
```

**Usage in this feature**: Seed data creation, display lists for user selection

---

### IFeedbackService
```csharp
public interface IFeedbackService
{
    Feedback SubmitFeedback(Guid exerciseAttemptId, string comment, EvaluationStatus status);
    Feedback? GetFeedbackByAttemptId(Guid exerciseAttemptId);
    IReadOnlyList<Feedback> GetFeedbackByStudentId(Guid studentId);
    IReadOnlyList<Feedback> GetFeedbackByStatus(EvaluationStatus status);
}
```

**Usage in this feature**: Submit feedback wizard, query feedbacks by student/status

---

## Why No New Contracts?

1. **Project Type**: Console application (executable), not a library or service
2. **Constitution Principle**: Simplicity first (V. "YAGNI") - no need for public API
3. **Scope**: Demonstration/testing tool, not reusable component
4. **Architecture**: Consumes services via DI, doesn't expose new abstractions

---

## Internal Helper Contracts (Non-Public)

The following are **internal helper classes** for the console UI, not public contracts:

### ConsoleHelper (static utility class)
```csharp
internal static class ConsoleHelper
{
    // Input parsing
    public static bool TryParseMenuOption(string? input, int min, int max, out int option);
    
    // Formatting
    public static string TranslateStatus(EvaluationStatus status);
    public static string FormatDateTime(DateTime dt);
    
    // Validation
    public static bool ValidateCommentLength(string? comment, out string validatedComment);
}
```

**Rationale**: Extract testable logic from Program.cs (TDD requirement), but keep internal (not part of public API)

---

## Contract Stability

Since this feature only **consumes** existing interfaces (doesn't define new public contracts), there are no breaking change concerns. Changes to `IMonitoriaService` or `IFeedbackService` would be managed in feature 001, not here.

---

**Conclusion**: No new public interface contracts are required or created by this feature.
