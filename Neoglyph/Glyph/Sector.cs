namespace Neoglyph.Glyph;

public record struct Sector(double From, double To);

public static class Sectors
{
	public static Sector BottomRight => new Sector(double.DegreesToRadians(0),
												   double.DegreesToRadians(90));
	
	public static Sector BottomLeft => new Sector(double.DegreesToRadians(90),
												  double.DegreesToRadians(180));
	
	public static Sector TopLeft => new Sector(double.DegreesToRadians(180),
											   double.DegreesToRadians(270));
	
	public static Sector TopRight => new Sector(double.DegreesToRadians(270),
												double.DegreesToRadians(360));
}