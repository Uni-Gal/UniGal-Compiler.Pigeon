using UniGal.Compiler.IR.Utilities;

namespace UniGal.Compiler.IR.Script.ScriptBody.Resource
{
	/// <summary>
	/// 文件系统路径
	/// </summary>
	public class FileSystemPath
	{
		/// <summary>
		/// 包名
		/// </summary>
		public readonly StringView Package;
		/// <summary>
		/// 包内路径
		/// </summary>
		public readonly StringView Path;

		/// <summary>
		/// 创建空路径
		/// </summary>
		public FileSystemPath()
		{

		}

		/// <summary>
		/// 根据包名和包内路径创建实例
		/// </summary>
		/// <param name="package"></param>
		/// <param name="path"></param>
		public FileSystemPath(StringView package, StringView path)
		{
			Package = package;
			Path = path;
		}

		public FileSystemPath(StringView path)
		{
			Package = StringView.Empty;
			Path = path;
		}
	}
}
