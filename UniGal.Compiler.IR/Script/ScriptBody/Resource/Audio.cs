namespace UniGal.Compiler.IR.Script.Resource
{
	/// <summary>
	/// 音频资源
	/// </summary>
	public class Audio : ResourceBase
	{

		/// <summary>
		/// 创建已解析的音频资源
		/// </summary>
		/// <param name="id"></param>
		/// <param name="path"></param>
		public Audio(string id, FileSystemPath path) : base(id, path)
		{

		}
	}
}
