
using System;

namespace UniGal.Compiler.IR
{
	[Flags]
	public enum TextStyle : uint
	{
		None = 0,
		Bold = 1,
		Italic = 2,
		Stroke = 4,
		Undelined = 8
	}
}