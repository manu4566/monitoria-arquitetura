using MonitoriaBase.Models;

namespace MonitoriaBase.Models;

/// <summary>
/// Aggregate view for displaying feedback with related entity names (not GUIDs).
/// This is a transient display structure, not persisted.
/// </summary>
public class ConsoleFeedbackView
{
    /// <summary>
    /// Gets the feedback identifier.
    /// </summary>
    public Guid FeedbackId { get; init; }

    /// <summary>
    /// Gets the exercise name.
    /// </summary>
    public required string ExerciseName { get; init; }

    /// <summary>
    /// Gets the student name.
    /// </summary>
    public required string StudentName { get; init; }

    /// <summary>
    /// Gets the evaluation status in Portuguese ("Aprovado" or "Precisa Revisão").
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// Gets the feedback comment.
    /// </summary>
    public required string Comment { get; init; }

    /// <summary>
    /// Gets the formatted date in Brazilian format ("dd/MM/yyyy HH:mm").
    /// </summary>
    public required string FormattedDate { get; init; }
}
