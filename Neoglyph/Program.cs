using Neoglyph.Dictionary;
using Neoglyph.Glyph;
using Neoglyph.Graphics;

var english = GlyphDictionary.FromFile("../../../Dictionary/english.json");
var writer = new GlyphParser(english);

var text = writer.ParseText("hello and welcome to neoglyph");

Canvas svgCanvas = new SvgCanvas(20, 60, 50,
					  Colors.Elegie,
					  Colors.RainyDay);
var renderer = new GlyphRenderer(svgCanvas);

renderer.RenderText(text.ToList(), new Cell(1, 1));

svgCanvas.SaveToFile("../../../neoglyph.svg");