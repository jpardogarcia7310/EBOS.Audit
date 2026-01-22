namespace EBOS.Audit.Contracts.Aggregates;

public sealed record CountByDateResponse(
    DateTime Date,
    int Count
);