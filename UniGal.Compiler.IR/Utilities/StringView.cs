using System;

namespace UniGal.Compiler.IR.Utilities
{
	/// <summary>
	/// 黑科技
	/// </summary>
	public readonly struct StringView
	{
		// sr，即string reference
		private readonly string sr;

		/// <summary>
		/// 起始点
		/// </summary>
		public readonly int Begin;
		/// <summary>
		/// 长度
		/// </summary>
		public readonly int Size;

		/// <summary>
		/// 终点位置
		/// </summary>
		public int End => Size + Begin;

		#region 构造函数
		/// <summary>
		/// 通过字符串创建实例
		/// </summary>
		/// <param name="str">字符串</param>
		/// <param name="begin">起始位置</param>
		/// <param name="size">长度</param>
		public StringView(string str, int begin, int size)
		{
			sr = str;
			Begin = begin;
			Size = size;
		}

		/// <summary>
		/// 通过另一个StringView创建实例
		/// </summary>
		/// <param name="rhs">另一个StringView</param>
		/// <param name="beg">起始位置</param>
		/// <param name="size">长度</param>
		public StringView(StringView rhs, int beg, int size)
		{
			sr = rhs.sr;
			Begin = rhs.Begin + beg;
			Size = size;
		} 
		#endregion

		#region 推荐使用的成员
		/// <summary>
		/// 获取索引处的字符码点
		/// </summary>
		/// <param name="pos">位置</param>
		/// <returns>字符码点</returns>
		public char this[int pos]
		{
			get =>
				pos < End - Begin &&
				pos >= 0
				?
				sr[Begin + pos] :
				throw new IndexOutOfRangeException("pos > size");
		}

		/// <summary>
		/// 相等性比较
		/// </summary>
		/// <remarks>该比较完全按值进行比较，这意味着不区分区域性且区分大小写</remarks>
		public static bool operator ==(StringView l, StringView r)
		{
			return l.AsSpan() == r.AsSpan();
		}
		/// <summary>
		/// 相等性比较
		/// </summary>
		/// <remarks>该比较完全按值进行比较，这意味着不区分区域性且区分大小写</remarks>
		public static bool operator !=(StringView l, StringView r)
		{
			return l.AsSpan() != r.AsSpan();
		}

		/// <summary>
		/// 空的
		/// </summary>
		public static readonly StringView Empty = new(string.Empty, 0, 0);

		#endregion

		#region 屁用没有
		/// <summary>
		/// 相等性比较
		/// </summary>
		public override bool Equals(object? obj)
		{
			if (obj is StringView rhs)
				return this == rhs;
			return false;
		}
		/// <summary>
		/// 屁用没有
		/// </summary>
		/// <returns>哈希值</returns>
		public override int GetHashCode()
		{
			return string.GetHashCode(AsSpan());
		} 
		#endregion

		#region Span类操作
		/// <summary>
		/// 懂的都懂
		/// </summary>
		public ReadOnlySpan<char> AsSpan()
		{
			return sr.AsSpan(Begin, Size);
		}
		/// <summary>
		/// 懂的都懂
		/// </summary>
		public ReadOnlyMemory<char> AsMemory()
		{
			return sr.AsMemory(Begin, Size);
		}
		#endregion

		/// <summary>
		/// 将字符串切片转回字符串
		/// </summary>
		public override string ToString()
		{
			return sr.Substring(Begin, Size);
		}
	}
	internal static class sv_extfnuncs
	{
		/// <summary>
		/// 将字符串切成<see cref="StringView"/>
		/// </summary>
		/// <param name="str">Autogen</param>
		/// <param name="begin"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		internal static StringView as_sv(this string str, int begin, int end)
		{
			return new StringView(str, begin, end);
		}
	}
}
