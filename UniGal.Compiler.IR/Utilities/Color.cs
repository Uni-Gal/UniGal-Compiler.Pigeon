using System.Runtime.InteropServices;

namespace UniGal.Compiler.IR.Utilities
{
	/// <summary>
	/// DXGI R8G8B8A8 颜色
	/// </summary>
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 4)]
	public struct Color
	{
		/// <summary>红色分量</summary>
		[FieldOffset(0)]
		public byte Red;
		/// <summary>绿色分量</summary>
		[FieldOffset(1)]
		public byte Green;
		/// <summary>蓝色分量</summary>
		[FieldOffset(2)]
		public byte Blue;
		/// <summary>透明分量，透明度Transparentcy</summary>
		[FieldOffset(3)]
		public byte Alpha;

		/// <summary>
		/// 
		/// </summary>
		public Color(byte r, byte g ,byte b, byte a)
		{
			Red = r;
			Green = g;
			Blue = b;
			Alpha = a;
		}

		/// <summary>
		/// 
		/// </summary>
		public Color(byte r, byte g, byte b) : this(r, g, b, 0)
		{
			
		}
	}
}
