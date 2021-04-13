using System;
using System.Collections.Generic;
using System.IO;

namespace UniGal.Compiler.LibDriver
{
	/// <summary>
	/// 编译组织器选项
	/// </summary>
	public class CompileOptions
	{
		/// <summary>
		/// 源文件
		/// </summary>
		public IEnumerable<FileInfo> Sources { get; set; } = Array.Empty<FileInfo>();
		/// <summary>
		/// 输出目录
		/// </summary>
		public DirectoryInfo OutDir { get; set; } = new(".\\");

		/// <summary>
		/// 后端名称
		/// </summary>
		public string? BackendName { get; set; } = null;
		/// <summary>
		/// 版本
		/// </summary>
		public string? BackendVersion { get; set; } = null;
		/// <summary>
		/// 目标语言
		/// </summary>
		public string? TargetLanguage { get; set; } = null;
		/// <summary>
		/// 目标引擎
		/// </summary>
		public string? TargetEngine { get; set; } = null;
	}
}
