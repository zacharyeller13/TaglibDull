using System.Text;

namespace TaglibDull.Frame;

/// <summary>
/// Represents all declared frame types in ID3v2.3
/// <see href="https://id3.org/id3v2.3.0#Declared_ID3v2_frames">ID3.org</see>
/// </summary>
/// <remarks>
/// We do it this way instead of an Enum, because it's easier for <c>Span</c> and <c>byte[]</c> comparisons whereas
/// enum members can only be easily converted to or cast as normal strings.
/// Then caching all the properties as a <c>HashSet&lt;byte[]&gt;</c> <see cref="Types"/> so we can do an easy <see cref="IsValid(ReadOnlySpan&lt;byte&gt;)"/> check
/// </remarks>
public static class FrameType
{
    // ReSharper disable InconsistentNaming
    /// <summary>
    /// [[#sec4.20|Audio encryption]]
    /// </summary>
    public static ReadOnlySpan<byte> AENC => "AENC"u8;

    /// <summary>
    /// [#sec4.15 Attached picture]
    /// </summary>
    public static ReadOnlySpan<byte> APIC => "APIC"u8;

    /// <summary>
    /// [#sec4.11 Comments]
    /// </summary>
    public static ReadOnlySpan<byte> COMM => "COMM"u8;

    /// <summary>
    /// [#sec4.25 Commercial frame]
    /// </summary>
    public static ReadOnlySpan<byte> COMR => "COMR"u8;

    /// <summary>
    /// [#sec4.26 Encryption method registration]
    /// </summary>
    public static ReadOnlySpan<byte> ENCR => "ENCR"u8;

    /// <summary>
    /// [#sec4.13 Equalization]
    /// </summary>
    public static ReadOnlySpan<byte> EQUA => "EQUA"u8;

    /// <summary>
    /// [#sec4.6 Event timing codes]
    /// </summary>
    public static ReadOnlySpan<byte> ETCO => "ETCO"u8;

    /// <summary>
    /// [#sec4.16 General encapsulated object]
    /// </summary>
    public static ReadOnlySpan<byte> GEOB => "GEOB"u8;

    /// <summary>
    /// [#sec4.27 Group identification registration]
    /// </summary>
    public static ReadOnlySpan<byte> GRID => "GRID"u8;

    /// <summary>
    /// [#sec4.4 Involved people list]
    /// </summary>
    public static ReadOnlySpan<byte> IPLS => "IPLS"u8;

    /// <summary>
    /// [#sec4.21 Linked information]
    /// </summary>
    public static ReadOnlySpan<byte> LINK => "LINK"u8;

    /// <summary>
    /// [#sec4.5 Music CD identifier]
    /// </summary>
    public static ReadOnlySpan<byte> MCDI => "MCDI"u8;

    /// <summary>
    /// [#sec4.7 MPEG location lookup table]
    /// </summary>
    public static ReadOnlySpan<byte> MLLT => "MLLT"u8;

    /// <summary>
    /// [#sec4.24 Ownership frame]
    /// </summary>
    public static ReadOnlySpan<byte> OWNE => "OWNE"u8;

    /// <summary>
    /// [#sec4.28 Private frame]
    /// </summary>
    public static ReadOnlySpan<byte> PRIV => "PRIV"u8;

    /// <summary>
    /// [#sec4.17 Play counter]
    /// </summary>
    public static ReadOnlySpan<byte> PCNT => "PCNT"u8;

    /// <summary>
    /// [#sec4.18 Popularimeter]
    /// </summary>
    public static ReadOnlySpan<byte> POPM => "POPM"u8;

    /// <summary>
    /// [#sec4.22 Position synchronisation frame]
    /// </summary>
    public static ReadOnlySpan<byte> POSS => "POSS"u8;

    /// <summary>
    /// [#sec4.19 Recommended buffer size]
    /// </summary>
    public static ReadOnlySpan<byte> RBUF => "RBUF"u8;

    /// <summary>
    /// [#sec4.12 Relative volume adjustment]
    /// </summary>
    public static ReadOnlySpan<byte> RVAD => "RVAD"u8;

    /// <summary>
    /// [#sec4.14 Reverb]
    /// </summary>
    public static ReadOnlySpan<byte> RVRB => "RVRB"u8;

    /// <summary>
    /// [#sec4.10 Synchronized lyric/text]
    /// </summary>
    public static ReadOnlySpan<byte> SYLT => "SYLT"u8;

    /// <summary>
    /// [#sec4.8 Synchronized tempo codes]
    /// </summary>
    public static ReadOnlySpan<byte> SYTC => "SYTC"u8;

    #region Text information frames

    /// <summary>
    /// [#TALB Album/Movie/Show title]
    /// </summary>
    /// <remarks>
    /// The 'Album/Movie/Show title' frame is intended for the title of the recording(/source of sound) which the audio in the file is taken from. 
    /// </remarks>
    public static ReadOnlySpan<byte> TALB => "TALB"u8;

    /// <summary>
    /// [#TBPM BPM (beats per minute)]
    /// </summary>
    /// <remarks>
    /// The 'BPM' frame contains the number of beats per minute in the mainpart of the audio. The BPM is an integer and represented as a numerical string. 
    /// </remarks>
    public static ReadOnlySpan<byte> TBPM => "TBPM"u8;

    /// <summary>
    /// [#TCOM Composer]
    /// </summary>
    /// <remarks>
    /// The 'Composer(s)' frame is intended for the name of the composer(s). They are seperated with the "/" character.
    /// </remarks>
    public static ReadOnlySpan<byte> TCOM => "TCOM"u8;

    /// <summary>
    /// [#TCON Content type]
    /// </summary>
    /// <remarks>
    /// The 'Content type', which previously was stored as a one byte numeric value only, is now a numeric string. You may use one or several of the types as ID3v1.1 did or, since the category list would be impossible to maintain with accurate and up to date categories, define your own.
    /// References to the ID3v1 genres can be made by, as first byte, enter "(" followed by a number from the genres list (appendix A) and ended with a ")" character. This is optionally followed by a refinement, e.g. "(21)" or "(4)Eurodisco". Several references can be made in the same frame, e.g. "(51)(39)". If the refinement should begin with a "(" character it should be replaced with "((", e.g. "((I can figure out any genre)" or "(55)((I think...)". The following new content types is defined in ID3v2 and is implemented in the same way as the numerig content types, e.g. "(RX)".
    /// </remarks>
    public static ReadOnlySpan<byte> TCON => "TCON"u8;

    /// <summary>
    /// [#TCOP Copyright message]
    /// </summary>
    /// <remarks>
    /// The 'Copyright message' frame, which must begin with a year and a space character (making five characters), is intended for the copyright holder of the original sound, not the audio file itself. The absence of this frame means only that the copyright information is unavailable or has been removed, and must not be interpreted to mean that the sound is public domain. Every time this field is displayed the field must be preceded with "Copyright © ". 
    /// </remarks>
    public static ReadOnlySpan<byte> TCOP => "TCOP"u8;

    /// <summary>
    /// [#TDAT Date]
    /// </summary>
    /// <remarks>
    /// The 'Date' frame is a numeric string in the DDMM format containing the date for the recording. This field is always four characters long. 
    /// </remarks>
    public static ReadOnlySpan<byte> TDAT => "TDAT"u8;

    /// <summary>
    /// [#TDLY Playlist delay]
    /// </summary>
    /// <remarks>
    /// The 'Playlist delay' defines the numbers of milliseconds of silence between every song in a playlist. The player should use the "ETC" frame, if present, to skip initial silence and silence at the end of the audio to match the 'Playlist delay' time. The time is represented as a numeric string. 
    /// </remarks>
    public static ReadOnlySpan<byte> TDLY => "TDLY"u8;

    /// <summary>
    /// [#TENC Encoded by]
    /// </summary>
    /// <remarks>
    /// The 'Encoded by' frame contains the name of the person or organisation that encoded the audio file. This field may contain a copyright message, if the audio file also is copyrighted by the encoder. 
    /// </remarks>

    public static ReadOnlySpan<byte> TENC => "TENC"u8;

    /// <summary>
    /// [#TEXT Lyricist/Text writer]
    /// </summary>
    /// <remarks>
    /// The 'Lyricist(s)/Text writer(s)' frame is intended for the writer(s) of the text or lyrics in the recording. They are seperated with the "/" character.  
    /// </remarks>
    public static ReadOnlySpan<byte> TEXT => "TEXT"u8;

    /// <summary>
    /// [#TFLT File type]
    /// </summary>
    /// <remarks>
    /// The 'File type' frame indicates which type of audio this tag defines.
    /// Other types may be used. This is used in a similar way to <see cref="TMED"/> frame, but w/out parenthesis.
    /// If not present, type is assumed to be "MPG"
    /// </remarks>
    /// <list type="table">
    /// <item><term>MPG</term><description>MPEG Audio</description></item>
    /// <item><term>/1</term><description>MPEG 1/2 layer I</description></item>
    /// <item><term>/2</term><description>MPEG 1/2 layer II</description></item>
    /// <item><term>/3</term><description>MPEG 1/2 layer III</description></item>
    /// <item><term>/2.5</term><description>MPEG 2.5</description></item>
    /// <item><term>/AAC</term><description>Advanced audio compression</description></item>
    /// <item><term>VQF</term><description>Transform-domain Weighted Interleave Vector Quantization</description></item>
    /// <item><term>PCM</term><description>Pulse Code Modulated audio</description></item>
    /// </list>
    public static ReadOnlySpan<byte> TFLT => "TFLT"u8;

    /// <summary>
    /// [#TIME Time]
    /// </summary>
    /// <remarks>
    /// The 'Time' frame is a numeric string in the HHMM format containing the time for the recording. This field is always four characters long. 
    /// </remarks>
    public static ReadOnlySpan<byte> TIME => "TIME"u8;

    /// <summary>
    /// [#TIT1 Content group description]
    /// </summary>
    /// <remarks>
    /// The 'Content group description' frame is used if the sound belongs to a larger category of sounds/music. For example, classical music is often sorted in different musical sections (e.g. "Piano Concerto", "Weather - Hurricane"). 
    /// </remarks>
    public static ReadOnlySpan<byte> TIT1 => "TIT1"u8;

    /// <summary>
    /// [#TIT2 Title/songname/content description]
    /// </summary>
    /// <remarks>
    /// The 'Title/Songname/Content description' frame is the actual name of the piece (e.g. "Adagio", "Hurricane Donna"). 
    /// </remarks>
    public static ReadOnlySpan<byte> TIT2 => "TIT2"u8;

    /// <summary>
    /// [#TIT3 Subtitle/Description refinement]
    /// </summary>
    /// <remarks>
    /// The 'Subtitle/Description refinement' frame is used for information directly related to the contents title (e.g. "Op. 16" or "Performed live at Wembley"). 
    /// </remarks>
    public static ReadOnlySpan<byte> TIT3 => "TIT3"u8;

    /// <summary>
    /// [#TKEY Initial key]
    /// </summary>
    /// <remarks>
    /// The 'Initial key' frame contains the musical key in which the sound starts. It is represented as a string with a maximum length of three characters. The ground keys are represented with "A","B","C","D","E", "F" and "G" and halfkeys represented with "b" and "#". Minor is represented as "m". Example "Cbm". Off key is represented with an "o" only. 
    /// </remarks>
    public static ReadOnlySpan<byte> TKEY => "TKEY"u8;

    /// <summary>
    /// [#TLAN Language(s)]
    /// </summary>
    /// <remarks> 
    /// The 'Language(s)' frame should contain the languages of the text or lyrics spoken or sung in the audio. The language is represented with three characters according to ISO-639-2. If more than one language is used in the text their language codes should follow according to their usage. 
    /// </remarks>
    public static ReadOnlySpan<byte> TLAN => "TLAN"u8;

    /// <summary>
    /// [#TLEN Length]
    /// </summary>
    /// <remarks> 
    /// The 'Length' frame contains the length of the audiofile in milliseconds, represented as a numeric string. 
    /// </remarks>
    public static ReadOnlySpan<byte> TLEN => "TLEN"u8;

    /// <summary>
    /// [#TMED Media type]
    /// </summary>
    /// <example>(MC) with four channels</example>
    /// <remarks>
    /// The 'Media type' frame describes from which media the sound originated. This may be a text string or a reference to the predefined media types
    /// found in the spec <see href="https://id3.org/id3v2.3.0#Text_information_frames">ID3</see>.
    /// References are made within "(" and ")" and are optionally followed by text refinement.
    /// </remarks>
    public static ReadOnlySpan<byte> TMED => "TMED"u8;

    /// <summary>
    /// [#TOAL Original album/movie/show title]
    /// </summary>
    /// <remarks>
    /// The 'Original album/movie/show title' frame is intended for the title of the original recording (or source of sound), if for example the music in the file should be a cover of a previously released song. 
    /// </remarks>
    public static ReadOnlySpan<byte> TOAL => "TOAL"u8;

    /// <summary>
    /// [#TOFN Original filename]
    /// </summary>
    /// <remarks>
    /// The 'Original filename' frame contains the preferred filename for the file, since some media doesn't allow the desired length of the filename. The filename is case sensitive and includes its suffix. 
    /// </remarks>
    public static ReadOnlySpan<byte> TOFN => "TOFN"u8;

    /// <summary>
    /// [#TOLY Original lyricist(s)/text writer(s)]
    /// </summary>
    /// <remarks>
    /// The 'Original lyricist(s)/text writer(s)' frame is intended for the text writer(s) of the original recording, if for example the music in the file should be a cover of a previously released song. The text writers are seperated with the "/" character. 
    /// </remarks>
    public static ReadOnlySpan<byte> TOLY => "TOLY"u8;

    /// <summary>
    /// [#TOPE Original artist(s)/performer(s)]
    /// </summary>
    /// <remarks>
    /// The 'Original artist(s)/performer(s)' frame is intended for the performer(s) of the original recording, if for example the music in the file should be a cover of a previously released song. The performers are seperated with the "/" character. 
    /// </remarks>
    public static ReadOnlySpan<byte> TOPE => "TOPE"u8;

    /// <summary>
    /// [#TORY Original release year]
    /// </summary>
    /// <remarks>
    /// The 'Original release year' frame is intended for the year when the original recording, if for example the music in the file should be a cover of a previously released song, was released. The field is formatted as in the "TYER" frame. 
    /// </remarks>
    public static ReadOnlySpan<byte> TORY => "TORY"u8;

    /// <summary>
    /// [#TOWN File owner/licensee]
    /// </summary>
    /// <remarks>
    /// The 'File owner/licensee' frame contains the name of the owner or licensee of the file and it's contents. 
    /// </remarks>
    public static ReadOnlySpan<byte> TOWN => "TOWN"u8;

    /// <summary>
    /// [#TPE1 Lead performer(s)/Soloist(s)]
    /// </summary>
    /// <remarks>
    /// The 'Lead artist(s)/Lead performer(s)/Soloist(s)/Performing group' is used for the main artist(s). They are seperated with the "/" character. 
    /// </remarks>
    public static ReadOnlySpan<byte> TPE1 => "TPE1"u8;

    /// <summary>
    /// [#TPE2 Band/orchestra/accompaniment]
    /// </summary>
    /// <remarks>
    /// The 'Band/Orchestra/Accompaniment' frame is used for additional information about the performers in the recording. 
    /// </remarks>
    public static ReadOnlySpan<byte> TPE2 => "TPE2"u8;

    /// <summary>
    /// [#TPE3 Conductor/performer refinement]
    /// </summary>
    /// <remarks>
    /// The 'Conductor' frame is used for the name of the conductor. 
    /// </remarks>
    public static ReadOnlySpan<byte> TPE3 => "TPE3"u8;

    /// <summary>
    /// [#TPE4 Interpreted, remixed, or otherwise modified by]
    /// </summary>
    /// <remarks>
    /// The 'Interpreted, remixed, or otherwise modified by' frame contains more information about the people behind a remix and similar interpretations of another existing piece. 
    /// </remarks>
    public static ReadOnlySpan<byte> TPE4 => "TPE4"u8;

    /// <summary>
    /// [#TPOS Part of a set]
    /// </summary>
    /// <remarks>
    /// The 'Part of a set' frame is a numeric string that describes which part of a set the audio came from. This frame is used if the source described in the "TALB" frame is divided into several mediums, e.g. a double CD. The value may be extended with a "/" character and a numeric string containing the total number of parts in the set. E.g. "1/2". 
    /// </remarks>
    public static ReadOnlySpan<byte> TPOS => "TPOS"u8;

    /// <summary>
    /// [#TPUB Publisher]
    /// </summary>
    /// <remarks>
    /// The 'Publisher' frame simply contains the name of the label or publisher. 
    /// </remarks>
    public static ReadOnlySpan<byte> TPUB => "TPUB"u8;

    /// <summary>
    /// [#TRCK Track number/Position in set]
    /// </summary>
    /// <remarks>
    /// The 'Track number/Position in set' frame is a numeric string containing the order number of the audio-file on its original recording. This may be extended with a "/" character and a numeric string containing the total numer of tracks/elements on the original recording. E.g. "4/9". 
    /// </remarks>
    public static ReadOnlySpan<byte> TRCK => "TRCK"u8;

    /// <summary>
    /// [#TRDA Recording dates]
    /// </summary>
    /// <remarks>
    /// The 'Recording dates' frame is a intended to be used as complement to the "TYER", "TDAT" and "TIME" frames. E.g. "4th-7th June, 12th June" in combination with the "TYER" frame.
    /// </remarks>
    public static ReadOnlySpan<byte> TRDA => "TRDA"u8;

    /// <summary>
    /// [#TRSN Internet radio station name]
    /// </summary>
    /// <remarks>
    /// The 'Internet radio station name' frame contains the name of the internet radio station from which the audio is streamed. 
    /// </remarks>
    public static ReadOnlySpan<byte> TRSN => "TRSN"u8;

    /// <summary>
    /// [#TRSO Internet radio station owner]
    /// </summary>
    /// <remarks>
    /// The 'Internet radio station owner' frame contains the name of the owner of the internet radio station from which the audio is streamed. 
    /// </remarks>
    public static ReadOnlySpan<byte> TRSO => "TRSO"u8;

    /// <summary>
    /// [#TSIZ Size]
    /// </summary>
    /// <remarks>
    /// The 'Size' frame contains the size of the audiofile in bytes, excluding the ID3v2 tag, represented as a numeric string. 
    /// </remarks>
    public static ReadOnlySpan<byte> TSIZ => "TSIZ"u8;

    /// <summary>
    /// [#TSRC ISRC (international standard recording code)]
    /// </summary>
    /// <remarks>
    /// The 'ISRC' frame should contain the International Standard Recording Code (ISRC) (12 characters). 
    /// </remarks>
    public static ReadOnlySpan<byte> TSRC => "TSRC"u8;

    /// <summary>
    /// [#TSEE Software/Hardware and settings used for encoding]
    /// </summary>
    /// <remarks>
    /// The 'Software/Hardware and settings used for encoding' frame includes the used audio encoder and its settings when the file was encoded. Hardware refers to hardware encoders, not the computer on which a program was run. 
    /// </remarks>
    public static ReadOnlySpan<byte> TSSE => "TSSE"u8;

    /// <summary>
    /// [#TYER Year]
    /// </summary>
    /// <remarks>
    /// The 'Year' frame is a numeric string with a year of the recording. This frames is always four characters long (until the year 10000). 
    /// </remarks>
    public static ReadOnlySpan<byte> TYER => "TYER"u8;

    /// <summary>
    /// [#TXXX User defined text information frame]
    /// </summary>
    /// <remarks>
    /// This frame is intended for one-string text information concerning the audiofile in a similar way to the other "T"-frames. The frame body consists of a description of the string, represented as a terminated string, followed by the actual string. There may be more than one "TXXX" frame in each tag, but only one with the same description.
    /// </remarks>
    public static ReadOnlySpan<byte> TXXX => "TXXX"u8;

    #endregion

    /// <summary>
    /// [#sec4.1 Unique file identifier]
    /// </summary>
    /// <remarks>
    /// This frame's purpose is to be able to identify the audio file in a database that may contain more information relevant to the content.
    /// Since standardisation of such a database is beyond this document, all frames begin with a null-terminated string with a URL containing an email address,
    /// or a link to a location where an email address can be found, that belongs to the organisation responsible for this specific database implementation.
    /// Questions regarding the database should be sent to the indicated email address.
    /// The URL should not be used for the actual database queries.
    /// The string "http://www.id3.org/dummy/ufid.html" should be used for tests.
    /// Software that isn't told otherwise may safely remove such frames. The 'Owner identifier' must be non-empty (more than just a termination).
    /// The 'Owner identifier' is then followed by the actual identifier, which may be up to 64 bytes.
    /// There may be more than one "UFID" frame in a tag, but only one with the same 'Owner identifier'. 
    /// </remarks>
    public static ReadOnlySpan<byte> UFID => "UFID"u8;

    /// <summary>
    /// [#sec4.23 Terms of use]
    /// </summary>
    public static ReadOnlySpan<byte> USER => "USER"u8;

    /// <summary>
    /// [#sec4.9 Unsychronized lyric/text transcription]
    /// </summary>
    public static ReadOnlySpan<byte> USLT => "USLT"u8;

    #region URL link frames

    /// <summary>
    /// [#WCOM Commercial information]
    /// The 'Commercial information' frame is a URL pointing at a webpage with information such as where the album can be bought.
    /// There may be more than one "WCOM" frame in a tag, but not with the same content. 
    /// </summary>
    public static ReadOnlySpan<byte> WCOM => "WCOM"u8;

    /// <summary>
    /// [#WCOP Copyright/Legal information]
    /// The 'Copyright/Legal information' frame is a URL pointing at a webpage where the terms of use and ownership of the file is described. 
    /// </summary>
    public static ReadOnlySpan<byte> WCOP => "WCOP"u8;

    /// <summary>
    /// [#WOAF Official audio file webpage]
    /// The 'Official audio file webpage' frame is a URL pointing at a file specific webpage. 
    /// </summary>
    public static ReadOnlySpan<byte> WOAF => "WOAF"u8;

    /// <summary>
    /// [#WOAR Official artist/performer webpage]
    /// The 'Official artist/performer webpage' frame is a URL pointing at the artists official webpage. There may be more than one "WOAR" frame in a tag if the audio contains more than one performer, but not with the same content. 
    /// </summary>
    public static ReadOnlySpan<byte> WOAR => "WOAR"u8;

    /// <summary>
    /// [#WOAS Official audio source webpage]
    /// The 'Official audio source webpage' frame is a URL pointing at the official webpage for the source of the audio file, e.g. a movie. 
    /// </summary>
    public static ReadOnlySpan<byte> WOAS => "WOAS"u8;

    /// <summary>
    /// [#WORS Official internet radio station homepage]
    /// The 'Official internet radio station homepage' contains a URL pointing at the homepage of the internet radio station.
    /// </summary>
    public static ReadOnlySpan<byte> WORS => "WORS"u8;

    /// <summary>
    /// [#WPAY Payment]
    /// The 'Payment' frame is a URL pointing at a webpage that will handle the process of paying for this file. 
    /// </summary>
    public static ReadOnlySpan<byte> WPAY => "WPAY"u8;

    /// <summary>
    /// [#WPUB Publishers official webpage]
    /// The 'Publishers official webpage' frame is a URL pointing at the official wepage for the publisher.
    /// </summary>
    public static ReadOnlySpan<byte> WPUB => "WPUB"u8;

    /// <summary>
    /// [#WXXX User defined URL link frame]
    /// </summary>
    /// <remarks>
    /// This frame is intended for URL links concerning the audiofile in a similar way to the other "W"-frames.
    /// The frame body consists of a description of the string, represented as a terminated string, followed by the actual URL.
    /// The URL is always encoded with ISO-8859-1. There may be more than one "WXXX" frame in each tag, but only one with the same description. 
    /// </remarks>
    public static ReadOnlySpan<byte> WXXX => "WXXX"u8;

    #endregion
    
    public static HashSet<byte[]> Types { get; } = SetTypes();

    private static HashSet<byte[]> SetTypes()
    {
        var properties = typeof(FrameType).GetProperties();
        return properties.Select(t => Encoding.UTF8.GetBytes(t.Name)).ToHashSet(new ByteArrayComparer());
    }

    public static bool IsValid(string type) => typeof(FrameType).GetProperty(type) is not null;

    public static bool IsValid(ReadOnlySpan<byte> type) => Types.Contains(type.ToArray());

    public static bool IsValidTextFrame(ReadOnlySpan<byte> data) =>
        Types.Where(x => x[0] == (byte)'T').Contains(data.ToArray(), new ByteArrayComparer());
}

public class ByteArrayComparer : IEqualityComparer<byte[]>
{
    public bool Equals(byte[]? x, byte[]? y)
    {
        if (x == null || y == null)
        {
            return x == y;
        }

        return x.AsSpan().SequenceEqual(y);
    }

    public int GetHashCode(byte[] obj)
    {
        HashCode hash = new();
        hash.AddBytes(obj);
        return hash.ToHashCode();
    }
}