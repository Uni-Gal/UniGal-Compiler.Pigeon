using System;
using System.Collections.Generic;
using System.IO;
using UniGal.Compiler.Backend;
using UniGal.Compiler.IR;
using UniGal.Compiler.IR.Script;

[assembly:UniGalBackendAssembly("FakeBackend", "SomeEngine", new string[] { "SomeLanguage" })]

namespace UniGal.Compiler.FakeBackend
{
	public class FakeBackend : IBackend
	{
		public bool TreatWarningsAsError { get; set; }
		public string TargetLanguage => "SomeLanguage";
		public string Version => "1.0.0.0";
		public IEnumerable<CompilerError> Errors => Array.Empty<CompilerError>();
		public EnvironmentInfo? EnvironmentProps { get; set; }

		public void Compile(ScriptDom dom)
		{
			
		}
	}

	[UniGalBackendFactory(Version = "1.0.0.0")]
	public class FakeBackendFactory : BackendFactory
	{
		public override IBackend CreateBackend(DirectoryInfo outdir, string? targetLanguage)
		{
			return new FakeBackend();
		}
	}
}
