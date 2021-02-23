using System;
using System.Collections.Generic;
using System.Text;
using UniGal.Compiler.IR.Utilities;

namespace UniGal.Compiler.IR
{
	/// <summary>
	/// 错误的严重性
	/// </summary>
	public enum ErrorServiety : ushort
	{
		/// <summary>
		/// 关键性错误
		/// </summary>
		CritialError,
		/// <summary>
		/// 警告
		/// </summary>
		Warning,
	}

	/// <summary>
	/// 错误代码
	/// </summary>
	public struct ErrorCode
	{
		/// <summary>
		/// 严重性
		/// </summary>
		public ErrorServiety Serviety;
		/// <summary>
		/// 数字编号
		/// </summary>
		public ushort NumericCode;

		/// <summary>
		/// 返回该对象的文字表示形式
		/// </summary>
		/// <returns>如UEE1234</returns>
		public override string ToString()
		{
			string a = Serviety switch
			{
				ErrorServiety.CritialError => "UEE",
				ErrorServiety.Warning => "UEW",
				_ => "",
			};
			return a + NumericCode.ToString("D4");
		}

		/// <summary>
		/// 创建实例
		/// </summary>
		/// <param name="ncode"></param>
		/// <param name="serviety"></param>
		public ErrorCode(ushort ncode, ErrorServiety serviety)
		{
			NumericCode = ncode;
			Serviety = serviety;
		}
	}

	/// <summary>
	/// 编译过程中的错误
	/// </summary>
	public class CompilerError
	{
		/// <summary>
		/// 错误代码
		/// </summary>
		public readonly ErrorCode Code;
		/// <summary>
		/// 附加信息
		/// </summary>
		public IEnumerable<string> Messages { get; protected set; }
		/// <summary>
		/// 解释信息
		/// </summary>
		public string Explaination { get; protected set; } = "";

		/// <summary>
		/// 唯一指定
		/// </summary>
		protected CompilerError(ErrorCode errc, IEnumerable<string> messages)
		{
			Code = errc;
			Messages = messages;
		}

		/// <summary>
		/// 类似<see cref="Exception.ToString"/>
		/// </summary>
		public override string ToString()
		{
			StringBuilder outstr = new(128);
			string errc = Code.ToString();
			outstr.AppendFormat("{0}：{1}", errc, Explaination).AppendLine();

			if (Messages.IsNotEmpty())
			{
				outstr.AppendLine("附加信息：");
				foreach (string msg in Messages)
					outstr.AppendLine(msg);
			}

			return outstr.ToString();
		}
	}
}
