using System.Text;
using TaglibDull.Frame;

namespace TaglibDull.Tests;

public class TestFrameType
{
    [Fact]
    public void TestFrameTypesSet()
    {
        Assert.True(FrameType.Types.GetType() == typeof(HashSet<byte[]>));
    }

    [Theory]
    [InlineData("LINK", true)]
    [InlineData("NANA", false)]
    public void TestFrameTypesStringValid(string input, bool output)
    {
        Assert.Equal(FrameType.IsValid(input), output);
    }

    [Theory]
    [InlineData("LINK", true)]
    [InlineData("NANA", false)]
    public void TestFrameTypesSetContains(string input, bool output)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(input);
        Assert.Equal(FrameType.IsValid(bytes), output);
    }
    
    [Theory]
    [InlineData("LINK", false)]
    [InlineData("TIT2", true)]
    public void TestTextFrameTypesSet(string input, bool output)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(input);
        Assert.Equal(FrameType.IsValidTextFrame(bytes), output);
    }
}