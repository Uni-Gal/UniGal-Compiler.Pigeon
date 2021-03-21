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
		/// <summary>音频资源</summary>
		public readonly IEnumerable<Audio> Audios;
		/// <summary>图像资源</summary>
		public readonly IEnumerable<Image> Images;

		/// <summary>
		/// 
		/// </summary>
		public Codes(IEnumerable<Audio> audios, IEnumerable<Image> imgs, IEnumerable<ActionRecord> actions)
		{
			Audios = audios;
			Images = imgs;
			// Actions = actions;
		}
	}
}
