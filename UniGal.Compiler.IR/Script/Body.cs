using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UniGal.Compiler.IR.Script.ScriptBody;

namespace UniGal.Compiler.IR.Script
{
	/// <summary>
	/// UniGal脚本的主要部分
	/// </summary>
	public class Body : BasicElement
	{
		public IEnumerable<ScriptText> Texts;
		public Codes Code;
	}
}
