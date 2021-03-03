namespace UniGal.Compiler.IR.Script.Resource
{
	/// <summary>
	/// 不建议直接使用的代码元素
	/// </summary>
	public class ResourceBase
	{
		/// <summary>
		/// 图像ID
		/// </summary>
		public readonly string ID;
		/// <summary>
		/// 路径
		/// </summary>
		public readonly FileSystemPath Path;

		/// <summary>
		/// 继承用的，不建议直接使用
		/// </summary>
		/// <param name="id"></param>
		/// <param name="path"></param>
		protected ResourceBase(string id, FileSystemPath path)
		{
			ID = id;
			Path = path;
		}
	}
}