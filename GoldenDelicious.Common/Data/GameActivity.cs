namespace GoldenDelicious.Common.Data;

public record GameActivity(
    ulong Id, 
    string UserName, 
    string UserDisplayName,
    ulong UserId, 
    ulong ServerId, 
    string GameName,
    DateTime StartTime, 
    DateTime? EndTime);