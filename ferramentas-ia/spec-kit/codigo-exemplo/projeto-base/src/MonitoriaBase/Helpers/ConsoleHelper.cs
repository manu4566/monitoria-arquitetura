using MonitoriaBase.Models;

namespace MonitoriaBase.Helpers;

/// <summary>
/// Static utility class providing console helper methods for parsing, validation, and formatting.
/// </summary>
public static class ConsoleHelper
{
    /// <summary>
    /// Tries to parse a menu option from user input.
    /// </summary>
    /// <param name="input">The user input string.</param>
    /// <param name="min">The minimum valid option value.</param>
    /// <param name="max">The maximum valid option value.</param>
    /// <param name="option">When this method returns, contains the parsed option if successful; otherwise, 0.</param>
    /// <returns><c>true</c> if the input was successfully parsed and is within range; otherwise, <c>false</c>.</returns>
    public static bool TryParseMenuOption(string? input, int min, int max, out int option)
    {
        option = 0;

        // Guard clause: null or empty input
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        // Try parse integer
        if (!int.TryParse(input, out int parsedValue))
        {
            return false;
        }

        // Validate range
        if (parsedValue < min || parsedValue > max)
        {
            return false;
        }

        option = parsedValue;
        return true;
    }

    /// <summary>
    /// Displays a formatted header with title.
    /// </summary>
    /// <param name="title">The title to display.</param>
    public static void DisplayHeader(string title)
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine($"  {title}");
        Console.WriteLine("========================================");
        Console.WriteLine();
    }

    /// <summary>
    /// Validates comment length and truncates if necessary.
    /// </summary>
    /// <param name="comment">The input comment to validate.</param>
    /// <param name="validatedComment">When this method returns, contains the validated (and possibly truncated) comment.</param>
    /// <returns><c>true</c> if the comment meets length requirements (10-5000 chars); otherwise, <c>false</c>.</returns>
    public static bool ValidateCommentLength(string? comment, out string validatedComment)
    {
        validatedComment = string.Empty;

        // Guard clause: null or whitespace
        if (string.IsNullOrWhiteSpace(comment))
        {
            return false;
        }

        // Trim whitespace
        comment = comment.Trim();

        // Check minimum length
        if (comment.Length < 10)
        {
            return false;
        }

        // Truncate if exceeds maximum length
        if (comment.Length > 5000)
        {
            validatedComment = comment.Substring(0, 5000);
            return true;
        }

        validatedComment = comment;
        return true;
    }

    /// <summary>
    /// Translates evaluation status enum to Portuguese string.
    /// </summary>
    /// <param name="status">The evaluation status.</param>
    /// <returns>Portuguese translation: "Aprovado" or "Precisa Revisão".</returns>
    public static string TranslateStatus(EvaluationStatus status)
    {
        return status switch
        {
            EvaluationStatus.Approved => "Aprovado",
            EvaluationStatus.NeedsRevision => "Precisa Revisão",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Status inválido")
        };
    }

    /// <summary>
    /// Formats a DateTime to Brazilian format.
    /// </summary>
    /// <param name="dateTime">The date/time to format.</param>
    /// <returns>Formatted string in "dd/MM/yyyy HH:mm" format.</returns>
    public static string FormatDateTime(DateTime dateTime)
    {
        return dateTime.ToString("dd/MM/yyyy HH:mm");
    }
}
