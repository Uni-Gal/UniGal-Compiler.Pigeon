using System.IO;
using System.Xml;

using UniGal.Compiler.IR.Script;

namespace UniGal.Compiler.IR
{
	/// <summary>
	/// 语法树
	/// </summary>
	public class ScriptSyntaxTree
	{
		/// <summary>
		/// 元数据
		/// </summary>
		public Metadata Metadata { get; init; }
		/// <summary>
		/// 运行环境属性
		/// </summary>
		public EnvironmentInfo? Environment { get; init; }

		/// <summary>
		/// ctor
		/// </summary>
		public ScriptSyntaxTree(Metadata md, EnvironmentInfo env)
		{
			Metadata = md;
			Environment = env;
		}
		/// <summary>
		/// ctor
		/// </summary>
		public ScriptSyntaxTree(Metadata md)
		{
			Metadata = md;
		}
	}
}
