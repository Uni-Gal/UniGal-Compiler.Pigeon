using System.Globalization;
using System.Text;

namespace UniGal.Compiler.IR
{
	/// <summary>
	/// 元数据信息
	/// </summary>
	public class Metadata : BasicElement
	{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
		public string Source = string.Empty;
		public string Target = string.Empty;
#pragma warning restore CS1591 // 暂时不知道干嘛用的

		/// <summary>源引擎</summary>
		public string SourceEngine = string.Empty;
		/// <summary>目标引擎</summary>
		public string TargetEngine = string.Empty;

		/// <summary>源编码</summary>
		public Encoding SourceEncoding = Encoding.UTF8;
		/// <summary>目标编码</summary>
		public Encoding TargetEncoding = Encoding.UTF8;

		/// <summary>源文化信息，可选</summary>
		public CultureInfo? SourceCulture;
		/// <summary>目标文化信息，可选</summary>
		public CultureInfo? TargetCulture;
	}
}
