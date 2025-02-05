namespace TaglibDull.Tests;

public static class HelperMethods
{
    public static byte[] GenerateBytes(ReadOnlySpan<byte> frameType, byte[]? size, byte[]? flags,
        params byte[] additionalBytes)
    {
        List<byte> bytes = [..frameType.ToArray(), ..(size ?? [0, 0, 0, 0]), ..flags ?? [0, 0]];
        bytes.AddRange(additionalBytes);
        return bytes.ToArray();
    }
}