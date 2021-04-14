using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using UniGal.Compiler.IR;
using UniGal.Compiler.Backend;
using UniGal.Compiler.Frontend;
using System.Security;

namespace UniGal.Compiler.LibDriver
{
	/// <summary>
	/// 编译组织器
	/// </summary>
	public sealed partial class CompileDriver
	{
		private readonly CompileOptions opt;
		private readonly List<CompilerError> errors;
		private readonly List<BackendRecord> backends;
		private bool cached_hascriterr = false;
		/// <summary>
		/// 当错误添加时发生
		/// </summary>
		public event Action<IEnumerable<CompilerError>>? OnErrorsAdded;
		/// <summary>
		/// 当开始编译单个源文件时发生
		/// </summary>
		public event Action<string>? OnCompileSource;
		/// <summary>
		/// 当编译完成时发生
		/// </summary>
		public event Action? OnComplete;
		/// <summary>
		/// 生成输出目录
		/// </summary>
		public DirectoryInfo OutputDirectory => opt.OutDir;
		/// <summary>
		/// 源文件
		/// </summary>
		public IEnumerable<FileInfo> SourceFiles => opt.Sources;
		/// <summary>
		/// 错误列表
		/// </summary>
		public IEnumerable<CompilerError> Errors => errors;
		/// <summary>
		/// 错误数量
		/// </summary>
		public int ErrorCount => errors.Count;
		/// <summary>
		/// 是否存在关键性错误
		/// </summary>
		public bool HasCriticalError 
		{
			get
			{
				if(!cached_hascriterr)
					foreach (CompilerError err in errors)
						if (err.Code.Serviety == ErrorServiety.CritialError)
							cached_hascriterr = true;
				
				return cached_hascriterr;
			}
		}
		/// <summary></summary>
		public IReadOnlyList<BackendRecord> LoadedBackends { get => backends; }
		/// <summary>
		/// 创建Driver
		/// </summary>
		/// <param name="options">编译选项</param>
		public CompileDriver(CompileOptions options)
		{
			opt = options;
			// 错误计数超过100，正在停止编译.jpg
			errors = new(100);
			backends = new(8);
			load_backends();
		}
	}
}
