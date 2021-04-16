#pragma warning disable CS1591
using System;

namespace UniGal.Compiler.IR.Script.ScriptBody
{
	[Flags]
	public enum TextStyle : uint
	{
		None = 0,
		Bold = 1,
		Italic = 2,
		Stroke = 4,
		Undelined = 8,
		Large = 16,
		Small = 32
	}
}