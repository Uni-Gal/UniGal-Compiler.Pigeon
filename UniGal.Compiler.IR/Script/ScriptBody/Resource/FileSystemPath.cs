using UniGal.Compiler.IR.Utilities;

namespace UniGal.Compiler.IR.Script.Resource
{
	/// <summary>
	/// 文件系统路径
	/// </summary>
	public class FileSystemPath
	{
		/// <summary>
		/// 包名
		/// </summary>
		public readonly string Package;
		/// <summary>
		/// 包内路径
		/// </summary>
		public readonly string Path;

		/// <summary>
		/// 创建空路径
		/// </summary>
		public FileSystemPath()
		{
			Package = "";
			Path = "";
		}

		/// <summary>
		/// 根据包名和包内路径创建实例
		/// </summary>
		/// <param name="package"></param>
		/// <param name="path"></param>
		public FileSystemPath(string package, string path)
		{
			Package = package;
			Path = path;
		}

		/// <summary>
		/// 创建包外路径
		/// </summary>
		/// <param name="path"></param>
		public FileSystemPath(string path)
		{
			Package = "";
			Path = path;
		}
	}
}
