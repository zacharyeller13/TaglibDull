using System.Buffers.Binary;

namespace TaglibDull;

/// <summary>
/// Static class providing methods for encoding and decoding synchsafe integers, which are encoded in order
/// to prevent being misread by media players as actual media frames.
/// </summary>
public static class Synchronization
{
    /// <summary>
    /// Decode a synchronized int32 back to a normal uint32
    /// </summary>
    /// <param name="bytes">A <c>ReadOnlySpan[byte]</c>referencing the synchronized int32</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">If provided more or less than 4 bytes</exception>
    public static uint ToUInt(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length != 4)
        {
            throw new ArgumentException($"Caller provided {bytes.Length} bytes. Expected 4 bytes.", nameof(bytes));
        }

        checked
        {
            uint intRepr = BinaryPrimitives.ReadUInt32BigEndian(bytes);
            uint ret = 0b_00000000_00000000_00000000_00000000; // Blank slate
            uint mask = 0b_01111111_00000000_00000000_00000000; // Remove the most significant bit from each byte
            for (int i = 0; i < 4; i++)
            {
                ret >>= 0b00000001; // Shift everything over 1 bit (e.g. 01010101 => 00101010)
                // & together to keep only the last 7 bits (e.g. mask: 01111111, intRepr: 10101111 => 00101111)
                // then |= for adding together (e.g. 10101010_00000000 |= 00101111 => 10101010_00101111)
                ret |= intRepr & mask;
                mask >>= 0b00001000; // finally, shift over to the next byte
            }

            return ret;
        }
    }

    public static ReadOnlySpan<byte> FromUInt(uint value)
    {
        throw new NotImplementedException();
    }
}
