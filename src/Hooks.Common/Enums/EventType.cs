using Hooks.Common.Enums.Abstract;

namespace Hooks.Common.Enums;

/// <summary>
/// Values available 
/// </summary>
public class EventType : BaseEnum<string, string>
{
    public static readonly EventType Created = new("created");
    public static readonly EventType Modified = new("modified");
    public static readonly EventType Deleted = new("deleted");
    public static readonly EventType Archived = new("archived");
    public static readonly EventType Restored = new("restored");

    private static readonly IEnumerable<EventType> Values = [
        Created,
        Modified,
        Deleted,
        Archived,
        Restored
    ];
    
    private EventType(string value) : base(value, value)
    {
    }

    public static implicit operator string(EventType entityType)
        => entityType.Value;
    
    public static bool TryGetValue(string? value, out EventType? entityType)
    {
        entityType = null;
        
        // If there are no entity types with this name, return false
        var lookupFunc = (EventType v) => v.Value.Equals(value);
        if (string.IsNullOrWhiteSpace(value) || !Values.Any(lookupFunc))
            return false;
        
        // Otherwise update the entityType and return true
        entityType = Values.SingleOrDefault(lookupFunc);
        return true;
    }
}