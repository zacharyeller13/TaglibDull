using TaglibDull.Frame;

namespace TaglibDull.Tests;

public class TestFrameHeader
{
    public static IEnumerable<object[]> FormatExceptionData =>
    [
        [Array.Empty<byte>(), typeof(FormatException), "Not enough bytes to determine frame header"],
        ["TEST0000000"u8.ToArray(), typeof(FormatException), "Read non-standard or unsupported frame type TEST"],
    ];
    
    
    [Theory]
    [MemberData(nameof(FormatExceptionData))]
    public void TestInvalidData_ThrowsFormatException(byte[] data, Type exceptionType, string exceptionMessage)
    {
        var exception = Assert.Throws(exceptionType, () => new FrameHeader(data));
        Assert.Equal(exceptionMessage ,exception.Message);
    }
    
    // TODO: Happy path tests
}