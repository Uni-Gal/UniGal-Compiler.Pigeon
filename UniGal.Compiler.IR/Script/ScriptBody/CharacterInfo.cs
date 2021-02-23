using UniGal.Compiler.IR.Utilities;

namespace UniGal.Compiler.IR.Script.ScriptBody
{
	/// <summary>
	/// 说话的角色
	/// </summary>
	public class CharacterInfo : BasicElement
	{
		/// <summary>
		/// 角色姓名
		/// </summary>
		public readonly string Name;
		/// <summary>
		/// 姓名所显示的颜色
		/// </summary>
		public readonly Color NameColor;
		/// <summary>
		/// 读音标注
		/// </summary>
		public StringView Ruby = StringView.Empty;
		/// <summary>
		/// 文字风格（斜体、加粗等）
		/// </summary>
		public TextStyle Style;
		/// <summary>
		/// 构造函数
		/// </summary>
		public CharacterInfo(string name, Color nameColor)
		{
			Name = name;
			NameColor = nameColor;
		}
	}

}
