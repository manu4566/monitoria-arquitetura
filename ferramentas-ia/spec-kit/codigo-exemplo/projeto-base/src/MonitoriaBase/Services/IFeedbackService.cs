using MonitoriaBase.Models;

namespace MonitoriaBase.Services;

/// <summary>
/// Contrato de Interface para o serviço de Avaliação e Feedback.
/// Define os métodos de negócio necessários para submeter, validar e pesquisar feedbacks.
/// </summary>
public interface IFeedbackService
{
    /// <summary>
    /// Submete um novo feedback para uma tentativa de exercício concluída.
    /// Valida as regras de negócio de tamanho de comentário e status de tentativa.
    /// Atualiza o status do respectivo ExerciseAttempt.
    /// </summary>
    /// <param name="exerciseAttemptId">ID único da tentativa de exercício</param>
    /// <param name="comment">Comentário descritivo com críticas e orientações de 10 a 5000 caracteres</param>
    /// <param name="status">Enumeração do status de avaliação (Approved ou NeedsRevision)</param>
    /// <returns>A entidade Feedback criada e populada com ID e data de criação</returns>
    Feedback SubmitFeedback(Guid exerciseAttemptId, string comment, EvaluationStatus status);

    /// <summary>
    /// Recupera um feedback de uma tentativa específica de exercício.
    /// Retorna null caso a tentativa especificada ainda não tenha sido avaliada.
    /// </summary>
    /// <param name="exerciseAttemptId">ID único da tentativa de exercício</param>
    /// <returns>O Feedback correspondente ou null caso não encontrado</returns>
    Feedback? GetFeedbackByAttemptId(Guid exerciseAttemptId);

    /// <summary>
    /// Recupera todo o histórico de feedbacks recebidos por um estudante específico.
    /// Retorna uma lista vazia caso o aluno não tenha tentativas com feedbacks cadastrados.
    /// </summary>
    /// <param name="studentId">ID único do estudante</param>
    /// <returns>Lista somente-leitura contendo todos os feedbacks deste estudante</returns>
    IReadOnlyList<Feedback> GetFeedbackByStudentId(Guid studentId);

    /// <summary>
    /// Filtra feedbacks globalmente do sistema com base em um status de avaliação específico.
    /// </summary>
    /// <param name="status">O status de filtragem (Approved ou NeedsRevision)</param>
    /// <returns>Lista somente-leitura dos feedbacks que combinam com o status desejado</returns>
    IReadOnlyList<Feedback> GetFeedbackByStatus(EvaluationStatus status);
}
