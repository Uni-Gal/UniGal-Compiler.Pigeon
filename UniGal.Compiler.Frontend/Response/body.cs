using System.Collections.Generic;
using System.Xml;
using UniGal.Compiler.IR;
using UniGal.Compiler.IR.Script.ScriptBody;

namespace UniGal.Compiler.Frontend
{
	internal partial class responses
	{
		internal static class body
		{
			internal static Codes on_code(XmlReader r, List<CompilerError> errors)
			{
				Codes ret = new();

				return ret;
			}
		}
	}
}
