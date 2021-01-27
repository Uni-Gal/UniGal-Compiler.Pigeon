using System;
using System.Collections.Generic;
using System.IO;

namespace UniGal.Compiler.Backend
{
	public abstract class BackendFactory
	{
		protected string[] supportedLanguages = Array.Empty<string>();
		protected BackendFactory() {}

		public abstract IBackend CreateBackend(DirectoryInfo outdir, string targetLanguage);

		public virtual IEnumerable<string> SupportedLanguages => supportedLanguages;
	}
}
