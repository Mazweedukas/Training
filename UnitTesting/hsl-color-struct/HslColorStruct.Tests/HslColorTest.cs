using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace HslColorStruct.Tests;

[TestFixture]
public class HslColorTests
{
    private static readonly HslColor color1 = new(255, 50, 50);
    private static readonly HslColor color2 = new(255, 50, 50);
    private static readonly HslColor color3 = new(100, 50, 50);
    private static readonly HslColor color4 = new(200, 50, 50);
    private static readonly HslColor color5 = new(255, 50, 0);
    private static readonly HslColor color6 = new(255, 50, 5);

    private static readonly object[] EqualCases =
    {
        new object[] {color1, color2, true},
        new object[] {color3, color4, false},
        new object[] {color5, color6, false}
    };

    [Test]
    public void Constructor_WithValidValues_CreatesColor()
    {
        // Arrange
        int hue = 120;
        int saturation = 50;
        int lightness = 75;

        // Act
        var color = new HslColor(hue, saturation, lightness);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(color.Hue, Is.EqualTo(hue));
            Assert.That(color.Saturation, Is.EqualTo(saturation));
            Assert.That(color.Lightness, Is.EqualTo(lightness));
        });
    }

    [TestCase(-1, 50, 50)]
    [TestCase(361, 50, 50)]
    [TestCase(120, -1, 50)]
    [TestCase(120, 101, 50)]
    [TestCase(120, 50, -1)]
    [TestCase(120, 50, 101)]
    public void Constructor_WithInvalidValues_ThrowsArgumentException(int hue, int saturation, int lightness)
    {
        // Act & Assert
        Assert.That(() => new HslColor(hue, saturation, lightness), Throws.ArgumentException);
    }

    [TestCase("255,A3,22")]
    [TestCase("AA,50,50")]
    [TestCase("255,50,AA")]
    [TestCase("25,25")]
    [TestCase("25,25,25,25")]
    [TestCase(" ")]
    [TestCase("")]
    [TestCase("0, 0, 0 ")]
    public void Parse_WithInvalidValues_ThrowsArgumentException(string parseString)
    {
        //Act & Assert
        Assert.That(() => HslColor.Parse(parseString), Throws.ArgumentException);
    }

    [Test]
    public void Parse_WithValidValues_ReturnsColor()
    {
        //Arrange
        string parseString = "255,50,50";
        var expectedColor = new HslColor(255, 50, 50);
        //Act
        var color = HslColor.Parse(parseString);
        //Assert
        Assert.That(color, Is.EqualTo(expectedColor));
    }

    [TestCase("255, A3, 22")]
    [TestCase("25, 25")]
    [TestCase("25, 25, 25, 25")]
    [TestCase(" ")]
    [TestCase("")]
    [TestCase("0, 0, 0 ")]
    public void TryParse_WithInvalidValues_ReturnsFalse(string parseString)
    {
        //Arrange
        var color = new HslColor();

        //Act & Assert
        Assert.That(() => HslColor.TryParse(parseString, out color), Is.False);
    }

    [Test]
    public void TryParse_WithValidValues_ReturnsTrueAndColor()
    {
        //Arrange
        string parseString = "255,50,50";
        var expectedColor = new HslColor(255, 50, 50);
        //Act
        var result = HslColor.TryParse(parseString, out var color);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(color, Is.EqualTo(expectedColor));
        });
    }

    [TestCaseSource(nameof(EqualCases))]
    public void Equals_WithValues_ReturnsTrueOrFalse(HslColor color1, HslColor color2, bool outcome)
    {
        //Act & Assert
        Assert.That(color1.Equals(color2), Is.EqualTo(outcome));
    }

    [Test]
    public void Equals_WithObject_ReturnsTrueOrFalse()
    {
        //Arrange
        string testString = "hello";
        int testInt = 123;
        bool testBool = true;
        HslColor testColor1 = new HslColor(255, 50, 50);
        HslColor testColor2 = new HslColor(255, 50, 50);
        object obj = testColor2;
        object testObject = new object();

        //Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(testColor1.Equals(testString), Is.False);
            Assert.That(testColor1.Equals(testInt), Is.False);
            Assert.That(testColor1.Equals(testBool), Is.False);
            Assert.That(testColor1.Equals(testObject), Is.False);
            Assert.That(testColor1.Equals(obj), Is.True);
        });
    }

    [Test]
    public void OperatorEquals_WithValues_ReturnsTrueOrFalse()
    {
        //Arrange
        HslColor testColor1 = new HslColor(255, 50, 50);
        HslColor testColor2 = new HslColor(255, 50, 50);
        HslColor testColor3 = new HslColor(100, 50, 50);

        //Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(testColor1 == testColor2, Is.True);
            Assert.That(testColor1 == testColor3, Is.False);
        });
    }

    [Test]
    public void OperatorNotEquals_WithValues_ReturnsTrueOrFalse()
    {
        //Arrange
        HslColor testColor1 = new HslColor(255, 50, 50);
        HslColor testColor2 = new HslColor(255, 50, 50);
        HslColor testColor3 = new HslColor(100, 50, 50);

        //Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(testColor1 != testColor2, Is.False);
            Assert.That(testColor1 != testColor3, Is.True);
        });
    }

    [Test]
    public void GetHashCode_WithEqualColors_ReturnsSameHashCode()
    {
        //Arrange
        HslColor testColor1 = new HslColor(255, 50, 50);
        HslColor testColor2 = new HslColor(255, 50, 50);
        HslColor testColor3 = new HslColor(100, 50, 50);

        //Act
        int hash1 = testColor1.GetHashCode();
        int hash2 = testColor2.GetHashCode();
        int hash3 = testColor3.GetHashCode();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(hash1, Is.EqualTo(hash2));
            Assert.That(testColor1.GetHashCode(), Is.Not.EqualTo(hash3));
        });
    }
}
