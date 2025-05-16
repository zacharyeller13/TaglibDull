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
        Assert.Equal<object>(exceptionMessage, exception.Message);
    }

    [Fact]
    public void TestValidData()
    {
        byte[] data = ByteArrayHelpers.GenerateBytes(
            frameType: FrameType.TIT2, // TIT2
            size: [0, 0, 0, 41], // 41 bytes
            flags: [0, 0], // flags
            0x01, 0xFF, 0xFE, // UTF16 LittleEndian
            67, 0, 104, 0, 97, 0, 110, 0, 103, 0, 101, 0, 115, 0, 32, 0, 40, 0, 111, 0, 114, 0, 105, 0, 103, 0, 105,
            0, 110, 0, 97, 0, 108, 0, 41, 0, // Changes (original)
            0x00, 0x00 // Null Terminator for UTF16
        );

        TextInformationFrame tit2 = new TextInformationFrame(data);
        Assert.Equal(0x01, tit2.TextEncoding);
        Assert.True(tit2.UnicodeBOM.SequenceEqual<byte>([0xFF, 0xFE]));
        Assert.Equal("Changes (original)\0", Encoding.Unicode.GetString(tit2.Information));
    }

    [Fact]
    public void TestValidUserData()
    {
        byte[] data = ByteArrayHelpers.GenerateBytes(
            frameType: FrameType.TXXX,
            size: [0, 0, 0, 59],
            flags: [0, 0],
            0x01, 0xFF, 0xFE, // UTF16 LittleEndian
            116, 0, 101, 0, 115, 0, 116, 0, 32, 0, 100, 0, 101, 0, 115, 0, 99, 0, 114, 0, 105, 0, 112, 0, 116, 0, 105,
            0, 111, 0, 110, 0, // "test description"
            0x00, 0x00, // Null terminated Description
            116, 0, 101, 0, 115, 0, 116, 0, 32, 0, 118, 0, 97, 0, 108, 0, 117, 0, 101, 0, // "test value"
            0x00, 0x00 // Null terminated for UTF16
        );

        UserTextInformationFrame txxx = new UserTextInformationFrame(data);
        Assert.Equal(0x01, txxx.TextEncoding);
        Assert.True(txxx.UnicodeBOM.SequenceEqual<byte>([0xFF, 0xFE]));
        Assert.Equal("test description\0", Encoding.Unicode.GetString(txxx.Description));
        Assert.Equal("test value\0", Encoding.Unicode.GetString(txxx.Value));
    }

    public static IEnumerable<object[]> FormatExceptionData =>
    [
        ["LINK0000000"u8.ToArray(), typeof(FormatException), "FrameType LINK is not a valid Text Information Frame"],
        [
            ByteArrayHelpers.GenerateBytes(FrameType.TIT2, size: null, flags: null, 0x02), typeof(FormatException),
            "Text encoding must be either 0x00 (ISO-8859-1) or 0x01 (Unicode)"
        ]
    ];
}