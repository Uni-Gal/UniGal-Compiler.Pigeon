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
	public sealed partial class CompileDriver
	{
		private readonly CompileOptions opt;
		private readonly List<CompilerError> errors;
		private readonly List<BackendRecord> backends;

		public DirectoryInfo OutputDirectory => opt.OutDir;
		public IEnumerable<FileInfo> SourceFiles => opt.Sources;
		public IEnumerable<CompilerError> Errors => errors;
		public CompileDriver(CompileOptions options)
		{
			opt = options;
			errors = new(48);
			backends = new(8);
		}

		internal partial BackendRecord SelectBackend(string engine, string name, string? version);
		public partial void LoadBackends();
		public partial void BeginCompile();
	}
}
