namespace Neoglyph.Graphics;

/// <summary>
/// Represents a color in RGBA color scheme. Values are from 0 to 255
/// </summary>
/// <param name="r">Red channel [0-255]</param>
/// <param name="g">Green channel [0-255]</param>
/// <param name="b">Blue channel [0-255]</param>
/// <param name="a">Alpha channel [0-255]</param>
public readonly struct Color(int r, int g, int b, int a = 255)
{
	public readonly int r = r;
	public readonly int g = g;
	public readonly int b = b;
	public readonly int a = a;
}

public static class Colors
{
	public static Color Black { get; }	= new Color(0, 0, 0, 255);
	public static Color Red { get; }	= new Color(255, 0, 0, 255);
	public static Color Green { get; }	= new Color(0, 255, 0, 255);
	public static Color Blue { get; }	= new Color(0, 0, 255, 255);
	public static Color White { get; }	= new Color(255, 255, 255, 255);
	
	public static Color Elegie { get; }	= new Color(37, 38, 40, 255);
	public static Color RainyDay { get; }	= new Color(160, 175, 201, 255);
}