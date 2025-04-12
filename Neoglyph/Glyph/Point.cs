namespace Neoglyph.Glyph;

public readonly struct Point(int x, int y)
{
	public readonly int x = x;
	public readonly int y = y;
	
	public static Point operator +(Point a, Point b) => new Point(a.x + b.x, a.y + b.y);
}

public readonly struct Cell(double x, double y)
{
	public readonly double x = x;
	public readonly double y = y;
	
	public static Cell operator +(Cell a, Cell b) => new Cell(a.x + b.x, a.y + b.y);
}