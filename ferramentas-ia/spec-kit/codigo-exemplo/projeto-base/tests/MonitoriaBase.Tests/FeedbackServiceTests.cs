using MonitoriaBase.Models;
using MonitoriaBase.Services;
using Xunit;

namespace MonitoriaBase.Tests;

/// <summary>
/// Tests for FeedbackService following TDD principles.
/// Tests are written BEFORE implementation to define expected behavior.
/// </summary>
public class FeedbackServiceTests
{
    [Fact]
    public void SubmitFeedback_WithValidDataAndApprovedStatus_CreatesFeedbackAndUpdatesAttemptStatus()
    {
        // Arrange
        var monitoriaService = new MonitoriaService();
        var feedbackService = new FeedbackService(monitoriaService);
        
        var student = monitoriaService.RegisterStudent("Alice", "alice@example.com");
        var exercises = monitoriaService.GetExercises();
        var attempt = monitoriaService.MarkExerciseAsCompleted(student.Id, exercises[0].Id);
        
        var comment = "Excellent application of SOLID principles and interface isolation!";
        
        // Act
        var feedback = feedbackService.SubmitFeedback(attempt.Id, comment, EvaluationStatus.Approved);
        
        // Assert
        Assert.NotNull(feedback);
        Assert.NotEqual(Guid.Empty, feedback.Id);
        Assert.Equal(attempt.Id, feedback.ExerciseAttemptId);
        Assert.Equal(comment, feedback.Comment);
        Assert.Equal(EvaluationStatus.Approved, feedback.Status);
        Assert.True(feedback.CreatedAt <= DateTime.UtcNow);
        
        var updatedAttempt = monitoriaService.GetExerciseAttempt(attempt.Id);
        Assert.NotNull(updatedAttempt);
        Assert.Equal("Approved", updatedAttempt.Status);
    }

    [Fact]
    public void SubmitFeedback_WithValidDataAndNeedsRevisionStatus_CreatesFeedbackAndUpdatesAttemptStatus()
    {
        // Arrange
        var monitoriaService = new MonitoriaService();
        var feedbackService = new FeedbackService(monitoriaService);
        
        var student = monitoriaService.RegisterStudent("Bob", "bob@example.com");
        var exercises = monitoriaService.GetExercises();
        var attempt = monitoriaService.MarkExerciseAsCompleted(student.Id, exercises[0].Id);
        
        var comment = "Code has tight coupling; use constructor dependency injection instead.";
        
        // Act
        var feedback = feedbackService.SubmitFeedback(attempt.Id, comment, EvaluationStatus.NeedsRevision);
        
        // Assert
        Assert.NotNull(feedback);
        Assert.Equal(attempt.Id, feedback.ExerciseAttemptId);
        Assert.Equal(comment, feedback.Comment);
        Assert.Equal(EvaluationStatus.NeedsRevision, feedback.Status);
        
        var updatedAttempt = monitoriaService.GetExerciseAttempt(attempt.Id);
        Assert.NotNull(updatedAttempt);
        Assert.Equal("NeedsRevision", updatedAttempt.Status);
    }

    [Fact]
    public void SubmitFeedback_WithNonExistentAttemptId_ThrowsArgumentException()
    {
        // Arrange
        var monitoriaService = new MonitoriaService();
        var feedbackService = new FeedbackService(monitoriaService);
        var nonExistentAttemptId = Guid.NewGuid();
        var comment = "This is a valid comment with more than 10 characters.";
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            feedbackService.SubmitFeedback(nonExistentAttemptId, comment, EvaluationStatus.Approved));
        
        Assert.Contains("ExerciseAttempt", exception.Message);
    }

    [Fact]
    public void SubmitFeedback_WithCommentTooShort_ThrowsArgumentException()
    {
        // Arrange
        var monitoriaService = new MonitoriaService();
        var feedbackService = new FeedbackService(monitoriaService);
        
        var student = monitoriaService.RegisterStudent("Charlie", "charlie@example.com");
        var exercises = monitoriaService.GetExercises();
        var attempt = monitoriaService.MarkExerciseAsCompleted(student.Id, exercises[0].Id);
        
        var shortComment = "Too short"; // 9 characters
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            feedbackService.SubmitFeedback(attempt.Id, shortComment, EvaluationStatus.Approved));
        
        Assert.Contains("10 and 5000 characters", exception.Message);
    }

    [Fact]
    public void SubmitFeedback_WithCommentTooLong_ThrowsArgumentException()
    {
        // Arrange
        var monitoriaService = new MonitoriaService();
        var feedbackService = new FeedbackService(monitoriaService);
        
        var student = monitoriaService.RegisterStudent("Diana", "diana@example.com");
        var exercises = monitoriaService.GetExercises();
        var attempt = monitoriaService.MarkExerciseAsCompleted(student.Id, exercises[0].Id);
        
        var longComment = new string('x', 5001); // 5001 characters
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            feedbackService.SubmitFeedback(attempt.Id, longComment, EvaluationStatus.Approved));
        
        Assert.Contains("10 and 5000 characters", exception.Message);
    }

    [Fact]
    public void GetFeedbackByAttemptId_WithExistingFeedback_ReturnsFeedback()
    {
        // Arrange
        var monitoriaService = new MonitoriaService();
        var feedbackService = new FeedbackService(monitoriaService);
        
        var student = monitoriaService.RegisterStudent("Eve", "eve@example.com");
        var exercises = monitoriaService.GetExercises();
        var attempt = monitoriaService.MarkExerciseAsCompleted(student.Id, exercises[0].Id);
        
        var comment = "Great work on implementing the observer pattern correctly!";
        var submittedFeedback = feedbackService.SubmitFeedback(attempt.Id, comment, EvaluationStatus.Approved);
        
        // Act
        var retrievedFeedback = feedbackService.GetFeedbackByAttemptId(attempt.Id);
        
        // Assert
        Assert.NotNull(retrievedFeedback);
        Assert.Equal(submittedFeedback.Id, retrievedFeedback.Id);
        Assert.Equal(submittedFeedback.Comment, retrievedFeedback.Comment);
        Assert.Equal(submittedFeedback.Status, retrievedFeedback.Status);
    }

    [Fact]
    public void GetFeedbackByAttemptId_WithNonExistentAttempt_ReturnsNull()
    {
        // Arrange
        var monitoriaService = new MonitoriaService();
        var feedbackService = new FeedbackService(monitoriaService);
        var nonExistentAttemptId = Guid.NewGuid();
        
        // Act
        var feedback = feedbackService.GetFeedbackByAttemptId(nonExistentAttemptId);
        
        // Assert
        Assert.Null(feedback);
    }

    // User Story 2 Tests - Student views received feedback

    [Fact]
    public void GetFeedbackByStudentId_WithMultipleFeedbacks_ReturnsAllFeedbacks()
    {
        // Arrange
        var monitoriaService = new MonitoriaService();
        var feedbackService = new FeedbackService(monitoriaService);
        
        var student = monitoriaService.RegisterStudent("Frank", "frank@example.com");
        var exercises = monitoriaService.GetExercises();
        var attempt1 = monitoriaService.MarkExerciseAsCompleted(student.Id, exercises[0].Id);
        var attempt2 = monitoriaService.MarkExerciseAsCompleted(student.Id, exercises[1].Id);
        
        var feedback1 = feedbackService.SubmitFeedback(attempt1.Id, "Great work on the first exercise!", EvaluationStatus.Approved);
        var feedback2 = feedbackService.SubmitFeedback(attempt2.Id, "Needs improvement on error handling.", EvaluationStatus.NeedsRevision);
        
        // Act
        var studentFeedbacks = feedbackService.GetFeedbackByStudentId(student.Id);
        
        // Assert
        Assert.Equal(2, studentFeedbacks.Count);
        Assert.Contains(studentFeedbacks, f => f.Id == feedback1.Id);
        Assert.Contains(studentFeedbacks, f => f.Id == feedback2.Id);
    }

    [Fact]
    public void GetFeedbackByStudentId_WithNoFeedbacks_ReturnsEmptyList()
    {
        // Arrange
        var monitoriaService = new MonitoriaService();
        var feedbackService = new FeedbackService(monitoriaService);
        
        var student = monitoriaService.RegisterStudent("Grace", "grace@example.com");
        
        // Act
        var studentFeedbacks = feedbackService.GetFeedbackByStudentId(student.Id);
        
        // Assert
        Assert.Empty(studentFeedbacks);
    }

    [Fact]
    public void GetFeedbackByStudentId_WithNonExistentStudent_ReturnsEmptyList()
    {
        // Arrange
        var monitoriaService = new MonitoriaService();
        var feedbackService = new FeedbackService(monitoriaService);
        var nonExistentStudentId = Guid.NewGuid();
        
        // Act
        var studentFeedbacks = feedbackService.GetFeedbackByStudentId(nonExistentStudentId);
        
        // Assert
        Assert.Empty(studentFeedbacks);
    }

    [Fact]
    public void GetFeedbackByStatus_WithApprovedStatus_ReturnsOnlyApprovedFeedbacks()
    {
        // Arrange
        var monitoriaService = new MonitoriaService();
        var feedbackService = new FeedbackService(monitoriaService);
        
        var student1 = monitoriaService.RegisterStudent("Henry", "henry@example.com");
        var student2 = monitoriaService.RegisterStudent("Iris", "iris@example.com");
        var exercises = monitoriaService.GetExercises();
        
        var attempt1 = monitoriaService.MarkExerciseAsCompleted(student1.Id, exercises[0].Id);
        var attempt2 = monitoriaService.MarkExerciseAsCompleted(student2.Id, exercises[1].Id);
        var attempt3 = monitoriaService.MarkExerciseAsCompleted(student1.Id, exercises[2].Id);
        
        feedbackService.SubmitFeedback(attempt1.Id, "Excellent work!", EvaluationStatus.Approved);
        feedbackService.SubmitFeedback(attempt2.Id, "Needs revision on error handling.", EvaluationStatus.NeedsRevision);
        feedbackService.SubmitFeedback(attempt3.Id, "Perfect implementation!", EvaluationStatus.Approved);
        
        // Act
        var approvedFeedbacks = feedbackService.GetFeedbackByStatus(EvaluationStatus.Approved);
        
        // Assert
        Assert.Equal(2, approvedFeedbacks.Count);
        Assert.All(approvedFeedbacks, f => Assert.Equal(EvaluationStatus.Approved, f.Status));
    }

    [Fact]
    public void GetFeedbackByStatus_WithNeedsRevisionStatus_ReturnsOnlyNeedsRevisionFeedbacks()
    {
        // Arrange
        var monitoriaService = new MonitoriaService();
        var feedbackService = new FeedbackService(monitoriaService);
        
        var student1 = monitoriaService.RegisterStudent("Jack", "jack@example.com");
        var student2 = monitoriaService.RegisterStudent("Kate", "kate@example.com");
        var exercises = monitoriaService.GetExercises();
        
        var attempt1 = monitoriaService.MarkExerciseAsCompleted(student1.Id, exercises[0].Id);
        var attempt2 = monitoriaService.MarkExerciseAsCompleted(student2.Id, exercises[1].Id);
        var attempt3 = monitoriaService.MarkExerciseAsCompleted(student1.Id, exercises[2].Id);
        
        feedbackService.SubmitFeedback(attempt1.Id, "Needs better error handling.", EvaluationStatus.NeedsRevision);
        feedbackService.SubmitFeedback(attempt2.Id, "Great job!", EvaluationStatus.Approved);
        feedbackService.SubmitFeedback(attempt3.Id, "Please review SOLID principles.", EvaluationStatus.NeedsRevision);
        
        // Act
        var needsRevisionFeedbacks = feedbackService.GetFeedbackByStatus(EvaluationStatus.NeedsRevision);
        
        // Assert
        Assert.Equal(2, needsRevisionFeedbacks.Count);
        Assert.All(needsRevisionFeedbacks, f => Assert.Equal(EvaluationStatus.NeedsRevision, f.Status));
    }

    [Fact]
    public void GetFeedbackByStatus_WithNoMatchingFeedbacks_ReturnsEmptyList()
    {
        // Arrange
        var monitoriaService = new MonitoriaService();
        var feedbackService = new FeedbackService(monitoriaService);
        
        var student = monitoriaService.RegisterStudent("Leo", "leo@example.com");
        var exercises = monitoriaService.GetExercises();
        var attempt = monitoriaService.MarkExerciseAsCompleted(student.Id, exercises[0].Id);
        
        feedbackService.SubmitFeedback(attempt.Id, "Excellent work!", EvaluationStatus.Approved);
        
        // Act
        var needsRevisionFeedbacks = feedbackService.GetFeedbackByStatus(EvaluationStatus.NeedsRevision);
        
        // Assert
        Assert.Empty(needsRevisionFeedbacks);
    }
}
