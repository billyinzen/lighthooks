using Hooks.Common.Helpers;

namespace Hooks.Common.Tests.Helpers;

public class DateTimeOffsetProviderTests
{
    [Fact]
    public void Now_ReturnsCurrentTime_WhenNoContextConfigured()
    {
        DateTimeOffsetProvider.Now.Should().BeCloseTo(DateTimeOffset.Now, TimeSpan.FromMilliseconds(1));
    }
    
    [Fact]
    public void Now_ReturnsConfiguredTime_WhenContextConfigured()
    {
        var fixedTimestamp = new DateTimeOffset(2016, 4, 16, 7, 53, 14, TimeSpan.FromHours(-5));
        using var ambientContext = new DateTimeOffsetProviderContext(fixedTimestamp);
        DateTimeOffsetProvider.Now.Should().Be(fixedTimestamp);
    }   
}