
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
		/// 主体
		/// </summary>
		public Body Body { get; init; }
		/// <summary>
		/// 运行环境属性
		/// </summary>
		public EnvironmentInfo? Environment { get; init; }

		/// <summary>
		/// ctor
		/// </summary>
		public ScriptSyntaxTree(Metadata md, Body body, EnvironmentInfo? env)
		{
			Metadata = md;
			Body = body;
			Environment = env;
		}
		/// <summary>
		/// ctor
		/// </summary>
		public ScriptSyntaxTree(Metadata md, Body body):this(md, body, null) { }
	}
}
