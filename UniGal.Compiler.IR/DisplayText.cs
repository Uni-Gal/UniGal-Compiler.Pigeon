#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

using System.Collections.Generic;

namespace UniGal.Compiler.IR
{
	/// <summary>
	/// 一段显示在屏幕上的话
	/// </summary>
	public class DisplayText : BasicElement
	{
		/// <summary>
		/// 说话的角色
		/// </summary>
		public class CharacterInfo : BasicElement
		{
			public readonly string Name;
			public readonly Color NameColor;
			public string Ruby = "";
			public TextStyle Style;
			public CharacterInfo(string name, Color nameColor)
			{
				Name = name;
				NameColor = nameColor;
			}
		}
		/// <summary>
		/// 实际文字
		/// </summary>
		public class TextContent : BasicElement
		{
			public TextStyle Style;
			public readonly IEnumerable<StringView> Paragraphes;

			public TextContent(IEnumerable<StringView> paras)
			{
				Paragraphes = paras;
			}
		}

		public readonly CharacterInfo Character;
		public readonly TextContent Text;

		public DisplayText(CharacterInfo ch, TextContent text)
		{
			Character = ch;
			Text = text;
		}
	}
}
