using MonitoriaBase.Models;

namespace MonitoriaBase.Services;

/// <summary>
/// Implementation of IFeedbackService for managing exercise feedback.
/// Provides in-memory storage and validation for feedback operations.
/// </summary>
public class FeedbackService : IFeedbackService
{
    private readonly IMonitoriaService _monitoriaService;
    private readonly Dictionary<Guid, Feedback> _feedbacks = new();
    private readonly Dictionary<Guid, Guid> _attemptToFeedback = new();

    /// <summary>
    /// Initializes a new instance of FeedbackService with dependency injection.
    /// </summary>
    /// <param name="monitoriaService">Service for accessing exercise attempts</param>
    public FeedbackService(IMonitoriaService monitoriaService)
    {
        _monitoriaService = monitoriaService ?? throw new ArgumentNullException(nameof(monitoriaService));
    }

    /// <inheritdoc/>
    public Feedback SubmitFeedback(Guid exerciseAttemptId, string comment, EvaluationStatus status)
    {
        // Validate that ExerciseAttempt exists
        var attempt = _monitoriaService.GetExerciseAttempt(exerciseAttemptId);
        if (attempt == null)
            throw new ArgumentException($"ExerciseAttempt {exerciseAttemptId} not found.", nameof(exerciseAttemptId));

        // Create feedback (Feedback constructor validates comment length)
        var feedback = new Feedback(exerciseAttemptId, comment, status);

        // Store feedback in memory
        _feedbacks[feedback.Id] = feedback;
        _attemptToFeedback[exerciseAttemptId] = feedback.Id;

        // Update ExerciseAttempt status
        var newStatus = status == EvaluationStatus.Approved ? "Approved" : "NeedsRevision";
        _monitoriaService.UpdateExerciseAttemptStatus(exerciseAttemptId, newStatus);

        return feedback;
    }

    /// <inheritdoc/>
    public Feedback? GetFeedbackByAttemptId(Guid exerciseAttemptId)
    {
        if (_attemptToFeedback.TryGetValue(exerciseAttemptId, out var feedbackId))
        {
            return _feedbacks[feedbackId];
        }
        return null;
    }

    /// <inheritdoc/>
    public IReadOnlyList<Feedback> GetFeedbackByStudentId(Guid studentId)
    {
        var studentProgress = _monitoriaService.GetStudentProgress(studentId);
        var attemptIds = studentProgress.Select(a => a.Id).ToHashSet();

        return _feedbacks.Values
            .Where(f => attemptIds.Contains(f.ExerciseAttemptId))
            .ToList()
            .AsReadOnly();
    }

    /// <inheritdoc/>
    public IReadOnlyList<Feedback> GetFeedbackByStatus(EvaluationStatus status)
    {
        return _feedbacks.Values
            .Where(f => f.Status == status)
            .ToList()
            .AsReadOnly();
    }
}
