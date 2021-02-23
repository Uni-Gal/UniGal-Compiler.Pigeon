using System.Collections.Generic;

namespace UniGal.Compiler.IR.Script.ScriptBody
{
	/// <summary>
	/// 台本
	/// </summary>
	public class ScriptText : BasicElement
	{
		/// <summary>
		/// 显示页面的集合
		/// </summary>
		public readonly IEnumerable<Page> Pages;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pages"></param>
		public ScriptText(IEnumerable<Page> pages)
		{
			Pages = pages;
		}
	}
}
