using Neoglyph.Dictionary;
using Neoglyph.Glyph;
using Neoglyph.Graphics;

var canvas = new SvgCanvas(20, 50, 50, 
				 Colors.White,
				 Colors.Black);
canvas.DrawGrid();

var glyphs = GlyphDictionary.FromFile("../../../Dictionary/english.json");

var glyphWriter = new GlyphWriter(glyphs, canvas);

glyphWriter.DrawText("hello world i am neoglyph a cryptographic program", new Cell(1, 1));

canvas.SaveToFile("../../../canvas.svg");