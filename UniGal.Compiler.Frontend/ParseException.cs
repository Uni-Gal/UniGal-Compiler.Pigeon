using System;

namespace UniGal.Compiler.Frontend
{
	/// <summary>
	/// 解析过程中发生的异常
	/// </summary>
	[Serializable]
	public class ParseException : Exception
	{

		private const string data_site = "UniGal.Compiler.FrontEnd.ParseException ";
		/// <summary>
		/// 解析器错误
		/// </summary>
		public ParserError ParserError
		{
			get
			{
				ParserError? val = (ParserError?)Data[data_site + "errobj"];
				return util.assert_notnull(val);
			}
			private set => Data[data_site + "errobj"] = value;
		}

		internal ParseException(ParserError e) : base(e.Explaination) { ParserError = e; }
		internal ParseException(ParserError e, Exception inner) : base(e.Explaination, inner) { ParserError = e; }
		internal ParseException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
