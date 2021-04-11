using System;
using System.Collections.Generic;
using UniGal.Compiler.IR;
using UniGal.Compiler.IR.Script;

namespace UniGal.Compiler.Backend
{
	/// <summary>
	/// 后端接口，建议使用
	/// </summary>
	public interface IBackend
	{
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
		/// 是否出现了致命性错误
		/// </summary>
		public bool HasCriticialError {
			get
			{
				foreach (CompilerError e in Errors)
					if (e.Code.Serviety == ErrorServiety.CritialError)
						return true;

				return false;
			}
		}
		/// <summary>
		/// 运行时的一些属性
		/// </summary>
		/// <remarks>值为null时，忽略</remarks>
		public EnvironmentInfo? EnvironmentProps { get; set; }

		/// <summary>
		/// 编译
		/// </summary>
		/// <param name="dom"></param>
		public void Compile(ScriptDom dom);
	}
}
