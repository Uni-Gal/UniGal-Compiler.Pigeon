using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UniGal.Compiler.IR.Script.Resource;

namespace UniGal.Compiler.IR.Script.ScriptBody
{
	/// <summary>
	/// 行为控制
	/// </summary>
	public class Codes : BasicElement
	{
		public readonly IEnumerable<Audio> Audios;
		public readonly IEnumerable<Image> Images;
		public readonly IEnumerable<ActionRecord> Actions;

		public Codes(IEnumerable<Audio> audios, IEnumerable<Image> imgs, IEnumerable<ActionRecord> actions)
		{
			Audios = audios;
			Images = imgs;
			Actions = actions;
		}
	}
}
