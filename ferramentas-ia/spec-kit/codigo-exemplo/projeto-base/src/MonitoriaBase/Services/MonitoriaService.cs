using MonitoriaBase.Models;

namespace MonitoriaBase.Services;

public class MonitoriaService : IMonitoriaService
{
    private readonly Dictionary<Guid, Student> _students = new();
    private readonly Dictionary<Guid, Exercise> _exercises = new();
    private readonly List<ExerciseAttempt> _attempts = new();

    public MonitoriaService()
    {
        SeedExercises();
    }

    public Student RegisterStudent(string name, string email)
    {
        var student = new Student(name, email);
        _students[student.Id] = student;
        return student;
    }

    public IReadOnlyList<Student> GetStudents() =>
        _students.Values.ToList().AsReadOnly();

    public IReadOnlyList<Exercise> GetExercises() =>
        _exercises.Values.ToList().AsReadOnly();

    public ExerciseAttempt MarkExerciseAsCompleted(Guid studentId, Guid exerciseId)
    {
        if (!_students.ContainsKey(studentId))
            throw new ArgumentException($"Student {studentId} not found.");

        if (!_exercises.ContainsKey(exerciseId))
            throw new ArgumentException($"Exercise {exerciseId} not found.");

        var attempt = new ExerciseAttempt(studentId, exerciseId);
        _attempts.Add(attempt);
        return attempt;
    }

    public IReadOnlyList<ExerciseAttempt> GetStudentProgress(Guid studentId) =>
        _attempts.Where(a => a.StudentId == studentId).ToList().AsReadOnly();

    public ExerciseAttempt? GetExerciseAttempt(Guid attemptId) =>
        _attempts.FirstOrDefault(a => a.Id == attemptId);

    public void UpdateExerciseAttemptStatus(Guid attemptId, string status)
    {
        var attempt = _attempts.FirstOrDefault(a => a.Id == attemptId);
        if (attempt == null)
            throw new ArgumentException($"ExerciseAttempt {attemptId} not found.");

        attempt.Status = status;
    }

    private void SeedExercises()
    {
        var exercises = new[]
        {
            new Exercise("Pipeline Básico CI/CD", "CI/CD", "Iniciante"),
            new Exercise("Pipeline Multi-Stage", "CI/CD", "Intermediário"),
            new Exercise("Sistema de Logging com Singleton", "Design Patterns", "Iniciante"),
            new Exercise("E-commerce com Decorator", "Design Patterns", "Intermediário"),
            new Exercise("Sistema de Notificações", "Design Patterns", "Avançado"),
        };

        foreach (var exercise in exercises)
            _exercises[exercise.Id] = exercise;
    }
}
