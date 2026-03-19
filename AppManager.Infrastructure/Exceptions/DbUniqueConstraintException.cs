using AppManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient; // SQL Server
using Npgsql; // PostgreSQL


namespace AppManager.Infrastructure.Exceptions;

public sealed record UniqueConflictInfo(string? Field, string? ConstraintName);

public static class DbUniqueConstraintException
{
    public static bool IsUniqueConstraintViolation(DbUpdateException ex, out UniqueConflictInfo? info)
    {
        info = null;

        // SQL Server
        if (ex.InnerException is SqlException sqlEx)
        {
            // 2601: Cannot insert duplicate key row with unique index
            // 2627: Violation of UNIQUE KEY constraint
            if (sqlEx.Number is 2601 or 2627)
            {
                var constraint = TryExtractConstraintName(sqlEx.Message);
                var field = MapConstraintToField(constraint);
                info = new UniqueConflictInfo(field, constraint);
                return true;
            }
        }

        // PostgreSQL
        if (ex.InnerException is PostgresException pgEx)
        {
            // 23505: unique_violation
            if (pgEx.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                var constraint = pgEx.ConstraintName;
                var field = MapConstraintToField(constraint);
                info = new UniqueConflictInfo(field, constraint);
                return true;
            }
        }

        return false;
    }


    public static string? TryExtractConstraintName(string message)
    {
        var tokens = new[] { "constraint", "index", "unique index", "UNIQUE constraint", "UNIQUE KEY constraint" };
        foreach (var t in tokens)
        {
            var i = message.IndexOf(t, StringComparison.OrdinalIgnoreCase);
            if (i >= 0)
            {
                var segment = message[(i)..];
                var startQuote = segment.IndexOf('"');
                var endQuote = segment.IndexOf('"', startQuote + 1);
                if (startQuote >= 0 && endQuote > startQuote)
                    return segment.Substring(startQuote + 1, endQuote - startQuote - 1);
            }
        }

        return null;
    }

    public static string? MapConstraintToField(string? constraintName)
    {
        if (string.IsNullOrEmpty(constraintName)) return null;
        if (constraintName.Contains("Name", StringComparison.OrdinalIgnoreCase)) return nameof(Tenant.Name);
        if (constraintName.Contains("Slug", StringComparison.OrdinalIgnoreCase)) return nameof(Tenant.Slug);
        return null;
    }
}