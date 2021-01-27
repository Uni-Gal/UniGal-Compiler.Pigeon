using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;

namespace UniGal.Compiler.Driver
{
	public class CompileOptions
	{
		[Option('s')]
		public IEnumerable<FileInfo> Sources { get; set; } = Array.Empty<FileInfo>();

		[Option('o')]
		public DirectoryInfo OutDir { get; set; } = new(".\\Compiled\\");

		public string BackendName { get; set; } = "";
		public string TargetLanguage { get; set; } = "";
		public string TargetEngine { get; set; } = "";
	}

	class EntryPoint
	{
		static void Main(string[] args)
		{
			Parser parser = new(with =>
			{
				with.AutoHelp = true;
				with.IgnoreUnknownArguments = true;
				with.CaseSensitive = false;
			});
			CompileOptions options = new();
			var result = parser.ParseArguments<CompileOptions>(args).WithParsed((opt) =>
			{
				options = opt;
			}).WithNotParsed((errs) =>
			{
				Environment.Exit(1);
			});

			CompileDriver driver = new(options);

			driver.BeginCompile();

		}
	}
}
