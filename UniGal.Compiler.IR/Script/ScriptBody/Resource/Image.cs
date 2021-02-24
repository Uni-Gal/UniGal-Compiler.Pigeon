using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniGal.Compiler.IR.Script.ScriptBody.Resource
{
	/// <summary>
	/// 图像资源
	/// </summary>
	public class Image
	{
		/// <summary>
		/// 图像ID
		/// </summary>
		public readonly int ID;
		/// <summary>
		/// 路径
		/// </summary>
		public readonly FileSystemPath Path;

		/// <summary>
		/// 256级透明度
		/// </summary>
		public byte Alpha { get; init; } = 255;

		/// <summary>
		/// 宽
		/// </summary>
		public readonly uint Width;
		/// <summary>
		/// 高
		/// </summary>
		public readonly uint Height;
		/// <summary>
		/// 创建图像
		/// </summary>
		/// <param name="id">图像ID</param>
		/// <param name="path">图像路径</param>
		/// <param name="w">宽</param>
		/// <param name="h">高</param>
		public Image(int id, FileSystemPath path, uint w, uint h)
		{
			ID = id;
			Path = path;
			Width = w;
			Height = h;
		}

	}
}
