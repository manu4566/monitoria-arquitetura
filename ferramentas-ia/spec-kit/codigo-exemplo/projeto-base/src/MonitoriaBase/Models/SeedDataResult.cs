using MonitoriaBase.Models;

namespace MonitoriaBase.Models;

/// <summary>
/// Encapsulates the result of demo data seeding operation.
/// Provides references to all created entities for debugging and validation.
/// </summary>
public class SeedDataResult
{
    /// <summary>
    /// Gets the list of seeded students.
    /// </summary>
    public required List<Student> Students { get; init; }

    /// <summary>
    /// Gets the list of seeded exercises.
    /// </summary>
    public required List<Exercise> Exercises { get; init; }

    /// <summary>
    /// Gets the list of seeded exercise attempts.
    /// </summary>
    public required List<ExerciseAttempt> Attempts { get; init; }

    /// <summary>
    /// Gets the list of pre-existing feedbacks created during seeding.
    /// </summary>
    public required List<Feedback> ExistingFeedbacks { get; init; }
}
