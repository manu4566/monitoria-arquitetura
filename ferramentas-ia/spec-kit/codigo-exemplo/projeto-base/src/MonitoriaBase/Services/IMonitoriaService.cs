using MonitoriaBase.Models;

namespace MonitoriaBase.Services;

public interface IMonitoriaService
{
    Student RegisterStudent(string name, string email);
    IReadOnlyList<Student> GetStudents();
    IReadOnlyList<Exercise> GetExercises();
    ExerciseAttempt MarkExerciseAsCompleted(Guid studentId, Guid exerciseId);
    IReadOnlyList<ExerciseAttempt> GetStudentProgress(Guid studentId);
    ExerciseAttempt? GetExerciseAttempt(Guid attemptId);
    void UpdateExerciseAttemptStatus(Guid attemptId, string status);
}
