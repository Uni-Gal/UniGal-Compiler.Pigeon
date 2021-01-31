using System;
using System.Collections.Generic;
using UniGal.Compiler.IR;

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
		/// 该事件在错误列表中添加了错误时触发
		/// </summary>
		public event ErrorsAddedEventHandler OnErrorsAdded;

		/// <summary>
		/// 编译
		/// </summary>
		/// <param name="ast"></param>
		public void Compile(ScriptSyntaxTree ast);
	}
}
