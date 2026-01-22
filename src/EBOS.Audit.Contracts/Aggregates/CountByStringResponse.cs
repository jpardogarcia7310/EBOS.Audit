namespace EBOS.Audit.Contracts.Aggregates;

public sealed record CountByStringResponse(
    string Key,
    int Count
);