using UniGal.Compiler.IR.Utilities;

namespace UniGal.Compiler.IR.Script.ScriptBody
{
	/// <summary>
	/// 段落
	/// </summary>
	public class Paragraph
	{
		/// <summary>
		/// 该值指示这个段落是不是复合式段落（即包含XML标记）
		/// </summary>
		public readonly bool IsComplex = false;

		/// <summary>
		/// 段落内容
		/// </summary>
		/// <remarks>
		/// 若该段落是复合式的，Content字段将包含起止XML标记在内
		/// </remarks>
		public readonly StringView Content;
	}
}
