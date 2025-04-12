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

	public virtual void DrawSymbol(Symbol symbol, Cell cell)
	{
		var location = new Point((int)(cell.x * _cellSize), (int)(cell.y * _cellSize));
		
		DrawParts(symbol.Parts, location);
	}

	protected abstract void DrawParts(Symbol.Part[] parts, Point location);
	
	protected abstract void DrawStraightLine(Point from, Point to);
}