using System;
using System.Collections.Generic;
using UniGal.Compiler.IR;
using UniGal.Compiler.IR.Script;

namespace UniGal.Compiler.Backend
{
	public delegate void ErrorsAddedEventHandler(IBackend sender, IEnumerable<CompilerError> addedErrors);

	public interface IBackend
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">发送者</param>
		/// <param name="addedErrors">被添加的错误</param>

		/// <summary>
		/// 是否将警告视为错误
		/// </summary>
		public bool TreatWarningsAsError { get; set; }
		/// <summary>
		/// 目标语言
		/// </summary>
		public string TargetLanguage { get; }
		/// <summary>
		/// 该后端的版本
		/// </summary>
		public string Version { get; }
		/// <summary>
		/// 编译过程中发生的全部错误
		/// </summary>
		public IEnumerable<CompilerError> Errors { get; }
		/// <summary>
		/// 运行时的一些属性
		/// </summary>
		/// <remarks>值为null时，忽略</remarks>
		public EnvironmentInfo? EnvironmentProps { get; set; }

		/// <summary>
		/// 编译
		/// </summary>
		/// <param name="ast"></param>
		public void Compile(ScriptSyntaxTree ast);
	}
}
