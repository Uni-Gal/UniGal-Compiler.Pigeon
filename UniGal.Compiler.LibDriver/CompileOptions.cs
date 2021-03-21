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
		/// 源文件
		/// </summary>
		public DirectoryInfo OutDir { get; set; } = new(".\\Compiled\\");

		/// <summary>
		/// 后端名称
		/// </summary>
		public string BackendName { get; set; } = "";
		/// <summary>
		/// 目标语言
		/// </summary>
		public string TargetLanguage { get; set; } = "";
		/// <summary>
		/// 目标引擎
		/// </summary>
		public string TargetEngine { get; set; } = "";
	}
}
