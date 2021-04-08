using System.Collections.Generic;

namespace UniGal.Compiler.IR.Script.ScriptBody
{
	/// <summary>
	/// 一个文本页面
	/// </summary>
	public class Page
	{
		/// <summary>
		/// 段落的集合
		/// </summary>
		public readonly IEnumerable<Paragraph> Paragraphies;
		/// <summary>
		/// 这一页说话的角色，可以没有，没有的情况下该字段值为null
		/// </summary>
		public readonly CharacterInfo? Character;
		/// <summary>
		/// 跳转时用的标签
		/// </summary>
		public readonly string? Label = null;
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="paras">段落的集合</param>
		/// <param name="character">可选</param>
		/// <param name="label">跳转标签</param>
		public Page(IEnumerable<Paragraph> paras, CharacterInfo? character = null, string? label = null)
		{
			Paragraphies = paras;
			Character = character;
			Label = label;
		}
	}
}
