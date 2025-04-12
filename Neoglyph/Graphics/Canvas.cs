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

	public virtual void DrawSymbol(Glyph.Glyph.Symbol symbol, Cell cell)
	{
		var location = new Point((int)(cell.x * _cellSize), (int)(cell.y * _cellSize));

		switch (symbol) {
			case Glyph.Glyph.Symbol.Vertical:
				DrawVerticalSymbol(location);
				break;
			case Glyph.Glyph.Symbol.Horizontal:
				DrawHorizontalSymbol(location);
				break;
			case Glyph.Glyph.Symbol.Cross:
				DrawCrossSymbol(location);
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(symbol), symbol, null);
		}
	}

	protected abstract void DrawCrossSymbol(Point location);

	protected abstract void DrawHorizontalSymbol(Point location);

	protected abstract void DrawVerticalSymbol(Point location);
	
	protected abstract void DrawStraightLine(Point from, Point to);
}