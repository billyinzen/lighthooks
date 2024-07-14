using Hooks.Common.Enums.Abstract;
using Microsoft.Extensions.Options;

namespace Hooks.Common.Enums;

/// <summary>
/// Values available 
/// </summary>
public class EntityType : BaseEnum<string, string> 
{
    public static readonly EntityType Property = new("property");
    
    public static readonly EntityType Contact = new("contact");

    private static readonly IEnumerable<EntityType> Values =
    [
        Property, 
        Contact
    ];

    private EntityType(string value) : base(value, value)
    {
    }

    public static implicit operator string(EntityType entityType)
        => entityType.Value;

    public static bool TryGetValue(string? value, out EntityType? entityType)
    {
        entityType = null;
        
        // If there are no entity types with this name, return false
        var lookupFunc = (EntityType v) => v.Value.Equals(value);
        if (string.IsNullOrWhiteSpace(value) || !Values.Any(lookupFunc))
            return false;
        
        // Otherwise update the entityType and return true
        entityType = Values.SingleOrDefault(lookupFunc);
        return true;
    }
}