using Xunit;
using MonitoriaBase.Helpers;
using MonitoriaBase.Models;

namespace MonitoriaBase.Tests;

/// <summary>
/// Unit tests for ConsoleHelper utility methods.
/// Following TDD: tests written first, implementation after.
/// </summary>
public class ConsoleHelperTests
{
    #region TryParseMenuOption Tests (User Story 1)

    [Fact]
    public void TryParseMenuOption_ValidNumber_ReturnsTrue()
    {
        // Arrange
        string input = "2";
        int min = 0;
        int max = 3;

        // Act
        bool result = ConsoleHelper.TryParseMenuOption(input, min, max, out int option);

        // Assert
        Assert.True(result);
        Assert.Equal(2, option);
    }

    [Fact]
    public void TryParseMenuOption_InvalidNumber_ReturnsFalse()
    {
        // Arrange
        string input = "abc";
        int min = 0;
        int max = 3;

        // Act
        bool result = ConsoleHelper.TryParseMenuOption(input, min, max, out int option);

        // Assert
        Assert.False(result);
        Assert.Equal(0, option);
    }

    [Fact]
    public void TryParseMenuOption_OutOfRange_ReturnsFalse()
    {
        // Arrange
        string input = "99";
        int min = 0;
        int max = 3;

        // Act
        bool result = ConsoleHelper.TryParseMenuOption(input, min, max, out int option);

        // Assert
        Assert.False(result);
        Assert.Equal(0, option);
    }

    [Fact]
    public void TryParseMenuOption_EmptyInput_ReturnsFalse()
    {
        // Arrange
        string? input = "";
        int min = 0;
        int max = 3;

        // Act
        bool result = ConsoleHelper.TryParseMenuOption(input, min, max, out int option);

        // Assert
        Assert.False(result);
        Assert.Equal(0, option);
    }

    #endregion

    #region ValidateCommentLength Tests (User Story 2)

    [Fact]
    public void ValidateCommentLength_ValidLength_ReturnsTrue()
    {
        // Arrange
        string comment = "Este é um comentário válido com mais de 10 caracteres.";

        // Act
        bool result = ConsoleHelper.ValidateCommentLength(comment, out string validatedComment);

        // Assert
        Assert.True(result);
        Assert.Equal(comment, validatedComment);
    }

    [Fact]
    public void ValidateCommentLength_TooShort_ReturnsFalse()
    {
        // Arrange
        string comment = "Curto";

        // Act
        bool result = ConsoleHelper.ValidateCommentLength(comment, out string validatedComment);

        // Assert
        Assert.False(result);
        Assert.Empty(validatedComment);
    }

    [Fact]
    public void ValidateCommentLength_TooLong_TruncatesAndReturnsTrue()
    {
        // Arrange
        string comment = new string('A', 6000); // 6000 caracteres

        // Act
        bool result = ConsoleHelper.ValidateCommentLength(comment, out string validatedComment);

        // Assert
        Assert.True(result);
        Assert.Equal(5000, validatedComment.Length);
        Assert.Equal(comment.Substring(0, 5000), validatedComment);
    }

    [Fact]
    public void ValidateCommentLength_EmptyInput_ReturnsFalse()
    {
        // Arrange
        string? comment = "";

        // Act
        bool result = ConsoleHelper.ValidateCommentLength(comment, out string validatedComment);

        // Assert
        Assert.False(result);
        Assert.Empty(validatedComment);
    }

    #endregion

    #region TranslateStatus Tests (User Story 2)

    [Fact]
    public void TranslateStatus_Approved_ReturnsAprovado()
    {
        // Arrange
        var status = EvaluationStatus.Approved;

        // Act
        string result = ConsoleHelper.TranslateStatus(status);

        // Assert
        Assert.Equal("Aprovado", result);
    }

    [Fact]
    public void TranslateStatus_NeedsRevision_ReturnsPrecisaRevisao()
    {
        // Arrange
        var status = EvaluationStatus.NeedsRevision;

        // Act
        string result = ConsoleHelper.TranslateStatus(status);

        // Assert
        Assert.Equal("Precisa Revisão", result);
    }

    #endregion
    #region FormatDateTime Tests (User Story 3)

    [Fact]
    public void FormatDateTime_ValidDate_ReturnsFormattedString()
    {
        // Arrange
        var dateTime = new DateTime(2024, 12, 25, 14, 30, 0);

        // Act
        var result = ConsoleHelper.FormatDateTime(dateTime);

        // Assert
        Assert.Equal("25/12/2024 14:30", result);
    }

    #endregion
}
