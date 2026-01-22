namespace EBOS.Audit.Contracts.Aggregates;

public sealed record CountByMonthResponse(
    int Year,
    int Month,
    int Count
);