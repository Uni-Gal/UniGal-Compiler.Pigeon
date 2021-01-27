using System.Runtime.InteropServices;

namespace UniGal.Compiler.IR
{
	/// <summary>
	/// 基本元素
	/// </summary>
	public class BasicElement
	{
		/// <summary>直观编辑器所使用的注释</summary>
		public string Comment = "";
	}

	/// <summary>
	/// DXGI R8G8B8A8 颜色
	/// </summary>
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 4)]
	public struct Color
	{
		/// <summary>红色分量</summary>
		[FieldOffset(0)]
		public byte R;
		/// <summary>绿色分量</summary>
		[FieldOffset(1)]
		public byte G;
		/// <summary>蓝色分量</summary>
		[FieldOffset(2)]
		public byte B;
		/// <summary>透明分量</summary>
		[FieldOffset(3)]
		public byte A;
	}
}
