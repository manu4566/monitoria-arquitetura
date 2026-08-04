namespace MonitoriaBase.Models;

public class ExerciseAttempt
{
    public Guid Id { get; init; }
    public Guid StudentId { get; init; }
    public Guid ExerciseId { get; init; }
    public DateTime CompletedAt { get; init; }
    public string Status { get; set; }

    public ExerciseAttempt(Guid studentId, Guid exerciseId, string status = "Completed")
    {
        Id = Guid.NewGuid();
        StudentId = studentId;
        ExerciseId = exerciseId;
        CompletedAt = DateTime.UtcNow;
        Status = status;
    }
}
