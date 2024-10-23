using System.Text;
using TaglibDull.Frame;
using Xunit.Abstractions;

namespace TaglibDull.Tests;

public class TestTextInformationFrames
{
    [Theory]
    [MemberData(nameof(FormatExceptionData))]
    public void TestInvalidData_ThrowsFormatException(byte[] data, Type exceptionType, string exceptionMessage)
    {
        var exception = Assert.Throws(exceptionType, () => new TextInformationFrame(data));
        Assert.Equal(exceptionMessage, exception.Message);
    }

    // TODO: Happy path tests
    [Fact]
    public void TestValidData()
    {
        byte[] data = GenerateBytes(
            frameType: FrameType.TIT2, // TIT2
            size: [0, 0, 0, 41], // 41 bytes
            0, 0, // flags
            0x01, 0xFF, 0xFE, // UTF16 BigEndian
            67, 0, 104, 0, 97, 0, 110, 0, 103, 0, 101, 0, 115, 0, 32, 0, 40, 0, 111, 0, 114, 0, 105, 0, 103, 0, 105,
            0, 110, 0, 97, 0, 108, 0, 41, 0, // Changes (original)
            0x00, 0x00// Null Terminator for UTF16
        );

        TextInformationFrame tit2 = new TextInformationFrame(data);
        Assert.Equal(0x01, tit2.TextEncoding);
        Assert.True(tit2.UnicodeBOM.SequenceEqual<byte>([0xFF, 0xFE]));
        Assert.Equal("Changes (original)\0", Encoding.Unicode.GetString(tit2.Information));
    }


    private static byte[] GenerateBytes(ReadOnlySpan<byte> frameType, byte[]? size, params byte[] additionalBytes)
    {
        List<byte> bytes = [..frameType.ToArray(), ..(size ?? [0, 0, 0, 0])];
        bytes.AddRange(additionalBytes);
        return bytes.ToArray();
    }

    public static IEnumerable<object[]> FormatExceptionData =>
    [
        ["LINK0000000"u8.ToArray(), typeof(FormatException), "FrameType LINK is not a valid Text Information Frame"],
        [
            GenerateBytes(FrameType.TIT2, size: null, 0x02), typeof(FormatException),
            "Text encoding must be either 0x00 (ISO-8859-1) or 0x01 (Unicode)"
        ]
    ];
}