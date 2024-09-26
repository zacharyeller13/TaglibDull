namespace TaglibDull;

/// <summary>
/// Represents an ID3v2.3 tag header
/// which roughly matches the byte pattern
/// <c>0x49 0x44 0x33 yy yy xx zz zz zz zz</c>
/// where yy is less than 0xFF, xx is the flags byte and zz is less than 0x80.
/// <see href="https://id3.org/id3v2.3.0#ID3v2_header">ID3.org</see>
/// </summary>
public struct Header
{
    /// <summary>
    /// Size of the tag header
    /// </summary>
    /// <remarks>
    /// Is always 10 for ID3 tags
    /// </remarks>
    public const uint Size = 10;

    /// <summary>
    /// Represents the tag file identifier.
    /// </summary>
    /// <remarks>
    /// The identifier should always be "ID3" as we don't support any other header types
    /// </remarks>
    public static readonly byte[] Identifier = "ID3"u8.ToArray();

    /// <summary>
    /// The first byte of the version header field representing the major version
    /// </summary>
    /// <remarks>
    /// This should always be 3 as we're only supporting ID3v2.3.n
    /// </remarks>
    public byte MajorVersion { get; }

    /// <summary>
    /// The second byte of the version header field representing the minor version/revision.
    /// </summary>
    /// <remarks>
    /// We don't actually care about what the revision is as they're backwards compatible,
    /// but we do need to keep it if we're going to overwrite tag headers later
    /// </remarks>
    public byte Revision { get; }

    /// <summary>
    /// Represents Header flags. The first 3 bits are each a specific flag and the rest are always cleared.
    /// </summary>
    /// <remarks>
    /// Currently none are supported so this should always be <c>0</c>
    /// or <c>0b00000000</c> in binary representation
    /// </remarks>
    public byte Flags { get; }

    /// <summary>
    /// Represents the total tag size excluding the 10 header bytes.
    /// Will allow a maximum size of 256MB 
    /// </summary>
    public uint TagSize { get; }

    public Header(ReadOnlySpan<byte> data)
    {
        if (data.Length < Size)
        {
            throw new FormatException("Not enough bytes to determine tag header");
        }

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
        TagSize = Synchronization.ToUInt(data[6..10]);
    }
}

/// <summary>
/// Represents the three header flags provided in ID3v2.3.n
/// </summary>
/// <remarks>
/// These are not currently supported so not being used anywhere.
/// </remarks>
[Flags]
public enum HeaderFlags
{
    Unsynchronization = 0b10000000,
    ExtendedHeader = 0b01000000,
    ExperimentalIndicator = 0b00100000
}
