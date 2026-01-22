namespace EBOS.Audit.Infrastructure.Options;

public sealed class AuditRetentionOptions
{
    public int ActivityDays { get; set; }
    public int ChangeDays { get; set; }
    public int EventDays { get; set; }
    public int BatchSize { get; set; } = 5000;
    public bool Enabled { get; set; } = true;
    public int RunAtHour { get; set; } = 3;
}