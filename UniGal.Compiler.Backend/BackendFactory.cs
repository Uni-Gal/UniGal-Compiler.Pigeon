using System;
using System.Collections.Generic;
using System.IO;

namespace UniGal.Compiler.Backend
{
	public abstract class BackendFactory
	{
		protected string[] supportedLanguages = Array.Empty<string>();

		/// <summary>
		/// 子类必须存在无参构造函数
		/// </summary>
		protected BackendFactory() {}

		/// <summary>
		/// 创建一个编译后端
		/// </summary>
		/// <param name="outdir">输出目录</param>
		/// <param name="targetLanguage">目标语言</param>
		/// <returns>后端</returns>
		/// <exception cref="InvalidOperationException">在不允许同时存在多个后端的情况下，尝试创建第二个后端</exception>
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
