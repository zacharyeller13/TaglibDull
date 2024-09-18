namespace TaglibDull;

public struct Header
{
    public byte[] Identifier;
    
    /// <summary>
    /// This should really always be 3.n and we can throw an exception
    /// if it ends up being 2.n or 4.n or something else
    /// </summary>
    public byte[] Version;
    
    public byte Flags;
    
    /// <summary>
    /// Header size is always 10 
    /// </summary>
    public const uint Size = 10;

    public uint TagSize;

    public Header(ReadOnlySpan<byte> data)
    {
        Identifier = data[..3].ToArray();
        Version = data[3..5].ToArray();
        Flags = data[5];
        TagSize = Synchronization.ToUInt(data[6..10]) + Size;
    }

}