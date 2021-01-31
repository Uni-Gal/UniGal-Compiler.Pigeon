using System;
using System.Collections.Generic;
using System.IO;

namespace UniGal.Compiler.LibDriver
{
	public class CompileOptions
	{
		public IEnumerable<FileInfo> Sources { get; set; } = Array.Empty<FileInfo>();
		public DirectoryInfo OutDir { get; set; } = new(".\\Compiled\\");

		public string BackendName { get; set; } = "";
		public string TargetLanguage { get; set; } = "";
		public string TargetEngine { get; set; } = "";
	}
}
