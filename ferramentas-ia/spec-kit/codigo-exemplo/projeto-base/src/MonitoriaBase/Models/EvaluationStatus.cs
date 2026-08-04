namespace MonitoriaBase.Models;

/// <summary>
/// Represents the evaluation status of an exercise attempt feedback.
/// </summary>
public enum EvaluationStatus
{
    /// <summary>
    /// The exercise attempt is approved and meets all criteria.
    /// </summary>
    Approved,
    
    /// <summary>
    /// The exercise attempt needs revision and resubmission.
    /// </summary>
    NeedsRevision
}
