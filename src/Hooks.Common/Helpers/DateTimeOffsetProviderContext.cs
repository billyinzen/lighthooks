using System.Collections;

namespace Hooks.Common.Helpers;

/// <summary>Execution context for the <see cref="DateTimeOffsetProvider"/> class</summary>
public class DateTimeOffsetProviderContext : IDisposable
{
    internal DateTimeOffset Timestamp;
    private static readonly ThreadLocal<Stack> ThreadScopeStack = new (() => new Stack());
    private Stack _contextStack = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeOffsetProviderContext"/> class
    /// </summary>
    /// <param name="timestamp"></param>
    public DateTimeOffsetProviderContext(DateTimeOffset timestamp)
    {
        Timestamp = timestamp;
        ThreadScopeStack.Value?.Push(this);
    }

    /// <summary>
    /// The timestamp configured for the current execution context
    /// </summary>
    public static DateTimeOffsetProviderContext? Current
    {
        get
        {
            if ((ThreadScopeStack.Value?.Count ?? 0) == 0)
                return null;
            
            return ThreadScopeStack.Value?.Peek() as DateTimeOffsetProviderContext;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        ThreadScopeStack.Value?.Pop();
        GC.SuppressFinalize(this);
    }
}