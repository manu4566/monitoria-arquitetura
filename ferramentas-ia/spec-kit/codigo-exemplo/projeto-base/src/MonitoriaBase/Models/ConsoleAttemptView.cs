using MonitoriaBase.Models;

namespace MonitoriaBase.Models;

/// <summary>
/// Aggregate view for displaying exercise attempts with sequential user-friendly IDs.
/// This is a transient display structure, not persisted.
/// </summary>
public class ConsoleAttemptView
{
    /// <summary>
    /// Gets the sequential display ID for user selection (1, 2, 3...).
    /// </summary>
    public int DisplayId { get; init; }

    /// <summary>
    /// Gets the actual attempt GUID for service calls.
    /// </summary>
    public Guid ActualAttemptId { get; init; }

    /// <summary>
    /// Gets the exercise name.
    /// </summary>
    public required string ExerciseName { get; init; }

    /// <summary>
    /// Gets the student name.
    /// </summary>
    public required string StudentName { get; init; }

    /// <summary>
    /// Gets the formatted submission date in Brazilian format ("dd/MM/yyyy HH:mm").
    /// </summary>
    public required string FormattedSubmissionDate { get; init; }

    /// <summary>
    /// Gets the attempt status ("Pendente", "Aprovado", "Precisa Revisão").
    /// </summary>
    public required string Status { get; init; }
}
