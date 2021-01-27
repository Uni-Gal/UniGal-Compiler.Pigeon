using System.Collections.Generic;
using UniGal.Compiler.IR;

namespace UniGal.Compiler.Backend
{
	public interface IBackend
	{
		public bool TreatWarningsAsError { get; set; }
		public string TargetLanguage { get; }
		public IEnumerable<CompilerError> Errors { get; }
		public void Compile(ScriptSyntaxTree script);
	}
}
