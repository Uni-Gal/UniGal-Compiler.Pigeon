using System.Collections.Generic;
using System.IO;

namespace UniGal.Compiler.Backend
{
	/// <summary>
	/// 不建议直接实现这个接口
	/// </summary>
	public interface IBackendFactory
	{
		/// <summary>
		/// 此值指示是否允许同时存在多个后端实例
		/// </summary>
		bool AllowMultipleInstance { get; }
		/// <summary>
		/// 获取该后端工厂支持的语言列表
		/// </summary>
		IEnumerable<string> SupportedLanguages { get; }
		/// <summary>
		/// 创建一个编译后端
		/// </summary>
		/// <param name="outdir">输出目录</param>
		/// <param name="targetLanguage">目标语言</param>
		/// <returns>后端</returns>
		/// <exception cref="System.InvalidOperationException">在不允许同时存在多个后端的情况下，尝试创建第二个后端</exception>
		IBackend CreateBackend(DirectoryInfo outdir, string targetLanguage);
	}
}