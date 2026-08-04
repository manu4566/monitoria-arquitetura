namespace MonitoriaBase.Models;

/// <summary>
/// Represents feedback provided by a monitor for an exercise attempt.
/// Feedback is immutable once created.
/// </summary>
public class Feedback
{
    /// <summary>
    /// Unique identifier for the feedback.
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Reference to the exercise attempt being evaluated.
    /// </summary>
    public Guid ExerciseAttemptId { get; init; }
    
    /// <summary>
    /// Feedback comment provided by the monitor.
    /// Must be between 10 and 5000 characters.
    /// </summary>
    public string Comment { get; init; }
    
    /// <summary>
    /// Evaluation status (Approved or NeedsRevision).
    /// </summary>
    public EvaluationStatus Status { get; init; }
    
    /// <summary>
    /// Timestamp when the feedback was created.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Creates a new feedback instance with validation.
    /// </summary>
    /// <param name="exerciseAttemptId">ID of the exercise attempt</param>
    /// <param name="comment">Feedback comment (10-5000 characters)</param>
    /// <param name="status">Evaluation status</param>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public Feedback(Guid exerciseAttemptId, string comment, EvaluationStatus status)
    {
        if (exerciseAttemptId == Guid.Empty)
            throw new ArgumentException("ExerciseAttemptId cannot be empty.", nameof(exerciseAttemptId));

        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("Comment cannot be null or empty.", nameof(comment));

        if (comment.Length < 10 || comment.Length > 5000)
            throw new ArgumentException("Comment must be between 10 and 5000 characters.", nameof(comment));

        Id = Guid.NewGuid();
        ExerciseAttemptId = exerciseAttemptId;
        Comment = comment;
        Status = status;
        CreatedAt = DateTime.UtcNow;
    }
}
