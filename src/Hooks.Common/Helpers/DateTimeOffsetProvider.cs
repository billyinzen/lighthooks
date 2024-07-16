namespace Hooks.Common.Helpers;

/// <summary>DateTime provider allowing fixture in test classes.</summary>
public class DateTimeOffsetProvider
{
    /// <inheritdoc cref="DateTimeOffset.Now"/>
    public static DateTimeOffset Now
        => DateTimeOffsetProviderContext.Current?.Timestamp ?? DateTimeOffset.Now;
}