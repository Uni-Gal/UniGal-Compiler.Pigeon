using System.Collections.Generic;

using UniGal.Compiler.IR.Script.ScriptBody;

#pragma warning disable CS1591
namespace UniGal.Compiler.IR.Script
{
	/// <summary>
	/// UniGal脚本的主要部分
	/// </summary>
	public class Body : BasicElement
	{
		public IEnumerable<ScriptText> Texts;
		public Codes Code;

		public Body(IEnumerable<ScriptText> texts, Codes code)
		{
			Texts = texts;
			Code = code;
		}
	}
}
