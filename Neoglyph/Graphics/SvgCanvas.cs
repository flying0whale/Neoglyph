using Neoglyph.Glyph;
using VectSharp;
using VectSharp.SVG;
using Point = Neoglyph.Glyph.Point;

namespace Neoglyph.Graphics;

public class SvgCanvas : Canvas
{
	private readonly Colour _glyphStroke;
	
	private readonly Page _page;
	private readonly VectSharp.Graphics _graphics;

	private readonly int _halfCell;
	
	private readonly double _lineWidth;
	
	public SvgCanvas(int widthInCells, int heightInCells, int cellSize,
					 Color background = default,
					 Color glyphStroke = default) : base(widthInCells, heightInCells, cellSize, background, glyphStroke)
	{
		_glyphStroke = NormalizeColor(glyphStroke);
		_halfCell = _cellSize / 2;
		_lineWidth = _cellSize / 10.0;
		
		_page = new Page(widthInCells * _cellSize, heightInCells * _cellSize) {
			Background = NormalizeColor(background),
		};
		_graphics = _page.Graphics;
	}

	public override void DrawGrid()
	{
		_graphics.StrokeRectangle(0, 0, 
			                _size.x * _cellSize, _size.y * _cellSize,
			                   Colours.Gray, lineWidth: 5);

		var path = new GraphicsPath();
		for (var x = 0; x < _size.x; x++) {
			path.MoveTo(x * _cellSize, 0)
				.LineTo(x * _cellSize, _size.y * _cellSize);
		}
		
		for (var y = 0; y < _size.y; y++) {
			path.MoveTo(0, y * _cellSize)
				.LineTo(_size.x * _cellSize, y * _cellSize);
		}
		
		_graphics.StrokePath(path, Colours.Gray, lineWidth: 2);
	}

	public override void SaveToFile(string path)
	{
		_page.SaveAsSVG(path);
	}

	protected override void DrawVerticalSymbol(Point location)
	{
		var x = location.x + _halfCell;
		DrawStraightLine(new Point(x, location.y),
						 new Point(x, location.y + _cellSize));
	}
	
	protected override void DrawHorizontalSymbol(Point location)
	{
		var y = location.y + _halfCell;
		DrawStraightLine(new Point(location.x, y),
				 new Point(location.x + _cellSize, y));
	}
	
	protected override void DrawCrossSymbol(Point location)
	{
		DrawVerticalSymbol(location);
		DrawHorizontalSymbol(location);
	}
	
	protected override void DrawStraightLine(Point from, Point to)
	{
		var path = new GraphicsPath();
		path.MoveTo(from.x, from.y)
			.LineTo(to.x, to.y);
		
		_graphics.StrokePath(path, _glyphStroke, lineWidth: _lineWidth,
												   lineCap: LineCaps.Round);
	}

	private static Colour NormalizeColor(Color color)
	{
		return new Colour {
			A = NormalizeBetween(0, 255, color.a),
			R = NormalizeBetween(0, 255, color.r),
			G = NormalizeBetween(0, 255, color.g),
			B = NormalizeBetween(0, 255, color.b),
		};
	}
	
	private static double NormalizeBetween (double min, double max, double value)
	{
		return (value - min) / (max - min);
	}
}