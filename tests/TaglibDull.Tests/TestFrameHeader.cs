using TaglibDull.Frame;

namespace TaglibDull.Tests;

public class TestFrameHeader
{
    [Theory]
    [MemberData(nameof(FormatExceptionData))]
    public void TestInvalid_FrameHeaderData_ThrowsFormatException(byte[] data, Type exceptionType,
        string exceptionMessage)
    {
        var exception = Assert.Throws(exceptionType, () => new FrameHeader(data));
        Assert.Equal(exceptionMessage, exception.Message);
    }

    [Theory]
    [MemberData(nameof(FrameHeaderData))]
    public void TestValid_FrameHeaderData(byte[] data, byte[] expectedFrameId, uint expectedSize, byte[] expectedFlags)
    {
        FrameHeader header = new FrameHeader(data);

        Assert.True(expectedFrameId.AsSpan().SequenceEqual(header.FrameId));
        Assert.Equal(expectedSize, header.Size);
        Assert.True(expectedFlags.AsSpan().SequenceEqual(header.Flags));
    }

    public static IEnumerable<object[]> FormatExceptionData =>
    [
        [Array.Empty<byte>(), typeof(FormatException), "Not enough bytes to determine frame header. Needed 10, got 0"],
        ["TEST0000000"u8.ToArray(), typeof(FormatException), "Read non-standard or unsupported frame type TEST"],
    ];

    public static IEnumerable<object[]> FrameHeaderData =>
    [
        [
            ByteArrayHelpers.GenerateBytes(
                frameType: FrameType.TIT2, // TIT2
                size: [0, 0, 0, 41], // 41 bytes
                flags: [0, 0], // flags
                0x01, 0xFF, 0xFE, // UTF16 LittleEndian
                67, 0, 104, 0, 97, 0, 110, 0, 103, 0, 101, 0, 115, 0, 32, 0, 40, 0, 111, 0, 114, 0, 105, 0, 103, 0, 105,
                0, 110, 0, 97, 0, 108, 0, 41, 0, // Changes (original)
                0x00, 0x00 // Null Terminator for UTF16
            ),
            FrameType.TIT2.ToArray(),
            41,
            "\0\0"u8.ToArray()
        ]
    ];
}