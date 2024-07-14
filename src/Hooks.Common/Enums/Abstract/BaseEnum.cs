namespace Hooks.Common.Enums.Abstract;

public abstract class BaseEnum<TKey, TValue>
{
    protected TKey Key { get; private init; }

    protected TValue Value { get; private init; }
    
    protected BaseEnum(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}