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
		/// 
		/// </summary>
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
		}

		internal partial BackendRecord select_factory(string engine, string name, string language, string? version);
		/// <summary>
		/// 加载后端
		/// </summary>
		public partial void LoadBackends();
		/// <summary>
		/// 开始编译
		/// </summary>
		public partial void BeginCompile();
	}
}
