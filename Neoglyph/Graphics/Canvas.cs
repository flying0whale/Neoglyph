using Neoglyph.Glyph;

namespace Neoglyph.Graphics;

public abstract class Canvas(int widthInCells, int heightInCells,
							 int cellSize,
							 Color background = default,
							 Color glyphStroke = default)
{
	protected readonly int _cellSize = cellSize;
	
	protected readonly Point _size = new(widthInCells, heightInCells);

	public abstract void DrawGrid();

	public virtual void SaveToFile(string path) { }

	public virtual void DrawSymbol(Glyph.Glyph.Part[] symbol, Cell cell)
	{
		var location = new Point((int)(cell.x * _cellSize), (int)(cell.y * _cellSize));
		
		DrawParts(symbol, location);
	}

	public abstract void DrawArc(Sector sector, Cell cell);

	protected abstract void DrawParts(Glyph.Glyph.Part[] parts, Point location);
	
	protected abstract void DrawStraightLine(Point from, Point to);
}