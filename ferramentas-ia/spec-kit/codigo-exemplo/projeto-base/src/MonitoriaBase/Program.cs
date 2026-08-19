using MonitoriaBase.Helpers;
using MonitoriaBase.Models;
using MonitoriaBase.Services;

// ============================================
// Sistema MonitoriaBase - Demo Console
// ============================================

// Inicializar serviços com DI
var monitoriaService = new MonitoriaService();
var feedbackService = new FeedbackService(monitoriaService);

// Seed data para demonstração
var seedResult = SeedDemoData(monitoriaService, feedbackService);
Console.WriteLine($"✓ Demo inicializado: {seedResult.Students.Count} alunos, {seedResult.Exercises.Count} exercícios, {seedResult.Attempts.Count} tentativas");
Console.WriteLine("Pressione Enter para continuar...");
Console.ReadLine();

// Menu loop principal
bool exit = false;
while (!exit)
{
    try
    {
        DisplayMainMenu();
        string? input = Console.ReadLine();

        if (!ConsoleHelper.TryParseMenuOption(input, 0, 3, out int option))
        {
            Console.WriteLine("\n✗ Opção inválida. Digite um número entre 0 e 3.");
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
            continue;
        }

        switch (option)
        {
            case 1:
                HandleSubmitFeedback(monitoriaService, feedbackService);
                break;
            case 2:
                HandleQueryByStudent(monitoriaService, feedbackService);
                break;
            case 3:
                HandleQueryByStatus(feedbackService);
                break;
            case 0:
                Console.WriteLine("\nEncerrando...");
                exit = true;
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n✗ Erro inesperado: {ex.Message}");
        Console.WriteLine("Por favor, reporte este erro ao desenvolvedor.");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
    }
}

// ============================================
// Helper Methods
// ============================================

static void DisplayMainMenu()
{
    ConsoleHelper.DisplayHeader("Sistema MonitoriaBase - Demo");
    Console.WriteLine("1. Submeter Feedback");
    Console.WriteLine("2. Consultar Feedbacks por Aluno");
    Console.WriteLine("3. Consultar Feedbacks por Status");
    Console.WriteLine("0. Sair");
    Console.WriteLine();
    Console.Write("Escolha uma opção: ");
}

static SeedDataResult SeedDemoData(IMonitoriaService monitoriaService, IFeedbackService feedbackService)
{
    // Criar 3 alunos (FR-003)
    var ana = monitoriaService.RegisterStudent("Ana Silva", "ana.silva@email.com");
    var carlos = monitoriaService.RegisterStudent("Carlos Souza", "carlos.souza@email.com");
    var maria = monitoriaService.RegisterStudent("Maria Santos", "maria.santos@email.com");

    // Criar 3 exercícios (FR-003)
    var exercises = monitoriaService.GetExercises();
    var exLista = exercises.FirstOrDefault(e => e.Title.Contains("Lista")) ?? exercises[0];
    var exAuth = exercises.FirstOrDefault(e => e.Title.Contains("Auth")) ?? exercises[1];
    var exApi = exercises.FirstOrDefault(e => e.Title.Contains("API")) ?? exercises[2];

    // Criar 5 tentativas de exercício (FR-003)
    // 2 serão avaliadas, 3 ficarão pendentes
    var attempt1 = monitoriaService.MarkExerciseAsCompleted(ana.Id, exLista.Id);
    var attempt2 = monitoriaService.MarkExerciseAsCompleted(carlos.Id, exAuth.Id);
    var attempt3 = monitoriaService.MarkExerciseAsCompleted(ana.Id, exApi.Id);
    var attempt4 = monitoriaService.MarkExerciseAsCompleted(maria.Id, exLista.Id);
    var attempt5 = monitoriaService.MarkExerciseAsCompleted(carlos.Id, exApi.Id);

    // Criar 2 feedbacks pré-existentes (FR-003)
    var feedback1 = feedbackService.SubmitFeedback(
        attempt1.Id,
        "Excelente implementação! Código limpo e bem testado.",
        EvaluationStatus.Approved
    );

    var feedback2 = feedbackService.SubmitFeedback(
        attempt2.Id,
        "A lógica de validação precisa revisar casos de token expirado.",
        EvaluationStatus.NeedsRevision
    );

    return new SeedDataResult
    {
        Students = new List<Student> { ana, carlos, maria },
        Exercises = new List<Exercise> { exLista, exAuth, exApi },
        Attempts = new List<ExerciseAttempt> { attempt1, attempt2, attempt3, attempt4, attempt5 },
        ExistingFeedbacks = new List<Feedback> { feedback1, feedback2 }
    };
}

// ============================================
// User Story 2: Submissão de Feedback
// ============================================

static void HandleSubmitFeedback(IMonitoriaService monitoriaService, IFeedbackService feedbackService)
{
    ConsoleHelper.DisplayHeader("Submeter Feedback");

    // Display pending attempts
    var attemptMapping = DisplayPendingAttempts(monitoriaService, feedbackService);

    if (attemptMapping.Count == 0)
    {
        Console.WriteLine("✗ Nenhuma tentativa disponível para avaliar no momento.");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
        return;
    }

    // Select attempt
    Console.Write("\nDigite o ID da tentativa: ");
    Guid? attemptId = SelectAttemptId(attemptMapping);

    if (attemptId == null)
    {
        Console.WriteLine("✗ ID inválido. Escolha um número da lista.");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
        return;
    }

    // Check if already evaluated (FR-006)
    var existingFeedback = feedbackService.GetFeedbackByAttemptId(attemptId.Value);
    if (existingFeedback != null)
    {
        Console.WriteLine("✗ Esta tentativa já foi avaliada. Escolha outra tentativa.");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
        return;
    }

    // Select status
    EvaluationStatus? status = SelectEvaluationStatus();
    if (status == null)
    {
        Console.WriteLine("✗ Escolha inválida. Digite 1 ou 2.");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
        return;
    }

    // Get comment
    string? comment = GetCommentInput();
    if (comment == null)
    {
        Console.WriteLine("✗ Operação cancelada.");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
        return;
    }

    // Submit feedback
    try
    {
        var feedback = feedbackService.SubmitFeedback(attemptId.Value, comment, status.Value);
        Console.WriteLine($"\n✓ Feedback submetido com sucesso!");
        Console.WriteLine($"  ID: {feedback.Id}");
        Console.WriteLine($"  Status: {ConsoleHelper.TranslateStatus(feedback.Status)}");
        Console.WriteLine($"  Data: {ConsoleHelper.FormatDateTime(feedback.CreatedAt)}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n✗ Erro ao submeter feedback: {ex.Message}");
    }

    Console.WriteLine("\nPressione Enter para voltar ao menu...");
    Console.ReadLine();
}

static Dictionary<int, Guid> DisplayPendingAttempts(IMonitoriaService monitoriaService, IFeedbackService feedbackService)
{
    var attempts = monitoriaService.GetStudents()
        .SelectMany(s => monitoriaService.GetStudentProgress(s.Id)
            .Select(a => new { Attempt = a, Student = s }))
        .ToList();

    var exercises = monitoriaService.GetExercises();
    var mapping = new Dictionary<int, Guid>();
    int displayId = 1;

    Console.WriteLine("Tentativas pendentes:\n");

    foreach (var item in attempts)
    {
        // Skip if already has feedback
        var feedback = feedbackService.GetFeedbackByAttemptId(item.Attempt.Id);
        if (feedback != null)
        {
            continue;
        }

        var exercise = exercises.FirstOrDefault(e => e.Id == item.Attempt.ExerciseId);
        if (exercise != null)
        {
            mapping[displayId] = item.Attempt.Id;
            Console.WriteLine($"  {displayId}. Exercício: {exercise.Title} | Aluno: {item.Student.Name} | Data: {ConsoleHelper.FormatDateTime(item.Attempt.CompletedAt)}");
            displayId++;
        }
    }

    return mapping;
}

static Guid? SelectAttemptId(Dictionary<int, Guid> mapping)
{
    string? input = Console.ReadLine();

    if (!int.TryParse(input, out int selectedId))
    {
        return null;
    }

    return mapping.TryGetValue(selectedId, out Guid attemptId) ? attemptId : null;
}

static EvaluationStatus? SelectEvaluationStatus()
{
    Console.WriteLine("\nEscolha o status:");
    Console.WriteLine("  1. Aprovado");
    Console.WriteLine("  2. Precisa Revisão");
    Console.Write("Opção: ");

    string? input = Console.ReadLine();

    if (!int.TryParse(input, out int choice))
    {
        return null;
    }

    return choice switch
    {
        1 => EvaluationStatus.Approved,
        2 => EvaluationStatus.NeedsRevision,
        _ => null
    };
}

static string? GetCommentInput()
{
    Console.WriteLine("\nDigite seu comentário (10-5000 caracteres):");
    string? input = Console.ReadLine();

    if (!ConsoleHelper.ValidateCommentLength(input, out string validatedComment))
    {
        Console.WriteLine("✗ Comentário deve ter entre 10 e 5000 caracteres. Tente novamente.");
        return GetCommentInput(); // Retry
    }

    // Show truncation warning if applicable
    if (input != null && input.Length > 5000)
    {
        Console.WriteLine("⚠ Comentário foi truncado para 5000 caracteres.");
    }

    return validatedComment;
}

// ============================================
// User Story 3: Consulta de Feedbacks
// ============================================

static void HandleQueryByStudent(IMonitoriaService monitoriaService, IFeedbackService feedbackService)
{
    ConsoleHelper.DisplayHeader("Consultar Feedbacks por Aluno");

    // Display students and get mapping
    var studentMapping = DisplayStudentList(monitoriaService);

    if (studentMapping.Count == 0)
    {
        Console.WriteLine("✗ Nenhum aluno cadastrado no momento.");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
        return;
    }

    // Select student
    Console.Write("\nDigite o ID do aluno: ");
    Guid? studentId = SelectStudentId(studentMapping);

    if (studentId == null)
    {
        Console.WriteLine("✗ ID inválido. Escolha um número da lista.");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
        return;
    }

    // Get student name
    var student = monitoriaService.GetStudents().FirstOrDefault(s => s.Id == studentId.Value);
    if (student == null)
    {
        Console.WriteLine("✗ Aluno não encontrado.");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
        return;
    }

    // Get feedbacks for the student
    var feedbacks = feedbackService.GetFeedbackByStudentId(studentId.Value).ToList();

    if (feedbacks.Count == 0)
    {
        Console.WriteLine($"\n✗ Nenhum feedback encontrado para {student.Name}.");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
        return;
    }

    // Display feedbacks
    Console.WriteLine($"\nFeedbacks de {student.Name}:\n");
    DisplayFeedbackList(feedbacks, monitoriaService);

    Console.WriteLine("\nPressione Enter para voltar ao menu...");
    Console.ReadLine();
}

static void HandleQueryByStatus(IFeedbackService feedbackService)
{
    ConsoleHelper.DisplayHeader("Consultar Feedbacks por Status");

    // Select status
    Console.WriteLine("Escolha o status:");
    Console.WriteLine("  1. Aprovado");
    Console.WriteLine("  2. Precisa Revisão");
    Console.Write("Opção: ");

    EvaluationStatus? status = SelectEvaluationStatus();
    if (status == null)
    {
        Console.WriteLine("✗ Escolha inválida. Digite 1 ou 2.");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
        return;
    }

    // Get feedbacks by status
    var feedbacks = feedbackService.GetFeedbackByStatus(status.Value);

    if (feedbacks.Count == 0)
    {
        Console.WriteLine($"\n✗ Nenhum feedback encontrado com status '{ConsoleHelper.TranslateStatus(status.Value)}'.");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
        return;
    }

    // Display feedbacks (need to inject monitoriaService - using a workaround here)
    Console.WriteLine($"\nFeedbacks com status '{ConsoleHelper.TranslateStatus(status.Value)}':\n");
    foreach (var feedback in feedbacks)
    {
        Console.WriteLine($"  • ID: {feedback.Id} | Status: {ConsoleHelper.TranslateStatus(feedback.Status)} | Data: {ConsoleHelper.FormatDateTime(feedback.CreatedAt)}");
        Console.WriteLine($"    Comentário: {(feedback.Comment.Length > 80 ? feedback.Comment.Substring(0, 77) + "..." : feedback.Comment)}");
    }

    Console.WriteLine("\nPressione Enter para voltar ao menu...");
    Console.ReadLine();
}

static Dictionary<int, Guid> DisplayStudentList(IMonitoriaService monitoriaService)
{
    var students = monitoriaService.GetStudents();
    var mapping = new Dictionary<int, Guid>();

    Console.WriteLine("Alunos cadastrados:\n");

    int displayId = 1;
    foreach (var student in students)
    {
        mapping[displayId] = student.Id;
        Console.WriteLine($"  {displayId}. {student.Name} ({student.Email})");
        displayId++;
    }

    return mapping;
}

static Guid? SelectStudentId(Dictionary<int, Guid> mapping)
{
    string? input = Console.ReadLine();

    if (!int.TryParse(input, out int selectedId))
    {
        return null;
    }

    return mapping.TryGetValue(selectedId, out Guid studentId) ? studentId : null;
}

static void DisplayFeedbackList(List<Feedback> feedbacks, IMonitoriaService monitoriaService)
{
    var exercises = monitoriaService.GetExercises();

    foreach (var feedback in feedbacks)
    {
        // Find the attempt to get exercise info
        var students = monitoriaService.GetStudents();
        ExerciseAttempt? attempt = null;
        
        foreach (var student in students)
        {
            attempt = monitoriaService.GetStudentProgress(student.Id)
                .FirstOrDefault(a => a.Id == feedback.ExerciseAttemptId);
            if (attempt != null) break;
        }

        var exercise = attempt != null 
            ? exercises.FirstOrDefault(e => e.Id == attempt.ExerciseId) 
            : null;

        string exerciseName = exercise?.Title ?? "Exercício desconhecido";

        Console.WriteLine($"  • Exercício: {exerciseName}");
        Console.WriteLine($"    Status: {ConsoleHelper.TranslateStatus(feedback.Status)} | Data: {ConsoleHelper.FormatDateTime(feedback.CreatedAt)}");
        Console.WriteLine($"    Comentário: {feedback.Comment}");
        Console.WriteLine();
    }
}
