#pragma warning disable CS1591
using System;
using System.Collections.Generic;
using System.IO;

namespace UniGal.Compiler.Backend
{
	/// <summary>
	/// 后端工厂类，继承该类以创建自己的工厂
	/// </summary>
	/// <remarks>最终的工厂不能是泛型类</remarks>
	public abstract class BackendFactory : IBackendFactory
	{
		/// <summary>默认的SupportedLanguages实现</summary>
		protected string[] supportedLanguages = Array.Empty<string>();

		/// <summary>
		/// 子类必须存在无参构造函数
		/// </summary>
		protected BackendFactory() { }

		/// <inheritdoc/>
		public abstract IBackend CreateBackend(DirectoryInfo outdir, string? targetLanguage);


		/// <inheritdoc/>
		public virtual IEnumerable<string> SupportedLanguages => supportedLanguages;

		/// <inheritdoc/>
		public virtual bool AllowMultipleInstance => true;
	}
}
