using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniGal.Compiler.IR.Script
{
	/// <summary>
	/// 运行环境的一些属性
	/// </summary>
	public class RuntimeEnvironment : BasicElement
	{
		/// <summary>
		/// 硬编码的显示设定
		/// </summary>
		public struct Display
		{
			/// <summary>宽，以像素为单位</summary>
			public uint Width;
			/// <summary>高，以像素为单位</summary>
			public uint Height;
			/// <summary>是不是全屏显示</summary>
			public bool FullScreen;

			public Display(uint w, uint h, bool fullscreen)
			{
				Width = w;
				Height = h;
				FullScreen = fullscreen;
			}
		}
		/// <summary>需要安装的再分发包</summary>
		public record RedistPackage
		{
			#pragma warning disable CS1591
			public string Name = "";
			public string Version = "";
			#pragma warning restore

			/// <summary>
			/// 确认该再分发包的值是否有效
			/// </summary>
			/// <returns></returns>
			public bool IsValid()
			{
				return Name != "" && Version != "";
			}
		}

		/// <summary>
		/// 默认值
		/// </summary>
		public static readonly RuntimeEnvironment Default = new(1920, 1080, true, Array.Empty<RedistPackage>());

		/// <summary>
		/// 显示设定
		/// </summary>
		public readonly Display DisplaySettings;
		/// <summary>需要安装的再分发包</summary>
		public IEnumerable<RedistPackage> Redists { get; init; }

		/// <summary>
		/// ctor(DisplayProp, Redists)
		/// </summary>
		public RuntimeEnvironment(Display displayProps, IEnumerable<RedistPackage> redists)
		{
			DisplaySettings = displayProps;
			Redists = redists;
		}
		/// <summary>
		/// ctor(Width, Height, IsFullscreen, Redists)
		/// </summary>
		public RuntimeEnvironment(uint w, uint h, bool fullscreen, IEnumerable<RedistPackage> redists) : this(new(w,h,fullscreen),redists)
		{

		}
	}
}
