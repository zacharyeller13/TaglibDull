namespace TaglibDull;

public struct Header
{
    /// <summary>
    /// Header size is always 10 
    /// </summary>
    public const uint Size = 10;

    /// <summary>
    /// The Identifier should always be "ID3" as we don't support any other header types
    /// </summary>
    public static readonly byte[] Identifier = "ID3"u8.ToArray();

    /// <summary>
    /// This should always be 3 as we're only supporting ID3v2.3.n
    /// </summary>
    public byte MajorVersion { get; }

    /// <summary>
    /// We don't actually care about what the revision is as they're backwards compatible,
    /// but we do need to keep it if we're going to overwrite tag headers later
    /// </summary>
    public byte Revision { get; }

    /// <summary>
    /// Represents Header flags. Currently none are supported so this should always be <c>0</c>
    /// or <c>0b00000000</c> in binary representation
    /// </summary>
    public byte Flags { get; }

    public uint TagSize { get; }

    public Header(ReadOnlySpan<byte> data)
    {
        if (!data[..3].SequenceEqual(Identifier))
        {
            throw new FormatException("Unsupported tag format.");
        }

        // Major version should always be 3
        if (data[3] != 3)
        {
            throw new FormatException("Unsupported ID3 major version.");
        }

        if (data[5] != 0)
        {
            throw new FormatException("Unsupported header flags.");
        }

        MajorVersion = data[3];
        Revision = data[4];
        Flags = data[5];
        TagSize = Synchronization.ToUInt(data[6..10]) + Size;
    }
}

/// <summary>
/// Represents the three header flags provided in ID3v2.3.n
/// These are not currently supported so not being used anywhere.
/// </summary>
[Flags]
public enum HeaderFlags
{
    Unsynchronization = 0b10000000,
    ExtendedHeader = 0b010000000,
    ExperimentalIndicator = 0b001000000
}