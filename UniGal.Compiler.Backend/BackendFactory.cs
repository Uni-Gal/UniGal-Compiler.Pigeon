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

		/// <summary>
		/// 获取该后端工厂支持的语言列表
		/// </summary>
		public virtual IEnumerable<string> SupportedLanguages => supportedLanguages;

		/// <summary>
		/// 此值指示是否允许同时存在多个后端实例
		/// </summary>
		public virtual bool AllowMultipleInstance => true;
	}
}
