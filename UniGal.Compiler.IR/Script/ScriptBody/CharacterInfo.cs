using UniGal.Compiler.IR.Utilities;

namespace UniGal.Compiler.IR.Script.ScriptBody
{
	/// <summary>
	/// 说话的角色，这玩意还有整成一张图的，建议后端灵活处理
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
		/// 该值指示这个角色是否具有除名字外的其它信息
		/// </summary>
		public readonly bool IsComplex;
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
