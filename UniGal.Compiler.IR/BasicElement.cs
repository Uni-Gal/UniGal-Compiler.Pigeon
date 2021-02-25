using UniGal.Compiler.IR.Utilities;

namespace UniGal.Compiler.IR
{
	/// <summary>
	/// 基本元素
	/// </summary>
	public class BasicElement
	{
		/// <summary>直观编辑器所使用的注释</summary>
		public StringView Comment = new StringView();
		/// <summary>
		/// 原始表示
		/// </summary>
		public readonly string Raw = "";
	}
}
