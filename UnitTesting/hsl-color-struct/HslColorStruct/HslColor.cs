namespace HslColorStruct;

/// <summary>
/// Represents a color in the HSL color model.
/// </summary>
public readonly struct HslColor : IEquatable<HslColor>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HslColor"/> struct with the specified <paramref name="hue"/>, <paramref name="saturation"/> and <paramref name="lightness"/>.
    /// </summary>
    /// <param name="hue">The hue component (0-360 degrees).</param>
    /// <param name="saturation">The saturation component (0-100%).</param>
    /// <param name="lightness">The lightness component (0-100%).</param>
    /// <exception cref="ArgumentException">Thrown when any of the components is outside its valid range.</exception>
    public HslColor(int hue, int saturation, int lightness)
    {
        this.Hue = hue;
        this.Saturation = saturation;
        this.Lightness = lightness;
    }

    private readonly int hue;
    private readonly int saturation;
    private readonly int lightness;

    /// <summary>
    /// Gets the hue component (0-360 degrees).
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when value is outside the valid range (0-360).</exception>
    public int Hue
    {
        get => this.hue;
        init
        {
            ThrowExceptionIfValueIsNotValid(value, "Hue", 0, 360);
            this.hue = value;
        }
    }

    /// <summary>
    /// Gets the saturation component (0-100%).
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when value is outside the valid range (0-100).</exception>
    public int Saturation
    {
        get => this.saturation;
        init
        {
            ThrowExceptionIfValueIsNotValid(value, "Saturation", 0, 100);
            this.saturation = value;
        }
    }

    /// <summary>
    /// Gets the lightness component (0-100%).
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when value is outside the valid range (0-100).</exception>
    public int Lightness
    {
        get => this.lightness;
        init
        {
            ThrowExceptionIfValueIsNotValid(value, "Lightness", 0, 100);
            this.lightness = value;
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="HslColor"/> struct with specified <paramref name="hue"/>, <paramref name="saturation"/> and <paramref name="lightness"/>.
    /// </summary>
    /// <param name="hue">The hue component (0-360 degrees).</param>
    /// <param name="saturation">The saturation component (0-100%).</param>
    /// <param name="lightness">The lightness component (0-100%).</param>
    /// <returns>A <see cref="HslColor"/> that is initialized with specified components.</returns>
    /// <exception cref="ArgumentException">Thrown when any of the components is outside its valid range.</exception>
    public static HslColor Create(int hue, int saturation, int lightness)
    {
        return new HslColor(hue, saturation, lightness);
    }

    /// <summary>
    /// Converts the string representation of a color to its <see cref="HslColor"/> equivalent.
    /// </summary>
    /// <param name="hslString">A string containing a color to convert in format "H,S,L".</param>
    /// <returns>An <see cref="HslColor"/> equivalent to the color contained in <paramref name="hslString"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="hslString"/> is invalid.</exception>
    public static HslColor Parse(string hslString)
    {
        const string invalidHslString = "Invalid HSL string";

        if (hslString == null || string.IsNullOrEmpty(hslString) || hslString.Contains(' ', StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException(invalidHslString, nameof(hslString));
        }

        string[] components = hslString.Split(',');

        if (components.Length != 3)
        {
            throw new ArgumentException(invalidHslString, nameof(hslString));
        }

        if (!int.TryParse(components[0], out int hue))
        {
            throw new ArgumentException(invalidHslString, nameof(hslString));
        }

        if (!int.TryParse(components[1], out int saturation))
        {
            throw new ArgumentException(invalidHslString, nameof(hslString));
        }

        if (!int.TryParse(components[2], out int lightness))
        {
            throw new ArgumentException(invalidHslString, nameof(hslString));
        }

        return Create(hue, saturation, lightness);
    }

    /// <summary>
    /// Converts the string representation of a color to its <see cref="HslColor"/> equivalent. A return value indicates whether the operation succeeded.
    /// </summary>
    /// <param name="hslString">A string containing a color to convert in format "H,S,L".</param>
    /// <param name="hslColor">A <see cref="HslColor"/> equivalent to the color contained in <paramref name="hslString"/>.</param>
    /// <returns>true if <paramref name="hslString"/> was converted successfully; otherwise, false.</returns>
    public static bool TryParse(string hslString, out HslColor hslColor)
    {
        try
        {
            hslColor = Parse(hslString);
            return true;
        }
        catch
        {
            hslColor = default;
            return false;
        }
    }

    /// <summary>
    /// Determines whether the specified <see cref="HslColor"/> is equal to the current <see cref="HslColor"/>.
    /// </summary>
    /// <param name="other">The <see cref="HslColor"/> to compare with the current <see cref="HslColor"/>.</param>
    /// <returns>true if the specified <see cref="HslColor"/> is equal to the current <see cref="HslColor"/>; otherwise, false.</returns>
    public bool Equals(HslColor other)
    {
        return this.Hue == other.Hue &&
               this.Saturation == other.Saturation &&
               this.Lightness == other.Lightness;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="HslColor"/>.
    /// </summary>
    /// <param name="obj">The object to compare with the current <see cref="HslColor"/>.</param>
    /// <returns>true if the specified object is equal to the current <see cref="HslColor"/>; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is HslColor otherColor)
        {
            return this.Equals(otherColor);
        }

        return false;
    }

    /// <summary>
    /// Gets a hash code of the current <see cref="HslColor"/>.
    /// </summary>
    /// <returns>A hash code of the current <see cref="HslColor"/>.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(this.Hue, this.Saturation, this.Lightness);
    }

    /// <summary>
    /// Returns a string that represents the current <see cref="HslColor"/>.
    /// </summary>
    /// <returns>A string that represents the current <see cref="HslColor"/>.</returns>
    public override string ToString()
    {
        return $"{this.Hue},{this.Saturation},{this.Lightness}";
    }

    /// <summary>
    /// Compares <paramref name="left"/> and <paramref name="right"/> objects. Returns true if the left <see cref="HslColor"/> is equal to the right <see cref="HslColor"/>; otherwise, false.
    /// </summary>
    /// <param name="left">A left <see cref="HslColor"/>.</param>
    /// <param name="right">A right <see cref="HslColor"/>.</param>
    /// <returns>true if the left <see cref="HslColor"/> is equal to the right <see cref="HslColor"/>; otherwise, false.</returns>
    public static bool operator ==(HslColor left, HslColor right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Compares <paramref name="left"/> and <paramref name="right"/> objects. Returns true if the left <see cref="HslColor"/> is not equal to the right <see cref="HslColor"/>; otherwise, false.
    /// </summary>
    /// <param name="left">A left <see cref="HslColor"/>.</param>
    /// <param name="right">A right <see cref="HslColor"/>.</param>
    /// <returns>true if the left <see cref="HslColor"/> is not equal to the right <see cref="HslColor"/>; otherwise, false.</returns>
    public static bool operator !=(HslColor left, HslColor right)
    {
        return !left.Equals(right);
    }

    private static void ThrowExceptionIfValueIsNotValid(int value, string parameterName, int minValue, int maxValue)
    {
        if (value < minValue || value > maxValue)
        {
            throw new ArgumentException($"{parameterName} parameter value is not valid");
        }
    }
}
