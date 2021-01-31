using System;
using System.Collections.Generic;
using System.IO;

using CommandLine;
using UniGal.Compiler.LibDriver;

namespace UniGal.Compiler.Driver
{
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
